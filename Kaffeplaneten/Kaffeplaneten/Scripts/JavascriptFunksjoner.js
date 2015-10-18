function createCart(){
    $.ajax({
        url: "/ShoppingCart/createCart",
        datatype: "text",
        type: "POST",
        success: function (data) {
        },
        error: function () {
            alert("ERROR: Shopping Cart was not created...");
        }
    });
}