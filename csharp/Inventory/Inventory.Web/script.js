const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeProductWindow = document.getElementById("close");
const saveProduct = document.getElementById("save");

addProductButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";
});

closeProductWindow.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "none";
})

saveProduct.addEventListener("click", ()=>{
    console.log("Save product");
})


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