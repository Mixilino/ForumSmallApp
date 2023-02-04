import * as React from "react";
import { createContext, useState } from "react";
import { PostResponse } from "../model/PostResponse";

interface ActivePostInterface {
  post: PostResponse | null;
  showModal: boolean;
  openModal: (post: PostResponse) => void;
  closeModal: () => void;
}

export const ActivePostContext = createContext<ActivePostInterface>({
  post: null,
  showModal: false,
  openModal: (post: PostResponse) => {},
  closeModal: () => {},
});

interface ActivePostProviderProps {
  children?: React.ReactNode;
}

function ActivePostContextProvider({ children }: ActivePostProviderProps) {
  const [activePost, setActivePost] = useState<PostResponse | null>(null);
  const openModal = (post: PostResponse) => {
    setActivePost(post);
  };
  const closeModal = () => {
    setActivePost(null);
  };
  const value = {
    post: activePost,
    showModal: !!activePost,
    openModal: openModal,
    closeModal: closeModal,
  };
  return (
    <ActivePostContext.Provider value={value}>
      {children}
    </ActivePostContext.Provider>
  );
}

export default ActivePostContextProvider;
