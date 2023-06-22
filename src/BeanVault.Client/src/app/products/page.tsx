import ProductsTable from '@/components/ProductTable';
import ProtectedPage from '@/components/ProtectedPage';
import Link from 'next/link';
import { AiOutlinePlusSquare } from 'react-icons/ai';
import styles from './page.module.css';

export default function ProductsPage() {
  return (
    <ProtectedPage>
      <div className={styles.card}>
        <div className={styles.cardHeader}>
          <h1>Products List</h1>
        </div>
        <div className={styles.cardBody}>
          <div className={styles.addButtonContainer}>
            <Link className={styles.addProductLink} href="products/add">
              <AiOutlinePlusSquare />
              Create New Product
            </Link>
          </div>
          <ProductsTable />
        </div>
      </div>
    </ProtectedPage>
  );
}
