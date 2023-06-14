'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { useRouter } from 'next/navigation';
import { ReactNode, useEffect } from 'react';

export default function ProtectedPage({ children }: { children: ReactNode }) {
  const { isLoading, userState } = useUserContext();
  const { push } = useRouter();

  useEffect(() => {
    if (isLoading == false && userState === null) {
      push('/login');
      return;
    }
  }, [isLoading, userState]);

  return isLoading || userState === null ? <div>Loading...</div> : children;
}
