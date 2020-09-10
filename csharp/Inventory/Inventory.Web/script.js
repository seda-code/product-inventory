const productsContainer = document.getElementById("product-items");

getProducts();


async function getCategories() {
    const response = await fetch("https://localhost:5001/api/category");
    const categories = await response.json();

    console.log(categories[0]);
}

async function getProducts() {
    const response = await fetch("https://localhost:5001/api/product/GetProducts");
    const products = await response.json();

    for (let i = 0; i < products.length; i++) {
        addProductItem(products[i]);
    }
}

function addProductItem(item){
    var itemElement = document.createElement("li");

    itemElement.innerHTML=`<span>"${item.id}"</span> | <span>"${item.name}"`;

    productsContainer.appendChild(itemElement);
}