import java.io.File
import java.lang.Exception

class Day16 {

    private val _inputFilePath = "input/2020-12-16.txt"
    private var _inputReader = File(_inputFilePath).bufferedReader()

    private var _validFieldValues = hashSetOf<Int>()
    private var _nearbyTickets = arrayListOf<String>()

    fun puzzle1() : Int {

        processFieldRules()
        processNearbyTickets()

        var ticketScanningErrorRate = 0

        for (ticket in _nearbyTickets) {
            var ticketFieldValues = ticket.split(",").map { it.toInt() }

            ticketFieldValues.filter { !_validFieldValues.contains(it) }
                             .forEach { ticketScanningErrorRate += it }
        }

        return ticketScanningErrorRate
    }

    private fun processFieldRules() {
        var currentInputLine = _inputReader.readLine()

        while (currentInputLine != "") {

            var match = _fieldRuleRegex.find(currentInputLine)

            if (match == null) {
                throw Exception("Invalid field rule.")
            }

            var firstRangeMin = match.groups[1]!!.value.toInt()
            var firstRangeMax = match.groups[2]!!.value.toInt()
            var secondRangeMin = match.groups[3]!!.value.toInt()
            var secondRangeMax = match.groups[4]!!.value.toInt()

            for (i in firstRangeMin..firstRangeMax) {
                _validFieldValues.add(i)
            }

            for (i in secondRangeMin..secondRangeMax) {
                _validFieldValues.add(i)
            }

            currentInputLine = _inputReader.readLine()
        }
    }
    private var _fieldRuleRegex = Regex("[.]*: ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)")

    private fun processNearbyTickets() {

        var currentInputLine = _inputReader.readLine()

        while (currentInputLine != "nearby tickets:") {
            currentInputLine = _inputReader.readLine()
        }
        currentInputLine = _inputReader.readLine()

        while (currentInputLine != null) {
            _nearbyTickets.add(currentInputLine)
            currentInputLine = _inputReader.readLine()
        }
    }

}
