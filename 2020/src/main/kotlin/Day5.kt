import java.io.File
import kotlin.math.max
import kotlin.math.min

class Day5 {

    private val _inputFilePath = "input/2020-12-05.txt"

    fun puzzle1() : Int {

        var maxSeatId = 0

        val seatCodes = File(_inputFilePath).readLines()
        for (seatCode in seatCodes) {

            val seat = Seat(seatCode)
            maxSeatId = max(seat.getSeatId(), maxSeatId)
        }

        return maxSeatId
    }

    fun puzzle2() : Int {

        var maxSeatNumber = puzzle1()
        var isSeatTaken = Array<Boolean>(maxSeatNumber + 1) { false }

        val seatCodes = File(_inputFilePath).readLines()
        var minSeatNumber = Int.MAX_VALUE
        for (seatCode in seatCodes) {

            val seat = Seat(seatCode)
            var thisSeatId = seat.getSeatId()
            isSeatTaken[thisSeatId] = true

            minSeatNumber = min(thisSeatId, minSeatNumber)
        }

        for (i in minSeatNumber..maxSeatNumber) {

            if (!isSeatTaken[i]) {
                return i
            }
        }

        throw Exception("There are no empty seats on the plane.")
    }
}

class Seat {

    private var _row: Int
    private var _column: Int

    constructor(seatCode: String) {
        val binarySeatCode = seatCode.replace('F', '0')
                                     .replace('B', '1')
                                     .replace('L', '0')
                                     .replace('R', '1')

        _row = binarySeatCode.take(7).toInt(2)
        _column = binarySeatCode.takeLast(3).toInt(2)
    }

    fun getSeatId() : Int {
        return (_row * 8) + _column
    }
}
