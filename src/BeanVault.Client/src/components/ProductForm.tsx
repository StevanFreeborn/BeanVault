import { FormState } from '@/types/FormState';
import Link from 'next/link';
import { ChangeEvent, FormEvent } from 'react';
import FormField from './FormField';
import styles from './ProductForm.module.css';

export default function ProductForm({
  formHeaderText,
  formState,
  formSubmitHandler,
  inputChangeHandler,
  actionButtonText,
}: {
  formHeaderText: string;
  formState: FormState;
  formSubmitHandler: (e: FormEvent<HTMLFormElement>) => void;
  inputChangeHandler: (e: ChangeEvent<HTMLInputElement>) => void;
  actionButtonText: string;
}) {
  return (
    <div className={styles.container}>
      <h1 className={styles.formHeader}>{formHeaderText}</h1>
      <hr className={styles.separator} />
      <form
        className={styles.productForm}
        onSubmit={formSubmitHandler}
        noValidate={true}
      >
        {Object.keys(formState).map(key => {
          const formField = formState[key];
          return (
            <FormField
              key={key}
              fieldName={key}
              formField={formField}
              onChangeHandler={inputChangeHandler}
            />
          );
        })}
        <div className={styles.actionContainer}>
          <Link className={styles.cancelButton} href="products">
            Cancel
          </Link>
          <button
            className={styles.addOrEditButton}
            title={`${actionButtonText} Product`}
            type="submit"
          >
            {actionButtonText}
          </button>
        </div>
      </form>
    </div>
  );
}
