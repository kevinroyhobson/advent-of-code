import java.io.File
import java.lang.Exception

class Day16 {

    private val _inputFilePath = "input/2020-12-16.txt"
    private var _inputReader = File(_inputFilePath).bufferedReader()

    private var _validFieldValues = hashSetOf<Int>()
    private var _validValuesByFieldName = hashMapOf<String, HashSet<Int>>()
    private var _validNearbyTickets = arrayListOf<Array<Int>>()

    private var _myTicket = arrayOf<Int>()

    fun puzzle2() : Long {

        processFieldRules()
        processMyTicket()
        processNearbyTickets()

        var ticketPositionByFieldName = hashMapOf<String, Int>()
        var positionsMappedToFields = hashSetOf<Int>()

        var numFields = _validValuesByFieldName.count()

        // Each time through this loop, determine which ticket positions are valid for which field names. Whenever there is
        // a field name that can only possibly match one ticket position, store that field name -> position mapping and then
        // do not process that field name or position ever again. Each time through the loop, the problem becomes a little
        // bit simpler until each field name must match only one position.
        while (ticketPositionByFieldName.count() < _validValuesByFieldName.count()) {

            for (fieldName in _validValuesByFieldName.keys) {

                // If this field name has already been mapped to its position, do not try to process it again.
                if (ticketPositionByFieldName.containsKey(fieldName)) {
                    continue
                }

                var validPositionsForThisFieldName = arrayListOf<Int>()
                for (i in 0 until numFields) {

                    // If this position has already been mapped to its field name, do not try to map any other fields to it.
                    if (positionsMappedToFields.contains(i)) {
                        continue
                    }

                    if (isPositionValidForFieldName(i, fieldName)) {
                        validPositionsForThisFieldName.add(i)
                    }
                }

                // If there was only one valid position for this field name, store the mapping and don't process this field name
                // or position anymore.
                if (validPositionsForThisFieldName.count() == 1) {
                    ticketPositionByFieldName[fieldName] = validPositionsForThisFieldName.single()
                    positionsMappedToFields.add(validPositionsForThisFieldName.single())
                }
            }
        }

        var departureProduct = 1L
        for (fieldName in ticketPositionByFieldName.keys.filter { it.startsWith("departure") } ) {
            var fieldPosition = ticketPositionByFieldName[fieldName]!!
            departureProduct *= _myTicket[fieldPosition]
        }

        return departureProduct
    }

    private fun processFieldRules() {
        var currentInputLine = _inputReader.readLine()

        while (currentInputLine != "") {

            var match = _fieldRuleRegex.find(currentInputLine)

            if (match == null) {
                throw Exception("Invalid field rule.")
            }

            var fieldName = match.groups[1]!!.value
            var firstRangeMin = match.groups[2]!!.value.toInt()
            var firstRangeMax = match.groups[3]!!.value.toInt()
            var secondRangeMin = match.groups[4]!!.value.toInt()
            var secondRangeMax = match.groups[5]!!.value.toInt()

            _validValuesByFieldName[fieldName] = hashSetOf<Int>()

            for (i in firstRangeMin..firstRangeMax) {
                _validFieldValues.add(i)
                _validValuesByFieldName[fieldName]!!.add(i)
            }

            for (i in secondRangeMin..secondRangeMax) {
                _validFieldValues.add(i)
                _validValuesByFieldName[fieldName]!!.add(i)
            }

            currentInputLine = _inputReader.readLine()
        }
    }
    private var _fieldRuleRegex = Regex("^(.*): ([0-9]*)-([0-9]*) or ([0-9]*)-([0-9]*)")

    private fun processMyTicket() {
        var currentInputLine = _inputReader.readLine()

        while (currentInputLine != "your ticket:") {
            currentInputLine = _inputReader.readLine()
        }
        currentInputLine = _inputReader.readLine()

        _myTicket = currentInputLine.split(",")
                                    .map { it.toInt() }
                                    .toTypedArray()
    }

    private fun processNearbyTickets() {

        var currentInputLine = _inputReader.readLine()

        while (currentInputLine != "nearby tickets:") {
            currentInputLine = _inputReader.readLine()
        }
        currentInputLine = _inputReader.readLine()

        while (currentInputLine != null) {

            var thisTicketFields = currentInputLine.split(",")
                                                   .map { it.toInt() }
                                                   .toTypedArray()

            if (thisTicketFields.all { _validFieldValues.contains(it) }) {
                _validNearbyTickets.add(thisTicketFields)
            }

            currentInputLine = _inputReader.readLine()
        }
    }

    private fun isPositionValidForFieldName(position: Int, fieldName: String) : Boolean {
        return _validNearbyTickets.all { ticket -> _validValuesByFieldName[fieldName]!!.contains(ticket[position])}
    }

}
