export default class {
  constructor (http) {
    this._http = http
  }
  async load () {
    const res = await this._http.get('teacher/Load')
    return res
  }
  async createRoom (data) {
    return this._http.post('teacher/CreateRoom', data)
  }
  async myRooms () {
    return this._http.get('teacher/MyRooms')
  }
}
