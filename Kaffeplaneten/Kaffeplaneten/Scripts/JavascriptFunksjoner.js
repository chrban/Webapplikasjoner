function createCart(){
    $.ajax({
        url: "/ShoppingCart/createCart",
        datatype: "text",
        type: "POST",
        success: function (data) {
            alert("SHOPPING CART CREATED!");
        },
        error: function () {
            alert("NOE SKJEDDE!");
        }
    });
}