import React, { useContext, useState } from "react";
import { Link, NavLink, useNavigate } from "react-router-dom";
import queryClient from "../../api/queryClientInstance";
import { CategoriesKey, PostsKey, UsersKey } from "../../constants/queryKeys";
import { UserRoles } from "../../model/UserRoles";
import { AuthContext } from "../../store/AuthContext";
import NavBarLogo from "./NavBarLogo";
import { useIntl } from 'react-intl';
import { messages } from './messages';
import { LanguageContext } from '../../store/LanguageProvider/LanguageProvider';

export const NavBar = ({ hideSignInButton }: { hideSignInButton?: boolean }) => {
  const authCtx = useContext(AuthContext);
  const navigate = useNavigate();
  const [showDropdown, setShowDropdown] = useState(false);
  const { formatMessage } = useIntl();
  const { isRTL } = useContext(LanguageContext);

  const isLoggedIn = authCtx.isAuthenticated;


  const onSignOutHandler = () => {
    queryClient.removeQueries([PostsKey]);
    queryClient.removeQueries([CategoriesKey]);
    queryClient.removeQueries([UsersKey]);
    authCtx.signOut();
    navigate("/auth");
  };

  return (
    <nav className="bg-gray-100 px-2 sm:px-4 py-2.5 dark:bg-gray-900 fixed w-full z-20 top-0 left-0 border-b border-gray-200 dark:border-gray-600" dir={isRTL ? 'rtl' : 'ltr'}>
      <div className="container flex flex-wrap justify-between items-center mx-auto">
        <NavBarLogo />
        <div className="flex md:order-2 justify-end w-[200px]">
          {isLoggedIn && <button
            onClick={onSignOutHandler}
            type="button"
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-3 md:mr-0 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            {formatMessage(messages.signOut)}
          </button>}
          {!isLoggedIn && !hideSignInButton && <button
            onClick={() => navigate("/auth")}
            type="button"
            className="text-white bg-blue-700 hover:bg-blue-800 focus:ring-4 focus:outline-none focus:ring-blue-300 font-medium rounded-lg text-sm px-5 py-2.5 text-center mr-3 md:mr-0 dark:bg-blue-600 dark:hover:bg-blue-700 dark:focus:ring-blue-800"
          >
            {formatMessage(messages.signIn)}
          </button>}
          <button
            data-collapse-toggle="navbar-sticky"
            type="button"
            className="inline-flex items-center p-2 text-sm text-gray-500 rounded-lg md:hidden hover:bg-gray-100 focus:outline-none focus:ring-2 focus:ring-gray-200 dark:text-gray-400 dark:hover:bg-gray-700 dark:focus:ring-gray-600"
            aria-controls="navbar-sticky"
            aria-expanded="false"
            onClick={() => setShowDropdown((prev) => !prev)}
          >
            <span className="sr-only">{formatMessage(messages.openMainMenu)}</span>
            <svg
              className="w-6 h-6"
              aria-hidden="true"
              fill="currentColor"
              viewBox="0 0 20 20"
              xmlns="http://www.w3.org/2000/svg"
            >
              <path
                fill-rule="evenodd"
                d="M3 5a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 10a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1zM3 15a1 1 0 011-1h12a1 1 0 110 2H4a1 1 0 01-1-1z"
                clip-rule="evenodd"
              ></path>
            </svg>
          </button>
        </div>
        <div
          className={`${showDropdown ? "" : "hidden"
            } 'justify-between items-center w-full md:flex md:w-auto md:order-1'`}
          id="navbar-sticky"
        >
          <ul className="flex flex-col p-4 mt-4 bg-gray-100 rounded-lg border border-gray-100 md:flex-row md:space-x-8 md:mt-0 md:text-sm md:font-medium md:border-0 md:bg-gray-100 dark:bg-gray-800 md:dark:bg-gray-900 dark:border-gray-700">
            <li>
              <NavLink
                to="/posts"
                className={(navData) =>
                  navData.isActive
                    ? "block py-2 pr-4 pl-3 text-white bg-blue-700 rounded md:bg-transparent md:text-blue-700 md:p-0 dark:text-white"
                    : "block py-2 pr-4 pl-3 text-gray-700 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 md:dark:hover:text-white dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
                }
              >
                {formatMessage(messages.posts)}
              </NavLink>
            </li>
            {authCtx.isAuthenticated && <li>
              <NavLink
                to="/new"
                className={(navData) =>
                  navData.isActive
                    ? "block py-2 pr-4 pl-3 text-white bg-blue-700 rounded md:bg-transparent md:text-blue-700 md:p-0 dark:text-white"
                    : "block py-2 pr-4 pl-3 text-gray-700 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 md:dark:hover:text-white dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
                }
              >
                {formatMessage(messages.newPost)}
              </NavLink>
            </li>}
            {authCtx.role === UserRoles.Admin && (
              <li>
                <NavLink
                  to="/admin/users"
                  className={(navData) =>
                    navData.isActive
                      ? "block py-2 pr-4 pl-3 text-white bg-blue-700 rounded md:bg-transparent md:text-blue-700 md:p-0 dark:text-white"
                      : "block py-2 pr-4 pl-3 text-gray-700 rounded hover:bg-gray-100 md:hover:bg-transparent md:hover:text-blue-700 md:p-0 md:dark:hover:text-white dark:text-gray-400 dark:hover:bg-gray-700 dark:hover:text-white md:dark:hover:bg-transparent dark:border-gray-700"
                  }
                >
                  {formatMessage(messages.manageUsers)}
                </NavLink>
              </li>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
};
