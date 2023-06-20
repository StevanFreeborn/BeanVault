'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { Product } from '@/types/Product';
import Image from 'next/image.js';
import Link from 'next/link.js';
import { MouseEvent, useEffect, useState } from 'react';
import { toast } from 'react-hot-toast';
import { AiFillEdit } from 'react-icons/ai';
import { MdDelete } from 'react-icons/md';
import styles from './ProductsGrid.module.css';

export default function ProductsGrid() {
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState<Product[]>([]);
  const { userState } = useUserContext();
  const { getProducts, deleteProduct } = productService({
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

  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

  async function handleDeleteButtonClick(e: MouseEvent<HTMLButtonElement>) {
    const id = e.currentTarget.dataset.productId;

    if (id == undefined) {
      return;
    }

    try {
      await deleteProduct({ id });
      setProducts(products.filter(p => p.id !== id));
      toast.success('Product deleted');
    } catch (error) {
      if (error instanceof Error) {
        console.error(error);
        toast.error(error.message);
      }
    }
  }

  return isLoading ? (
    <div>Loading...</div>
  ) : (
    <div className={styles.container}>
      <div className={styles.gridContainer}>
        {products.map(product => (
          <div className={styles.gridItem}>
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
                href={`products/edit/${product.id}`}
                className={styles.editProductLink}
              >
                <AiFillEdit />
              </Link>
              <button
                onClick={handleDeleteButtonClick}
                data-product-id={product.id}
                className={styles.deleteProductButton}
                type="button"
              >
                <MdDelete />
              </button>
            </div>
          </div>
        ))}
      </div>
    </div>
  );
}
