export interface ReservationUserInfoVM {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
}

export interface ReservationCreationVM {
  start: Date;
  end: Date;
  price: number;
  guestId: string;
  accommodationId: number;
}

export interface ReservationVM {
  id: number;
  start: Date;
  end: Date;
  price: number;
  accommodation: string;
  status: string;
}
