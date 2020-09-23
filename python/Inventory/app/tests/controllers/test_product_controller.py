import json

def test_get_all(app, client):
    url = "/products/"
    
    response = client.get(url)

    assert response.status_code == 200
    assert response.data == b"Get all products"

def test_get_one_product(app, client):
    productId = "1"
    url = f"/products/{productId}"
    
    response = client.get(url)

    assert response.status_code == 200
    assert response.data == b"Get product 1"

def test_post_product(app, client):
    product = {"name":"product A"}
    url = "/products/"
    
    response = client.post(url, 
    data=json.dumps(dict(product)), 
    content_type="application/json")

    assert response.status_code == 200
    # response.data ==> "Add product {'name': 'product A'}"
    assert b"Add product" in response.data
    assert b"product A" in response.data

def test_put_product(app, client):
    product = {"name":"product A"}
    productId = "1"
    url = f"/products/{productId}"
    
    response = client.put(url, 
    data=json.dumps(dict(product)), 
    content_type="application/json")

    assert response.status_code == 200
    assert b"Update product" in response.data
    assert f"{productId}".encode() in response.data
    assert b"product A" in response.data

def test_delete_product(app, client):
    productId = "1"
    url = f"/products/{productId}"

    response = client.delete(url)

    assert response.status_code == 200
    assert b"Delete product" in response.data
    assert f"{productId}".encode() in response.data