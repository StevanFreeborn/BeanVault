'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { cartService } from '@/services/cartService';
import { productService } from '@/services/productService';
import { FormState } from '@/types/FormState';
import { Product } from '@/types/Product';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import Image from 'next/image';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { ChangeEvent, FormEvent, useEffect, useReducer, useState } from 'react';
import toast from 'react-hot-toast';
import FormField from './FormField';
import styles from './ProductDetails.module.css';

export default function ProductDetails({ productId }: { productId: string }) {
  const initialFormState: FormState = {
    count: {
      labelText: 'Count',
      type: 'number',
      value: '1',
      errors: [],
      inputProps: {
        placeholder: '1',
        required: true,
        min: 1,
      },
    },
  };

  const router = useRouter();
  const [formState, dispatch] = useReducer(formReducer, initialFormState);
  const [product, setProduct] = useState<Product | null>(null);
  const { userState } = useUserContext();
  const client = fetchClient({
    headers: {
      Authorization: `Bearer ${userState?.token}`,
    },
  });

  const { getProductById } = productService({
    client,
  });

  const { addItemToCart } = cartService({
    client,
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

  function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
    dispatch({
      type: 'updateValue',
      payload: { field: e.target.name, value: e.target.value },
    });
  }

  async function handleFormSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    dispatch({ type: 'resetAllErrors' });
    const formDataErrors = getFormErrors({ formState });

    if (formDataErrors.length > 0) {
      formDataErrors.forEach(error => {
        dispatch({ type: 'updateErrors', payload: error });
      });

      return;
    }

    try {
      const formData = getFormData({ formState });

      if (userState?.user.id === undefined) {
        throw new Error('Unable to add item to cart');
      }

      await addItemToCart({
        userId: userState?.user.id,
        cartItem: { productId, ...formData },
      });
      toast.success('Item added to cart');
      router.push('cart');
    } catch (error) {
      if (error instanceof Error) {
        console.error(error);
        toast.error(error.message);
      }
    }
  }

  return product === null ? (
    <div>Loading...</div>
  ) : (
    <form onSubmit={handleFormSubmit} noValidate={true}>
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
            {Object.keys(formState).map(key => {
              const formField = formState[key];
              return (
                <FormField
                  key={key}
                  fieldName={key}
                  formField={formField}
                  onChangeHandler={handleInputChange}
                />
              );
            })}
          </div>
        </div>
        <div className={styles.cardFooter}>
          <Link className={styles.backLink} href="/">
            Back to List
          </Link>
          <button className={styles.addToCartButton} type="submit">
            Add to Cart
          </button>
        </div>
      </div>
    </form>
  );
}
