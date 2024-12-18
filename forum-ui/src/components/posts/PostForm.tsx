import { Button, Card, Label, TextInput } from "flowbite-react";
import React, { FormEvent, useContext, useEffect, useState } from "react";
import { PostCategory } from "../../model/PostCategories";
import Select from "react-select";
import { useNewPost } from "../../hooks/posts/useNewPost";
import { AuthContext } from "../../store/AuthContext";
import { useEditPost } from "../../hooks/posts/useEditPost";
import { PostResponse } from "../../model/PostResponse";
import { useIntl } from 'react-intl';
import { messages } from './messages';

type NewPostFormProps = {
  categories: PostCategory[];
  editMode?: boolean;
  post?: PostResponse | null;
};
export const NewPostForm = ({
  categories,
  editMode,
  post,
}: NewPostFormProps) => {
  const [selectedCategories, setSelectedCategories] = useState<PostCategory[]>(
    []
  );
  const [postTitle, setPostTitle] = useState("");
  const [postText, setPostText] = useState("");
  const { newPostFunc } = useNewPost();
  const { editPostFunc } = useEditPost();

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

  useEffect(() => {
    if (post) {
      setPostTitle(post?.postTitle);
      setPostText(post?.postText);
      setSelectedCategories(post.postCategories);
    }
  }, [categories, post]);

  const handleOnCreatePost = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (!editMode) {
      newPostFunc({
        jwtToken: authCtx.jwtToken!,
        postText: postText as string,
        postTitle: postTitle as string,
        postCategoryIds: selectedCategories?.map((c) => c.pcId) ?? [],
      });
      return;
    }
    editPostFunc({
      postId: post?.postId!,
      jwtToken: authCtx.jwtToken!,
      postText: postText as string,
      postTitle: postTitle as string,
      postCategoryIds: selectedCategories?.map((c) => c.pcId) ?? [],
    });
  };

  return (
    <div className="w-3/4 md:w-144">
      <Card>
        <div>
          <h3 className="text-3xl font-bold dark:text-white">
            {!post
              ? formatMessage(messages.createPost)
              : formatMessage(messages.editPost)
            }
          </h3>
        </div>
        <form className="flex flex-col gap-4" onSubmit={handleOnCreatePost}>
          <div>
            <div className="mb-2 block">
              <Label
                htmlFor="post-title"
                value={formatMessage(messages.postTitle)}
              />
            </div>
            <TextInput
              id="post-title"
              type="text"
              required={true}
              value={postTitle}
              onChange={(e) => setPostTitle(e.target.value)}
            />
          </div>
          <div>
            <div className="mb-2 block">
              <Label
                htmlFor="post-text"
                value={formatMessage(messages.postText)}
              />
            </div>
            <TextInput
              id="post-text"
              type="text"
              required={true}
              value={postText}
              onChange={(e) => setPostText(e.target.value)}
            />
          </div>
          <div className="mb-6">
            <div className="mb-2 block">
              <Label
                htmlFor="post-categories"
                value={formatMessage(messages.categories)}
              />
            </div>
            <Select
              id="post-categories"
              isMulti
              name="categories"
              onChange={handleCategoryChange}
              getOptionLabel={getOptionLabel}
              getOptionValue={getOptionValue}
              options={categories}
              defaultValue={selectedCategories}
              className="basic-multi-select"
            />
          </div>
          <Button type="submit">
            {formatMessage(messages.submit)}
          </Button>
        </form>
      </Card>
    </div>
  );
};
