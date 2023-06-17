import { FetchClientType } from '@/types/FetchClientType';
import { Product } from '@/types/Product';

export function productService({ client }: { client: FetchClientType }) {
  const PRODUCT_SERVICE_URL = process.env.NEXT_PUBLIC_PRODUCT_SERVICE_URL;

  async function getProducts(): Promise<Product[]> {
    const res = await client.get(`${PRODUCT_SERVICE_URL}/api/products`);

    if (res.ok === false) {
      throw new Error('Unable to get products');
    }

    return await res.json();
  }

  return {
    getProducts,
  };
}
