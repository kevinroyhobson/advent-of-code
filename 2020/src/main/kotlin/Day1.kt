import java.io.File

class Day1 {

    private val _inputFilePath = "input/2020-12-01.txt"
    private val _theYear = 2020

    public fun puzzle1() : Int {

        val inputIntsSet = getInputIntegers().toHashSet()

        for (number in inputIntsSet) {

            val thisNumberPair = _theYear - number
            if (inputIntsSet.contains(thisNumberPair)) {
                return number * thisNumberPair
            }
        }

        throw Exception("No two numbers add up to 2020.")
    }

    public fun puzzle2() : Int {

        val inputIntsSet = getInputIntegers().toHashSet()

        for (a in inputIntsSet) {
            for (b in inputIntsSet) {

                val c = _theYear - a - b
                if (inputIntsSet.contains(c)) {
                    return a * b * c
                }

            }
        }

        throw Exception("No three numbers add up to 2020.")
    }

    private fun getInputIntegers() : List<Int> {
        return File(_inputFilePath).readLines()
                                   .map { it.toInt() }
    }
}
