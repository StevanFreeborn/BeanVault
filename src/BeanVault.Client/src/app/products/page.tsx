import ProductsGrid from '@/components/ProductsGrid';
import ProtectedPage from '@/components/ProtectedPage';
import Link from 'next/link';
import { AiOutlinePlusSquare } from 'react-icons/ai';
import styles from './page.module.css';

export default function ProductsPage() {
  return (
    <ProtectedPage>
      <div className={styles.addProductContainer}>
        <h1>Products</h1>
        <Link className={styles.addProductLink} href="products/add">
          <AiOutlinePlusSquare />
          Create New Product
        </Link>
      </div>
      <ProductsGrid />
    </ProtectedPage>
  );
}
