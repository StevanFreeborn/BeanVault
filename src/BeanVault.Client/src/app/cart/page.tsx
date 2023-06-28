import ProtectedPage from '@/components/ProtectedPage';
import ShoppingCart from '@/components/ShoppingCart';
import Link from 'next/link';
import { AiOutlineShoppingCart } from 'react-icons/ai';
import styles from './page.module.css';

export default function CartPage() {
  return (
    <ProtectedPage>
      <div className={styles.cart}>
        <div className={styles.cartHeader}>
          <h2 className={styles.cartHeaderTitle}>
            <AiOutlineShoppingCart /> Shopping Cart
          </h2>
          <Link className={styles.continueShoppingLink} href={'/'}>
            Continue Shopping
          </Link>
        </div>
        <div className={styles.cartBody}>
          <ShoppingCart />
        </div>
        <div className={styles.cartFooter}>
          <button className={styles.emailButton} type="button">
            Email Cart
          </button>
          <Link className={styles.checkoutLink} href="#">
            Checkout
          </Link>
        </div>
      </div>
    </ProtectedPage>
  );
}
