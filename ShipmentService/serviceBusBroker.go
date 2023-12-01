package main

import (
	"context"
	"encoding/json"
	"os"

	"github.com/Azure/azure-sdk-for-go/sdk/messaging/azservicebus"
)

type MessageData struct {
	OrderNumber string
}

type Message struct {
	Specversion     string
	Type            string
	Source          string
	Subject         string
	Id              string
	Time            string
	Datacontenttype string
	Data            MessageData
}

func GetClient() *azservicebus.Client {
	connString, ok := os.LookupEnv("AZURE_SERVICEBUS_CONNECTION_STRING")
	if !ok {
		panic("AZURE_SERVICEBUS_CONNECTION_STRING environment variable not found")
	}

	client, err := azservicebus.NewClientFromConnectionString(connString, nil)
	if err != nil {
		panic(err)
	}
	return client
}

func GetMessage(c chan Message) {
	client := GetClient()

	receiver, err := client.NewReceiverForSubscription("orders-events", "shipiment-service", nil)
	if err != nil {
		panic(err)
	}
	defer receiver.Close(context.TODO())

	for ok := true; ok; ok = true {

		messages, err := receiver.ReceiveMessages(context.TODO(), 10, nil)
		if err != nil {
			panic(err)
		}

		for _, message := range messages {
			body := message.Body

			var messageOb Message
			json.Unmarshal([]byte(body), &messageOb)

			c <- messageOb

			err = receiver.CompleteMessage(context.TODO(), message, nil)
			if err != nil {
				panic(err)
			}
		}
	}
}
