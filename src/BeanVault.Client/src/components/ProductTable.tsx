'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { Product } from '@/types/Product.js';
import Link from 'next/link.js';
import { MouseEvent, useEffect, useState } from 'react';
import toast from 'react-hot-toast';
import { AiFillEdit } from 'react-icons/ai';
import { BsFillTrash3Fill } from 'react-icons/bs';
import styles from './ProductTable.module.css';

export default function ProductsTable() {
  const { userState } = useUserContext();
  const { getProducts, deleteProduct } = productService({
    client: fetchClient({
      headers: {
        Authorization: `Bearer ${userState?.token}`,
      },
    }),
  });
  const [isLoading, setIsLoading] = useState(true);
  const [products, setProducts] = useState<Product[]>([]);

  useEffect(() => {
    getProducts()
      .then(p => {
        setProducts(p);
        setIsLoading(false);
      })
      .catch(error => {
        if (error instanceof Error) {
          toast.error(error.message);
        }
      });
  }, [products.length]);

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

  return (
    <table className={styles.productTable}>
      <thead className={styles.productTableHeader}>
        <tr>
          <th>Name</th>
          <th>Category Name</th>
          <th>Price</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        {isLoading ? (
          <tr>
            <td style={{ textAlign: 'center' }} colSpan={100}>
              Loading...
            </td>
          </tr>
        ) : (
          products.map(product => (
            <tr key={product.id}>
              <td>{product.name}</td>
              <td>{product.categoryName}</td>
              <td>{formatter.format(product.price)}</td>
              <td>
                <div className={styles.actionContainer}>
                  <Link
                    href={`products/edit/${product.id}`}
                    className={styles.editProductLink}
                  >
                    <AiFillEdit />
                  </Link>
                  <button
                    data-product-id={product.id}
                    title="delete product button"
                    type="button"
                    className={styles.deleteProductButton}
                    onClick={handleDeleteButtonClick}
                  >
                    <BsFillTrash3Fill />
                  </button>
                </div>
              </td>
            </tr>
          ))
        )}
      </tbody>
    </table>
  );
}
