import { Enum } from 'enumify'

class EProfile extends Enum {}

EProfile.initEnum(['Teacher', 'Student', 'NotShown'])

export default EProfile
