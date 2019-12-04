package main

import (
	"fmt"
	"strconv"
)

func main() {

	var numValidPasswords = 0
	for password := 372304; password <= 847060; password++ {
		if isValidPassword(password) {

			numValidPasswords++
		}
	}

	fmt.Println(numValidPasswords)
}

func isValidPassword(password int) bool {
	var passwordAsString = strconv.Itoa(password)

	return doesPasswordContainAdjacentDoubles(passwordAsString) &&
		   areNumbersMonotonicallyIncreasing(passwordAsString)
}

func doesPasswordContainAdjacentDoubles(password string) bool {
	var previousChar = '0'
	var currentNumberInARow = 1

	for _, c := range password {
		if c == previousChar {
			currentNumberInARow++
		} else {
			if currentNumberInARow == 2 {
				return true // This means that a continguous span of length two has just been ended.
			}
			currentNumberInARow = 1
		}

		previousChar = c
	}

	return currentNumberInARow == 2 // This means that the last two digits are a valid span of two
}

func doesPasswordContainAdjacentTriples(password string) bool {
	for i := 0; i <= len(password) - 3; i++ {
		var thisSubtring = password[i:i+3]
		if areAllCharactersEquivalent(thisSubtring) {
			return true
		}
	}

	return false
}

func areAllCharactersEquivalent(s string) bool {
	var theChar = rune(s[0])
	for _, c := range s {
		if c != theChar {
			return false
		}
	}

	return true
}

func areNumbersMonotonicallyIncreasing(password string) bool {
	var previousNum = 0
	for _, c := range password {
		var thisNum, _ = strconv.Atoi(string(c))
		if thisNum < previousNum {
			return false
		}
		previousNum = thisNum
	}

	return true
}
