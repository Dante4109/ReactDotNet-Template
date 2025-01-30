export const server = 'https://localhost:7252';
// export const server = 'https://localhost:5062';

export const webAPIUrl = `${server}/api`;

const redirect_uri =
  typeof window === 'undefined' ? 'None' : window.location.origin;

export const authSettings = {
  domain: 'dev-66s14eg78q03bxdn.us.auth0.com',
  client_id: 'rNYMrVvOGfvtBphpmDvylgzYO2D8vmAv',
  redirect_uri: redirect_uri + '/signin-callback',
  scope: 'openid profile QandAAPI email',
  audience: 'https://qanda',
};
