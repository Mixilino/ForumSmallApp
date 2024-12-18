import { defineMessages } from "react-intl";


export const messages = defineMessages({
    comment: {
        id: 'comments.comment',
        defaultMessage: 'Comment',
    },
    placeholderComment: {
        id: 'comments.placeholderComment',
        defaultMessage: 'Comment something interesting',
    },
    newComment: {
        id: 'comments.newComment',
        defaultMessage: 'New comment',
    },
    edit: {
        id: 'comments.edit',
        defaultMessage: 'Edit',
    },
    cancelEdit: {
        id: 'comments.cancelEdit',
        defaultMessage: 'Edit',
    },
    submit: {
        id: 'comments.submit',
        defaultMessage: 'Submit',
    },
    delete: {
        id: 'comments.delete',
        defaultMessage: 'Delete',
    },
    postedBy: {
        id: 'comments.postedBy',
        defaultMessage: 'Posted by {isCurrentUser, select, true {you} other {' +
            '{userName}} } {when}',
    },
})