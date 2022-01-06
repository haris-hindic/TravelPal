export interface LocationVM {
  id: number;
  country: string;
  city: string;
  address: string;
  latitude: number;
  longitude: number;
}

export interface LocationCreationVM {
  country: string;
  city: string;
  address: string;
  latitude: number;
  longitude: number;
}

export interface country {
  id: number;
  name: string;
  iso2: string;
}
export interface city {
  id: number;
  name: string;
}
