import { CommentResponse } from "./CommentResponse";
import { PostCategory } from "./PostCategories";
import { UserResponse } from "./UserResponse";

export type PostResponse = {
  postId: number;
  postTitle: string;
  postText: string;
  datePosted: string;
  user: UserResponse;
  postCategories: PostCategory[];
  votesCount: number;
  voted: boolean;
  upvote: boolean;
  comments: CommentResponse[];
};
