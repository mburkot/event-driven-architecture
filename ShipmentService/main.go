package main

import (
	"fmt"
)

func main() {

	fmt.Println("\nget messages:")
	c := make(chan string)
	go GetMessage(c)

	for u := range c {
		go func(body string) { // Function literal
			fmt.Printf("%s\n", string(body))
		}(u)
	}
}
