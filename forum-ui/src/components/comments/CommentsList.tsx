import { Button, Label, Textarea } from "flowbite-react";
import React, { useContext, useState } from "react";
import { useNewComment } from "../../hooks/comments/useNewComment";
import { CommentResponse } from "../../model/CommentResponse";
import { AuthContext } from "../../store/AuthContext";
import { SingleComment } from "./SingleComment";
import { useIntl } from "react-intl";
import { messages } from "./messages";

type CommentsListProps = {
  comments: CommentResponse[];
  postId: number;
};
const CommentsList = ({ comments, postId }: CommentsListProps) => {
  const [newCommentText, setNewCommentText] = useState("");
  const authCtx = useContext(AuthContext);
  const { newCommentFunc } = useNewComment();
  const { formatMessage } = useIntl();

  return (
    <>
      <form className="flex justify-between items-center p-6 py-2">
        <div className="flex flex-col justify-center w-10/12">
          <div className="mb-1 block">
            <Label htmlFor="new-comment" value={formatMessage(messages.newComment)} />
          </div>
          <Textarea
            id="new-comment"
            placeholder={formatMessage(messages.placeholderComment)}
            rows={2}
            value={newCommentText}
            onChange={(e) => setNewCommentText(e.target.value)}
          />
        </div>
        <div className="">
          <Button
            onClick={() => {
              newCommentFunc({
                CommentText: newCommentText,
                jwtToken: authCtx.jwtToken!,
                postId,
              });
            }}
          >
            {formatMessage(messages.comment)}
          </Button>
        </div>
      </form>
      <ul className="p-6 pt-2">
        {comments
          .slice()
          .sort(
            (c1, c2) =>
              new Date(c2.dateCreated).getTime() -
              new Date(c1.dateCreated).getTime()
          )
          .map((c) => (
            <SingleComment comment={c} key={c.commentId} />
          ))}
      </ul>
    </>
  );
};

export default CommentsList;
