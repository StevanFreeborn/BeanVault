'use client';

import { useUserContext } from '@/hooks/useUserContext';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { MouseEvent, useState } from 'react';
import { RxChevronDown } from 'react-icons/rx';
import styles from './Navbar.module.css';

export default function Navbar() {
  const { userIsLoading, userState } = useUserContext();
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const { logUserOut } = useUserContext();
  const { push } = useRouter();

  function handleLogOutButtonClick(e: MouseEvent<HTMLButtonElement>) {
    logUserOut();
    push('login');
  }

  return (
    <nav className={styles.navbar}>
      <div className={styles.navbarBrand}>
        <Link href="/">
          <h1>BeanVault</h1>
        </Link>
      </div>
      {userIsLoading ? (
        <div className={styles.navbarsContainer}>
          <div className={styles.navbarLeft}>
            <ul className={styles.nav}>
              <li className={styles.navItem}>Loading...</li>
            </ul>
          </div>
        </div>
      ) : (
        <div className={styles.navbarsContainer}>
          <div className={styles.navbarLeft}>
            <ul className={styles.nav}>
              <li className={styles.navItem}>Home</li>
              <li className={styles.navItem}>Privacy</li>
              <li className={styles.navItem}>
                <button
                  type="button"
                  onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                  className={styles.dropdownButton}
                >
                  Content Management <RxChevronDown />
                </button>
                <ul
                  className={
                    isDropdownOpen
                      ? styles.dropdownNavOpen
                      : styles.dropdownNavClosed
                  }
                >
                  <li className={styles.dropdownNavItem}>
                    <Link
                      onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                      href="products"
                    >
                      Products
                    </Link>
                  </li>
                  <li className={styles.dropdownNavItem}>
                    <Link
                      onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                      href="coupons"
                    >
                      Coupons
                    </Link>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
          <div className={styles.navbarRight}>
            <ul className={styles.nav}>
              {userState !== null ? (
                <>
                  <li className={styles.navItem}>
                    Hello, {userState.user.name}
                  </li>
                  <li className={styles.navItem}>
                    <button
                      onClick={handleLogOutButtonClick}
                      type="button"
                      className={styles.logoutButton}
                    >
                      Logout
                    </button>
                  </li>
                </>
              ) : (
                <>
                  <li className={styles.navItem}>
                    <Link href="register">Register</Link>
                  </li>
                  <li className={styles.navItem}>
                    <Link href="login">Login</Link>
                  </li>
                </>
              )}
            </ul>
          </div>
        </div>
      )}
    </nav>
  );
}
