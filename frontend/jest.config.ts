// jest.config.ts
export default {
  preset: 'ts-jest',
  testEnvironment: 'jsdom',
  transform: {
    '^.+\\.tsx?$': 'ts-jest',
    '^.+\\.(js|jsx)$': 'babel-jest',
    'node_modules/variables/.+\\.(j|t)sx?$': 'babel-jest',
  },
  transformIgnorePatterns: ['node_modules/(?!(@auth0/auth0-spa-js)/)'],
  moduleNameMapper: {
    '\\.(css|less|sass|scss)$': 'identity-obj-proxy',
    '\\.(jpg|jpeg|png|gif|eot|otf|webp|svg|ttf|woff|woff2|mp4|webm|wav|mp3|m4a|aac|oga)$':
      'jest-transform-stub',
  },
  setupFilesAfterEnv: ['<rootDir>/jest.setup.ts'],
};
