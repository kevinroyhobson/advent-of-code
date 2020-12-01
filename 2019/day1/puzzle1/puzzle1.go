package main

import (
	"bufio"
	"fmt"
	"io"
	"log"
	"os"
	"strconv"
)

func main() {

	var file, fileOpenError = os.Open("c:/Users/khobson/go/src/advent/day1/puzzle1/input.txt")
	if fileOpenError != nil {
		log.Fatal(fileOpenError)
	}

	var componentMasses, fileReadError = ReadIntsFromFile(file)
	if fileReadError != nil {
		log.Fatal(fileReadError)
	}

	var totalRequiredFuel = 0
	for _, mass := range componentMasses {
		var thisFuel = ComputeFuelRequiredForMass(mass)
		totalRequiredFuel += thisFuel
	}

	fmt.Println(totalRequiredFuel)
}

func ReadIntsFromFile(reader io.Reader) ([]int, error) {
	var scanner = bufio.NewScanner(reader)
	scanner.Split(bufio.ScanWords)

	var result []int
	for scanner.Scan() {
		x, err := strconv.Atoi(scanner.Text())
		if err != nil {
			return result, err
		}

		result = append(result, x)
	}

	return result, scanner.Err()
}

func ComputeFuelRequiredForMass(mass int) int {
	if mass <= 0 {
		return 0
	}

	var fuel = (mass / 3) - 2
	fuel = Max(fuel, 0)

	var fuelRequiredForFuel = ComputeFuelRequiredForMass(fuel)

	return fuel + fuelRequiredForFuel
}

func Max(x int, y int) int {
	if x > y {
		return x
	}
	return y
}