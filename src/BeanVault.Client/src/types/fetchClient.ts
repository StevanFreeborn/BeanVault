export type fetchClient = {
  get: (url: string, config?: RequestInit) => Promise<Response>;
};
