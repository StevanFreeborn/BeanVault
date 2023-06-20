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

  const { isLoading, userState } = useUserContext();
  const router = useRouter();
  const [formState, dispatch] = useReducer(formReducer, initialFormState);
  const { getProductById } = productService({
    client: fetchClient({
      headers: { Authorization: `Bearer ${userState?.token}` },
    }),
  });

  // TODO: populate form state with product info returned
  // TODO: might need to also show loading state until fetching
  // product is complete

  useEffect(() => {
    if (isLoading) {
      return;
    }

    getProductById({ id: productId })
      .then(p => console.log(p))
      .catch(error => {
        if (error instanceof Error) {
          console.error(error);
          toast.error(error.message);
        }
      });
  }, [isLoading]);

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

  return isLoading ? (
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
