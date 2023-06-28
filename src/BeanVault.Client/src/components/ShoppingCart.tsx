'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { cartService } from '@/services/cartService';
import { Cart } from '@/types/Cart';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import Image from 'next/image.js';
import { ChangeEvent, FormEvent, useEffect, useReducer, useState } from 'react';
import { toast } from 'react-hot-toast';
import { BsFillTrash3Fill } from 'react-icons/bs';
import FormField from './FormField';
import styles from './ShoppingCart.module.css';

export default function ShoppingCart() {
  const initialFormState: FormState = {
    coupon: {
      labelText: 'Coupon',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        required: true,
      },
    },
  };

  const { userIsLoading, userState } = useUserContext();
  const client = fetchClient({
    headers: {
      Authorization: `Bearer ${userState?.token}`,
    },
  });
  const { applyCoupon, getCart } = cartService({ client });
  const [formState, dispatch] = useReducer(formReducer, initialFormState);
  const [cart, setCart] = useState<Cart | null>(null);

  useEffect(() => {
    if (userState === null) {
      return;
    }

    getCart({ userId: userState.user.id })
      .then(c => {
        setCart(c);
        dispatch({
          type: 'updateValue',
          payload: { field: 'coupon', value: c.couponCode },
        });
      })
      .catch(error => {
        if (error instanceof Error) {
          console.error(error);
          toast.error(error.message);
        }
      });
  }, [userIsLoading]);

  const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
  });

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
        throw new Error('Unable to apply coupon to cart');
      }

      await applyCoupon({
        userId: userState.user.id,
        applyCouponReq: formData,
      });
      toast.success('Coupon applied');
    } catch (error) {
      if (error instanceof Error) {
        console.error(error);
        toast.error(error.message);
      }
    }
  }

  return cart === null ? (
    <div>Loading...</div>
  ) : (
    <div className={styles.container}>
      <table className={styles.productsTable}>
        <thead className={styles.productsTableHeader}>
          <tr>
            <th colSpan={2}>Product Details</th>
            <th>Count</th>
            <th>Price</th>
            <th></th>
            <th style={{ textAlign: 'right' }}>Subtotal</th>
          </tr>
        </thead>
        <tbody>
          {cart.items.map(item => {
            return (
              <tr key={item.id}>
                <td>
                  <div className={styles.image}>
                    <Image
                      alt={item.name}
                      src={item.imageUrl}
                      fill
                      style={{ objectFit: 'cover', borderRadius: '5px' }}
                    />
                  </div>
                </td>
                <td>{item.name}</td>
                <td>{item.count}</td>
                <td>{formatter.format(item.price)}</td>
                <td>
                  <button
                    data-item-id={item.id}
                    title="delete item button"
                    type="button"
                    className={styles.deleteItemButton}
                  >
                    <BsFillTrash3Fill />
                  </button>
                </td>
                <td style={{ textAlign: 'right' }}>
                  {formatter.format(item.subTotal)}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
      <div className={styles.cartDetails}>
        <form
          className={styles.applyCouponForm}
          onSubmit={handleFormSubmit}
          noValidate={true}
        >
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
          <button className={styles.applyCouponButton} type="submit">
            Apply
          </button>
        </form>
        <div className={styles.cartTotals}>
          <div className={styles.total}>
            <div>Total:</div>
            <div>{formatter.format(cart.total)}</div>
          </div>
          <div className={styles.total}>
            <div>Discount:</div>{' '}
            <div>{formatter.format(cart.discountAmount)}</div>
          </div>
          <div className={styles.total}>
            <div>Discounted Total:</div>
            <div>{formatter.format(cart.discountedTotal)}</div>
          </div>
        </div>
      </div>
    </div>
  );
}
