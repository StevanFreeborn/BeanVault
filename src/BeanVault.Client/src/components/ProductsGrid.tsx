'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { Product } from '@/types/Product';
import Image from 'next/image';
import Link from 'next/link';
import { useEffect, useState } from 'react';
import { toast } from 'react-hot-toast';
import styles from './ProductsGrid.module.css';

export default function ProductsGrid() {
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState<Product[]>([]);
  const { userIsLoading, userState } = useUserContext();
  const { getProducts } = productService({
    client: fetchClient({
      headers: {
        Authorization: `Bearer ${userState?.token}`,
      },
    }),
  });

  useEffect(() => {
    if (userIsLoading) {
      return;
    }

    getProducts()
      .then(p => {
        setProducts(p);
        setIsLoading(false);
      })
      .catch(error => {
        if (error instanceof Error) {
          console.error(error);
          toast.error(error.message);
        }
      });
  }, [products.length, userIsLoading]);

  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  return isLoading ? (
    <div>Loading...</div>
  ) : (
    <div className={styles.container}>
      <div className={styles.gridContainer}>
        {products.map(product => (
          <div className={styles.gridItem} key={product.id}>
            <div className={styles.name}>{product.name}</div>
            <div className={styles.image}>
              <Image
                alt={product.name}
                src={product.imageUrl}
                fill
                style={{ objectFit: 'cover', borderRadius: '5px' }}
              />
            </div>
            <div className={styles.details}>
              <div>{product.categoryName}</div>
              <div>{formatter.format(product.price)}</div>
            </div>
            <div className={styles.description}>{product.description}</div>
            <div className={styles.actionContainer}>
              <Link
                href={`products/details/${product.id}`}
                className={styles.productDetailLink}
              >
                Details
              </Link>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
