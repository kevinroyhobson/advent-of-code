import java.io.File

class Day8 {

    fun puzzle1() : Int {
        var instructionSet = InstructionSet()
        instructionSet.executeUntilRepeatedInstruction()
        return instructionSet.accumulator
    }

    fun puzzle2() : Int {
        for (i in 0 until 608) {
            var instructionSet = InstructionSet()
            instructionSet.alterInstruction(i)

            if (instructionSet.doesInstructionSetTerminate()) {
                return instructionSet.accumulator
            }
        }

        throw Exception("No alteration fixes the instruction set.")
    }
}

class InstructionSet() {

    private val _inputFilePath = "input/2020-12-08.txt"
    private var _instructions = File(_inputFilePath).readLines().map { Instruction(it) }

    var accumulator = 0
    private var _currentInstructionIndex = 0

    fun alterInstruction(instructionIndex: Int) {
        _instructions[instructionIndex].alterInstruction()
    }

    fun executeUntilRepeatedInstruction() {
        var executedInstructions = hashSetOf<Int>()

        while (!executedInstructions.contains(_currentInstructionIndex)) {
            executedInstructions.add(_currentInstructionIndex)
            executeNextInstruction()
        }
    }

    fun doesInstructionSetTerminate() : Boolean {
        var executedInstructions = hashSetOf<Int>()

        while (_currentInstructionIndex != _instructions.count()) {

            if (executedInstructions.contains(_currentInstructionIndex)) {
                return false
            }

            executedInstructions.add(_currentInstructionIndex)
            executeNextInstruction()
        }

        return true
    }

    private fun executeNextInstruction() {

        val currentInstruction = _instructions[_currentInstructionIndex]

        when (currentInstruction.operation) {
            "acc" -> {
                accumulator += currentInstruction.argument
                _currentInstructionIndex++
            }
            "jmp" -> {
                _currentInstructionIndex += currentInstruction.argument
            }
            "nop" -> {
                _currentInstructionIndex++
            }
        }
    }
}

class Instruction(rawInstruction: String) {
    var operation = rawInstruction.split(" ")[0]
    val argument = rawInstruction.split(" ")[1].toInt()

    fun alterInstruction() {
        if (operation == "jmp") {
            operation = "nop"
        } else if (operation == "nop") {
            operation = "jmp"
        }
    }
}
