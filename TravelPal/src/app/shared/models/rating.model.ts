import { AccommodationVM } from 'src/app/stays/stays.model';
import { userVM } from './user.model';

export interface RatingCreationVM {
  userId: string;
  accommodationId: number;
  rate: number;
  comment: string;
}
export interface RatingVM {
  id: number;
  rate: number;
  comment: string;
  user: string;
  accommodation: AccommodationVM;
}
