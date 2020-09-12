const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeWindowButton = document.getElementById("close");
const saveProductButton = document.getElementById("save");

addProductButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";
});

closeWindowButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "none";
});

saveProductButton.addEventListener("click", ()=>{
    saveProduct();
});


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

async function postData(data){
    const response = await fetch("https://localhost:5001/api/product", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });
}

function addProductItem(item){
    var itemElement = document.createElement("tr");

    // itemElement.innerHTML=`<span class='hidden'>"${item.id}"</span><span>"${item.name}"`;
    itemElement.innerHTML=`
        <td class='item-id' title='${item.id}'>${item.id}</td>
        <td>${item.name}</td>
        <td>${item.units}</td>
        <td>${item.category.name}</td>
        <td>${item.value}</td>`

    productsContainer.appendChild(itemElement);
}

function saveProduct(){
    const name = document.getElementById("product-name").value;
    const units = document.getElementById("product-units").value;
    const category = document.getElementById("product-category").value;
    const value = document.getElementById("product-value").value;

    const product = {
        name: name,
        units: Number(units),
        category: category,
        value: Number(value)
    };

    console.log(product);
    postData(product);
}