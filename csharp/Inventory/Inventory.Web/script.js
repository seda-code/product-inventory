const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeWindowButton = document.getElementById("close");
const saveProductButton = document.getElementById("save");

// EVENTS 
addProductButton.addEventListener("click", () => {
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";

    clearInfo();
    loadCategories();
});

closeWindowButton.addEventListener("click", () => {
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "none";
});

saveProductButton.addEventListener("click", () => {
    saveProduct();
    clearInfo();
    closeWindowButton.click();
});

// REQUESTS
async function getRequestTo(url, callback) {
    fetch(url)
        .then(response => response.json())
        .then(data => callback(data));
}

async function postRequestTo(url, data, callback) {
    fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    })
        .then(response => response.json())
        .then(data => callback(data));
}

async function deleteRequestTo(url, callback) {
    fetch(url, {
        method: "DELETE"
    })
        .then(response => callback());
}

loadProducts();


function loadCategories() {
    const url = "https://localhost:5001/api/category/GetCategories";
    getRequestTo(url, addCategoriesToList);
}

async function loadProducts() {
    const url = "https://localhost:5001/api/product/GetProducts";
    getRequestTo(url, addProdutsToList);
}

async function addCategoriesToList(categories) {
    const categoryList = document.getElementById("category-list");
    categoryList.innerHTML = "";

    for (let x = 0; x < categories.length; x++) {
        const optionElement = document.createElement("option");
        optionElement.setAttribute("value", categories[x].id);

        const optionText = document.createTextNode(categories[x].name);
        optionElement.appendChild(optionText);

        categoryList.appendChild(optionElement);
    }
}

function addProdutsToList(products) {
    for (let i = 0; i < products.length; i++) {
        createProductRow(products[i]);
    }
}

function createProductRow(item) {
    var itemElement = document.createElement("tr");

    itemElement.innerHTML = `
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

    deleteButton.addEventListener("click", (x) => {
        const row = x.target.closest("tr");
        deleteProduct(row);
    });

    productsContainer.appendChild(itemElement);
}

function saveProduct() {
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
            id: categoryValue,
            name: categoryText
        },
        value: Number(value)
    };

    // console.log("Saving..", product);

    const url = "https://localhost:5001/api/product";
    postRequestTo(url, product, createProductRow);
}

function deleteProduct(productRow) {
    // console.log("Deleting: ", productRow.children[0].textContent);

    const productId = productRow.children[0].textContent;
    const url = "https://localhost:5001/api/product/" + productId;

    deleteRequestTo(url, function () {
        removeProductFromList(productRow);
    });
}

function removeProductFromList(product) {
    product.parentNode.removeChild(product);
}

function clearInfo() {
    document.getElementById("product-name").value = "";
    document.getElementById("product-units").value = "";
    document.getElementById("product-value").value = "";
}