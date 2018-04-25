function validarCpf (strCPF) {
  strCPF = strCPF.replace(/[^\d]/g, '')
  let soma = 0
  let resto
  const regex = /^(\d)\1+$/
  let i

  if (strCPF === undefined) return false
  if (regex.test(strCPF)) return false

  for (i = 1; i <= 9; i++) {
    soma += parseInt(strCPF.substring(i - 1, i)) * (11 - i)
  }
  resto = (soma * 10) % 11
  if ((resto === 10) || (resto === 11)) resto = 0
  if (resto !== parseInt(strCPF.substring(9, 10))) return false

  soma = 0
  for (i = 1; i <= 10; i++) soma += parseInt(strCPF.substring(i - 1, i)) * (12 - i)

  resto = (soma * 10) % 11

  if ((resto === 10) || (resto === 11)) resto = 0
  if (resto !== parseInt(strCPF.substring(10, 11))) return false
  return true
}

const validPassword = pass => pass && /\d/.test(pass) && /\D/.test(pass)

export { validarCpf, validPassword }
