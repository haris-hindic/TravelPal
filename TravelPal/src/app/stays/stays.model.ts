export interface AccommodationVM {
  id: number;
  name: string;
  price: number;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
  images: Image[];
}

export interface AccommodationCreationVM {
  name: string;
  price: number;
  location: LocationCreationVM;
  accommodationDetails: AccommodationDetailsCreationVM;
}

export interface AccommodationEditVM {
  name: string;
  price: number;
  location: LocationVM;
  accommodationDetails: AccommodationDetailsVM;
}

export interface Image {
  id: number;
  imagePath: string;
}

export interface LocationVM {
  id: number;
  country: string;
  city: string;
  address: string;
}

export interface LocationCreationVM {
  country: string;
  city: string;
  address: string;
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
