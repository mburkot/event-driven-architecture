from pymongo import MongoClient
import os

class OrderRepository:
    

    def __init__(self):
        self.client = MongoClient(os.environ["MONGO_DB_CONN_STRING"])
        self.orders_db = self.client.orders_db
        self.orders_collection = self.orders_db.orders

    def get_order_by_order_number(self, order_number):
        order = self.orders_collection.find_one({"OrderNumber": order_number})
        if not order:
            return {}
        order.pop("_id")
        return order