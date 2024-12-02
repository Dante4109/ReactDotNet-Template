/** @jsxImportSource @emotion/react */
import { css } from '@emotion/react';

import React from 'react';
import user from './assets/user.svg';

export const UserIcon = () => (
  <img
    src={user}
    alt="User"
    css={css`
      width: 12px;
      opacity: 0.6;
    `}
  />
);
