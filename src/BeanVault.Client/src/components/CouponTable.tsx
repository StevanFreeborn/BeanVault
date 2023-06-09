import { Coupon } from '@/types/Coupon';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './CouponTable.module.css';

export default function CouponTable({ coupons }: { coupons: Coupon[] }) {
  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  return (
    <table className={styles.couponTable}>
      <thead className={styles.couponTableHeader}>
        <tr>
          <th>Coupon Code</th>
          <th>Discount Amount</th>
          <th>Minimum Amount</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {coupons.map(coupon => (
          <tr>
            <td>{coupon.couponCode}</td>
            <td>{formatter.format(coupon.discountAmount)}</td>
            <td>{formatter.format(coupon.minAmount)}</td>
            <td>
              <button
                data-coupon-id={coupon.id}
                title="delete coupon button"
                type="button"
                className={styles.deleteCouponButton}
              >
                <BsFillTrash3Fill />
              </button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}
