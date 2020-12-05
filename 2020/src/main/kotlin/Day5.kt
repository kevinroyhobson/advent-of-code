import java.io.File
import kotlin.math.max

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
