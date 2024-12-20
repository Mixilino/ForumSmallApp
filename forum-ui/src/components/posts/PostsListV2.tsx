import Select from "react-select";
import React, { useContext, useState, useEffect, useMemo } from "react";
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
import { Spinner, TextInput } from "flowbite-react";
import { HiSearch } from 'react-icons/hi';
import debounce from 'lodash/debounce';

interface PostsListV2Type {
  posts: PostResponse[];
  categories: PostCategory[];
  hasMore: boolean;
  loadMore: () => void;
  totalPosts: number;
  onCategoryChange: (categories: PostCategory[]) => void;
  onSearchChange: (searchText: string) => void;
  searchText: string;
}

export const PostsListV2 = ({
  posts,
  categories,
  hasMore,
  loadMore,
  onCategoryChange,
  onSearchChange,
  searchText: initialSearchText
}: PostsListV2Type) => {
  const activePostCtx = useContext(ActivePostContext);
  const authCtx = useContext(AuthContext);
  const { formatMessage } = useIntl();
  const [inputValue, setInputValue] = useState(initialSearchText);

  useEffect(() => {
    setInputValue(initialSearchText);
  }, [initialSearchText]);

  const handleCategoryChange = (selected: any) => {
    onCategoryChange(selected || []);
  };

  const debouncedSearch = useMemo(
    () => debounce((value: string) => {
      onSearchChange(value);
    }, 500),
    [onSearchChange]
  );

  const handleSearchChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = event.target.value;
    setInputValue(newValue);
    debouncedSearch(newValue);
  };

  useEffect(() => {
    return () => {
      debouncedSearch.cancel();
    };
  }, [debouncedSearch]);

  const getOptionLabel = (pc: PostCategory) => {
    return pc.categoryName;
  };

  const getOptionValue = (pc: PostCategory) => {
    return "" + pc.pcId;
  };

  const noPostsLoaded = posts.length === 0;

  return (
    <>
      <div>
        <SinglePostInfoModal postId={activePostCtx.post?.postId!} />
      </div>
      <div
        className={`${authCtx.role !== UserRoles.Banned ? "mt-20" : ""
          } flex flex-col items-center gap-4`}
      >
        <div
          className="w-3/4 md:w-144">
          <TextInput
            id="search"
            type="text"
            icon={HiSearch}
            value={inputValue}
            placeholder={formatMessage(messages.searchPosts)}
            onChange={handleSearchChange}
          />
        </div>
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
      {noPostsLoaded && (
        <div className={authCtx.role !== UserRoles.Banned ? "mt-20" : ""}>
          <h5 className="text-2xl font-bold tracking-tight text-gray-900 dark:text-white text-center">
            {formatMessage(messages.noPosts)}
          </h5>
          <p className="font-normal text-gray-700 dark:text-gray-400 text-center">
            {formatMessage(messages.createPostPrompt)}
          </p>
        </div>
      )}
      {!noPostsLoaded && <InfiniteScroll
        dataLength={posts.length}
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
          {posts.map((post) => (
            <SinglePost post={post} key={post.postId} />
          ))}
        </ul>
      </InfiniteScroll>}
    </>
  );
}; 