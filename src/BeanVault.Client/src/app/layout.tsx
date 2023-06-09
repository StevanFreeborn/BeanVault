import { Inter } from 'next/font/google';
import Navbar from '../components/Navbar';
import './globals.css';

const inter = Inter({ subsets: ['latin'] });

export const metadata = {
  title: 'Bean Vault',
  description: 'A place to fill all your coffee needs',
};

export default function RootLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  return (
    <html lang="en">
      <body className={inter.className}>
        <Navbar />
        <main className="main-container">{children}</main>
      </body>
    </html>
  );
}