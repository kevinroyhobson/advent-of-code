import java.io.File

class Day8 {

    private val _inputFilePath = "input/2020-12-08.txt"
    private val _instructions = File(_inputFilePath).readLines()
                                                    .map { Instruction(it) }

    fun puzzle1() : Int {

        var accumulator = 0
        var currentInstructionIndex = 0
        var executedInstructions = hashSetOf<Int>()

        while (!executedInstructions.contains(currentInstructionIndex)) {

            val currentInstruction = _instructions[currentInstructionIndex]
            executedInstructions.add(currentInstructionIndex)

            when (currentInstruction.operation) {
                "acc" -> {
                    accumulator += currentInstruction.argument
                    currentInstructionIndex++
                }
                "jmp" -> {
                    currentInstructionIndex += currentInstruction.argument
                }
                "nop" -> {
                    currentInstructionIndex++
                }
            }
        }

        return accumulator
    }
}

class Instruction(rawInstruction: String) {
    val operation = rawInstruction.split(" ")[0]
    val argument = rawInstruction.split(" ")[1].toInt()
}
