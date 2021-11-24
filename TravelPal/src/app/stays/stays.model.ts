export interface AccommodationVM {
  id: number;
  name: string;
  price: number;
  location: Location;
  images: Image[];
  accommodationDetails: AccommodationDetails;
}

export interface Image {
  id: number;
  imagePath: string;
}

export interface Location {
  id: number;
  country: string;
  city: string;
  address: string;
}

export interface AccommodationDetails {
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
