import ProductDetails from '@/components/ProductDetails';
import ProtectedPage from '@/components/ProtectedPage';

export default function ProductDetailPage({
  params,
}: {
  params: { id: string };
}) {
  return (
    <ProtectedPage>
      <ProductDetails productId={params.id} />
    </ProtectedPage>
  );
}
