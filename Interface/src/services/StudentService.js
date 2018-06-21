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
  async loadRoom (roomCode) {
    return this._http.get(`student/LoadRoom/${roomCode}`)
  }
  async exitRoom (roomCode) {
    return this._http.get(`student/ExitRoom/${roomCode}`)
  }
  async loadActivity (roomCode, activityId) {
    return this._http.get(`student/LoadActivity/${roomCode}/${activityId}`)
  }
  async answer (roomCode, activityId, answer, attachments) {
    return this._http.post(`student/Answer/${roomCode}/${activityId}`, {
      Answer: answer,
      Attachments: attachments
    })
  }
}
