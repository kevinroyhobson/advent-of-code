import java.io.File
import kotlin.math.abs

class Day12 {

    private val _inputFilePath = "input/2020-12-12.txt"

    private var _x = 0
    private var _y = 0
    private var _facingDegrees = 90

    fun puzzle1() : Int {

        var input = File(_inputFilePath).readLines()
        for (line in input) {
            processAction(line)
        }

        return abs(_x) + abs(_y)
    }

    private fun processAction(inputLine: String) {
        var action = inputLine.first()
        var value = inputLine.substring(1).toInt()

        when (action) {
            'N', 'E', 'S', 'W', 'F' -> {
                var directionToMove = getDirectionByLetter(action)
                move(directionToMove, value)
            }
            'L', 'R' -> {
                changeDirection(action, value)
            }
        }
    }

    private fun move(direction: Direction, distance: Int) {
        when (direction) {
            Direction.North -> _y += distance
            Direction.East -> _x += distance
            Direction.South -> _y  -= distance
            Direction.West -> _x -= distance
        }
    }

    private fun getDirectionByLetter(letter: Char) : Direction {
        return when (letter) {
            'N' -> Direction.North
            'E' -> Direction.East
            'S' -> Direction.South
            'W' -> Direction.West
            'F' -> getCurrentFacingDirection()
            else -> throw Exception("invalid action")
        }
    }

    private fun getCurrentFacingDirection() : Direction {
        return when (_facingDegrees) {
            0 -> Direction.North
            90 -> Direction.East
            180 -> Direction.South
            270 -> Direction.West
            else -> throw Exception("invalid direction")
        }
    }

    private fun changeDirection(turnDirection: Char, degrees: Int) {

        when (turnDirection) {
            'R' -> _facingDegrees += degrees
            'L' -> _facingDegrees -= degrees
        }

        if (_facingDegrees < 0) {
            _facingDegrees += 360
        }

        _facingDegrees %= 360
    }
}

enum class Direction {
    North,
    East,
    South,
    West
}
