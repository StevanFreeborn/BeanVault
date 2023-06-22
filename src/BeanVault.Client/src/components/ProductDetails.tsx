'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { Product } from '@/types/Product';
import Image from 'next/image';
import Link from 'next/link.js';
import { useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import styles from './ProductDetails.module.css';

export default function ProductDetails({ productId }: { productId: string }) {
  const [product, setProduct] = useState<Product | null>(null);
  const { userState } = useUserContext();
  const { getProductById } = productService({
    client: fetchClient({
      headers: {
        Authorization: `Bearer ${userState?.token}`,
      },
    }),
  });

  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  useEffect(() => {
    getProductById({ id: productId })
      .then(p => setProduct(p))
      .catch(error => {
        if (error instanceof Error) {
          console.error(error);
          toast.error(error.message);
        }
      });
  }, []);

  return product === null ? (
    <div>Loading...</div>
  ) : (
    <div className={styles.card}>
      <div className={styles.cardHeader}>
        <div>{product.name}</div>
        <div>{formatter.format(product.price)}</div>
      </div>
      <div className={styles.cardBody}>
        <div className={styles.productImage}>
          <Image
            alt={product.name}
            src={product.imageUrl}
            fill
            style={{ objectFit: 'cover', borderRadius: '5px' }}
          />
        </div>
        <div className={styles.productDetails}>
          <div className={styles.categoryName}>{product.categoryName}</div>
          <div className={styles.productDescription}>
            <div>Description</div>
            <div>{product.description}</div>
          </div>
        </div>
      </div>
      <div className={styles.cardFooter}>
        <Link className={styles.backLink} href="/">
          Back to List
        </Link>
        <button className={styles.addToCartButton} type="button">
          Add to Cart
        </button>
      </div>
    </div>
  );
}
