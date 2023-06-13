import { USER_KEY } from '@/constants';
import { UserContext } from '@/contexts/UserContext';
import { AuthUser } from '@/types/AuthUser';
import { UserAction } from '@/types/UserAction';
import { UserReducer } from '@/types/UserReducer';
import { ReactNode, useEffect, useReducer, useState } from 'react';

export function userReducer(
  state: AuthUser | null,
  action: UserAction
): AuthUser | null {
  switch (action.type) {
    case 'login':
      localStorage.setItem(USER_KEY, JSON.stringify(action.payload.user));
      return action.payload.user;
    case 'logout':
      localStorage.removeItem(USER_KEY);
      return null;
    default:
      return state;
  }
}

export function createInitialState(): AuthUser | null {
  const storedUser = localStorage.getItem(USER_KEY);

  if (storedUser === null) {
    return null;
  }

  return JSON.parse(storedUser);
}

export function UserContextProvider({
  initialUserState = createInitialState,
  userStateReducer = userReducer,
  children,
}: {
  initialUserState?: () => AuthUser | null;
  userStateReducer?: UserReducer;
  children: ReactNode;
}) {
  const [isLoading, setIsLoading] = useState(true);
  const [userState, dispatchUserAction] = useReducer(userStateReducer, null);

  useEffect(() => {
    const initialState = initialUserState();

    if (initialState !== null) {
      dispatchUserAction({
        type: 'login',
        payload: { user: initialState },
      });
    }

    setIsLoading(false);
  }, []);

  return (
    <UserContext.Provider value={{ isLoading, userState, dispatchUserAction }}>
      {children}
    </UserContext.Provider>
  );
}
