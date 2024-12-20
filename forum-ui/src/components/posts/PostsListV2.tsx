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
import InfiniteScroll from 'react-infinite-scroll-component';
import { Spinner } from "flowbite-react";

interface PostsListV2Type {
  posts: PostResponse[];
  categories: PostCategory[];
  hasMore: boolean;
  loadMore: () => void;
  totalPosts: number;
}

export const PostsListV2 = ({ posts, categories, hasMore, loadMore, totalPosts }: PostsListV2Type) => {
  const [selectedCategories, setSelectedCategories] = useState<PostCategory[]>([]);
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

  const filteredPosts = selectedCategories.length === 0
    ? posts
    : posts.filter((p) =>
        p.postCategories.some((pc) =>
          selectedCategories.find((sc) => sc.pcId === pc.pcId)
        )
      );

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
      <InfiniteScroll
        dataLength={filteredPosts.length}
        next={loadMore}
        hasMore={hasMore}
        loader={
          <div className="flex justify-center my-4">
            <Spinner size="xl" />
          </div>
        }
        endMessage={
          <p className="text-center text-gray-500 my-4">
            {formatMessage(messages.noMorePosts)}
          </p>
        }
      >
        <ul className="flex flex-col items-center gap-10 mb-20 mt-10">
          {filteredPosts.map((post) => (
            <SinglePost post={post} key={post.postId} />
          ))}
        </ul>
      </InfiniteScroll>
    </>
  );
}; 