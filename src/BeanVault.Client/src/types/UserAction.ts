import { AuthUser } from './AuthUser';

export type UserAction =
  | { type: 'login'; payload: { user: AuthUser } }
  | { type: 'logout' };
