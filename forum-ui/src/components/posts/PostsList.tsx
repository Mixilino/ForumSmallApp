import Select from "react-select";
import React, { useContext, useState } from "react";
import { PostCategory } from "../../model/PostCategories";
import { PostResponse } from "../../model/PostResponse";
import { SinglePost } from "./SinglePost";
import { SinglePostInfoModal } from "./SinglePostInfoModal";
import { ActivePostContext } from "../../store/ActivePostContext";
import { AuthContext } from "../../store/AuthContext";
import { UserRoles } from "../../model/UserRoles";
import { useIntl } from 'react-intl';
import { messages } from './messages';

interface PostsListType {
  posts: PostResponse[];
  categories: PostCategory[];
}
export const PostsList = ({ posts, categories }: PostsListType) => {
  const [selectedCategories, setSelectedCategories] = useState<PostCategory[]>(
    []
  );
  const activePostCtx = useContext(ActivePostContext);
  const authCtx = useContext(AuthContext);
  const { formatMessage } = useIntl();

  const handleCategoryChange = (selected: any) => {
    setSelectedCategories(selected);
  };

  const getOptionLabel = (pc: PostCategory) => {
    return pc.categoryName;
  };

  const getOptionValue = (pc: PostCategory) => {
    return "" + pc.pcId;
  };

  if (posts.length === 0) {
    return (
      <div className={authCtx.role !== UserRoles.Banned ? "mt-20" : ""}>
        <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white">
          {formatMessage(messages.noPosts)}
        </h5>
        <p className="font-normal text-gray-700 dark:text-gray-400">
          {formatMessage(messages.createPostPrompt)}
        </p>
      </div>
    );
  }

  return (
    <>
      <div>
        <SinglePostInfoModal postId={activePostCtx.post?.postId!} />
      </div>
      <div
        className={`${
          authCtx.role !== UserRoles.Banned ? "mt-20" : ""
        } flex flex-col items-center`}
      >
        <Select
          id="post-categories2"
          isMulti
          name="categories"
          onChange={handleCategoryChange}
          getOptionLabel={getOptionLabel}
          getOptionValue={getOptionValue}
          options={categories}
          className="basic-multi-select w-3/4 md:w-144"
          isSearchable={false}
          placeholder={formatMessage(messages.filterByCategory)}
        />
      </div>
      <ul className="flex flex-col items-center gap-10 mb-20 mt-10">
        {selectedCategories.length === 0 &&
          posts.map((post) => {
            return <SinglePost post={post} key={post.postId} />;
          })}
        {selectedCategories.length !== 0 &&
          posts
            .filter((p) =>
              p.postCategories.some((pc) =>
                selectedCategories.find((sc) => sc.pcId === pc.pcId)
              )
            )
            .map((post) => {
              return <SinglePost post={post} key={post.postId} />;
            })}
      </ul>
    </>
  );
};
