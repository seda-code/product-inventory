const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeWindowButton = document.getElementById("close");
const saveProductButton = document.getElementById("save");

addProductButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";

    loadCategories();
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
    const response = await fetch("https://localhost:5001/api/category/GetCategories");
    const categories = await response.json();

    return categories;
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

    const result = await response.json();
    console.log("postData response: ", result);
}

async function loadCategories(){
    const categories = await getCategories();
    console.log(categories);
    const categoryList = document.getElementById("category-list");

    for(let x=0; x<categories.length; x++){
        const optionElement = document.createElement("option");
        optionElement.setAttribute("value", categories[x].id);
        
        const optionText = document.createTextNode(categories[x].name);
        optionElement.appendChild(optionText);

        categoryList.appendChild(optionElement);
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

function saveProduct(){
    const name = document.getElementById("product-name").value;
    const units = document.getElementById("product-units").value;
    const categoryList = document.getElementById("category-list");
    const categoryValue = categoryList.options[categoryList.selectedIndex].value;
    const categoryText = categoryList.options[categoryList.selectedIndex].text;
    const value = document.getElementById("product-value").value;

    const product = {
        name: name,
        units: Number(units),
        category: {
            id:categoryValue,
            name: categoryText
        },
        value: Number(value)
    };

    console.log("Saving..", product);
    postData(product);
}