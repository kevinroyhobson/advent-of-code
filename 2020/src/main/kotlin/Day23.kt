class Day23 {

    private var _cupInput = arrayListOf<Int>(4, 6, 9, 2, 1, 7, 5, 3, 8)

    fun thePuzzle(): Long {

        var cupsByValue = hashMapOf<Int, Cup>()

        // Initialize the next cup in the circle for each cup
        for (i in 1..1000000) {

            var thisCupValue = if (i <= _cupInput.count()) _cupInput[i-1] else i
            var thisCup = Cup(thisCupValue)
            cupsByValue[thisCupValue] = thisCup

            if (i > 1) {
                var previousClockwiseCupValue = if (i <= _cupInput.count() + 1) _cupInput[i-2] else i-1
                var previousCup = cupsByValue[previousClockwiseCupValue]!!
                previousCup.nextClockwiseCup = thisCup
            }
        }

        var firstCup = cupsByValue[_cupInput.first()]!!
        var lastCup = cupsByValue[1000000]!!
        lastCup.nextClockwiseCup = firstCup

        // Initialize the previous cup by value for each cup
        for (i in 2..1000000) {
            var cupWithThisValue = cupsByValue[i]!!
            var cupWithPreviousValue = cupsByValue[i - 1]!!
            cupWithThisValue.previousCupByValue = cupWithPreviousValue
        }

        var minimumValueCup = cupsByValue[1]!!
        var maximumValueCup = cupsByValue[1000000]!!
        minimumValueCup.previousCupByValue = maximumValueCup

        var currentCup = cupsByValue[_cupInput.first()]!!

        for (i in 1..10000000) {

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

        var cupOne = cupsByValue[1]!!
        println(cupOne.value)
        println(cupOne.nextClockwiseCup!!.value)
        println(cupOne.nextClockwiseCup!!.nextClockwiseCup!!.value)

        return cupOne.nextClockwiseCup!!.value.toLong() * cupOne.nextClockwiseCup!!.nextClockwiseCup!!.value.toLong()
    }
}

class Cup(val value: Int) {
    var nextClockwiseCup: Cup? = null
    var previousCupByValue: Cup? = null
}
