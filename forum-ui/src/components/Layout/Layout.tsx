import React from 'react';
import { Footer } from '../Footer/Footer';
import { NavBar } from '../navbar/NavBar';

interface LayoutProps {
    children: React.ReactNode;
    hideSignInButton?: boolean;
}

const Layout: React.FC<LayoutProps> = ({ children, hideSignInButton }) => {
    return (
        <div>
            <NavBar hideSignInButton={hideSignInButton} />
            <div style={{ minHeight: "calc(100vh - 70px)" }}>
                {children}</div>
            <Footer />
        </div>
    );
};

export default Layout;
