import { FormAction } from '@/types/FormAction';
import { FormData } from '@/types/FormData';
import { FormState } from '@/types/FormState';
import isEmail from 'validator/lib/isEmail';
import isMobilePhone from 'validator/lib/isMobilePhone';
import isURL from 'validator/lib/isURL';

export function formReducer(state: FormState, action: FormAction) {
  switch (action.type) {
    case 'updateValue': {
      const field = state[action.payload.field];
      const newField = { ...field };
      newField.value = action.payload.value;
      newField.errors = [];
      return {
        ...state,
        [action.payload.field]: newField,
      };
    }
    case 'updateErrors': {
      const field = state[action.payload.field];
      const newField = { ...field };
      newField.errors.push(action.payload.error);
      return {
        ...state,
        [action.payload.field]: newField,
      };
    }
    case 'resetAllErrors': {
      const newState = { ...state };
      Object.keys(state).forEach(key => (newState[key].errors = []));
      return newState;
    }
    default:
      return state;
  }
}

export function getFormData({ formState }: { formState: FormState }): FormData {
  return Object.keys(formState).reduce((prev, curr) => {
    return {
      ...prev,
      [curr]: formState[curr].value,
    };
  }, {});
}

export function getFormErrors({ formState }: { formState: FormState }) {
  return Object.keys(formState)
    .map(field => {
      const errors = [];
      const formField = formState[field];
      const formDataValue = formField.value;

      if (formField.inputProps?.required && !formDataValue) {
        errors.push({
          field,
          error: `${formField.labelText} is required.`,
        });
      }

      if (
        formField.inputProps?.maxLength &&
        formDataValue.length > formField.inputProps.maxLength
      ) {
        errors.push({
          field,
          error: `${formField.labelText} cannot be more than ${formField.inputProps.maxLength} characters.`,
        });
      }

      if (
        formField.inputProps?.min &&
        Number(formDataValue) < Number(formField.inputProps.min)
      ) {
        errors.push({
          field,
          error: `${formField.labelText} must be greater than or equal to ${formField.inputProps.min}.`,
        });
      }

      if (formField.type === 'email' && isEmail(formDataValue) === false) {
        errors.push({
          field,
          error: `${formField.labelText} must be a valid email address.`,
        });
      }

      if (formField.type === 'tel' && isMobilePhone(formDataValue) === false) {
        errors.push({
          field,
          error: `${formField.labelText} must be a valid phone number.`,
        });
      }

      if (formField.type === 'url' && isURL(formDataValue) === false) {
        errors.push({
          field,
          error: `${formField.labelText} must be a valid url.`,
        });
      }

      return errors;
    })
    .flat();
}
