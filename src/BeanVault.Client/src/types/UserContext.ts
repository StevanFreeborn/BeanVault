import { AuthUser } from './AuthUser.js';
import { UserAction } from './UserAction';

export type UserContextType = {
  isLoading: boolean;
  userState: AuthUser | null;
  dispatchUserAction: React.Dispatch<UserAction>;
};
