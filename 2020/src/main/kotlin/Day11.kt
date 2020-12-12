import java.io.File

class Day11 {

    private val _inputFilePath = "input/2020-12-11.txt"

    private var _waitingAreaBoard = arrayOf(arrayOf<SeatStatus>())
    private var _boardWidth = 0
    private var _boardHeight = 0

    fun thePuzzle() : Int {

        loadInitialBoardFromInput()
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

    private fun loadInitialBoardFromInput() {

        var inputLines = File(_inputFilePath).readLines()
        var inputBoardWidth = inputLines[0].length
        var emptyRowInput = ".".repeat(inputBoardWidth)

        var board = arrayListOf<Array<SeatStatus>>()
        board.add(initializeRowFromString(emptyRowInput))
        board.addAll(inputLines.map { initializeRowFromString(it) })
        board.add(initializeRowFromString(emptyRowInput))

        _waitingAreaBoard = board.toTypedArray()
        _boardWidth = _waitingAreaBoard[0].count()
        _boardHeight = _waitingAreaBoard.count()
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

        var nextBoard = Array(_boardHeight) { Array<SeatStatus>(_boardWidth) { SeatStatus.Floor } }

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

        var numOccupiedAdjacentSeats = getNumOccupiedSeatsVisibleFromLocation(x, y)

        if (currentStatus == SeatStatus.EmptySeat && numOccupiedAdjacentSeats == 0) {
            return SeatStatus.OccupiedSeat
        }

        if (currentStatus == SeatStatus.OccupiedSeat && numOccupiedAdjacentSeats >= 5) {
            return SeatStatus.EmptySeat
        }

        return currentStatus
    }

    private fun getNumOccupiedSeatsVisibleFromLocation(x: Int, y: Int) : Int {

        var numOccupiedVisibleSeats = 0
        for (i in -1..1) {
            for (j in -1..1) {
                if (i == 0 && j == 0) { continue } // Do not count this location itself.

                if (isOccupiedSeatVisibleInDirection(x, y, i, j)) {
                    numOccupiedVisibleSeats++
                }
            }
        }

        return numOccupiedVisibleSeats
    }

    private fun isOccupiedSeatVisibleInDirection(x: Int, y: Int, i: Int, j: Int) : Boolean {

        var xToCheck = x + i
        var yToCheck = y + j

        while (xToCheck in 0 until _boardWidth && yToCheck in 0 until _boardHeight) {

            var seatToCheck = _waitingAreaBoard[yToCheck][xToCheck]

            if (seatToCheck == SeatStatus.OccupiedSeat) {
                return true
            }

            if (seatToCheck == SeatStatus.EmptySeat) {
                return false
            }

            xToCheck += i
            yToCheck += j
        }

        return false
    }

    private fun isBoardIdentical(otherBoard: Array<Array<SeatStatus>>) : Boolean {

        for (x in 0 until _boardWidth) {
            for (y in 0 until _boardHeight) {
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
