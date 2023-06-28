import { CartItem } from './CartItem';

export type Cart = {
  id: string;
  userId: string;
  couponCode: string;
  discountAmount: number;
  items: CartItem[];
  total: number;
  discountedTotal: number;
};
