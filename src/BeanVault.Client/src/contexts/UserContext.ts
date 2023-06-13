import { createContext } from 'react';
import { UserContextType } from '../types/UserContext';

export const UserContext = createContext<UserContextType | undefined>(
  undefined
);
