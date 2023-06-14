'use client';

import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { Coupon } from '@/types/Coupon';
import { MouseEvent, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './CouponTable.module.css';

export default function CouponTable({
  initialCouponState = [],
}: {
  initialCouponState?: Coupon[];
}) {
  const [coupons, setCoupons] = useState<Coupon[]>(initialCouponState);

  useEffect(() => {
    const { getCoupons } = couponService({ client: fetchClient() });
    getCoupons()
      .then(c => setCoupons(c))
      .catch(error => {
        if (error instanceof Error) {
          toast.error(error.message);
        }
      });
  }, [coupons.length]);

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

    try {
      await deleteCoupon({ id });
      setCoupons(coupons.filter(c => c.id !== id));
      toast.success('Coupon deleted');
    } catch (error) {
      if (error instanceof Error) {
        console.log(error);
        toast.error(error.message);
      }
    }
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
