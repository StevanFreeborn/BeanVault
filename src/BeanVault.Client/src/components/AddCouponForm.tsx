'use client';

import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { FormAction } from '@/types/FormAction';
import { FormData } from '@/types/FormData';
import { FormState } from '@/types/FormState';
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

  function formReducer(state: FormState, action: FormAction) {
    switch (action.type) {
      case 'updateValue': {
        const field = state[action.payload.field];
        const newField = { ...field };
        newField.value = action.payload.value;
        newField.errors = [];
        return {
          ...state,
          [action.payload.field]: newField,
        };
      }
      case 'updateErrors': {
        const field = state[action.payload.field];
        const newField = { ...field };
        newField.errors.push(action.payload.error);
        return {
          ...state,
          [action.payload.field]: newField,
        };
      }
      case 'resetAllErrors': {
        const newState = { ...state };
        Object.keys(state).forEach(key => (newState[key].errors = []));
        return newState;
      }
      default:
        return state;
    }
  }

  const [formState, dispatch] = useReducer(formReducer, initialFormState);

  async function handleFormSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    dispatch({ type: 'resetAllErrors' });

    const formData: FormData = Object.keys(formState).reduce((prev, curr) => {
      return {
        ...prev,
        [curr]: formState[curr].value,
      };
    }, {});

    const formDataErrors = Object.keys(formData)
      .map(field => {
        const errors = [];
        const formDataValue = formData[field];
        const formField = formState[field];

        if (formField.inputProps?.required && !formDataValue) {
          errors.push({
            field,
            error: `${formState[field].labelText} is required.`,
          });
        }

        if (
          formField.inputProps?.maxLength &&
          formDataValue.length > formField.inputProps.maxLength
        ) {
          errors.push({
            field,
            error: `${formState[field].labelText} cannot be more than ${formField.inputProps.maxLength} characters.`,
          });
        }

        if (
          formField.inputProps?.min &&
          Number(formDataValue) < Number(formField.inputProps.min)
        ) {
          errors.push({
            field,
            error: `${formState[field].labelText} must be greater than or equal to ${formField.inputProps.min}.`,
          });
        }

        return errors;
      })
      .flat();

    if (formDataErrors.length > 0) {
      formDataErrors.forEach(error => {
        dispatch({ type: 'updateErrors', payload: error });
      });

      return;
    }

    try {
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
