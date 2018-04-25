export default class {
  constructor (resource) {
    this._resource = resource('institution{/name}')
  }
  async get (name) {
    const res = await this._resource.query({name})
    return res.json()
  }
}
