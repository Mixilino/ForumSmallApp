import * as React from "react";
import { createContext, useState } from "react";
import { CommentResponse } from "../model/CommentResponse";

interface ActiveCommentInterface {
  activeComment: CommentResponse | null;
  setActiveComment: (comment: CommentResponse) => void;
  removeActiveComment: () => void;
}

export const ActiveCommentContext = createContext<ActiveCommentInterface>({
  activeComment: null,
  setActiveComment: (comment: CommentResponse) => {},
  removeActiveComment: () => {},
});

interface ActiveCommentProviderProps {
  children?: React.ReactNode;
}

function ActiveCommentContextProvider({
  children,
}: ActiveCommentProviderProps) {
  const [activeComment, setActiveCommentstate] =
    useState<CommentResponse | null>(null);
  const setActiveComment = (comment: CommentResponse) => {
    setActiveCommentstate(comment);
  };
  const removeActiveComment = () => {
    setActiveCommentstate(null);
  };
  const value = {
    activeComment: activeComment,
    setActiveComment: setActiveComment,
    removeActiveComment: removeActiveComment,
  };
  return (
    <ActiveCommentContext.Provider value={value}>
      {children}
    </ActiveCommentContext.Provider>
  );
}

export default ActiveCommentContextProvider;
