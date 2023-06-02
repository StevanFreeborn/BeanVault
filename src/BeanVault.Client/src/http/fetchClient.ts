import { fetchClient } from '@/types/fetchClient.js';

export function fetchClient(): fetchClient {
  async function request(url: string, config?: RequestInit): Promise<Response> {
    return await fetch(new Request(url, config));
  }

  async function get(url: string, config?: RequestInit) {
    const requestConfig = { ...config, method: 'GET' };
    return await request(url, requestConfig);
  }

  return {
    get,
  };
}
