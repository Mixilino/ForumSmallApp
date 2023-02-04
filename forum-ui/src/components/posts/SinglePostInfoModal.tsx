import React, { useContext, useEffect, useState } from "react";
import { BiDownvote, BiUpvote } from "react-icons/bi";
import { RiDeleteBin6Line, RiEdit2Line } from "react-icons/ri";
import ReactModal from "react-modal";
import { useNavigate } from "react-router-dom";
import { PostResponse } from "../../model/PostResponse";
import { ActivePostContext } from "../../store/ActivePostContext";
import { AuthContext } from "../../store/AuthContext";
import { CalculateTime } from "../../util/time-before";
import CommentsList from "../comments/CommentsList";
import { useDeletePost } from "../../hooks/posts/useDeletePost";
import { useGetAllPosts } from "../../hooks/posts/useGetAllPosts";
import { useUpvotePost } from "../../hooks/posts/useUpvote.Post";
import ActiveCommentContextProvider from "../../store/ActiveCommentContext";
import { Tooltip } from "flowbite-react";

type SinglePostInfoProps = {
  postId: number;
};

export const SinglePostInfoModal = ({ postId }: SinglePostInfoProps) => {
  const activePostCtx = useContext(ActivePostContext);
  const authCtx = useContext(AuthContext);
  const { deletePostFunc } = useDeletePost();
  const navigate = useNavigate();
  const { upvoteFunc } = useUpvotePost();
  const { posts } = useGetAllPosts();

  const [post, setPost] = useState<PostResponse | null>(null);

  useEffect(() => {
    const p = posts.find((p) => p.postId === postId);
    setPost(p ?? null);
  }, [postId, posts]);

  if (post == null) return <></>;

  const voteFuncHandler = (vote: boolean) => {
    upvoteFunc({
      jwtToken: authCtx.jwtToken!,
      postId: post.postId,
      upvote: vote,
    });
  };

  const onDeletePostHandler = () => {
    deletePostFunc({
      jwtToken: authCtx.jwtToken!,
      PostId: post.postId,
    });
    activePostCtx.closeModal();
  };

  const customStyles = {
    overlay: {
      // background: "rgba(0, 0, 0, 0.2)",
      overflowY: "scroll",
    },
    content: {
      // your code
    },
  } as ReactModal.Styles;

  return (
    <ReactModal
      isOpen={activePostCtx.showModal}
      contentLabel="onRequestClose Example"
      onRequestClose={() => activePostCtx.closeModal()}
      className="mt-40 sm:w-1/2 mx-auto bg-white border-4 rounded-2xl w-11/12"
      ariaHideApp={false}
      style={customStyles}
    >
      <div className="p-6 pb-2 ">
        <p className="font-normal text-sm text-gray-700  dark:text-gray-400">
          Posted by{" "}
          {authCtx.nameid === parseInt(post.user.userId)
            ? " you" + CalculateTime(post.datePosted)
            : post.user.userName + CalculateTime(post.datePosted)}
        </p>
        <div className="flex justify-between items-center">
          <h5 className="text-2xl line-clamp-3 mb-3 font-bold tracking-tight text-gray-900 dark:text-white">
            {post.postTitle}
          </h5>
          {authCtx.nameid === parseInt(post.user.userId) && (
            <div className="flex gap-1">
              <Tooltip content="Edit">
                <RiEdit2Line
                  className="cursor-pointer rounded-lg p-2 hover:bg-gray-100"
                  size={40}
                  onClick={() => navigate(`/edit-post/${post.postId}`)}
                />
              </Tooltip>
              <Tooltip content="Delete">
                <RiDeleteBin6Line
                  className="cursor-pointer rounded-lg p-2 hover:bg-gray-100"
                  size={40}
                  onClick={onDeletePostHandler}
                />
              </Tooltip>
            </div>
          )}
        </div>
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
      <div className="border-b-2 flex justify-between pt-2 mx-6 pb-2">
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
      </div>
      <div>
        <ActiveCommentContextProvider>
          <CommentsList comments={post.comments ?? []} postId={post.postId} />
        </ActiveCommentContextProvider>
      </div>
    </ReactModal>
  );
};
