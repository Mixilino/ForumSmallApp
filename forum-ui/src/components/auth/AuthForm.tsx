import React, { FormEvent, useState } from "react";
import {
  Card,
  Label,
  TextInput,
  ToggleSwitch,
  Button,
} from "flowbite-react/lib/esm/components";
import { RiLockPasswordLine, RiUser3Line } from "react-icons/ri";
import { useSignIn } from "../../hooks/auth/useSignIn";
import { useSignUp } from "../../hooks/auth/useSignUp";

export const AuthForm = () => {
  const [isSignInMode, setIsSignInMode] = useState(true);
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");

  const { signInFunc } = useSignIn();
  const { signUpFunc } = useSignUp();

  const handleOnSubmit = (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    if (isSignInMode) {
      signInFunc({ username, password });
    } else {
      signUpFunc({ username, password });
      setIsSignInMode(true);
    }
  };
  return (
    <div className="w-96">
      <Card imgSrc="https://vectorlogoseek.com/wp-content/uploads/2019/07/founders-forum-vector-logo.png">
        <div>
          <h3 className="text-3xl font-bold dark:text-white">
            {isSignInMode ? "Sign In to your account" : "Create an account"}
          </h3>
        </div>
        <form className="flex flex-col gap-4" onSubmit={handleOnSubmit}>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="username" value="Username" />
            </div>
            <TextInput
              icon={RiUser3Line}
              id="username"
              type="username"
              value={username}
              onChange={(e) => setUsername(e.target.value)}
              required={true}
            />
          </div>
          <div>
            <div className="mb-2 block">
              <Label htmlFor="password1" value="Your password" />
            </div>
            <TextInput
              id="password1"
              type="password"
              required={true}
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              icon={RiLockPasswordLine}
            />
          </div>
          <div className="flex items-center gap-2">
            <ToggleSwitch
              checked={isSignInMode}
              label={`Click to ${isSignInMode ? "Register" : "Sign In"}`}
              onChange={() => setIsSignInMode((prev) => !prev)}
            />
          </div>
          <Button type="submit">{isSignInMode ? "Sign In" : "Register"}</Button>
        </form>
      </Card>
    </div>
  );
};
