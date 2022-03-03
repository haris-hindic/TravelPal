import { Image } from '../shared/models/image.model';
import {
  LocationCreationVM,
  LocationVM,
} from '../shared/models/location.model';
import { RatingVM } from '../shared/models/rating.model';
import { userVM } from '../shared/models/user.model';
import { ReservationDatesVM } from './reservations/reservation.model';

export interface AccommodationVM {
  id: number;
  name: string;
  price: number;
  description: string;
  rooms: number;
  capacity: number;
  user: userVM;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
  images: Image[];
  dateReserved: ReservationDatesVM[];
  ratings: RatingVM[];
}

export interface AccommodationBasicVM {
  id: number;
  name: string;
  price: number;
  image: string;
  country: string;
  address: string;
  city: string;
  latitude: number;
  longitude: number;
  userImage: string;
}

export interface AccommodationCreationVM {
  name: string;
  price: number;
  description: string;
  rooms: number;
  capacity: number;
  hostId: string;
  location: LocationCreationVM;
  accommodationDetails: AccommodationDetailsCreationVM;
}

export interface AccommodationEditVM {
  name: string;
  price: number;
  description: string;
  rooms: number;
  capacity: number;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
}

export interface AccommodationDetailsVM {
  accommodationDetailsId: number;
  parking: boolean;
  wiFi: boolean;
  shower: boolean;
  minibar: boolean;
  airConditioning: boolean;
  safe: boolean;
  dryer: boolean;
  flatScreenTV: boolean;
  petFriendly: boolean;
  bbq: boolean;
  refrigerator: boolean;
  balcony: boolean;
  mosquitoNet: boolean;
  cancellation: string;
  houseRules: string;
}

export interface AccommodationDetailsCreationVM {
  parking: boolean;
  wiFi: boolean;
  shower: boolean;
  minibar: boolean;
  airConditioning: boolean;
  safe: boolean;
  dryer: boolean;
  flatScreenTV: boolean;
  petFriendly: boolean;
  bbq: boolean;
  refrigerator: boolean;
  balcony: boolean;
  mosquitoNet: boolean;
  cancellation: string;
  houseRules: string;
}

export interface AccommodationSearchVM {
  price: number;
  location: string;
}
