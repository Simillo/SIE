export default class {
  constructor (http) {
    this._http = http
  }
  async upload (files) {
    return this._http.post(`upload/Upload`, files, {
      headers: {
        'Content-Type': 'multipart/form-data'
      }
    })
  }
}
