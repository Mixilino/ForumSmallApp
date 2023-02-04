import { Card, Tooltip } from "flowbite-react";
import React, { useContext } from "react";
import { PostResponse } from "../../model/PostResponse";
import { BiUpvote, BiDownvote, BiCommentDetail } from "react-icons/bi";
import { CalculateTime } from "../../util/time-before";
import { ActivePostContext } from "../../store/ActivePostContext";
import { AuthContext } from "../../store/AuthContext";
import { useUpvotePost } from "../../hooks/posts/useUpvote.Post";

interface SinglePostProps {
  post: PostResponse;
}
export const SinglePost = ({ post }: SinglePostProps) => {
  const activePostCtx = useContext(ActivePostContext);
  const authCtx = useContext(AuthContext);
  const { upvoteFunc } = useUpvotePost();

  const voteFuncHandler = (vote: boolean) => {
    upvoteFunc({
      jwtToken: authCtx.jwtToken!,
      postId: post.postId,
      upvote: vote,
    });
  };

  return (
    <li
      className="w-3/4 md:w-144  cursor-pointer"
      onClick={() => activePostCtx.openModal(post)}
    >
      <Card>
        <div>
          <p className="font-normal text-sm text-gray-700  dark:text-gray-400">
            Posted by{" "}
            {authCtx.nameid === parseInt(post.user.userId)
              ? " you" + CalculateTime(post.datePosted)
              : post.user.userName + CalculateTime(post.datePosted)}
          </p>
          <h5 className="text-2xl line-clamp-3 mb-3 font-bold tracking-tight text-gray-900 dark:text-white">
            {post.postTitle}
          </h5>
          <p className="font-normal text-gray-700 line-clamp-5 dark:text-gray-400">
            {post.postText}
          </p>
          <ul className="font-normal flex flex-row flex-wrap gap-2 mt-6 text-gray-700 dark:text-gray-400">
            {post.postCategories.map((pc) => (
              <li
                className="inline-block bg-gray-100 rounded-lg p-1"
                key={pc.categoryName}
              >
                {pc.categoryName}
              </li>
            ))}
          </ul>
        </div>
        <div className="border-t-2 flex justify-between pt-2">
          <div className="flex flex-row items-center gap-2">
            <Tooltip content="Upvote">
              <BiUpvote
                className={`${
                  post.voted && post.upvote && "bg-gray-100"
                } cursor-pointer rounded-lg p-2 hover:bg-gray-100`}
                size={40}
                onClick={(event) => {
                  event.stopPropagation();
                  voteFuncHandler(true);
                }}
              />
            </Tooltip>
            <span>{post.votesCount}</span>
            <Tooltip content="Downvote">
              <BiDownvote
                className={`${
                  post.voted && !post.upvote && "bg-gray-100"
                } cursor-pointer rounded-lg p-2 hover:bg-gray-100`}
                size={40}
                onClick={(event) => {
                  event.stopPropagation();
                  voteFuncHandler(false);
                }}
              />
            </Tooltip>
          </div>
          <Tooltip content="Open post">
            <div className="flex flex-row gap-2 rounded-lg p-2 items-center cursor-pointer hover:bg-gray-100">
              <BiCommentDetail size={22} />
              <p>Comments: {post.comments.length}</p>
            </div>
          </Tooltip>
        </div>
      </Card>
    </li>
  );
};
