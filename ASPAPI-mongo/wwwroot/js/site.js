let orderArray = [];
let customerArray = [];
let productArray = [];

let Order = function (pCustomerID, pProductID, pTransactionType) {

    this.customerID = parseInt(pCustomerID);
    this.productID = parseInt(pProductID);
    this.transactionType = pTransactionType;
}

let Customer = function (pCustomerName, pPhoneNumber) {
    this.customerName = pCustomerName;
    this.phoneNumber = parseInt(pPhoneNumber);
}

let Product = function (pProductName, pProductDesc, pProductRating) {
    this.productName = pProductName;
    this.productDesc = pProductDesc;
    this.productRating = parseInt(pProductRating);
}


document.addEventListener("DOMContentLoaded", function () {
    updateOrderDisplay();
    updateCustomerList();
    updateProductList();
});

function updateOrderDisplay() {
    $.get("api/Order", function (data, status) {  // AJAX get
        orderArray = data;  // put the returned server json data into our local array
        console.log(data);

        let table = document.getElementById('ordertable');
        // clear old values first
        while (table.rows.length > 0) {
            table.deleteRow(0);
        }
        // create table and headers


        let tr = document.createElement('tr');
        let td1 = document.createElement('td');
        td1.textContent = "Customer Name";
        tr.appendChild(td1);



        let td2 = document.createElement('td');
        td2.textContent = "Phone Number";
        tr.appendChild(td2);
        table.appendChild(tr);

        let td3 = document.createElement('td');
        td3.textContent = "Product";
        tr.appendChild(td3);
        table.appendChild(tr);

        let td4 = document.createElement('td');
        td4.textContent = "Product Description";
        tr.appendChild(td4);
        table.appendChild(tr);

        let td5 = document.createElement('td');
        td5.textContent = "Product Rating";
        tr.appendChild(td5);
        table.appendChild(tr);

        let td6 = document.createElement('td');
        td6.textContent = "Transaction Type";
        tr.appendChild(td6);
        table.appendChild(tr);

        // add rows
        for (let anOrder of orderArray) {
            let tr = document.createElement('tr');

            //let td1 = document.createElement('td');
            //td1.textContent = anOrder.orderID;
            //tr.appendChild(td1);

            let td1 = document.createElement('td');
            td1.textContent = anOrder.customerName;
            tr.appendChild(td1);

            let td2 = document.createElement('td');
            td2.textContent = anOrder.phoneNumber;
            tr.appendChild(td2);

            let td3 = document.createElement('td');
            td3.textContent = anOrder.productName;
            tr.appendChild(td3);

            let td4 = document.createElement('td');
            td4.textContent = anOrder.productDesc;
            tr.appendChild(td4);
            

            let td5 = document.createElement('td');
            td5.textContent = anOrder.productRating;
            tr.appendChild(td5);
            

            let td6 = document.createElement('td');
            td6.textContent = anOrder.transactionType;
            tr.appendChild(td6);

            table.appendChild(tr);
        }

    });
}

function updateCustomerList() {
    $.get("api/Customer", function (data, status) {  // AJAX get
        let customerSelect = document.getElementById('customerList');
        customerArray = data;  // put the returned server json data into our local array

        while (customerSelect.options.length > 0) {
            customerSelect.remove(0);
        }

        for (let item of customerArray) {
            let newOption = document.createElement('option');
            let optionText = document.createTextNode(item.customerName);
            newOption.appendChild(optionText);
            newOption.setAttribute('value', item.customerId);
            customerSelect.appendChild(newOption);
        }

    });
}



function updateProductList() {
    $.get("api/Product", function (data, status) {  // AJAX get
        let productSelect = document.getElementById('productList');
        productArray = data;  // put the returned server json data into our local array

        while (productSelect.options.length > 0) {
            productSelect.remove(0);
        }

        for (let item of productArray) {
            let newOption = document.createElement('option');
            let optionText = document.createTextNode(item.productName);
            newOption.appendChild(optionText);
            newOption.setAttribute('value', item.productId);
            productSelect.appendChild(newOption);
        }

    });
}


function newProductButton() {
    let newProductName = document.getElementById("newProduct").value
    let newProductDesc = document.getElementById("newProductDesc").value
    let newProductRating = document.getElementById("newProductRating").value
    let newProduct = new Product(newProductName, newProductDesc, newProductRating);

    $.ajax({
        url: "api/Product",
        type: "POST",
        data: JSON.stringify(newProduct),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            document.getElementById("newProduct").value = "";
            document.getElementById("newProductDesc").value = "";
            document.getElementById("newProductRating").value = "";
            updateProductList();
        }
    });
}

