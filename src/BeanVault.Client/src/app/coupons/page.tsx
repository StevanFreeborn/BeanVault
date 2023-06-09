import CouponTable from '@/components/CouponTable';
import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import Link from 'next/link';
import { AiOutlinePlusSquare } from 'react-icons/ai';
import styles from './Page.module.css';

export default async function CouponsPage() {
  const { getCoupons } = couponService({ client: fetchClient() });
  const coupons = await getCoupons();

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
        <CouponTable coupons={coupons} />
      </div>
    </div>
  );
}