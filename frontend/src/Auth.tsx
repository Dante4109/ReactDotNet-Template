import React from 'react';
import { createAuth0Client, Auth0Client } from '@auth0/auth0-spa-js';
import { authSettings } from './AppSettings';

interface Auth0User {
  name?: string;
  email?: string;
}
interface IAuth0Context {
  isAuthenticated: boolean;
  user?: Auth0User;
  signIn: () => void;
  signOut: () => void;
  loading: boolean;
}
// TO-DO move to a separate file

export const Auth0Context = React.createContext<IAuth0Context>({
  isAuthenticated: false,
  signIn: () => {},
  signOut: () => {},
  loading: true,
});

// TO-DO move to a separate file

export const useAuth = () => React.useContext(Auth0Context);

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({
  children,
}) => {
  const [isAuthenticated, setIsAuthenticated] = React.useState<boolean>(false);
  const [user, setUser] = React.useState<Auth0User | undefined>(undefined);
  const [auth0Client, setAuth0Client] = React.useState<Auth0Client>();
  const [loading, setLoading] = React.useState<boolean>(true);

  React.useEffect(() => {
    const initAuth0 = async () => {
      setLoading(true);
      const auth0FromHook = await createAuth0Client({
        domain: authSettings.domain,
        clientId: authSettings.client_id,
        authorizationParams: { redirect_uri: authSettings.redirect_uri },
      });
      setAuth0Client(auth0FromHook);

      if (
        window.location.pathname === '/signin-callback' &&
        window.location.search.indexOf('code=') > -1
      ) {
        await auth0FromHook.handleRedirectCallback();
        window.location.replace(window.location.origin);
      }

      const isAuthenticatedFromHook = await auth0FromHook.isAuthenticated();
      if (isAuthenticatedFromHook) {
        const user = await auth0FromHook.getUser();
        setUser(user);
      }
      setIsAuthenticated(isAuthenticatedFromHook);
      setLoading(false);
    };
    initAuth0();
  }, []);

  const getAuth0ClientFromState = () => {
    if (auth0Client === undefined) {
      throw new Error('Auth0 client not set');
    }
    return auth0Client;
  };

  return (
    <Auth0Context.Provider
      value={{
        isAuthenticated,
        user,
        signIn: () => getAuth0ClientFromState().loginWithRedirect(),
        signOut: () =>
          getAuth0ClientFromState().logout({
            clientId: authSettings.client_id,
            returnTo: window.location.origin + '/signout-callback',
          }),
        loading,
      }}
    >
      {children}
    </Auth0Context.Provider>
  );
};

// TO-DO move to a separate file

export const getAccessToken = async () => {
  const auth0FromHook = await createAuth0Client({
    domain: authSettings.domain,
    clientId: authSettings.client_id,
    authorizationParams: { redirect_uri: authSettings.redirect_uri },
  });
  const accessToken = await auth0FromHook.getTokenSilently();
  return accessToken;
};
