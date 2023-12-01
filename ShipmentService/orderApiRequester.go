package main

import (
	"fmt"
	"io/ioutil"
	"log"
	"net/http"
	"os"
)

func GetSalesOrderByNumber(salesOrderNumber string) {
	url, ok := os.LookupEnv("ORDER_API_URL")
	if !ok {
		panic("ORDER_API_URL environment variable not found")
	}

	response, err := http.Get(url + "/orders/" + salesOrderNumber)

	if err != nil {
		fmt.Print(err.Error())
		os.Exit(1)
	}

	responseData, err := ioutil.ReadAll(response.Body)
	if err != nil {
		log.Fatal(err)
	}

	fmt.Println(string(responseData))
}
