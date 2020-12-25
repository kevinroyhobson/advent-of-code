class Day23 {

    private var _cupInput = arrayListOf<Int>(4, 6, 9, 2, 1, 7, 5, 3, 8)

    fun thePuzzle(): Int {

        var cupsByValue = hashMapOf<Int, Cup>()

        // Initialize the next cup in the circle for each cup
        for (i in _cupInput.indices) {

            var thisCup = Cup(_cupInput[i])
            cupsByValue[_cupInput[i]] = thisCup

            if (i > 0) {
                var previousClockwiseCupValue = _cupInput[i - 1]
                var previousCup = cupsByValue[previousClockwiseCupValue]!!
                previousCup.nextClockwiseCup = thisCup
            }
        }

        var firstCup = cupsByValue[_cupInput.first()]!!
        var lastCup = cupsByValue[_cupInput.last()]!!
        lastCup.nextClockwiseCup = firstCup


        // Initialize the previous cup by value for each cup
        for (i in 2.._cupInput.count()) {
            var cupWithThisValue = cupsByValue[i]!!
            var cupWithPreviousValue = cupsByValue[i - 1]!!
            cupWithThisValue.previousCupByValue = cupWithPreviousValue
        }

        var minimumValueCup = cupsByValue[1]!!
        var maximumValueCup = cupsByValue[_cupInput.count()]!!
        minimumValueCup.previousCupByValue = maximumValueCup

        var currentCup = cupsByValue[_cupInput.first()]!!

//        for (i in 1..10000000) {
        for (i in 1..100) {

            var pickedUpCup1 = currentCup.nextClockwiseCup!!
            var pickedUpCup2 = pickedUpCup1.nextClockwiseCup!!
            var pickedUpCup3 = pickedUpCup2.nextClockwiseCup!!

            var pickedUpValues = hashSetOf(pickedUpCup1.value, pickedUpCup2.value, pickedUpCup3.value)

            var destinationCup = currentCup.previousCupByValue!!
            while (pickedUpValues.contains(destinationCup.value)) {
                destinationCup = destinationCup.previousCupByValue!!
            }

            // Change the pointers to move the picked up cups
            var newCupNextToCurrentCup = pickedUpCup3.nextClockwiseCup
            pickedUpCup3.nextClockwiseCup = destinationCup.nextClockwiseCup
            destinationCup.nextClockwiseCup = pickedUpCup1
            currentCup.nextClockwiseCup = newCupNextToCurrentCup

            // Set the next current cup
            currentCup = currentCup.nextClockwiseCup!!
        }

        print("[ ")
        for (i in 0 until _cupInput.count()) {
            print("${currentCup.value} ")
            currentCup = currentCup.nextClockwiseCup!!
        }
        println("]")

        var cupOne = cupsByValue[1]!!
        return cupOne.nextClockwiseCup!!.value * cupOne.nextClockwiseCup!!.nextClockwiseCup!!.value
    }
}

class Cup(val value: Int) {
    var nextClockwiseCup: Cup? = null
    var previousCupByValue: Cup? = null
}
