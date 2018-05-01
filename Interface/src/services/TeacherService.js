export default class {
  constructor (http) {
    this._http = http
  }
  async load () {
    const res = await this._http.get('teacher/Load')
    return res
  }
}
