import { ValidationError } from '@/errors/validationError';
import { FetchClientType } from '@/types/FetchClientType';
import { FormData } from '@/types/FormData';

async function getErrors({ res }: { res: Response }) {
  const problemDetails = (await res.json()) as {
    detail: string;
    errors: object;
  };

  const errors = problemDetails?.errors as { [key: string]: string[] };

  if (errors === null || errors === undefined) {
    return [problemDetails.detail];
  }

  return Object.keys(errors).reduce((prev: string[], curr) => {
    const currErrors = errors[curr];
    return prev.concat(currErrors);
  }, []);
}

export function authService({ client }: { client: FetchClientType }) {
  const AUTH_SERVICE_URL = process.env.NEXT_PUBLIC_AUTH_SERVICE_URL;

  async function login({ userLogin }: { userLogin: FormData }) {
    const res = await client.post(
      `${AUTH_SERVICE_URL}/api/users/login`,
      undefined,
      userLogin
    );

    if (res.ok === false) {
      switch (res.status) {
        case 400: {
          const errors = await getErrors({ res });
          throw new ValidationError('Unable to login', errors);
        }
        default:
          throw new Error('Unable to login');
      }
    }

    return await res.json();
  }

  async function register({ newUser }: { newUser: FormData }) {
    const res = await client.post(
      `${AUTH_SERVICE_URL}/api/users/register`,
      undefined,
      newUser
    );

    if (res.ok === false) {
      switch (res.status) {
        case 400: {
          const errors = await getErrors({ res });
          throw new ValidationError('Unable to register', errors);
        }
        default:
          throw new Error('Unable to register');
      }
    }

    return await res.json();
  }

  async function addRole() {}

  return {
    login,
    register,
    addRole,
  };
}
