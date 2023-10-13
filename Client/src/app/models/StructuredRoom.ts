import { Room } from "./Room";

export class StructuredRoom {
  constructor(name: string, room: Room[]) {
    this.name = name
    this.room = room
  }

  name: string
  room: Room[]
  minCapacity: number
  maxCapacity: number
  minPrice: number
  maxPrice: number
}
