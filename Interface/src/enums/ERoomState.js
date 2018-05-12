import { Enum } from 'enumify'

class ERoomState extends Enum {}

ERoomState.initEnum(['', 'Building', 'Open', 'Closed'])

export default ERoomState
