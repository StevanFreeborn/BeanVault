'use client';

import { fetchClient } from '@/http/fetchClient';
import { couponService } from '@/services/couponService';
import { useEffect, useState } from 'react';

export default function Home() {
  const [coupons, setCoupons] = useState([]);

  useEffect(() => {
    const { getCoupons } = couponService({ client: fetchClient() });
    getCoupons()
      .then(res => res.json())
      .then(coupons => setCoupons(coupons));
  }, [coupons]);

  return (
    <div>
      {coupons.map(c => (
        <div>{JSON.stringify(c)}</div>
      ))}
    </div>
  );
}
