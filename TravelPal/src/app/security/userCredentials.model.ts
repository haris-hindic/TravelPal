export interface signUpCredentials {
  userName: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  phoneNumber: number;
}

export interface signInCredentials {
  userName: string;
  password: string;
}

export interface authResponse {
  token: string;
  expiration: Date;
}
