export default class {
  constructor (resource, http = null) {
    this._resource = resource('dashboard')
    if (http) {
      this._http = http
    }
  }
  async load () {
    const res = await this._http.get('dashboard/Load')
    return res
  }
}
