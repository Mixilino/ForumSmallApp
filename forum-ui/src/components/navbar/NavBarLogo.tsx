import React from 'react'
import { Link } from 'react-router-dom'

const NavBarLogo = () => {
    return (
        <Link to="/posts" className="flex items-center">
            <img
                src="https://vectorlogoseek.com/wp-content/uploads/2019/07/founders-forum-vector-logo.png"
                className="mr-3 h-6 sm:h-9"
                alt="Forum Api logo"
            />
            <span className="self-center text-xl font-semibold whitespace-nowrap dark:text-white">
                Forum Api
            </span>
        </Link>
    )
}

export default NavBarLogo