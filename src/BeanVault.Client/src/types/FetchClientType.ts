export type FetchClientType = {
  get: (url: string, config?: RequestInit) => Promise<Response>;
  post: <T>(
    url: string,
    config?: RequestInit,
    body?: T | undefined
  ) => Promise<Response>;
  delete: (url: string, config?: RequestInit) => Promise<Response>;
  put: <T>(
    url: string,
    config?: RequestInit,
    body?: T | undefined
  ) => Promise<Response>;
};
