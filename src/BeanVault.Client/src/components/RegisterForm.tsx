'use client';

import { ValidationError } from '@/errors/validationError';
import { fetchClient } from '@/http/fetchClient';
import { authService } from '@/services/authService';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import { useRouter } from 'next/navigation';
import { ChangeEvent, FormEvent, useReducer, useState } from 'react';
import toast from 'react-hot-toast';
import FormField from './FormField';
import styles from './RegisterForm.module.css';

export default function LoginForm() {
  const initialFormState: FormState = {
    email: {
      labelText: 'Email',
      type: 'email',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'myemail@domain.com',
        required: true,
      },
    },
    name: {
      labelText: 'Name',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'Your name',
        required: true,
        maxLength: 150,
      },
    },
    phoneNumber: {
      labelText: 'Phone Number',
      type: 'tel',
      value: '',
      errors: [],
      inputProps: {
        placeholder: '(555) 555-5555',
        required: true,
      },
    },
    password: {
      labelText: 'Password',
      type: 'password',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'astrongpassword',
        required: true,
        autoComplete: 'current-password',
      },
    },
  };

  const router = useRouter();
  const [formErrors, setFormErrors] = useState<string[]>([]);
  const [formState, dispatch] = useReducer(formReducer, initialFormState);

  function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
    dispatch({
      type: 'updateValue',
      payload: { field: e.target.name, value: e.target.value },
    });
  }

  async function handleFormSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();

    setFormErrors([]);
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
      const { register } = authService({ client: fetchClient() });
      await register({ newUser: formData });
      toast.success('Registration successful');
      router.push('login');
    } catch (error) {
      if (error instanceof ValidationError) {
        setFormErrors(error.errors);
        return;
      }

      if (error instanceof Error) {
        console.log(error);
        toast.error(error.message);
        return;
      }
    }
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Register</h1>
      <hr className={styles.separator} />
      <ul className={styles.formErrors}>
        {formErrors.map((error, i) => (
          <li key={i}>{error}</li>
        ))}
      </ul>
      <form
        className={styles.loginForm}
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
        <div className={styles.actionContainer}>
          <button
            className={styles.registerButton}
            title="Register"
            type="submit"
          >
            Register
          </button>
        </div>
      </form>
    </div>
  );
}
