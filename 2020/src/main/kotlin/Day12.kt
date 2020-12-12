import java.io.File
import kotlin.math.abs

class Day12 {

    private val _inputFilePath = "input/2020-12-12.txt"

    private var _shipX = 0
    private var _shipY = 0

    private var _waypointX = 10
    private var _waypointY = 1

    fun puzzle1() : Int {

        var input = File(_inputFilePath).readLines()
        for (instruction in input) {
            processInstruction(instruction)
        }

        return abs(_shipX) + abs(_shipY)
    }

    private fun processInstruction(instruction: String) {
        var action = instruction.first()
        var value = instruction.substring(1).toInt()

        when (action) {
            'N', 'E', 'S', 'W' -> {
                moveWaypoint(action, value)
            }
            'F' -> moveShipTowardWaypoint(value)
            'L', 'R' -> {
                rotateWaypoint(action, value)
            }
        }
    }

    private fun moveWaypoint(direction: Char, distance: Int) {
        when (direction) {
            'N' -> _waypointY += distance
            'E' -> _waypointX += distance
            'S' -> _waypointY  -= distance
            'W' -> _waypointX -= distance
        }
    }

    private fun moveShipTowardWaypoint(waypointIncrements: Int) {
        _shipX += _waypointX * waypointIncrements
        _shipY += _waypointY * waypointIncrements
    }

    private fun rotateWaypoint(turnDirection: Char, degrees: Int) {

        var timesToRotateClockwise = degrees / 90
        if (turnDirection == 'L') {
            timesToRotateClockwise = 4 - timesToRotateClockwise
        }

        for (i in 0 until timesToRotateClockwise) {
            rotateWaypointClockwise()
        }
    }

    private fun rotateWaypointClockwise() {
        var newWaypointX = _waypointY
        _waypointY = _waypointX * -1
        _waypointX = newWaypointX
    }
}
