import { User } from './User';

export type AuthUser = {
  user: User;
  token: string;
  expiration: string;
};
