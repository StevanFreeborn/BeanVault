import Link from 'next/link';
import styles from './AddCouponForm.module.css';

export default function AddCouponForm() {
  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Add Coupon</h1>
      <hr className={styles.separator} />
      <form className={styles.addCouponForm}>
        <div className={styles.formGroup}>
          <label className={styles.formLabel} htmlFor="couponCode">
            Coupon Code
          </label>
          <input
            placeholder="BuyOneGetOne"
            id="couponCode"
            name="couponCode"
            className={styles.formControl}
            type="text"
          />
          <span className={styles.error}></span>
        </div>
        <div className={styles.formGroup}>
          <label className={styles.formLabel} htmlFor="discountAmount">
            Discount Amount ($)
          </label>
          <input
            placeholder="1"
            id="discountAmount"
            name="discountAmount"
            className={styles.formControl}
            type="number"
            min="1"
          />
          <span className={styles.error}></span>
        </div>
        <div className={styles.formGroup}>
          <label className={styles.formLabel} htmlFor="minAmount">
            Minimum Amount ($)
          </label>
          <input
            placeholder="1"
            id="minAmount"
            name="minAmount"
            className={styles.formControl}
            type="number"
            min="1"
          />
          <span className={styles.error}></span>
        </div>
        <div className={styles.actionContainer}>
          <Link className={styles.cancelButton} href="coupons">
            Cancel
          </Link>
          <button className={styles.addButton} title="Add Coupon" type="submit">
            Add
          </button>
        </div>
      </form>
    </div>
  );
}
