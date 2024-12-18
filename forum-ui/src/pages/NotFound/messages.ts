import { defineMessages } from "react-intl";


export const messages = defineMessages({
    notFound: {
        id: 'notFoundPage.notFound',
        defaultMessage: '404 Page not found...',
    },
    goTo: {
        id: 'notFoundPage.goTo',
        defaultMessage: 'Go to {isAuthenticated, select, true {All Posts Page} other {Auth Page}}',
    }
})