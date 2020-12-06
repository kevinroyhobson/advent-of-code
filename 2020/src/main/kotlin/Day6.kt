import java.io.File

class Day6 {

    private val _inputFilePath = "input/2020-12-06.txt"

    fun puzzle1(): Int {

        val input = File(_inputFilePath).readLines()
        var yesAnswerSum = 0

        var currentAnswerGroup = AnswerGroup()

        for (personAnswers in input) {
            currentAnswerGroup.processPersonAnswers(personAnswers)

            if (personAnswers.isBlank()){
                yesAnswerSum += currentAnswerGroup.howManyQuestionsAreAnsweredYesByAnyone()
                currentAnswerGroup = AnswerGroup()
            }
        }

        return yesAnswerSum
    }
}

class AnswerGroup() {

    private val _questionsAnsweredYesByAnyone = HashSet<Char>()

    fun processPersonAnswers(personAnswers: String) {
        personAnswers.forEach { _questionsAnsweredYesByAnyone.add(it) }
    }

    fun howManyQuestionsAreAnsweredYesByAnyone() : Int {
        return _questionsAnsweredYesByAnyone.count()
    }
}
