import { TextInput, Tooltip } from "flowbite-react";
import React, { useContext, useEffect, useState } from "react";
import {
  RiCloseCircleLine,
  RiDeleteBin6Line,
  RiEdit2Line,
} from "react-icons/ri";
import { IoCheckmarkCircleOutline } from "react-icons/io5";
import { useDeleteComment } from "../../hooks/comments/useDeleteComment";
import { CommentResponse } from "../../model/CommentResponse";
import { ActiveCommentContext } from "../../store/ActiveCommentContext";
import { AuthContext } from "../../store/AuthContext";
import { CalculateTime } from "../../util/time-before";
import { useEditComment } from "../../hooks/comments/useEditComment";
import { useIntl } from "react-intl";
import { messages } from "./messages";

type SingleCommentProps = {
  comment: CommentResponse;
};
export const SingleComment = ({ comment }: SingleCommentProps) => {
  const authCtx = useContext(AuthContext);
  const commentCtx = useContext(ActiveCommentContext);
  const { deleteCommentFunc } = useDeleteComment();
  const [editCommentText, setEditCommentText] = useState<string | undefined>();
  const { editCommentFunc } = useEditComment();
  const { formatMessage } = useIntl();

  const onDeleteCommentHandler = () => {
    deleteCommentFunc({
      jwtToken: authCtx.jwtToken!,
      comment: comment,
    });
  };

  useEffect(() => {
    setEditCommentText(commentCtx.activeComment?.commentText);
  }, [commentCtx.activeComment]);

  const editMode =
    commentCtx.activeComment &&
    commentCtx.activeComment.commentId === comment.commentId;

  return (
    <div className="border-b-2 border-b-slate-100 flex flex-col pb-2">
      <div className="flex justify-between items-center pt-1">
        <p className="font-normal text-sm text-gray-700  dark:text-gray-400">
          {formatMessage(messages.postedBy, {
            isCurrentUser: authCtx.nameid === parseInt(comment.user.userId),
            userName: comment.user.userName,
            when: CalculateTime(comment.dateCreated)
          })}
        </p>
        {authCtx.nameid === parseInt(comment.user.userId) && (
          <div className="flex gap-1">
            {!editMode && (
              <>
                <Tooltip content={formatMessage(messages.edit)}>
                  <RiEdit2Line
                    className="cursor-pointer rounded-lg p-0 hover:bg-gray-100"
                    size={22}
                    onClick={() => {
                      commentCtx.setActiveComment(comment);
                    }}
                  />
                </Tooltip>
                <Tooltip content={formatMessage(messages.delete)}>
                  <RiDeleteBin6Line
                    className="cursor-pointer rounded-lg p-0 hover:bg-gray-100"
                    size={22}
                    onClick={onDeleteCommentHandler}
                  />
                </Tooltip>
              </>
            )}
            {editMode && (
              <>
                <Tooltip content={formatMessage(messages.submit)}>
                  <IoCheckmarkCircleOutline
                    className="cursor-pointer rounded-lg p-0 hover:bg-gray-100"
                    size={22}
                    onClick={() => {
                      editCommentFunc({
                        commentId: comment.commentId,
                        commentText: editCommentText ?? "",
                        jwtToken: authCtx.jwtToken!,
                      });
                      commentCtx.removeActiveComment();
                    }}
                  />
                </Tooltip>
                <Tooltip content={formatMessage(messages.cancelEdit)}>
                  <RiCloseCircleLine
                    className="cursor-pointer rounded-lg p-0 hover:bg-gray-100"
                    size={22}
                    onClick={() => {
                      commentCtx.removeActiveComment();
                    }}
                  />
                </Tooltip>
              </>
            )}
          </div>
        )}
      </div>
      {editMode && (
        <div className=" w-10/12">
          <TextInput
            value={editCommentText}
            size={22}
            onChange={(e) => setEditCommentText(e.target.value)}
          />
        </div>
      )}
      {!editMode && (
        <p className="font-normal text-gray-700 line-clamp-5 dark:text-gray-400">
          {comment.commentText}
        </p>
      )}
    </div>
  );
};
