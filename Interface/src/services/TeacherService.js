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
  async loadRoom (roomCode) {
    return this._http.get(`teacher/LoadRoom/${roomCode}`)
  }
  async saveOrUpdateActivity (data, roomCode) {
    return this._http.post(`teacher/SaveActivity/${roomCode}`, data)
  }
  async loadActivity (roomCode, activityId) {
    return this._http.get(`teacher/LoadActivity/${roomCode}/${activityId}`)
  }
  async initiateActivity (activityId) {
    return this._http.get(`teacher/InitiateActivity/${activityId}`)
  }
  async finalizeActivity (activityId) {
    return this._http.get(`teacher/FinalizeActivity/${activityId}`)
  }
}
