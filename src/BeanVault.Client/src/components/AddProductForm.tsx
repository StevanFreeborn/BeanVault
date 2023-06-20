'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import Link from 'next/link';
import { useRouter } from 'next/navigation';
import { ChangeEvent, FormEvent, useReducer } from 'react';
import toast from 'react-hot-toast';
import styles from './AddProductForm.module.css';
import FormField from './FormField';

export default function AddProductForm() {
  const initialFormState: FormState = {
    name: {
      labelText: 'Name',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'Honduran',
        required: true,
        maxLength: 150,
      },
    },
    price: {
      labelText: 'Price',
      type: 'number',
      value: '',
      errors: [],
      inputProps: {
        placeholder: '9.99',
        required: true,
        min: 0,
      },
    },
    description: {
      labelText: 'Description',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'Brews a fine cup of coffee',
        required: true,
      },
    },
    categoryName: {
      labelText: 'Category Name',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'Dark Roast',
        required: true,
        maxLength: 150,
      },
    },
    imageUrl: {
      labelText: 'Image Url',
      type: 'url',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'https://animage.com',
        required: true,
        maxLength: 150,
      },
    },
  };

  const { userState } = useUserContext();
  const router = useRouter();
  const [formState, dispatch] = useReducer(formReducer, initialFormState);

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
      const { addProduct } = productService({
        client: fetchClient({
          headers: { Authorization: `Bearer ${userState?.token}` },
        }),
      });
      await addProduct({ newProduct: formData });
      router.push('products');
    } catch (error) {
      if (error instanceof Error) {
        console.error(error);
        toast.error(error.message);
      }
    }
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Add Product</h1>
      <hr className={styles.separator} />
      <form
        className={styles.addProductForm}
        onSubmit={handleFormSubmit}
        noValidate={true}
      >
        {Object.keys(formState).map(key => {
          const formField = formState[key];
          return (
            <FormField
              fieldName={key}
              formField={formField}
              onChangeHandler={handleInputChange}
            />
          );
        })}
        <div className={styles.actionContainer}>
          <Link className={styles.cancelButton} href="products">
            Cancel
          </Link>
          <button
            className={styles.addButton}
            title="Add Product"
            type="submit"
          >
            Add
          </button>
        </div>
      </form>
    </div>
  );
}
