'use client';

import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { productService } from '@/services/productService';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import { useRouter } from 'next/navigation';
import { ChangeEvent, FormEvent, useEffect, useReducer, useState } from 'react';
import toast from 'react-hot-toast';
import ProductForm from './ProductForm';

export default function EditProductForm({ productId }: { productId: string }) {
  const [initialFormState, setInitialFormState] = useState<FormState>({
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
  });

  const { userIsLoading, userState } = useUserContext();
  const [productIsLoading, setProductIsLoading] = useState(true);
  const router = useRouter();
  const [formState, dispatch] = useReducer(formReducer, initialFormState);
  const { getProductById } = productService({
    client: fetchClient({
      headers: { Authorization: `Bearer ${userState?.token}` },
    }),
  });

  useEffect(() => {
    if (userIsLoading) {
      return;
    }

    getProductById({ id: productId })
      .then(p => {
        for (const key in p) {
          if (key === 'id') {
            continue;
          }

          dispatch({
            type: 'updateValue',
            payload: { field: key, value: p[key] },
          });
        }

        setProductIsLoading(false);
      })
      .catch(error => {
        if (error instanceof Error) {
          console.error(error);
          toast.error(error.message);
        }
      });
  }, [userIsLoading]);

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
      const { updateProductById } = productService({
        client: fetchClient({
          headers: { Authorization: `Bearer ${userState?.token}` },
        }),
      });
      await updateProductById({
        updatedProduct: { id: productId, ...formData },
      });
      router.push('products');
    } catch (error) {
      if (error instanceof Error) {
        console.error(error);
        toast.error(error.message);
      }
    }
  }

  return productIsLoading ? (
    <div>Loading...</div>
  ) : (
    <ProductForm
      formHeaderText="Edit Product"
      formState={formState}
      formSubmitHandler={handleFormSubmit}
      inputChangeHandler={handleInputChange}
      actionButtonText="Save"
    />
  );
}
