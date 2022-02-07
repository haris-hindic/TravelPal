export interface userVM {
  id: string;
  userName: string;
  staysListed: number;
  eventsListed: number;
  isAdmin: boolean;
  picture: string;
}

export interface userProfileVM {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  emailVerified: boolean;
  phoneNumber: string;
  phoneNumberVerified: boolean;
  picture: string;
}

export interface userEditVM {
  userName: string;
  firstName: string;
  lastName: string;
}
