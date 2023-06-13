import { FetchClientType } from '@/types/FetchClientType';

export function authService({ client }: { client: FetchClientType }) {
  const AUTH_SERVICE_URL = process.env.NEXT_PUBLIC_AUTH_SERVICE_URL;

  async function login() {}

  async function register() {}

  async function addRole() {}

  return {
    login,
    register,
    addRole,
  };
}
