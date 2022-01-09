export interface LocationVM {
  id: number;
  country: string;
  countryId: number;
  city: string;
  cityId: number;
  address: string;
  latitude: number;
  longitude: number;
}

export interface LocationCreationVM {
  cityId: number;
  address: string;
  latitude: number;
  longitude: number;
}

export interface country {
  id: number;
  name: string;
}
export interface city {
  id: number;
  name: string;
}
