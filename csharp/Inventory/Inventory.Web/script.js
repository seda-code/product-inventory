const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeWindowButton = document.getElementById("close");
const saveProductButton = document.getElementById("save");

addProductButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";

    clearInfo();
    loadCategories();
});

closeWindowButton.addEventListener("click", ()=>{
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "none";
});

saveProductButton.addEventListener("click", ()=>{
    saveProduct();
    clearInfo();
    closeWindowButton.click();
});

getProducts();


async function getCategories() {
    const response = await fetch("https://localhost:5001/api/category/GetCategories");
    const categories = await response.json();

    return categories;
}

async function getProducts(){
    fetch("https://localhost:5001/api/product/GetProducts")
        .then(response => response.json())
        .then(data => loadProducts(data));
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

async function deleteData(data){
    const response = await fetch("https://localhost:5001/api/product/" + data, {
        method: "DELETE"
    });
}

async function loadCategories(){
    const categories = await getCategories();
    const categoryList = document.getElementById("category-list");
    categoryList.innerHTML = "";

    for(let x=0; x<categories.length; x++){
        const optionElement = document.createElement("option");
        optionElement.setAttribute("value", categories[x].id);
        
        const optionText = document.createTextNode(categories[x].name);
        optionElement.appendChild(optionText);

        categoryList.appendChild(optionElement);
    }
}

function loadProducts(products){
    for (let i = 0; i < products.length; i++) {
        addProductRow(products[i]);
    }
}

function addProductRow(item){
    var itemElement = document.createElement("tr");

    itemElement.innerHTML=`
        <td class='item-id' title='${item.id}'>${item.id}</td>
        <td>${item.name}</td>
        <td>${item.units}</td>
        <td>${item.category.name}</td>
        <td>${item.value}</td>
        <td>
            <button class="delete-product" id="delete-product" title="Delete product">
                <i class="fas fa-trash"></i>
            </button>
        </td>`;

        const deleteButton = itemElement.querySelector(".delete-product");

        deleteButton.addEventListener("click", (x)=>{
            const row = x.target.closest("tr");
            deleteProduct(row);
        });


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
    addProductItem(product); // Wrong place! Wait for the result from postData in order to get the ID
}

function deleteProduct(productRow){
    console.log("Deleting: ", productRow.children[0].textContent);
    
    deleteData(productRow.children[0].textContent);

    productRow.parentNode.removeChild(productRow); // Check if there is an error before delete the element
}

function clearInfo(){
    document.getElementById("product-name").value = "";
    document.getElementById("product-units").value = "";
    document.getElementById("product-value").value = "";
}