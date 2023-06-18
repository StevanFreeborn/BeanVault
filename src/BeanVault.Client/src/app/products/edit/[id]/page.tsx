export default function EditProductPage({
  params,
}: {
  params: { id: string };
}) {
  return <h1>Edit Product {params.id}</h1>;
}
