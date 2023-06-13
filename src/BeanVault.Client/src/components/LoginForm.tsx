'use client';

import { FormState } from '@/types/FormState';
import { formReducer } from '@/utils/forms';
import { ChangeEvent, FormEvent, useReducer } from 'react';
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

  const [formState, dispatch] = useReducer(formReducer, initialFormState);

  function handleInputChange(e: ChangeEvent<HTMLInputElement>) {
    dispatch({
      type: 'updateValue',
      payload: { field: e.target.name, value: e.target.value },
    });
  }

  function handleFormSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    // TODO: wire up login form
    console.log(formState);
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Login</h1>
      <hr className={styles.separator} />
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
