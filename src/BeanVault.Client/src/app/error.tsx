'use client';

import { useEffect } from 'react';
import { AiOutlineReload } from 'react-icons/ai';
import styles from './error.module.css';

export default function Error({
  error,
  reset,
}: {
  error: Error;
  reset: () => void;
}) {
  useEffect(() => {
    console.error(error);
  }, [error]);

  return (
    <div className={styles.container}>
      <h2>Something went wrong!</h2>
      <button className={styles.resetButton} onClick={() => reset()}>
        <AiOutlineReload />
        Reset
      </button>
    </div>
  );
}
