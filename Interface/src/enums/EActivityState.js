import { Enum } from 'enumify'

class EActivityState extends Enum {}

EActivityState.initEnum(['', 'Building', 'InProgress', 'Done'])

export default EActivityState
