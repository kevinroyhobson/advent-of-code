import java.io.File

class Day6 {

    private val _inputFilePath = "input/2020-12-06.txt"

    fun thePuzzle(): Int {

        val input = File(_inputFilePath).readLines()
        var yesAnswerSum = 0

        var currentAnswerGroup = AnswerGroup()

        for (personAnswers in input) {
            currentAnswerGroup.processPersonAnswers(personAnswers)

            if (personAnswers.isBlank()){
                yesAnswerSum += currentAnswerGroup.howManyQuestionsAreAnsweredYesByEveryone()
                currentAnswerGroup = AnswerGroup()
            }
        }

        return yesAnswerSum
    }
}

class AnswerGroup() {

    private val _questionsAnsweredYesByAnyone = hashSetOf<Char>()
    private var _questionsAnsweredYesByEveryone = listOf('a'..'z').flatten()

    fun processPersonAnswers(personAnswers: String) {

        if (personAnswers.isBlank()) {
            return
        }

        personAnswers.forEach { _questionsAnsweredYesByAnyone.add(it) }

        var thisYesSet = personAnswers.toHashSet()
        _questionsAnsweredYesByEveryone = _questionsAnsweredYesByEveryone.filter { thisYesSet.contains(it) }
    }

    fun howManyQuestionsAreAnsweredYesByAnyone() : Int {
        return _questionsAnsweredYesByAnyone.count()
    }

    fun howManyQuestionsAreAnsweredYesByEveryone() : Int {
        return _questionsAnsweredYesByEveryone.count()
    }
}
