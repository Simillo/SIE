export default class {
  constructor (resource, http = null) {
    this._resource = resource('person{/id}{/text}')
    if (http) {
      this._http = http
    }
  }
  async loadPersons () {
    const res = await this._resource.query()
    return res.json()
  }
  async savePerson (person) {
    const res = await this._resource.save(person)
    return res.json()
  }

  async canUseEmailOrCpf (text) {
    const res = await this._resource.query({text})
    return res.json()
  }

  async login (login) {
    const res = await this._http.post('person/Login', login, {
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    })
    return res.json()
  }
}
