import java.io.File

class Day24 {

    private val _inputFilePath = "input/2020-12-24.txt"

    fun puzzle1() : Int {
        return getBlackTilesAfterInputIsFlipped().count()
    }

    fun puzzle2() : Int {

        var currentBlackTileSet = getBlackTilesAfterInputIsFlipped()

        for (i in 1..100) {

            var nextBlackTileSet = hashSetOf<Coordinate>()

            for (blackTile in currentBlackTileSet) {

                var numAdjacentBlackTiles = getNumBlackTilesAdjacentToCoordinate(blackTile, currentBlackTileSet)
                if (numAdjacentBlackTiles in 1..2) {
                    nextBlackTileSet.add(blackTile)
                }
            }

            var whiteTilesToProcess = currentBlackTileSet.flatMap { getCoordinatesAdjacentToCoordinate(it) }
                                                         .filter { !currentBlackTileSet.contains(it) }

            for (whiteTile in whiteTilesToProcess) {
                var numAdjacentBlackTiles = getNumBlackTilesAdjacentToCoordinate(whiteTile, currentBlackTileSet)
                if (numAdjacentBlackTiles == 2) {
                    nextBlackTileSet.add(whiteTile)
                }
            }

            println("Day $i: ${nextBlackTileSet.count()}")
            currentBlackTileSet = nextBlackTileSet
        }

        return currentBlackTileSet.count()
    }

    private fun getBlackTilesAfterInputIsFlipped() : HashSet<Coordinate> {

        var numTimesFlippedByTileCoordinate = hashMapOf<Coordinate, Int>()

        var tileLines = File(_inputFilePath).readLines()

        for (line in tileLines) {

            var directions = parseDirectionsFromDirectionString(line)
            var targetCoordinate = Coordinate(0.0, 0.0)

            for (direction in directions) {

                var thisCoordinateShift = _coordinateShiftByDirection[direction]!!
                targetCoordinate.x += thisCoordinateShift.x
                targetCoordinate.y += thisCoordinateShift.y

            }

            var numTimesThisTileFlipped = numTimesFlippedByTileCoordinate.getOrDefault(targetCoordinate, 0)
            numTimesThisTileFlipped++
            numTimesFlippedByTileCoordinate[targetCoordinate] = numTimesThisTileFlipped
        }

        return numTimesFlippedByTileCoordinate.filter { it.value % 2 == 1 }
                                              .keys
                                              .toHashSet()
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

    private fun getCoordinatesAdjacentToCoordinate(coordinate: Coordinate) : List<Coordinate> {
        return _coordinateShiftByDirection.values
                                          .map { Coordinate(coordinate.x + it.x, coordinate.y + it.y) }
    }

    private fun getNumBlackTilesAdjacentToCoordinate(coordinate: Coordinate, blackTileSet: HashSet<Coordinate>) : Int {
        var adjacentCoordinates = getCoordinatesAdjacentToCoordinate(coordinate)
        return adjacentCoordinates.count { blackTileSet.contains(it) }
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
