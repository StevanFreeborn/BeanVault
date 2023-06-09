'use client';

import Link from 'next/link';
import { HTMLInputTypeAttribute, InputHTMLAttributes, useReducer } from 'react';
import styles from './AddCouponForm.module.css';

export default function AddCouponForm() {
  type FormAction =
    | { type: 'updateValue'; payload: { name: string; value: string } }
    | { type: 'updateErrors' };

  type FormState = { [key: string]: FormField };

  type FormField = {
    labelText: string;
    type: HTMLInputTypeAttribute;
    value: string;
    errors: string[];
    inputProps?: InputHTMLAttributes<HTMLInputElement>;
  };

  const initialFormState: FormState = {
    couponCode: {
      labelText: 'Coupon Code',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'BuyOneGetOne',
      },
    },
    discountAmount: {
      labelText: 'Discount Amount',
      type: 'number',
      value: '',
      errors: [],
      inputProps: {
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
        placeholder: '0',
        min: '0',
      },
    },
  };

  function formReducer(state: FormState, action: FormAction) {
    switch (action.type) {
      case 'updateValue': {
        const field = state[action.payload.name];
        const newField = { ...field };
        newField.value = action.payload.value;
        return {
          ...state,
          [action.payload.name]: newField,
        };
      }
      case 'updateErrors':
        return state;
      default:
        return state;
    }
  }

  const [formState, dispatch] = useReducer(formReducer, initialFormState);

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Add Coupon</h1>
      <hr className={styles.separator} />
      <form className={styles.addCouponForm}>
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
                onChange={e =>
                  dispatch({
                    type: 'updateValue',
                    payload: { name: e.target.name, value: e.target.value },
                  })
                }
              />
              <ul className={styles.error}>
                {formField.errors.map(error => (
                  <li>{error}</li>
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
