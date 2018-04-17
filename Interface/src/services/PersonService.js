export default class {
  constructor (resource) {
    this._resource = resource('person{/id}')
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
      throw new Error('Não foi possível salvar a pessoa')
    }
  }
}
