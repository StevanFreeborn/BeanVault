'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { Coupon } from '@/types/Coupon';
import { MouseEvent, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './CouponTable.module.css';

export default function CouponTable() {
  const { userState } = useUserContext();
  const authorizedClient = fetchClient({
    headers: {
      Authorization: `Bearer ${userState?.token}`,
    },
  });
  const [isLoading, setIsLoading] = useState(true);
  const [coupons, setCoupons] = useState<Coupon[]>([]);

  useEffect(() => {
    const { getCoupons } = couponService({ client: authorizedClient });
    getCoupons()
      .then(c => {
        setCoupons(c);
        setIsLoading(false);
      })
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
    const { deleteCoupon } = couponService({ client: authorizedClient });
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
        console.error(error);
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
        {isLoading ? (
          <tr>
            <td style={{ textAlign: 'center' }} colSpan={100}>
              Loading...
            </td>
          </tr>
        ) : (
          coupons.map(coupon => (
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
          ))
        )}
      </tbody>
    </table>
  );
}
