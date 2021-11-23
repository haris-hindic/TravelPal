export interface Accommodation {
  id: number;
  name: string;
  price: number;
  location: { id: number; country: string; city: string; address: string };
  accommodationImages: AccommodationImages[];
}

export interface AccommodationImages {
  accommodationId: number;
  imageId: number;
  image: { imageId: number; imagePath: string };
}
