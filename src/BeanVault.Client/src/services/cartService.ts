import { Cart } from '@/types/Cart.js';
import { FetchClientType } from '@/types/FetchClientType';
import { FormData } from '@/types/FormData';

export function cartService({ client }: { client: FetchClientType }) {
  const CART_SERVICE_URL = process.env.NEXT_PUBLIC_CART_SERVICE_URL;

  async function addItemToCart({
    userId,
    cartItem,
  }: {
    userId: string;
    cartItem: FormData;
  }) {
    const res = await client.put(
      `${CART_SERVICE_URL}/api/carts/${userId}/items`,
      undefined,
      cartItem
    );

    if (res.ok === false) {
      throw new Error('Unable to add item to cart');
    }

    return await res.json();
  }

  async function applyCoupon({
    userId,
    applyCouponReq,
  }: {
    userId: string;
    applyCouponReq: FormData;
  }) {
    const res = await client.put(
      `${CART_SERVICE_URL}/api/carts/${userId}/apply-coupon`,
      undefined,
      applyCouponReq
    );

    if (res.ok === false) {
      throw new Error('Unable to apply coupon code to cart');
    }

    return await res.json();
  }

  async function getCart({ userId }: { userId: string }): Promise<Cart> {
    const res = await client.get(`${CART_SERVICE_URL}/api/carts/${userId}`);

    if (res.ok === false) {
      throw new Error('Unable to get cart');
    }

    return await res.json();
  }

  return {
    addItemToCart,
    applyCoupon,
    getCart,
  };
}
