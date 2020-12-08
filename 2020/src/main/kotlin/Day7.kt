import java.io.File
import java.util.*

class Day7 {

    private val _inputFilePath = "input/2020-12-07.txt"
    private val _tracker = BagRuleTracker()

    init {
        File(_inputFilePath).readLines().forEach { _tracker.processRule(it) }
    }

    fun puzzle1() : Int {

        var ancestorBags = _tracker.getAncestorBagsOf("shiny gold")
        return ancestorBags.count()
    }

    fun puzzle2() : Int {

        return _tracker.getNumberOfBagsInsideOfBag("shiny gold")
    }
}

class BagRuleTracker {

    private val _parentBagsByChildBag = hashMapOf<String, MutableList<String>>()
    private val _bagRulesByParentBag = hashMapOf<String, BagRule>()

    fun processRule(bagRuleString: String) {

        val rule = BagRule(bagRuleString)

        for (child in rule.childBags) {

            if (!_parentBagsByChildBag.containsKey(child.bagType)) {
                _parentBagsByChildBag[child.bagType] = mutableListOf<String>()
            }
            _parentBagsByChildBag[child.bagType]?.add(rule.parentBag)
        }

        _bagRulesByParentBag[rule.parentBag] = rule
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

    fun getNumberOfBagsInsideOfBag(bag: String) : Int {

        var numBags = 0

        var rule = _bagRulesByParentBag[bag]
        for (childBag in rule!!.childBags) {
            numBags += childBag.numberOfBags
            numBags += childBag.numberOfBags * getNumberOfBagsInsideOfBag(childBag.bagType)
        }

        return numBags
    }
}

class BagRule private constructor() {

    var parentBag = ""
    var childBags = listOf<BagQuantity>()

    constructor(bagRule: String) : this() {
        val ruleHalves = bagRule.split(" bags contain ")

        parentBag = ruleHalves[0]

        if (!ruleHalves[1].contains("no other bags")) {
            childBags = ruleHalves[1].split(",")
                                     .map { BagQuantity(it) }
        }
    }
}

class BagQuantity private constructor() {

    var bagType = ""
    var numberOfBags = 0

    constructor(bagClause: String) : this() {

        val tokens = bagClause.trim().split(" ")
        numberOfBags = tokens[0].toInt()
        bagType = "${tokens[1]} ${tokens[2]}"
    }
}
