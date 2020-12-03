import java.io.File

class Day3 {

    private val _inputFilePath = "input/2020-12-03_input-1.txt"

    fun puzzle1() : Int {

        val input = File(_inputFilePath).readLines()
        val isTreeByLocation = getBoardFromInput(input)

        val mountainWidth = isTreeByLocation[0].count()
        val mountainLength = isTreeByLocation.count()

        var numTreesEncountered = 0
        var x = 0
        for (y in 0 until mountainLength) {

            if (isTreeByLocation[y][x]) {
                numTreesEncountered++
            }

            x += 3
            if (x >= mountainWidth) {
                x %= mountainWidth
            }
        }

        return numTreesEncountered
    }

    private fun getBoardFromInput(input: List<String>) : Array<Array<Boolean>> {
        val width = input.first().length
        val length = input.count()

        var isTreeByLocation = Array(length) { Array<Boolean>(width) { false } }
        for ((y, line) in input.withIndex()) {
            for ((x, ch) in line.withIndex()) {
                isTreeByLocation[y][x] = ch == '#'
            }
        }

        return isTreeByLocation
    }
}