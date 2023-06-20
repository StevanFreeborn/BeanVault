import { FetchClientType } from '@/types/FetchClientType';
import { FormData } from '@/types/FormData';
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

  async function addProduct({
    newProduct,
  }: {
    newProduct: FormData;
  }): Promise<Product> {
    const res = await client.post(
      `${PRODUCT_SERVICE_URL}/api/products`,
      undefined,
      newProduct
    );

    if (res.ok === false) {
      throw new Error('Unable to add product');
    }

    return await res.json();
  }

  async function deleteProduct({ id }: { id: string }) {
    const res = await client.delete(
      `${PRODUCT_SERVICE_URL}/api/products/${id}`
    );

    if (res.ok === false) {
      throw new Error('Unable to delete product');
    }
  }

  return {
    getProducts,
    addProduct,
    deleteProduct,
  };
}
