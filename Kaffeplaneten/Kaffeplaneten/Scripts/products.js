function getProductsInCat(cat) {
    $.ajax({
        url: "/Product/ProductsInCategory", data: {
            category: cat
        }, success: function (retur) {
            $("#productCanvas").empty();
            $("#productCanvas").html(retur);
        }
    });
}



function putInCart(id) {
    $.ajax({
        url: "/ShoppingCart/addToCart",
        type: 'POST',
        data: {newProd: id
        }, success: function (retur) {
            //window.alert(retur); display message
            

        }
    });
}
