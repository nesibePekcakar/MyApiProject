document.addEventListener('DOMContentLoaded', function () {
    // Add event listeners after the document has loaded
    document.getElementById('loginButton').addEventListener('click', login);
    document.getElementById('registerButton').addEventListener('click', register);
    // Add other event listeners as needed
});
const apiBaseUrl = 'https://localhost:7171/api';
let token = '';

function login() {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;

    fetch(`${apiBaseUrl}/auth/login`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email,
            password
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.token) {
                token = data.token;
                document.getElementById('auth').style.display = 'none';
                document.getElementById('product-management').style.display = 'block';
                document.getElementById('app').classList.add('loggedin');
            } else {
                alert('Login failed');
            }
        });
}

function register() {
    const email = document.getElementById('loginEmail').value;
    const password = document.getElementById('loginPassword').value;

    fetch(`${apiBaseUrl}/auth/register`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            email,
            password
        })
    })
        .then(response => response.json())
        .then(data => {
            if (data.token) {
                token = data.token;
                document.getElementById('auth').style.display = 'none';
                document.getElementById('product-management').style.display = 'block';
                document.getElementById('app').classList.add('loggedin');
            } else {
                alert('Registration failed');
            }
        });
}

function showAction(action) {
    document.querySelectorAll('.action-section').forEach(section => section.classList.remove('active'));
    document.getElementById(`${action}-section`).classList.add('active');
    document.getElementById('backButton').style.display = 'block';
}

function backToActions() {
    document.querySelectorAll('.action-section').forEach(section => section.classList.remove('active'));
    document.getElementById('backButton').style.display = 'none';
}


function displayProductList(response) {
    const productListDiv = document.getElementById('productList');
    productListDiv.innerHTML = ''; // Clear previous content

    // Check if response is already parsed JSON or needs parsing
    let jsonResponse;
    if (typeof response === 'string') {
        try {
            jsonResponse = JSON.parse(response);
        } catch (error) {
            productListDiv.innerHTML = `<p>Error parsing JSON response: ${error.message}</p>`;
            return;
        }
    } else {
        jsonResponse = response;
    }

    if (jsonResponse.isSuccess) {
        let products = jsonResponse.data;

        // Ensure products is an array
        if (!Array.isArray(products)) {
            products = [products];
        }

        for (const product of products) {
            const productItem = document.createElement('div');
            productItem.classList.add('product-item');
            productItem.innerHTML = `
                <strong>ID:</strong> ${product.productId}<br>
                <strong>Name:</strong> ${product.productName}<br>
                <strong>Category ID:</strong> ${product.categoryId}<br>
                <strong>Units in Stock:</strong> ${product.unitsInStock}<br>
                <strong>Unit Price:</strong> ${product.unitPrice}<br>
            `;
            productListDiv.appendChild(productItem);
        }
    } else {
        productListDiv.append(response.message);
        productListDiv.innerHTML = `<p>Error fetching products: ${jsonResponse.message}</p>`;
    }
}

