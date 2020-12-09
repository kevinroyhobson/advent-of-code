import java.io.File
import kotlin.math.max
import kotlin.math.min

class Day9 {

    private val _inputFilePath = "input/2020-12-09.txt"
    private val windowWidth = 25

    fun puzzle1() : Long {

        val input = File(_inputFilePath).readLines()
                                           .map { it.toLong() }
                                           .toLongArray()

        var indexToCheck = windowWidth
        while (indexToCheck < input.count()) {

            val numberToCheck = input[indexToCheck]
            val subsetToCheck = input.copyOfRange(indexToCheck - windowWidth, indexToCheck)
            if (!isNumberSumOfTwoOtherNumbers(numberToCheck, subsetToCheck)) {
                return numberToCheck
            }

            indexToCheck++
        }

        throw Exception("All numbers are valid.")
    }

    fun puzzle2() : Long {

        val targetSum = puzzle1()

        val input = File(_inputFilePath).readLines()
                                           .map { it.toLong() }

        var leftWindowIndex = 0
        var rightWindowIndex = 0
        var currentWindowSum = input[0]

        while (currentWindowSum != targetSum) {

            if (currentWindowSum < targetSum) {
                rightWindowIndex++
                currentWindowSum += input[rightWindowIndex]
            } else {
                currentWindowSum -= input[leftWindowIndex]
                leftWindowIndex++
            }
        }

        val contiguousNumbers = input.filterIndexed { i, value -> i in leftWindowIndex..rightWindowIndex }
        var minValue = contiguousNumbers.minOrNull()
        var maxValue = contiguousNumbers.maxOrNull()

        return minValue!! + maxValue!!
    }

    private fun isNumberSumOfTwoOtherNumbers(number: Long, candidatePairs: LongArray) : Boolean {

        var pairSet = candidatePairs.toHashSet()

        for (a in candidatePairs) {
            var b = number - a
            if (pairSet.contains(b)) {
                return true
            }
        }

        return false
    }
}
