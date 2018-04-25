import Institution from './Institution'

export default class {
  constructor () {
    this.Name = ''
    this.Cpf = ''
    this.Email = ''
    this.Institution = new Institution()
    this.BirthDate = null
    this.Password = ''
    this.Sex = 1
    this.Profile = 0
  }
}
