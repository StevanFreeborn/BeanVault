import ProductsGrid from '@/components/ProductsGrid';
import ProtectedPage from '@/components/ProtectedPage';

export default function ProductsPage() {
  return (
    <ProtectedPage>
      <ProductsGrid />
    </ProtectedPage>
  );
}
