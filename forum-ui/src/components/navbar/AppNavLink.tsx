import { useLinkClickHandler, useLocation } from "react-router-dom";
import { Navbar } from "flowbite-react";

export interface AppNavLinkProps {
  to: string;
  text: string;
}

export default function AppNavLink(props: AppNavLinkProps) {
  const location = useLocation();
  const clickHandler = useLinkClickHandler(props.to);

  return (
    <span onClick={clickHandler}>
      <Navbar.Link href={props.to} active={location.pathname === props.to}>
        {props.text}
      </Navbar.Link>
    </span>
  );
}
