import { useQuery } from "@tanstack/react-query";
import axiosInstanceTs from "../../api/axiosInstance";
import { RestResponse } from "../../model/RestResponse";
import { PostCategory } from "../../model/PostCategories";
import { CategoriesKey } from "../../constants/queryKeys";
import { useContext } from "react";
import { AuthContext } from "../../store/AuthContext";

const getAllCategories = async (
  token: string
): Promise<RestResponse<PostCategory[]>> => {
  const config = {
    headers: { Authorization: `Bearer ${token}` },
  };
  const { data } = await axiosInstanceTs.get(`/api/categories/all`, config);
  return data;
};

export const useGetAllCategories = () => {
  const authCtx = useContext(AuthContext);
  const { data: postCategories = null, isLoading: isLoadingCategories } =
    useQuery([CategoriesKey], () => getAllCategories(authCtx.jwtToken!));
  return { postCategories, isLoadingCategories };
};
