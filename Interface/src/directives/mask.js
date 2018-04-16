export default {
  bind (el, binding, vnode) {
    if (binding.arg === 'emailOrCpf') {
      _renderEmailOrCpf(el, vnode)
    }
  }
}

function _renderEmailOrCpf (el, vnode) {
  el.addEventListener('keyup', event => {
    let currentValue = el.value
    if (!currentValue) return
    if (!/\D/.test(currentValue)) {
      if (currentValue.length === 11) {
        currentValue = currentValue.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/, '$1.$2.$3-$4')
      } else {
        currentValue = currentValue.replace(/\D/g, '')
      }
    } else if (!/[^\d.-]/.test(currentValue)) {
      currentValue = currentValue.replace(/\D/g, '')
    }
    el.value = currentValue
  })
}
