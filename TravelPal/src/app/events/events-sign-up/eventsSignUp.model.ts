export interface PaymentInfoVM {
  country: string;
  city: string;
  postalCode: string;
  ccNumber: string;
  expDate: Date;
  ccvCode: string;
}

export interface EventSignUpCreationVM {
  eventParticipantId: string;
  eventId: number;
  price: number;
  signUpDate: Date;
  paymentInfo: PaymentInfoVM;
}

export interface EventSignUpVM {
  id: number;
  signUpDate: Date;
  price: number;
  event: string;
  status: string;
  eventDate: Date;
}
