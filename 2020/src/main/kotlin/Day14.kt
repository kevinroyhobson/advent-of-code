import java.io.File
import java.lang.StringBuilder
import java.util.*

class Day14 {

    private val _inputFilePath = "input/2020-12-14.txt"
    private val _assignmentRegex = Regex("mem\\[([0-9]*)] = ([0-9]*)")

    private val _memoryValueByAddress = hashMapOf<Long, Long>()

    fun thePuzzle() : Long {

        var currentMask = Bitmask("X")

        var input = File(_inputFilePath).readLines()
        for (line in input) {

            if (line.startsWith("mask")) {
                var nextMaskString = line.split("=")[1].trim()
                currentMask = Bitmask(nextMaskString)
            }

            else {
                var destructuredMatch = _assignmentRegex.find(line)!!
                                                        .destructured
                                                        .toList()
                var originalAddress = destructuredMatch[0].toLong()
                var value = destructuredMatch[1].toLong()

                var allMemoryAddresses = currentMask.getMemoryAddressesWithMaskApplied(originalAddress)
                for (address in allMemoryAddresses) {
                    _memoryValueByAddress[address] = value
                }
            }
        }

        return _memoryValueByAddress.values.sum()
    }
}

class Bitmask(private val _mask: String) {

    fun getValueWithMaskApplied(value: Long) : Long {

        var valueBinaryString = getPaddedBinaryStringForValue(value)

        var resultBinaryValue = StringBuilder()
        for (i in _mask.indices) {

            var thisBitValue = if (_mask[i] == 'X') valueBinaryString[i] else _mask[i]
            resultBinaryValue.append(thisBitValue)
        }

        return resultBinaryValue.toString().toLong(2)
    }

    fun getMemoryAddressesWithMaskApplied(memoryAddress: Long) : List<Long> {

        var memoryAddressBinaryString = getPaddedBinaryStringForValue(memoryAddress)

        var newAddressMask = StringBuilder()
        for (i in _mask.indices) {

            var thisBitValue =
                when (_mask[i]) {
                    '0' -> memoryAddressBinaryString[i]
                    else -> _mask[i]
                }

            newAddressMask.append(thisBitValue)
        }

        var possibleBinaryAddresses: Queue<String> = LinkedList<String>()
        possibleBinaryAddresses.add(newAddressMask.toString())

        while (possibleBinaryAddresses.peek().contains("X")) {

            var thisAddressRepresentation = possibleBinaryAddresses.remove()
            var firstXLocation = thisAddressRepresentation.indexOf("X")

            var prefix = thisAddressRepresentation.take(firstXLocation)
            var suffix = thisAddressRepresentation.takeLast(thisAddressRepresentation.length - firstXLocation - 1)

            possibleBinaryAddresses.add(prefix + "0" + suffix)
            possibleBinaryAddresses.add(prefix + "1" + suffix)
        }

        return possibleBinaryAddresses.map { it.toLong(2) }
    }

    private fun getPaddedBinaryStringForValue(value: Long) : String {

        var valueBinaryString = value.toString(2)
        var numZeroesToPad = _mask.length - valueBinaryString.count()

        return "0".repeat(numZeroesToPad) + valueBinaryString
    }
}
