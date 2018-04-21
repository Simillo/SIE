export default class {
  constructor (resource, http = null) {
    this._resource = resource('person{/id}{/cpf}')
    if (http) {
      this._http = http
    }
  }
  async loadPersons () {
    try {
      const res = await this._resource.query()
      return res.json()
    } catch (ex) {
      console.log(ex)
      throw new Error('Não foi possível carregar as pessoas cadastradas!')
    }
  }
  async savePerson (person) {
    try {
      const res = await this._resource.save(person)
      return res.json()
    } catch (ex) {
      console.log(ex)
      throw new Error('Não foi possível salvar a pessoa.')
    }
  }

  async canUseCpf (cpf) {
    try {
      const res = await this._resource.query({cpf})
      return res.json()
    } catch (ex) {
      console.log(ex)
      throw new Error('Não foi possível consultar o CPF.')
    }
  }

  async login (login) {
    try {
      const res = await this._http.post('person/Login', login, {
        headers: {
          'Content-Type': 'application/json'
        }
      })
      return res.json()
    } catch (ex) {
      console.log(ex)
      throw new Error('Não foi possível autenticar.')
    }
  }
}
