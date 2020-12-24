import java.io.File

class Day24 {

    private val _inputFilePath = "input/2020-12-24.txt"

    private val _numTimesFlippedByTileCoordinate = hashMapOf<Coordinate, Int>()

    fun puzzle1() : Int {

        var tileLines = File(_inputFilePath).readLines()

        for (line in tileLines) {

            var directions = parseDirectionsFromDirectionString(line)
            var targetCoordinate = Coordinate(0.0, 0.0)

            for (direction in directions) {

                var thisCoordinateShift = _coordinateShiftByDirection[direction]!!
                targetCoordinate.x += thisCoordinateShift.x
                targetCoordinate.y += thisCoordinateShift.y

            }

            var numTimesThisTileFlipped = _numTimesFlippedByTileCoordinate.getOrDefault(targetCoordinate, 0)
            numTimesThisTileFlipped++
            _numTimesFlippedByTileCoordinate[targetCoordinate] = numTimesThisTileFlipped
        }

        var numTilesFlippedToBlack = _numTimesFlippedByTileCoordinate.values.count { it % 2 == 1 }
        return numTilesFlippedToBlack
    }


    private fun parseDirectionsFromDirectionString(directionString: String) : List<Direction> {

        var directions = arrayListOf<Direction>()

        var i = 0
        while (i < directionString.length) {
            var thisAbbreviation = getNextDirectionAbbreviationFromString(directionString, i)
            directions.add(_directionByAbbreviation[thisAbbreviation]!!)
            i += thisAbbreviation.length
        }

        return directions
    }

    private fun getNextDirectionAbbreviationFromString(directionString: String, index: Int) : String {

        return when (directionString[index]) {
            'e', 'w' -> directionString[index].toString()
            else -> directionString.substring(index, index + 2)
        }
    }

    private val _directionByAbbreviation = hashMapOf("e" to Direction.East,
                                                     "se" to Direction.Southeast,
                                                     "sw" to Direction.Southwest,
                                                     "w" to Direction.West,
                                                     "nw" to Direction.Northwest,
                                                     "ne" to Direction.Northeast)

    private val _coordinateShiftByDirection = hashMapOf(Direction.East to Coordinate(1.0, 0.0),
                                                        Direction.Southeast to Coordinate(0.5, -1.0),
                                                        Direction.Southwest to Coordinate(-0.5, -1.0),
                                                        Direction.West to Coordinate(-1.0, 0.0),
                                                        Direction.Northwest to Coordinate(-0.5, 1.0),
                                                        Direction.Northeast to Coordinate(0.5, 1.0))
}

enum class Direction {
    East,
    Southeast,
    Southwest,
    West,
    Northwest,
    Northeast
}

data class Coordinate(var x: Double, var y: Double) { }
