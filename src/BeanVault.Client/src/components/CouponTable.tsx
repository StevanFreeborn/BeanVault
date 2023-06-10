'use client';

import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { Coupon } from '@/types/Coupon';
import { MouseEvent, useState } from 'react';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './CouponTable.module.css';

export default function CouponTable({
  initialCouponState = [],
}: {
  initialCouponState?: Coupon[];
}) {
  const [coupons, setCoupons] = useState(initialCouponState);

  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  async function handleDeleteButtonClick(e: MouseEvent<HTMLButtonElement>) {
    const { deleteCoupon } = couponService({ client: fetchClient() });
    const id = e.currentTarget.dataset.couponId;
    if (id == undefined) {
      return;
    }
    await deleteCoupon({ id });
    setCoupons(coupons.filter(c => c.id !== id));
  }

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
          <tr key={coupon.id}>
            <td>{coupon.couponCode}</td>
            <td>{formatter.format(coupon.discountAmount)}</td>
            <td>{formatter.format(coupon.minAmount)}</td>
            <td>
              <button
                data-coupon-id={coupon.id}
                title="delete coupon button"
                type="button"
                className={styles.deleteCouponButton}
                onClick={handleDeleteButtonClick}
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
