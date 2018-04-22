export default class {
  constructor (resource, http = null) {
    this._resource = resource('dashboard')
    if (http) {
      this._http = http
    }
  }
  async load () {
    try {
      const res = await this._http.get('dashboard/Load')
      return res
    } catch (ex) {
      console.log(ex)
      throw new Error('Não foi possível carregar!')
    }
  }
}
