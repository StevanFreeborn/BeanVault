import { Coupon } from '@/types/Coupon';
import { FetchClientType } from '@/types/FetchClientType';
import { FormData } from '@/types/FormData';

export function couponService({ client }: { client: FetchClientType }) {
  const COUPON_SERVICE_URL = process.env.NEXT_PUBLIC_COUPON_SERVICE_URL;

  async function getCoupons(): Promise<Coupon[]> {
    const res = await client.get(`${COUPON_SERVICE_URL}/api/coupons`);

    if (res.ok === false) {
      throw new Error('Unable to get coupons');
    }

    return await res.json();
  }

  async function addCoupon({
    newCoupon,
  }: {
    newCoupon: FormData;
  }): Promise<Coupon> {
    const res = await client.post(
      `${COUPON_SERVICE_URL}/api/coupons`,
      undefined,
      newCoupon
    );

    if (res.ok === false) {
      throw new Error('Unable to add coupon');
    }

    return await res.json();
  }

  async function deleteCoupon({ id }: { id: string }) {
    const res = await client.delete(`${COUPON_SERVICE_URL}/api/coupons/${id}`);

    if (res.ok === true) {
      return;
    }

    throw new Error('Unable to delete coupon');
  }

  return {
    getCoupons,
    addCoupon,
    deleteCoupon,
  };
}
