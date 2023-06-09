import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import Link from 'next/link.js';
import { AiOutlinePlusSquare } from 'react-icons/ai';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './Page.module.css';

export default async function CouponsPage() {
  const { getCoupons } = couponService({ client: fetchClient() });
  const coupons = await getCoupons();
  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  return (
    <div className={styles.card}>
      <div className={styles.cardHeader}>
        <h1>Coupons List</h1>
      </div>
      <div className={styles.cardBody}>
        <div className={styles.addButtonContainer}>
          <Link
            href="coupons/add"
            title="Add New Coupon"
            className={styles.addCouponLink}
          >
            <AiOutlinePlusSquare />
            Create New Coupon
          </Link>
        </div>
        <table className={styles.couponTable}>
          <thead className={styles.couponTableHeader}>
            <tr>
              <th>Coupon Code</th>
              <th>Discount Amount</th>
              <th>Minimum Amount</th>
            </tr>
          </thead>
          <tbody className={styles.couponTableBody}>
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
      </div>
    </div>
  );
}
