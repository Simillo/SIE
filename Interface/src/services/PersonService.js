export default class {
  constructor (resource, http = null) {
    this._resource = resource('person{/id}{/cpf}')
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

  async canUseCpf (cpf) {
    const res = await this._resource.query({cpf})
    return res.json()
  }

  async login (login) {
    const res = await this._http.post('person/Login', login, {
      headers: {
        'Content-Type': 'application/json'
      }
    })
    return res.json()
  }
}
