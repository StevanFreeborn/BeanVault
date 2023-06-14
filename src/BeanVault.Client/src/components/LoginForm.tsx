'use client';

import { ValidationError } from '@/errors/validationError';
import { useUserContext } from '@/hooks/useUserContext';
import { fetchClient } from '@/http/fetchClient';
import { authService } from '@/services/authService';
import { FormState } from '@/types/FormState';
import { formReducer, getFormData, getFormErrors } from '@/utils/forms';
import { useRouter } from 'next/navigation';
import { ChangeEvent, FormEvent, useReducer, useState } from 'react';
import toast from 'react-hot-toast';
import FormField from './FormField';
import styles from './LoginForm.module.css';

export default function LoginForm() {
  const initialFormState: FormState = {
    username: {
      labelText: 'Username',
      type: 'text',
      value: '',
      errors: [],
      inputProps: {
        placeholder: 'myusername@domain.com',
        required: true,
        autoComplete: 'username',
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
  const { logUserIn } = useUserContext();

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
      const { login } = authService({ client: fetchClient() });
      var user = await login({ userLogin: formData });
      logUserIn({ user });
      router.push('/');
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
      <h1 className={styles.formHeader}>Login</h1>
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
          <button className={styles.loginButton} title="Login" type="submit">
            Login
          </button>
        </div>
      </form>
    </div>
  );
}
