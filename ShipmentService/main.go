package main

import (
	"fmt"
)

func main() {

	fmt.Println("\nget messages:")
	c := make(chan Message)
	go GetMessage(c)

	for u := range c {
		go func(message Message) {
			GetSalesOrderByNumber(message.Data.OrderNumber)
		}(u)
	}
}
