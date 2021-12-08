import { LocationVM } from "../models/location.model";

export class EventCreationVM
{
    name!: string;
    price!: number;
    date!: string;
    duration!: string;
    eventDescription!: string;
    locationVM!: LocationVM;
}

export class EventEditVM
{
    name!: string;
    price!: number;
    date!: string;
    duration!: string;
    eventDescription!: string;
    locationVM!: LocationVM;
}

export class EventVM
{
    id!: number;
    name!: string;
    price!: number;
    date!: string;
    duration!: string;
    eventDescription!: string;
    locationVM!: LocationVM;
}