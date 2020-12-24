class Day23 {

    var _allCupsInOrder = "469217538"
    var _currentCupIndex = 0
    var _currentCupValue = 0
    var _destinationCupIndex = 0
    var _pickedUpCupsIndices = arrayListOf<Int>()

    fun puzzle1() {

        for (i in 1..100) {

            _currentCupValue = _allCupsInOrder[_currentCupIndex].toString().toInt()
            setPickedUpCupIndexes()
            setDestinationCupIndex()

            var nextCupOrder = _allCupsInOrder[_destinationCupIndex].toString()
            _pickedUpCupsIndices.forEach { nextCupOrder += _allCupsInOrder[it] }

            var nextCupIndex = (_destinationCupIndex + 1) % _allCupsInOrder.length
            while (nextCupOrder.length < _allCupsInOrder.length) {

                if (!_pickedUpCupsIndices.contains(nextCupIndex)) {
                    nextCupOrder += _allCupsInOrder[nextCupIndex]
                }

                nextCupIndex = (nextCupIndex + 1) % _allCupsInOrder.length
            }

            var newIndexOfCurrentCup = nextCupOrder.indexOf(_currentCupValue.toString())
            var nextCurrentCupIndex = (newIndexOfCurrentCup + 1) % nextCupOrder.length

            _allCupsInOrder = nextCupOrder
            _currentCupIndex = nextCurrentCupIndex

            println(_allCupsInOrder)
        }

    }

    private fun setPickedUpCupIndexes() {

        _pickedUpCupsIndices.clear()

        var currentIndexToUse = (_currentCupIndex + 1) % _allCupsInOrder.length
        for (x in 1..3) {
            _pickedUpCupsIndices.add(currentIndexToUse)

            currentIndexToUse = (currentIndexToUse + 1) % _allCupsInOrder.length
        }
    }

    private fun setDestinationCupIndex() {

        var pickedUpCupValues = _pickedUpCupsIndices.map { _allCupsInOrder[it].toString().toInt() }
                                                    .toHashSet()

        var destinationCupValue = _currentCupValue - 1
        if (destinationCupValue == 0) {
            destinationCupValue = _allCupsInOrder.length
        }

        while (pickedUpCupValues.contains(destinationCupValue)) {
            destinationCupValue--
            if (destinationCupValue == 0) {
                destinationCupValue = _allCupsInOrder.length
            }
        }

        _destinationCupIndex = _allCupsInOrder.indexOf(destinationCupValue.toString())
    }
}
