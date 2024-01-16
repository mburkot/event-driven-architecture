from repositories.order_repository import OrderRepository
from flask import Flask

order_repository = OrderRepository()
app = Flask(__name__)


@app.route("/orders/<order_number>", methods=['GET'])
def get_order_by_order_number(order_number):
    return order_repository.get_order_by_order_number(order_number)



if __name__ == "__main__":
    app.run(debug=True)