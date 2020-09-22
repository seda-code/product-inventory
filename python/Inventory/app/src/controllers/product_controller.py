from flask import request
from flask.views import MethodView

class ProductController(MethodView):
    # GET http://127.0.0.1:5000/products/[<productId>]
    def get(self, productId):
        if productId is None:
            return "Get all products"
        else:
            return f"Get product {productId}"

    # POST http://127.0.0.1:5000/products/
    def post(self):
        data = request.get_json()
        # data is a dictionary -> data["key"]
        return f"Add product {data}"

    # PUT http://127.0.0.1:5000/products/<productId>
    def put(self, productId):
        data = request.get_json()
        return f"Update product {productId} :: {data}"

    # DELETE http://127.0.0.1:5000/products/<productId>
    def delete(self, productId):
        return f"Delete product {productId}"