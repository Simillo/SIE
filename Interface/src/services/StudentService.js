export default class {
  constructor (http) {
    this._http = http
  }
  async load () {
    const res = await this._http.get('student/Load')
    return res
  }
  async loadRooms () {
    return this._http.get('student/LoadRooms')
  }
  async join (roomCode) {
    return this._http.get(`student/Join/${roomCode}`)
  }
  async loadMyRooms () {
    return this._http.get('student/LoadMyRooms')
  }
}
