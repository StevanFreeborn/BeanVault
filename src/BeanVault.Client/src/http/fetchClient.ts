import { FetchClientType } from '@/types/FetchClientType';

export function fetchClient(clientConfig?: RequestInit): FetchClientType {
  async function request(url: string, config?: RequestInit): Promise<Response> {
    var fetchConfig = {
      ...config,
      ...clientConfig,
      headers: {
        ...config?.headers,
        ...clientConfig?.headers,
      },
    };
    return await fetch(new Request(url, fetchConfig));
  }

  async function get(url: string, config?: RequestInit) {
    const requestConfig = {
      ...config,
      method: 'GET',
    };
    return await request(url, requestConfig);
  }

  async function post<T>(url: string, config?: RequestInit, body?: T) {
    const requestConfig = {
      ...config,
      method: 'POST',
      body: JSON.stringify(body),
      headers: {
        'Content-Type': 'application/json',
      },
    };
    return await request(url, requestConfig);
  }

  async function del(url: string, config?: RequestInit) {
    const requestConfig = { ...config, method: 'DELETE' };
    return await request(url, requestConfig);
  }

  async function put<T>(url: string, config?: RequestInit, body?: T) {
    const requestConfig = {
      ...config,
      method: 'PUT',
      body: JSON.stringify(body),
      headers: {
        'Content-Type': 'application/json',
      },
    };
    return await request(url, requestConfig);
  }

  return {
    get,
    post,
    delete: del,
    put,
  };
}
