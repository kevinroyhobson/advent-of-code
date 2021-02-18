class Day15 {

    var _numberToTurnsSpokenMapping = hashMapOf<Int, ArrayList<Int>>()

    var _startingNumbers = listOf<Int>(12,20,0,6,1,17,7)

    fun puzzle1() : Int {

        var currentTurnNumber = 1

        for (startingNumber in _startingNumbers) {
            setNumberSpokenDuringTurn(startingNumber, currentTurnNumber)
            currentTurnNumber++
        }

        var mostRecentlySpokenNumber = _startingNumbers.last()

        while (currentTurnNumber <= 2020) {

            var turnsThatMostRecentlySpokenNumberWasSpoken = getTurnsOnWhichNumberWasSpoken(mostRecentlySpokenNumber)
            var wasMostRecentlySpokenNumberNew = turnsThatMostRecentlySpokenNumberWasSpoken.count() == 1

            var currentSpokenNumber = 0

            if (wasMostRecentlySpokenNumberNew) {
                currentSpokenNumber = 0
            }
            else {
                var priorTurnSpoken = turnsThatMostRecentlySpokenNumberWasSpoken[turnsThatMostRecentlySpokenNumberWasSpoken.count() - 2]
                currentSpokenNumber = currentTurnNumber - 1 - priorTurnSpoken
            }

            setNumberSpokenDuringTurn(currentSpokenNumber, currentTurnNumber)
            mostRecentlySpokenNumber = currentSpokenNumber
            currentTurnNumber++
        }

        return mostRecentlySpokenNumber
    }

    private fun setNumberSpokenDuringTurn(numberSpoken: Int, turnNumber: Int) {

        if (_numberToTurnsSpokenMapping.containsKey(numberSpoken)) {
            _numberToTurnsSpokenMapping[numberSpoken]!!.add(turnNumber)
        }
        else {
            _numberToTurnsSpokenMapping[numberSpoken] = arrayListOf<Int>(turnNumber)
        }

    }

    private fun getTurnsOnWhichNumberWasSpoken(numberSpoken: Int) : ArrayList<Int> {
        return _numberToTurnsSpokenMapping[numberSpoken]!!
    }


}