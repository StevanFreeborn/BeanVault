export default function ProductDetailPage({
  params,
}: {
  params: { id: string };
}) {
  return <h1>Details for {params.id}</h1>;
}
