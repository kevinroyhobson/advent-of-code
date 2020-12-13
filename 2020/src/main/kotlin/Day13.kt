import java.io.File

class Day13 {

    private val _inputFilePath = "input/2020-12-13.txt"
    private var _leaveTimestamp = 0

    fun puzzle1() : Int {

        val input = File(_inputFilePath).readLines()
        _leaveTimestamp = input.first()
                               .toInt()
        val busIds = input.last()
                          .split(",")
                          .filter { it != "x" }
                          .map { it.toInt() }

        var minimumWaitBusId = Int.MAX_VALUE
        var minimumMinutesToWait = Int.MAX_VALUE
        for (busId in busIds) {

            var firstPossibleDepartureTime = getFirstPossibleDepartureTimeForBusId(busId)
            var minutesToWaitForThisBus = firstPossibleDepartureTime - _leaveTimestamp

            if (minutesToWaitForThisBus < minimumMinutesToWait) {
                minimumWaitBusId = busId
                minimumMinutesToWait = minutesToWaitForThisBus
            }
        }

        return minimumWaitBusId * minimumMinutesToWait
    }

    private fun getFirstPossibleDepartureTimeForBusId(busId: Int) : Int {

        if (_leaveTimestamp % busId == 0) {
            return _leaveTimestamp
        }

        var firstPossibleBusRunNumber = (_leaveTimestamp / busId) + 1
        return firstPossibleBusRunNumber * busId
    }
}
