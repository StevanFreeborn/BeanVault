import { HTMLInputTypeAttribute, InputHTMLAttributes } from 'react';

export type FormField = {
  labelText: string;
  type: HTMLInputTypeAttribute;
  value: string;
  errors: string[];
  inputProps?: InputHTMLAttributes<HTMLInputElement>;
};
