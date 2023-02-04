import { UserResponse } from "./UserResponse";

export type CommentResponse = {
  commentId: number;
  commentText: string;
  dateCreated: string;
  userId: number;
  user: UserResponse;
  postId: number;
};
