export interface userVM {
  id: string;
  userName: string;
  staysListed: number;
  eventsListed: number;
  isAdmin: boolean;
}

export interface userProfileVM {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  emailVerified: boolean;
  phoneNumber: string;
  phoneNumberVerified: boolean;
}

export interface userEditVM {
  userName: string;
  firstName: string;
  lastName: string;
}
