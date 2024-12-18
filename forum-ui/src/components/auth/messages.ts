import { defineMessages } from "react-intl";


export const messages = defineMessages({
    header: {
        id: 'authForm.header',
        defaultMessage: '{isSignInMode, select, true {Sign In to your account} other {Create an account}}',
    },
    username: {
        id: 'authForm.username',
        defaultMessage: 'Username',
    },
    password: {
        id: 'authForm.password',
        defaultMessage: 'Your password',
    },
    toggleRegister: {
        id: 'authForm.toggleRegister',
        defaultMessage: 'Click to Register',
    },
    toggleSignIn: {
        id: 'authForm.toggleSignIn',
        defaultMessage: 'Click to Sign In',
    },
    signInButton: {
        id: 'authForm.signInButton',
        defaultMessage: 'Sign In',
    },
    registerButton: {
        id: 'authForm.registerButton',
        defaultMessage: 'Register',
    },
})