from flask import Flask
from app.src.controllers.product_controller import ProductController

app = Flask(__name__)

product_view = ProductController.as_view("product")
app.add_url_rule("/products/", defaults={"productId": None}, view_func=product_view, methods=["GET",])
app.add_url_rule("/products/", view_func=product_view, methods=["POST",])
app.add_url_rule("/products/<string:productId>", view_func=product_view, methods=["GET", "PUT", "DELETE"])

if __name__ == "__main__":
	app.run(debug=True)