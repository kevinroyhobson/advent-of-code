import java.io.File
import java.util.*

class Day7 {

    private val _inputFilePath = "input/2020-12-07.txt"

    fun puzzle1() : Int {

        val tracker = BagRuleTracker()
        File(_inputFilePath).readLines().forEach { tracker.processRule(it) }

        var ancestorBags = tracker.getAncestorBagsOf("shiny gold")
        return ancestorBags.count()
    }
}

class BagRuleTracker {

    private val _parentBagsByChildBag = hashMapOf<String, MutableList<String>>()

    fun processRule(bagRuleString: String) {

        val rule = BagRule(bagRuleString)
        for (child in rule.childBags) {
            if (!_parentBagsByChildBag.containsKey(child)) {
                _parentBagsByChildBag[child] = mutableListOf<String>()
            }
            _parentBagsByChildBag[child]?.add(rule.parentBag)
        }
    }

    fun getAncestorBagsOf(bag: String) : List<String> {

        val ancestorBags = hashSetOf<String>()
        val bagsToVisit: Queue<String> = LinkedList<String>()
        bagsToVisit.add(bag)

        while (!bagsToVisit.isEmpty()) {

            val thisBag = bagsToVisit.remove()
            if (_parentBagsByChildBag.containsKey(thisBag)) {

                val parentsOfThisBag = _parentBagsByChildBag[thisBag]
                for (parent in parentsOfThisBag!!) {

                    if (!ancestorBags.contains(parent)) {
                        ancestorBags.add(parent)
                        bagsToVisit.add(parent)
                    }
                }
            }
        }

        return ancestorBags.toList()
    }
}

class BagRule private constructor() {

    var parentBag = ""
    var childBags = listOf("")

    constructor(bagRule: String) : this() {
        val ruleHalves = bagRule.split(" bags contain ")

        parentBag = ruleHalves[0]
        childBags = ruleHalves[1].split(",")
                                     .map { it.substring(2)
                                              .replace("bags", "")
                                              .replace("bag", "")
                                              .replace(".", "")
                                              .trim() }
    }
}
