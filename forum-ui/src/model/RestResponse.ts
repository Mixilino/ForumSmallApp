export type RestResponse<T = null> = {
  data: T;
  success: boolean;
  message: string;
};
