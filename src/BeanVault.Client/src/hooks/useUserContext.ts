import { UserContext } from '@/contexts/UserContext';
import { useContext } from 'react';
import { AuthUser } from './../types/AuthUser';

export function useUserContext() {
  const userContext = useContext(UserContext);

  if (userContext === undefined) {
    throw new Error('User context must be used within UserContext provider');
  }

  const { isLoading, userState, dispatchUserAction } = userContext;

  function isLoggedIn() {
    if (userState == null) {
      return false;
    }

    const expirationTime = new Date(userState.expiration).getTime();

    if (expirationTime < Date.now()) {
      return false;
    }

    return true;
  }

  function logUserIn({ user }: { user: AuthUser }) {
    dispatchUserAction({ type: 'login', payload: { user } });
  }

  function logUserOut() {
    dispatchUserAction({ type: 'logout' });
  }

  return {
    isLoading,
    userState,
    isLoggedIn,
    logUserIn,
    logUserOut,
  };
}
