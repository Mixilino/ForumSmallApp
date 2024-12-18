import { defineMessages } from 'react-intl';

export const messages = defineMessages({
  createPost: {
    id: 'posts.form.title.create',
    defaultMessage: 'Create new post',
  },
  editPost: {
    id: 'posts.form.title.edit',
    defaultMessage: 'Edit post',
  },
  postTitle: {
    id: 'posts.form.label.title',
    defaultMessage: 'Post Title',
  },
  postText: {
    id: 'posts.form.label.text',
    defaultMessage: 'Post Text',
  },
  categories: {
    id: 'posts.form.label.categories',
    defaultMessage: 'Categories',
  },
  submit: {
    id: 'posts.form.button.submit',
    defaultMessage: 'Submit',
  },
  noPosts: {
    id: 'posts.list.noPosts',
    defaultMessage: 'No posts',
  },
  createPostPrompt: {
    id: 'posts.list.createPostPrompt',
    defaultMessage: 'Go to New Post page to create a post',
  },
  filterByCategory: {
    id: 'posts.list.filterByCategory',
    defaultMessage: 'Filter by category',
  },
  postedBy: {
    id: 'posts.single.postedBy',
    defaultMessage: 'Posted by',
  },
  you: {
    id: 'posts.single.you',
    defaultMessage: 'you',
  },
  comments: {
    id: 'posts.single.comments',
    defaultMessage: 'Comments: {count}',
  },
  upvoteTooltip: {
    id: 'posts.single.upvoteTooltip',
    defaultMessage: 'Upvote',
  },
  downvoteTooltip: {
    id: 'posts.single.downvoteTooltip',
    defaultMessage: 'Downvote',
  },
  openPostTooltip: {
    id: 'posts.single.openPostTooltip',
    defaultMessage: 'Open post',
  },
  editTooltip: {
    id: 'posts.modal.editTooltip',
    defaultMessage: 'Edit',
  },
  deleteTooltip: {
    id: 'posts.modal.deleteTooltip',
    defaultMessage: 'Delete',
  },
  titleRequired: {
    id: 'post.title.required',
    defaultMessage: 'Title is required'
  },
  titleMinLength: {
    id: 'post.title.minLength',
    defaultMessage: 'Title must be at least 3 characters long'
  },
  textRequired: {
    id: 'post.text.required',
    defaultMessage: 'Post text is required'
  },
  textMinLength: {
    id: 'post.text.minLength',
    defaultMessage: 'Post text must be at least 3 characters long'
  },
  categoriesRequired: {
    id: 'post.categories.required',
    defaultMessage: 'Please select at least one category'
  }
}); 