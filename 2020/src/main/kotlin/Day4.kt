import java.io.File

class Day4 {

    private val _inputFilePath = "input/2020-12-04_input-1.txt"

    fun thePuzzle(): Int {

        val input = File(_inputFilePath).readLines()
        var numValidPassports = 0

        var currentPassport = Passport()

        for (line in input) {

            if (line.isBlank()) {
                if (currentPassport.isValidPassportForPuzzle2()) {
                    numValidPassports++
                }

                // Clear the current passport to begin processing the next one.
                currentPassport = Passport()

            } else {
                currentPassport.processPropertiesInput(line)
            }
        }

        return numValidPassports
    }
}

class Passport {

    private var _birthYear: Int? = null
    private var _issueYear: Int? = null
    private var _expirationYear: Int? = null
    private var _height: Int? = null
    private var _heightUnits: String? = null
    private var _hairColor: String? = null
    private var _eyeColor: String? = null
    private var _passportId: String? = null

    fun processPropertiesInput(propertiesInput: String) {

        var properties = propertiesInput.split(" ")
        for (property in properties) {
            processProperty(property)
        }
    }

    private fun processProperty(property: String) {

        if (!property.contains(":")) {
            return
        }

        val key = property.split(":")[0]
        val value = property.split(":")[1]
        when (key) {
            "byr" -> _birthYear = value.toIntOrNull()
            "iyr" -> _issueYear = value.toIntOrNull()
            "eyr" -> _expirationYear = value.toIntOrNull()
            "hgt" -> {
                if (value.length in 4..5) {
                    _height = value.substring(0, value.length - 2).toIntOrNull()
                    _heightUnits = value.substring(value.length - 2)
                }
            }
            "hcl" -> _hairColor = value
            "ecl" -> _eyeColor = value
            "pid" -> _passportId = value
        }
    }

    fun isValidPassportForPuzzle1(): Boolean {
        return _birthYear != null &&
               _issueYear != null &&
               _expirationYear != null &&
               _height != null &&
               _hairColor != null &&
               _eyeColor != null &&
               _passportId != null
    }

    fun isValidPassportForPuzzle2() : Boolean {
        return isValidPassportForPuzzle1() &&
               isBirthYearValid() &&
               isIssueYearValid() &&
               isExpirationYearValid() &&
               isHeightValid() &&
               isHairColorValid() &&
               isEyeColorValid() &&
               isPassportIdValid()
    }

    private fun isBirthYearValid() : Boolean {
        return _birthYear in 1920..2002
    }

    private fun isIssueYearValid() : Boolean {
        return _issueYear in 2010..2020
    }

    private fun isExpirationYearValid() : Boolean {
        return _expirationYear in 2020..2030
    }

    private fun isHeightValid() : Boolean {
        return (_heightUnits == "cm" && _height in 150..193) ||
               (_heightUnits == "in" && _height in 59..76)
    }

    private fun isHairColorValid() : Boolean {
        return _hairColorRegex matches _hairColor!!
    }
    private val _hairColorRegex = Regex("#[0-9a-f]{6}")

    private fun isEyeColorValid() : Boolean {
        return _validEyeColors.contains(_eyeColor)
    }
    private val _validEyeColors = hashSetOf("amb", "blu", "brn", "gry", "grn", "hzl", "oth")

    private fun isPassportIdValid() : Boolean {
        return _passportIdRegex matches _passportId!!
    }
    private val _passportIdRegex = Regex("[0-9]{9}")
}



