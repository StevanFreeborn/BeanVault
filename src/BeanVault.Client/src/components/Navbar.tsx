'use client';

import Link from 'next/link';
import { useState } from 'react';
import { RxChevronDown } from 'react-icons/rx';
import styles from './Navbar.module.css';

export default function Navbar() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  return (
    <nav className={styles.navbar}>
      <div className={styles.navbarLeft}>
        <div className={styles.navbarBrand}>
          <Link href="/">
            <h1>BeanVault</h1>
          </Link>
        </div>
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
          <li className={styles.navItem}>
            <Link href="register">Register</Link>
          </li>
          <li className={styles.navItem}>
            <Link href="login">Login</Link>
          </li>
          <li className={styles.navItem}>
            <button type="button" className={styles.logoutButton}>
              Logout
            </button>
          </li>
        </ul>
      </div>
    </nav>
  );
}
