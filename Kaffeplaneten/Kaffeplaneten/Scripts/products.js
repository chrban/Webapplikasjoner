﻿function getProductsInCat(cat) {
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
    var antall = $('#quantity').val();
    if (antall == null)
        antall = 1;
    $.ajax({
        url: "/ShoppingCart/addToCart",
        type: 'POST',
        data: {
            newProd: id,
            inQuantity: antall
        }, success: function (retur) {
            console.log('Product: '+id+' added to cart! '+retur);
            
        }
    });
    $('#quantity').val(1);
}

