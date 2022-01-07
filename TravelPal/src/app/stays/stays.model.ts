import { Image } from '../shared/models/image.model';
import {
  LocationCreationVM,
  LocationVM,
} from '../shared/models/location.model';
import { userVM } from '../shared/models/user.model';

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
