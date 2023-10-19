export class FilterRooms {
  checkInFilter: Date
  checkOutFilter: Date
  capacityFilter: number
  namesFilter: string[]

  constructor() {
    this.checkInFilter = new Date()
    this.checkOutFilter = new Date()
    this.checkOutFilter.setDate(this.checkInFilter.getDate() + 1)
    this.capacityFilter = 0
    this.namesFilter = []
  }
}
