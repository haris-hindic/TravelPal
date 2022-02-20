import { LocationVM } from '../shared/models/location.model';
import { userVM } from '../shared/models/user.model';

export class EventCreationVM {
  name!: string;
  price!: number;
  date!: string;
  duration!: string;
  eventDescription!: string;
  locationVM!: LocationVM;
}

export class EventEditVM {
  name!: string;
  price!: number;
  date!: string;
  duration!: string;
  eventDescription!: string;
  locationVM!: LocationVM;
}

export class EventVM {
  id!: number;
  name!: string;
  price!: number;
  date!: string;
  duration!: string;
  user!: userVM;
  eventDescription!: string;
  locationVM!: LocationVM;
  images!: EventImages[];
}

export class EventImages {
  id!: number;
  imagePath!: string;
  eventId!: number;
  event!: Event;
}
export class EventSearchVM {
  location!: string;
  from!: string;
  to!: string;
}
