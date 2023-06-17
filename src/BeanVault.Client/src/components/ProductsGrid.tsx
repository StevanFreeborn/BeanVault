'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { Product } from '@/types/Product';
import Image from 'next/image.js';
import { useEffect, useState } from 'react';
import { toast } from 'react-hot-toast';
import styles from './ProductsGrid.module.css';

export default function ProductsGrid() {
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState<Product[]>([]);
  const { userState } = useUserContext();
  const { getProducts } = productService({
    client: fetchClient({
      headers: {
        Authorization: `Bearer ${userState?.token}`,
      },
    }),
  });

  useEffect(() => {
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
  });

  return isLoading ? (
    <div>Loading...</div>
  ) : (
    <div className={styles.container}>
      <div className={styles.gridContainer}>
        {products.map(product => (
          <div className={styles.gridItem}>
            <div
              style={{ width: '100%', height: '100%', position: 'relative' }}
            >
              <Image
                alt={product.name}
                src={product.imageUrl}
                fill
                style={{ objectFit: 'cover' }}
              />
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
