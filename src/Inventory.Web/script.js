const productsContainer = document.getElementById("product-items");
const addProductButton = document.getElementById("add-product");
const closeWindowButton = document.getElementById("close");
const saveProductButton = document.getElementById("save");
const updateProductButton = document.getElementById("update");

// EVENTS 
addProductButton.addEventListener("click", () => {
    openProductWindow();
});

closeWindowButton.addEventListener("click", () => {
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "none";
});

saveProductButton.addEventListener("click", () => {
    saveProduct();

    closeWindowButton.click();
});

// REQUESTS

// Get Products 
async function getRequestTo(url) {
    const resp = await fetch(url);
    return resp;
}

// Delete Products
async function deleteRequestTo(url) {
    const resp = await fetch(url, {
        method: "DELETE"
    });

    return resp;
}

// Insert new product
async function postRequestTo(url, data) {
    const resp = await fetch(url, {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    return resp;
}

// Update product
async function putRequestTo(url, data) {
    const resp = await fetch(url, {
        method: "PUT",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(data)
    });

    return resp;
}

async function loadProducts() {
    const url = "https://localhost:5001/api/product/GetProducts"

    getRequestTo(url)
        .then(response => response.json())
        .then(data => addProdutsToList(data));
}

function addProdutsToList(products) {
    for (let i = 0; i < products.length; i++) {
        createProductRow(products[i]);
    }
}

function createProductRow(item) {
    var itemElement = document.createElement("tr");

    itemElement.innerHTML = `
        <td class='item-id' id='row-${item.id}' title='${item.id}'>${item.id}</td>
        <td>${item.name}</td>
        <td>${item.units}</td>
        <input type="hidden" value="${item.category.id}"/>
        <td>${item.category.name}</td>
        <td>${item.value}</td>
        <td>
            <button class="edit-product" id="edit-product" title="Edit product">
                <i class="fas fa-pencil-alt"></i>
            </button>
        </td>
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

    const editButton = itemElement.querySelector(".edit-product");
    editButton.addEventListener("click", (x) => {
        const row = x.target.closest("tr");
        editProduct(row);
    });

    productsContainer.appendChild(itemElement);
}

async function openProductWindow(product = {}) {
    const productWindow = document.getElementById("product-window");
    productWindow.style.display = "block";
    document.getElementById("product-name").focus();

    clearInfo();

    const url = "https://localhost:5001/api/category/GetCategories";

    getRequestTo(url)
        .then(response => response.json())
        .then(data => addCategoriesToList(data))
        .then(() => {
            if (Object.keys(product).length > 0) {
                document.getElementById("product-id").value = product["id"];
                document.getElementById("product-name").value = product["name"];
                document.getElementById("product-units").value = product["units"];
                document.getElementById("product-value").value = product["value"];
                const dd = document.getElementById("category-list").value = product["category"];
            }
        });
}

function clearInfo() {
    document.getElementById("product-id").value = "";
    document.getElementById("product-name").value = "";
    document.getElementById("product-units").value = "";
    document.getElementById("product-value").value = "";
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

function saveProduct() {
    const productId = document.getElementById("product-id").value;
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

    if (productId == "") {
        // Add new product
        const url = "https://localhost:5001/api/product";
        postRequestTo(url, product)
            .then(response => response.json())
            .then(data => createProductRow(data));
    } else {
        // Update product
        const url = "https://localhost:5001/api/product/" + productId;
        putRequestTo(url, product)
            .then(response => response.json())
            .then(data => updateProductRow(data));
    }
}

function updateProductRow(item) {
    const controlId = "row-" + item.id;
    const productRow = document.getElementById(controlId).closest("tr");
    productRow.children[1].textContent = item.name;
    productRow.children[2].textContent = item.units;
    productRow.children[4].textContent = item.category.name;
    productRow.children[5].textContent = item.value;
}

function editProduct(productRow) {
    const productId = productRow.children[0].textContent;
    const name = productRow.children[1].textContent;
    const units = productRow.children[2].textContent;
    const category = productRow.children[3].value;
    const value = productRow.children[5].textContent;

    productInfo = {
        "id": productId,
        "name": name,
        "units": units,
        "category": category,
        "value": value
    };

    openProductWindow(productInfo);
}

async function deleteProduct(productRow) {
    const productId = productRow.children[0].textContent;
    const url = "https://localhost:5001/api/product/" + productId;

    deleteRequestTo(url).then(response => {
        if (response.ok) {
            removeProductFromList(productRow);
        }
    });
}

function removeProductFromList(product) {
    product.parentNode.removeChild(product);
}

// Check for the user name in the url params
if(window.location.search){
    let params = new URLSearchParams(window.location.search);

    if(params.has("user")){
        const signinLink = document.getElementById("sign-in-link");
        signinLink.style.display = "none";

        const userNameLabel = document.getElementById("user-info-label");
        userNameLabel.innerHTML = `Hello ${params.get("user")}`;
        userNameLabel.style.display = "block";

    }
}


loadProducts();
