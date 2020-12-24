class Day23 {

    var _allCupsInOrder = arrayListOf<Int>(4, 6, 9, 2, 1, 7, 5, 3, 8)
//    var _cupCount = 1000000
    var _cupCount = 9
    var _currentCupIndex = 0
    var _currentCupValue = 0
    var _destinationCupIndex = 0
    var _pickedUpCupValues = arrayListOf<Int>()

    fun thePuzzle() : Int {

//        for (i in 10.._cupCount) {
//            _allCupsInOrder.add(i)
//        }

//        for (i in 1..10000000) {
        for (i in 1..100) {

            _currentCupValue = _allCupsInOrder[_currentCupIndex]

            removePickedUpCups()
            setDestinationCupIndex()
            reinsertPickedUpCups()

            var newCurrentCupIndex = _allCupsInOrder.indexOf(_currentCupValue)
            _currentCupIndex = (newCurrentCupIndex + 1) % _cupCount

            if (i % 1000 == 0) {
                println("Processed $i moves.")
            }

            println(_allCupsInOrder)

        }

        var indexOfCupNumberOne = _allCupsInOrder.indexOf(1)
        var cupAIndex = (indexOfCupNumberOne + 1) % _cupCount
        var cupBIndex = (indexOfCupNumberOne + 2) % _cupCount

        println(_allCupsInOrder)
        println(indexOfCupNumberOne)
        println(_allCupsInOrder[cupAIndex])
        println(_allCupsInOrder[cupBIndex])

        return _allCupsInOrder[cupAIndex] * _allCupsInOrder[cupBIndex]
    }

    private fun removePickedUpCups() {

        _pickedUpCupValues.clear()

        var indexToRemoveAt = (_currentCupIndex + 1) % _cupCount
        for (x in 0..2) {

            var numRemainingCups = _cupCount - x
            if (indexToRemoveAt >= numRemainingCups) {
                indexToRemoveAt = 0
            }

            _pickedUpCupValues.add(_allCupsInOrder.removeAt(indexToRemoveAt))
        }
    }

    private fun reinsertPickedUpCups() {
        for (x in 0..2) {
            _allCupsInOrder.add(_destinationCupIndex + 1 + x, _pickedUpCupValues[x])
        }
    }

    private fun setDestinationCupIndex() {

        var pickedUpCupValues = _pickedUpCupValues.toHashSet()

        var destinationCupValue = _currentCupValue - 1
        if (destinationCupValue == 0) {
            destinationCupValue = _cupCount
        }

        while (pickedUpCupValues.contains(destinationCupValue)) {
            destinationCupValue--
            if (destinationCupValue == 0) {
                destinationCupValue = _cupCount
            }
        }

        _destinationCupIndex = _allCupsInOrder.indexOf(destinationCupValue)
    }
}
