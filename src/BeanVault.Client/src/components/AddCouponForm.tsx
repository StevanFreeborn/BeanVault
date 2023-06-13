'use client';

import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { FormEvent, useReducer } from 'react';
import { toast } from 'react-hot-toast';
import styles from './AddCouponForm.module.css';

export default function AddCouponForm() {
  const router = useRouter();

  const initialFormState: FormState = {
    couponCode: {
      labelText: 'Coupon Code',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'BuyOneGetOne',
        required: true,
        maxLength: 150,
      },
    },
    discountAmount: {
      labelText: 'Discount Amount',
      type: 'number',
      value: '',
      errors: [],
      inputProps: {
        required: true,
        placeholder: '1',
        min: '1',
      },
    },
    minAmount: {
      labelText: 'Minimum Amount',
      type: 'number',
      value: '',
      errors: [],
      inputProps: {
        required: true,
        placeholder: '1',
        min: '1',
      },
    },
  };

  const [formState, dispatch] = useReducer(formReducer, initialFormState);

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
      const { addCoupon } = couponService({ client: fetchClient() });
      await addCoupon({ newCoupon: formData });
      router.push('coupons');
    } catch (error) {
      if (error instanceof Error) {
        console.log(error);
        toast.error(error.message);
      }
    }
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Add Coupon</h1>
      <hr className={styles.separator} />
      <form
        className={styles.addCouponForm}
        onSubmit={handleFormSubmit}
        noValidate={true}
      >
        {Object.keys(formState).map(key => {
          const formField = formState[key];
          return (
            <div key={key} className={styles.formGroup}>
              <label className={styles.formLabel} htmlFor={key}>
                {formField.labelText}
              </label>
              <input
                {...formField.inputProps}
                id={key}
                name={key}
                className={styles.formControl}
                type={formField.type}
                value={formField.value}
                onChange={e => {
                  dispatch({
                    type: 'updateValue',
                    payload: { field: e.target.name, value: e.target.value },
                  });
                }}
              />
              <ul className={styles.error}>
                {formField.errors.map((error, i) => (
                  <li key={i}>{error}</li>
                ))}
              </ul>
            </div>
          );
        })}
        <div className={styles.actionContainer}>
          <Link className={styles.cancelButton} href="coupons">
            Cancel
          </Link>
          <button className={styles.addButton} title="Add Coupon" type="submit">
            Add
          </button>
        </div>
      </form>
    </div>
  );
}
