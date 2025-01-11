export const server = 'http://localhost:7252';

export const webAPIUrl = `${server}/api`;

export const authSettings = {
  domain: 'dev-66s14eg78q03bxdn.us.auth0.com',
  client_id: 'rNYMrVvOGfvtBphpmDvylgzYO2D8vmAv',
  redirect_uri: window.location.origin + '/signin-callback',
  scope: 'openid profile QandAAPI email',
  audience: 'https://qanda',
};
