'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { useRouter } from 'next/navigation';
import { ReactNode, useEffect } from 'react';

export default function AnonymousPage({ children }: { children: ReactNode }) {
  const { userIsLoading, userState } = useUserContext();
  const { push } = useRouter();

  useEffect(() => {
    if (userState !== null) {
      push('/');
      return;
    }
  }, [userIsLoading, userState]);

  return userIsLoading || userState !== null ? <div>Loading...</div> : children;
}
