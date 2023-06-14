'use client';

import { AppProviders } from '@/providers/AppProviders';
import { Inter } from 'next/font/google';
import { ReactNode } from 'react';
import { Toaster } from 'react-hot-toast';
import Navbar from '../components/Navbar';
import './globals.css';

const inter = Inter({ subsets: ['latin'] });

export const metadata = {
  title: 'Bean Vault',
  description: 'A place to fill all your coffee needs',
};

export default function RootLayout({ children }: { children: ReactNode }) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <AppProviders>
          <Navbar />
          <main className="main-container">{children}</main>
          <Toaster position="top-right" />
        </AppProviders>
      </body>
    </html>
  );
}
