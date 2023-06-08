import { fetchClient } from '@/types/fetchClient.js';

type Coupon = {
  id: string;
  couponCode: string;
  discountAmount: number;
  minAmount: number;
};

export function couponService({ client }: { client: fetchClient }) {
  const COUPON_SERVICE_URL = process.env.NEXT_PUBLIC_COUPON_SERVICE_URL;

  async function getCoupons(): Promise<Coupon[]> {
    const res = await client.get(`${COUPON_SERVICE_URL}/api/coupons`);

    if (res.ok === false) {
      throw new Error('Unable to get coupons');
    }

    return await res.json();
  }

  return {
    getCoupons,
  };
}
