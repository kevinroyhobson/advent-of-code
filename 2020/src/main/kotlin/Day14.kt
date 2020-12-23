import java.io.File
import java.lang.StringBuilder

class Day14 {

    private val _inputFilePath = "input/2020-12-14.txt"
    private val _assignmentRegex = Regex("mem\\[([0-9]*)] = ([0-9]*)")

    private val _memoryValueByAddress = hashMapOf<Int, Long>()

    fun puzzle1() : Long {

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
                var memoryAddress = destructuredMatch[0].toInt()
                var value = destructuredMatch[1].toLong()

                _memoryValueByAddress[memoryAddress] = currentMask.getValueWithMaskApplied(value)
            }

        }

        return _memoryValueByAddress.values.sum()
    }
}

class Bitmask(private val _mask: String) {

    fun getValueWithMaskApplied(value: Long) : Long {

        var valueBinaryString = value.toString(2)
        var numZeroesToPad = _mask.length - valueBinaryString.count()
        valueBinaryString = "0".repeat(numZeroesToPad) + valueBinaryString

        var resultBinaryValue = StringBuilder()
        for (i in _mask.indices) {

            var thisBitValue = if (_mask[i] == 'X') valueBinaryString[i] else _mask[i]
            resultBinaryValue.append(thisBitValue)
        }

        return resultBinaryValue.toString().toLong(2)
    }
}
