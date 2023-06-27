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

  return {
    addItemToCart,
  };
}
