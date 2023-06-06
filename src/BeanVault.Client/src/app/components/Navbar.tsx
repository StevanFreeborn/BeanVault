'use client';

import { useState } from 'react';
import { RxChevronDown } from 'react-icons/rx';
import styles from './Navbar.module.css';

export default function Navbar() {
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);

  return (
    <nav className={styles.navbar}>
      <div className={styles.navbarBrand}>
        <h1>BeanVault</h1>
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
              isDropdownOpen ? styles.dropdownNavOpen : styles.dropdownNavClosed
            }
          >
            <li className={styles.dropdownNavItem}>Coupon</li>
          </ul>
        </li>
      </ul>
    </nav>
  );
}
