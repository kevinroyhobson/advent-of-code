import java.io.File

class Day10 {

    private val _inputFilePath = "input/2020-12-10.txt"
    private val _joltageAdapterSet = File(_inputFilePath).readLines()
                                                         .map { it.toInt() }
                                                         .toHashSet()

    private val _deviceJoltage = _joltageAdapterSet.maxOrNull()!! + 3

    init {
        _joltageAdapterSet.add(_deviceJoltage)
    }

    fun puzzle1() : Int {

        var numOneJoltDifferences = 0
        var numThreeJoltDifferences = 0

        var lastJoltageSeen = 0
        for (thisJoltage in 1.._deviceJoltage) {

            if (_joltageAdapterSet.contains(thisJoltage)) {
                val joltageDifference = thisJoltage - lastJoltageSeen
                when (joltageDifference) {
                    1 -> numOneJoltDifferences++
                    3 -> numThreeJoltDifferences++
                }

                lastJoltageSeen = thisJoltage
            }
        }

        return numOneJoltDifferences * numThreeJoltDifferences
    }

    fun puzzle2() : Long {
        return getNumberOfCombinationsFromAdapterToDevice(0)
    }

    private fun getNumberOfCombinationsFromAdapterToDevice(adapterJoltage: Int) : Long {

        if(adapterJoltage == _deviceJoltage) {
            return 1
        }

        if (_numCombinationsFromAdapterToDeviceCache.containsKey(adapterJoltage)) {
            return _numCombinationsFromAdapterToDeviceCache[adapterJoltage]!!
        }

        var numCombinations: Long = 0
        for (joltageIncrease in 1..3) {
            if (_joltageAdapterSet.contains(adapterJoltage + joltageIncrease)) {
                numCombinations += getNumberOfCombinationsFromAdapterToDevice(adapterJoltage + joltageIncrease)
            }
        }

        _numCombinationsFromAdapterToDeviceCache[adapterJoltage] = numCombinations

        return numCombinations
    }

    private val _numCombinationsFromAdapterToDeviceCache = hashMapOf<Int, Long>()
}
