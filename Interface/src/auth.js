export default {
  setSession (session) {
    window.sessionStorage.session = session
  },
  getSession () {
    return window.sessionStorage.session
  }
}
