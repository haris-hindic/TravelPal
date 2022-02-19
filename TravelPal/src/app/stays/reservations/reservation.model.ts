export interface ReservationUserInfoVM {
  userName: string;
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
}

export interface ReservationTempInfo {
  name: string;
  start: Date;
  end: Date;
  price: number;
  guestId: string;
  accommodationId: number;
}

export interface ReservationCreationVM {
  start: Date;
  end: Date;
  price: number;
  guestId: string;
  accommodationId: number;
  paymentInfo: PaymentInfoCreationVM;
}

export interface PaymentInfoCreationVM {
  country: string;
  city: string;
  postalCode: string;
  ccNumber: string;
  expDate: Date;
  ccvCode: string;
}

export interface ReservationVM {
  id: number;
  start: Date;
  end: Date;
  price: number;
  accommodation: string;
  status: string;
}

export interface ReservationDatesVM {
  start: Date;
  end: Date;
}
