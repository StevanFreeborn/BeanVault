'use client';

import { FormState } from '@/types/FormState';
import { formReducer } from '@/utils/forms';
import { FormEvent, useReducer } from 'react';
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

  const [formState, dispatch] = useReducer(formReducer, initialFormState);

  function handleFormSubmit(e: FormEvent<HTMLFormElement>) {
    e.preventDefault();
    // TODO: wire up login form
    console.log(formState);
  }

  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>Register</h1>
      <hr className={styles.separator} />
      <form
        className={styles.loginForm}
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
