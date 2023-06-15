import { FormField } from '@/types/FormField';
import { ChangeEvent } from 'react';
import styles from './FormField.module.css';

export default function FormField({
  fieldName,
  formField,
  onChangeHandler,
}: {
  fieldName: string;
  formField: FormField;
  onChangeHandler: (e: ChangeEvent<HTMLInputElement>) => void;
}) {
  return (
    <div key={fieldName} className={styles.formGroup}>
      <label className={styles.formLabel} htmlFor={fieldName}>
        {formField.labelText}
      </label>
      <input
        {...formField.inputProps}
        id={fieldName}
        name={fieldName}
        className={styles.formControl}
        type={formField.type}
        value={formField.value}
        onChange={onChangeHandler}
      />
      <ul className={styles.error}>
        {formField.errors.map((error, i) => (
          <li key={i}>{error}</li>
        ))}
      </ul>
    </div>
  );
}
