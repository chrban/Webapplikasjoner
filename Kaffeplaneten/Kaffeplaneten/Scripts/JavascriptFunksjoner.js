﻿
function createCart() {
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
    var antall = $('#quantity-'+id).val();
    if (antall == null)
        antall = 1;
    $.ajax({
        url: "/ShoppingCart/addToCart",
        type: 'POST',
        data: {
            newProd: id,
            inQuantity: antall
        }, success: function (retur) {
            console.log('Product: '  +id+  ' added to cart! '  +retur);

        }
    });
    $('#quantity').val(1);
}

$(document).ready(function(){
    $('[datatoggle="popover"]').popover();
});


    function sameAdress() {
        alert("TEST!!");
        if ($('#checkbox').is(":checked")) {
            $('#adress').html(function () {
                $('#payAdress').val($(this).val());
            });
            $('#adress').keyup(function () {
                $('#payAdress').val($(this).val());
            });
            $('#adress').change(function () {
                $('#payAdress').val($(this).val());
            });

            $('#zipcode').html(function () {
                $('#payZipcode').val($(this).val());
            });
            $('#zipcode').keyup(function () {
                $('#payZipcode').val($(this).val());
            });
            $('#zipcode').change(function () {
                $('#payZipcode').val($(this).val());
            });

            $('#province').html(function () {
                $('#payProvince').val($(this).val());
            });
            $('#province').keyup(function () {
                $('#payProvince').val($(this).val());
            });
            $('#province').change(function () {
                $('#payProvince').val($(this).val());
            });

        }
        else {
            $('#payAdress').attr("disabled", false);
            $('#payAdress').val("");
            $('#payProvince').attr("disabled", false);
            $('#payProvince').val("");
            $('#payZipcode').attr("disabled", false);
            $('#payZipcode').val("");
        }

    }

