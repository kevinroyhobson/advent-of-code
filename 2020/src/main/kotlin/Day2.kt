import java.io.File

class Day2 {

    private val _inputFilePath = "input/2020-12-02_input-1.txt"

    fun puzzle1() : Int {

        var validPasswords = 0
        var passwordAndPolicyCombinations = File(_inputFilePath).readLines()

        for (passwordAndPolicy in passwordAndPolicyCombinations) {
            val policyString = passwordAndPolicy.split(':')[0]
            val policy = PasswordPolicy(policyString)
            val password = passwordAndPolicy.split(':')[1].trim()

            if (policy.isPasswordValidForPuzzle1(password)) {
                validPasswords++
            }
        }

        return validPasswords
    }

    fun puzzle2() : Int {

        var validPasswords = 0
        var passwordAndPolicyCombinations = File(_inputFilePath).readLines()

        for (passwordAndPolicy in passwordAndPolicyCombinations) {
            val policyString = passwordAndPolicy.split(':')[0]
            val policy = PasswordPolicy(policyString)
            val password = passwordAndPolicy.split(':')[1].trim()

            if (policy.isPasswordValidForPuzzle2(password)) {
                validPasswords++
            }
        }

        return validPasswords
    }
}

class PasswordPolicy(policyString: String) {
    private val _leftPolicyValue: Int = policyString.split(' ')[0].split('-')[0].toInt()
    private val _rightPolicyValue: Int = policyString.split(' ')[0].split('-')[1].toInt()
    private val _theCharacterInQuestion: Char = policyString.split(' ')[1][0]

    fun isPasswordValidForPuzzle1(password: String) : Boolean {

        var numOccurrences = 0
        for (character in password) {
            if (character == _theCharacterInQuestion) {
                numOccurrences++
            }
        }

        return numOccurrences in _leftPolicyValue.._rightPolicyValue
    }

    fun isPasswordValidForPuzzle2(password: String) : Boolean {

        return (password[_leftPolicyValue-1] == _theCharacterInQuestion) xor
               (password[_rightPolicyValue-1] == _theCharacterInQuestion)
    }
}
