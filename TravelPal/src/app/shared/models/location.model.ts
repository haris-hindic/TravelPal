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