function newCustomerButton() {
    let newCustomerName = document.getElementById("newCustomerName").value
    let newPhoneNumber = document.getElementById("newPhoneNumber").value
    let newCustomer = new Customer(newCustomerName, newPhoneNumber);
    

    $.ajax({
        url: "api/Customer",
        type: "POST",
        data: JSON.stringify(newCustomer),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            console.log(result);
            document.getElementById("newCustomerName").value = "";
            document.getElementById("newPhoneNumber").value = "";
            updateCustomerList();
        }
    });

}


function addOrderButton() {
    let customerSelect = document.getElementById('customerList');
    let productSelect = document.getElementById('productList');

    /*let newOrderID = document.getElementById("orderID").value;*/
    let newCustomerID = customerSelect.value;
    console.log(newCustomerID);
    let newProductID = productSelect.value;
    let newTransactionType = document.getElementById("transactionType").value;

    let newOrder = new Order(newCustomerID, newProductID, newTransactionType);
    console.log(newOrder);

    $.ajax({
        url: "api/Order",
        type: "POST",
        data: JSON.stringify(newOrder),
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            /*document.getElementById("orderID").value = "";*/
            document.getElementById("transactionType").value = "";
            console.log(result);
            updateOrderDisplay();
        }
    });

}

function showButton() {
    updateOrderDisplay();
}





let MongoOrder = function (pCustomerName, pPhoneNumber, pProductName, pProductDesc, pProductRating, pTransactionType) {
    this.customerName = pCustomerName;
    this.phoneNumber = parseInt(pPhoneNumber);
    this.productName = pProductName;
    this.productDesc = pProductDesc;
    this.productRating = parseInt(pProductRating);
    this.transactionType = pTransactionType
}





function backupButton() {    // get all orders in mongo
    $.get("api/MongoOrders", function (data, status) {
        let orderArray = data;
        for (let anOrder of orderArray) {  // delete one at a time
            $.ajax({
                url: "api/MongoOrders/" + anOrder.id,
                type: "DELETE",
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                }
            });
        };
    });

    $.get("api/Order", function (data, status) { // get all orders from SQL
        let mongoArray = data;
        for (let anOrder of mongoArray) {
            let nextMongoOrder = new MongoOrder(anOrder.customerName, anOrder.phoneNumber, anOrder.productName, anOrder.productDesc, anOrder.productRating, anOrder.transactionType);
            $.ajax({
                url: "api/MongoOrders",
                type: "POST",
                data: JSON.stringify(nextMongoOrder),
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    console.log(result);
                }
            });
        }
    });
}


function displayBackupButton() {
    $.get("api/MongoOrders", function (data, status) {
        let orderArray = data;
        let table = document.getElementById('ordertable');
        while (table.rows.length > 0) {      // clear old values first
            table.deleteRow(0);
        }
        let tr = document.createElement('tr');   // create table and headers
        let td1 = document.createElement('td');
        td1.textContent = "Backup Data";
        tr.appendChild(td1);
        table.appendChild(tr);
        for (let anOrder of orderArray) {      // add rows
            let tr = document.createElement('tr');
            let td1 = document.createElement('td');
            td1.textContent = anOrder.customerName;
            tr.appendChild(td1);
            let td2 = document.createElement('td');
            td2.textContent = anOrder.phoneNumber;
            tr.appendChild(td2);
            let td3 = document.createElement('td');
            td3.textContent = anOrder.productName;
            tr.appendChild(td3);
            let td4 = document.createElement('td');
            td4.textContent = anOrder.productDesc;
            tr.appendChild(td4);
            let td5 = document.createElement('td');
            td5.textContent = anOrder.productRating;
            tr.appendChild(td5);
            let td6 = document.createElement('td');
            td6.textContent = anOrder.transactionType;
            tr.appendChild(td6);

            table.appendChild(tr);
        }
    });
}


//button stuff
// In order to return an element with a class. Element is .btn
const btnEl = document.querySelector(".btn");

btnEl.addEventListener("mouseover", (event) => {
    //we save these numbers
    const x = (event.pageX - btnEl.offsetLeft);
    const y = (event.pageY - btnEl.offsetTop);

    btnEl.style.setProperty("--xPos", x + "px");
    btnEl.style.setProperty("--yPos", y + "px");
})
