import java.io.File

class Day11 {

    private val _inputFilePath = "input/2020-12-11.txt"

    private var _waitingAreaBoard = arrayOf(arrayOf<SeatStatus>())


    fun puzzle1() : Int {

        _waitingAreaBoard = loadInitialBoardFromInput()
        printBoard()
        println()

        var isSystemInEquilibrium = false

        var i = 0
        while (!isSystemInEquilibrium) {

            var nextBoard = getNextBoard()
            isSystemInEquilibrium = isBoardIdentical(nextBoard)

            _waitingAreaBoard = nextBoard

            printBoard()
            println()

            i++
        }

        println("$i iterations.")

        return getNumOccupiedSeats()
    }

    private fun loadInitialBoardFromInput() : Array<Array<SeatStatus>> {

        var inputLines = File(_inputFilePath).readLines()
        var boardWidth = inputLines[0].length
        var emptyRowInput = ".".repeat(boardWidth)

        var board = arrayListOf<Array<SeatStatus>>()
        board.add(initializeRowFromString(emptyRowInput))
        board.addAll(inputLines.map { initializeRowFromString(it) })
        board.add(initializeRowFromString(emptyRowInput))

        return board.toTypedArray()
    }

    private fun printBoard() {
        for (row in _waitingAreaBoard) {
            for (cell in row) {
                when (cell) {
                    SeatStatus.EmptySeat -> print("L")
                    SeatStatus.Floor -> print(".")
                    SeatStatus.OccupiedSeat -> print("#")
                }
            }
            println()
        }
    }

    private fun initializeRowFromString(line: String) : Array<SeatStatus> {

        var lineWithBlankBoundaries = ".$line."

        return lineWithBlankBoundaries.map { ch -> when (ch) {
            'L' -> SeatStatus.EmptySeat
            else -> SeatStatus.Floor
        } }.toTypedArray()
    }

    private fun getNextBoard() : Array<Array<SeatStatus>> {

        var width = _waitingAreaBoard[0].count()
        var height = _waitingAreaBoard.count()
        var nextBoard = Array(height) { Array<SeatStatus>(width) { SeatStatus.Floor } }

        for (x in 0 until nextBoard[0].count()) {
            for (y in 0 until nextBoard.count()) {
                nextBoard[y][x] = getNextSeatStatusForPosition(x, y)
            }
        }

        return nextBoard
    }

    private fun getNextSeatStatusForPosition(x: Int, y: Int) : SeatStatus {

        var currentStatus = _waitingAreaBoard[y][x]

        if (currentStatus == SeatStatus.Floor) {
            return SeatStatus.Floor
        }

        var numOccupiedAdjacentSeats = getNumOccupiedSeatsAdjacentToLocation(x, y)

        if (currentStatus == SeatStatus.EmptySeat && numOccupiedAdjacentSeats == 0) {
            return SeatStatus.OccupiedSeat
        }

        if (currentStatus == SeatStatus.OccupiedSeat && numOccupiedAdjacentSeats >= 4) {
            return SeatStatus.EmptySeat
        }

        return currentStatus
    }

    private fun getNumOccupiedSeatsAdjacentToLocation(x: Int, y: Int) : Int {

        var numOccupiedAdjacentSeats = 0
        for (i in -1..1) {
            for (j in -1..1) {
                if (i == 0 && j == 0) { continue } // Do not count this location itself.

                if (_waitingAreaBoard[y+i][x+j] == SeatStatus.OccupiedSeat) {
                    numOccupiedAdjacentSeats++
                }
            }
        }

        return numOccupiedAdjacentSeats
    }

    private fun isBoardIdentical(otherBoard: Array<Array<SeatStatus>>) : Boolean {

        for (x in 0 until _waitingAreaBoard[0].count()) {
            for (y in 0 until _waitingAreaBoard.count()) {
                if (_waitingAreaBoard[y][x] != otherBoard[y][x]) {
                    return false
                }
            }
        }

        return true
    }

    private fun getNumOccupiedSeats() : Int {
        return _waitingAreaBoard.map { row -> row.count { it == SeatStatus.OccupiedSeat} }
                                .sum()
    }
}

enum class SeatStatus {
    Floor,
    EmptySeat,
    OccupiedSeat
}
