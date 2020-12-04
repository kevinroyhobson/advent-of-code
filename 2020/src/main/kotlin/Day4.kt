import java.io.File

class Day4 {

    private val _inputFilePath = "input/2020-12-04_input-1.txt"

    fun puzzle1(): Int {

        val input = File(_inputFilePath).readLines()
        var numValidPassports = 0

        var currentPassport = Passport()

        for (line in input) {

            if (line == "") {
                if (currentPassport.isValidPassport()) {
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

    var birthYear: String? = null
    var issueYear: String? = null
    var expirationYear: String? = null
    var height: String? = null
    var hairColor: String? = null
    var eyeColor: String? = null
    var passportId: String? = null

    fun isValidPassport(): Boolean {
        return birthYear != null &&
                issueYear != null &&
                expirationYear != null &&
                height != null &&
                hairColor != null &&
                eyeColor != null &&
                passportId != null
    }

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
            "byr" -> birthYear = value
            "iyr" -> issueYear = value
            "eyr" -> expirationYear = value
            "hgt" -> height = value
            "hcl" -> hairColor = value
            "ecl" -> eyeColor = value
            "pid" -> passportId = value
        }
    }
}



