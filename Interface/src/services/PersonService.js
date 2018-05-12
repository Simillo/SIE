export default class {
  constructor (http) {
    this._http = http
  }
  async loadPersons () {
    const res = await this._resource.query()
    return res.json()
  }
  async savePerson (person) {
    return this._http.post('person/Save', person)
  }
  async login (login) {
    const res = await this._http.post('person/Login', login)
    return res.json()
  }
  async logout () {
    return this._http.get('person/Logout')
  }
}