function getAllProducts() {
    fetch(`${apiBaseUrl}/products/GetAll`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(displayProductList)
        .catch(error => {
            const productListDiv = document.getElementById('productList');
            productListDiv.innerHTML = `<p>Error fetching products: ${error.message}</p>`;
            console.error('Error fetching products:', error);
        });
}

function getProductsByCategory() {
    const category = document.getElementById('category').value;
    fetch(`${apiBaseUrl}/products/GetAllByCategory/${category}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(displayProductList)
        .catch(error => {
            const productListDiv = document.getElementById('productList');
            productListDiv.innerHTML = `<p>Error fetching products: ${error.message}</p>`;
            console.error('Error fetching products:', error);
        });
}

function getProductsByUnitPrice() {
    
    const minPrice = parseFloat(document.getElementById('minPrice').value.trim());
    const maxPrice = parseFloat(document.getElementById('maxPrice').value.trim());

    if (isNaN(minPrice) || isNaN(maxPrice)) {
        alert('Please enter valid prices.');
        return;
    }


    fetch(`${apiBaseUrl}/products/GetByUnitPrice?min=${minPrice}&max=${maxPrice}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(displayProductList)
        .catch(error => {
            productListDiv.innerHTML = `<p>Error fetching products: ${response.message}</p>`
            productListDiv.innerHTML = `<p>Error fetching products: ${error.message}</p>`;
            console.error('Error fetching products:', error);
        });
}

function getProductById() {
    const id = document.getElementById('id').value;

    fetch(`${apiBaseUrl}/products/GetByID/${id}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {

        if (data.error) {
            throw new Error(data.message);
        } 
        displayProductList(data);
         })
        .catch(error => {
            productListDiv.innerHTML = `<p>Error fetching products: ${error.message}</p>`;
            console.error('Error fetching products:', error);
        });
}

function getProductByName() {
    const name = document.getElementById('name').value;

    fetch(`${apiBaseUrl}/products/GetByName/${name}`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(displayProductList)
        .catch(error => {
            productListDiv.innerHTML = `<p>Error fetching products by name: ${error.message}</p>`;
            console.error('Error fetching products by name:', error);
        });
}
function displayProductDetails(response) {
    const productListDiv = document.getElementById('productList');
    productListDiv.innerHTML = ''; // Clear previous content
    if (response.isSuccess) {
        let productDetails = response.data;

        // Ensure productDetails is an array
        if (!Array.isArray(productDetails)) {
            productDetails = [productDetails];
        }

        for (const detail of productDetails) {
            const detailItem = document.createElement('div');
            detailItem.classList.add('product-detail');
            detailItem.innerHTML = `
                <strong>Product ID:</strong> ${detail.productId}<br>
                <strong>Name:</strong> ${detail.productName}<br>
                <strong>Category Name:</strong> ${detail.categoryName}<br>         
                <strong>Units in Stock:</strong> ${detail.unitsInStock}<br>
            `;
            productListDiv.appendChild(detailItem);
        }

    } else {
        productListDiv.innerHTML = `<p>Error fetching product details: ${response.message}</p>`;
    }
}
function getProductDetails() {
    fetch(`${apiBaseUrl}/products/GetProductDetails`)
        .then(response => {
            if (!response.ok) {
                throw new Error(`HTTP error! Status: ${response.status}`);
            }
            return response.json();
        })
        .then(data => {
            displayProductDetails(data);
        })
        .catch(error => {
            productDetailsDiv.innerHTML = `<p>Error fetching product details: ${error.message}</p>`;
            console.error('Error fetching product details:', error);
        });
}

function addProduct() {
    const productId = parseInt(document.getElementById('productId').value.trim(), 10);
    const productName = document.getElementById('productName').value.trim();
    const categoryId = parseInt(document.getElementById('productCategory').value.trim(), 10);
    const unitPrice = parseFloat(document.getElementById('productPrice').value.trim());
    const unitsInStock = parseInt(document.getElementById('unitsInStock').value.trim(), 10);

    const productData = {
        productId: productId,
        categoryId: categoryId,
        productName: productName,
        unitPrice: unitPrice,
        unitsInStock: unitsInStock
    };

    fetch(`${apiBaseUrl}/products/Add`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(productData)
    })
        .then(response => {
            if (response.ok) {
                alert('Product added successfully');
                backToActions(); 
            } else {
                throw new Error('Failed to add product');
            }
        })
        .catch(error => {
            console.error('Error adding product:', error);
            alert('Failed to add product. Please try again.');
        });
}




function updateProduct() {
    const productId = parseInt(document.getElementById('updateProductId').value.trim(), 10);
    const productName = document.getElementById('updateProductName').value.trim();
    const categoryId = parseInt(document.getElementById('updateProductCategory').value.trim(), 10);
    const unitPrice = parseFloat(document.getElementById('updateUnitPrice').value.trim());
    const unitsInStock = parseInt(document.getElementById('updateUnitsInStock').value.trim(), 10);

    const updatedProductData = {
        productId: productId,
        categoryId: categoryId,
        productName: productName,
        unitPrice: unitPrice,
        unitsInStock: unitsInStock
    };

    fetch(`${apiBaseUrl}/products/Update`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(updatedProductData)
    })
        .then(response => {
            if (response.ok) {
                alert('Product updated successfully');
                backToActions();
            } else {
                alert('Failed to update product');
            }
        });
}

function deleteProduct() {
    const productId = document.getElementById('ProductId').value;

    fetch(`${apiBaseUrl}/products/Delete/${productId}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`
        }
    })
        .then(response => {
            if (response.ok) {
                alert('Product deleted successfully');
                backToActions();
            } else {
                alert('Failed to delete product');
            }
        });
}

function showFilter(filterType) {
    // Hide all filter sections
    document.getElementById('categoryFilter').style.display = 'none';
    document.getElementById('unitPriceFilter').style.display = 'none';
    document.getElementById('productIdInput').style.display = 'none';
    document.getElementById('productNameInput').style.display = 'none';

    // Show the selected filter section
    switch (filterType) {
        case 'category':
            document.getElementById('categoryFilter').style.display = 'block';
            break;
        case 'unitPrice':
            document.getElementById('unitPriceFilter').style.display = 'block';
            break;
        case 'productId':
            document.getElementById('productIdInput').style.display = 'block';
            break;
        case 'productName':
            document.getElementById('productNameInput').style.display = 'block';
            break;
    }
}


