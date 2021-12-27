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
  user: userVM;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
  images: Image[];
}

export interface AccommodationCreationVM {
  name: string;
  price: number;
  hostId: string;
  location: LocationCreationVM;
  accommodationDetails: AccommodationDetailsCreationVM;
}

export interface AccommodationEditVM {
  name: string;
  price: number;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
}

export interface AccommodationDetailsVM {
  id: number;
  parking: boolean;
  wifi: boolean;
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
}

export interface AccommodationDetailsCreationVM {
  parking: boolean;
  wifi: boolean;
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
}
