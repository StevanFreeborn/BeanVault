import { fetchClient } from '@/types/fetchClient.js';

export function couponService({ client }: { client: fetchClient }) {
  const COUPON_SERVICE_URL = process.env.NEXT_PUBLIC_COUPON_SERVICE_URL;

  async function getCoupons() {
    return await client.get(`${COUPON_SERVICE_URL}/api/coupons`);
  }

  return {
    getCoupons,
  };
}
