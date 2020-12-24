class Day23 {

    var _allCupsInOrder = arrayListOf(4, 6, 9, 2, 1, 7, 5, 3, 8)
    var _currentCupIndex = 0
    var _currentCupValue = 0
    var _destinationCupIndex = 0
    var _pickedUpCupsIndices = arrayListOf<Int>()

    fun puzzle1() {

        for (i in 1..100) {

            _currentCupValue = _allCupsInOrder[_currentCupIndex]
            setPickedUpCupIndexes()
            setDestinationCupIndex()

            var nextCupOrder = arrayListOf<Int>()
            nextCupOrder.add(_allCupsInOrder[_destinationCupIndex])
            _pickedUpCupsIndices.forEach { nextCupOrder.add(_allCupsInOrder[it]) }

            var nextCupIndex = (_destinationCupIndex + 1) % _allCupsInOrder.count()
            while (nextCupOrder.count() < _allCupsInOrder.count()) {

                if (!_pickedUpCupsIndices.contains(nextCupIndex)) {
                    nextCupOrder.add(_allCupsInOrder[nextCupIndex])
                }

                nextCupIndex = (nextCupIndex + 1) % _allCupsInOrder.count()
            }

            var newIndexOfCurrentCup = nextCupOrder.indexOf(_currentCupValue)
            var nextCurrentCupIndex = (newIndexOfCurrentCup + 1) % nextCupOrder.count()

            _allCupsInOrder = nextCupOrder
            _currentCupIndex = nextCurrentCupIndex

            println(_allCupsInOrder)
        }

    }

    private fun setPickedUpCupIndexes() {

        _pickedUpCupsIndices.clear()

        var currentIndexToUse = (_currentCupIndex + 1) % _allCupsInOrder.count()
        for (x in 1..3) {
            _pickedUpCupsIndices.add(currentIndexToUse)

            currentIndexToUse = (currentIndexToUse + 1) % _allCupsInOrder.count()
        }
    }

    private fun setDestinationCupIndex() {

        var pickedUpCupValues = _pickedUpCupsIndices.map { _allCupsInOrder[it] }
                                                    .toHashSet()

        var destinationCupValue = _currentCupValue - 1
        if (destinationCupValue == 0) {
            destinationCupValue = _allCupsInOrder.count()
        }

        while (pickedUpCupValues.contains(destinationCupValue)) {
            destinationCupValue--
            if (destinationCupValue == 0) {
                destinationCupValue = _allCupsInOrder.count()
            }
        }

        _destinationCupIndex = _allCupsInOrder.indexOf(destinationCupValue)
    }
}
