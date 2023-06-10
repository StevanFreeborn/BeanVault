export type AddCouponFormAction =
  | { type: 'updateValue'; payload: { field: string; value: string } }
  | { type: 'updateErrors'; payload: { field: string; error: string } }
  | { type: 'resetAllErrors' };
