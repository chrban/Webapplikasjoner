﻿
function createCart() {
    $.ajax({
        url: "/ShoppingCart/createCart",
        datatype: "text",
        type: "POST",
        success: function (data) {

        },
        error: function () {

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

function forgot() {
    var inUser = $('#forgotPW').val();

    $.ajax({
        url: "/Security/ForgotPassword",
        type: 'POST',
        data: {
            email: inUser
        }, success: function (retur) {

            if (retur == 'NF') {
                $("#pWfeedback").empty();
                $("#pWfeedback").attr('class', 'alert alert-danger');
                $("#pWfeedback").html("Finner ikke bruker: " + inUser);
            }
            else {

                $("#forgotBody").empty();
                $("#forgotBody").attr('class', 'alert alert-success');
                $("#forgotBody").append("Midelrtidig passord sent til" + inUser + '<br/>');
                $("#forgotBody").append("(TestingPuposeOnly - Så slipper du å sjekke mailen: " + retur );
            }
        }
        , error: function (retur) {
            $("#pWfeedback").empty();
            $("#pWfeedback").attr('class', 'alert alert-danger');
            $("#pWfeedback").html("Finner ikke bruker: " + inUser);
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



    function sameAdress() {
        if ($('#checkbox').is(":checked")) {
            $('#adress').text(function () {
                $('#payAdress').val($(this).val());
            });
            $('#adress').keyup(function () {
                $('#payAdress').val($(this).val());
            });
            $('#adress').change(function () {
                $('#payAdress').val($(this).val());
            });

            $('#zipcode').text(function () {
                $('#payZipcode').val($(this).val());
            });
            $('#zipcode').keyup(function () {
                $('#payZipcode').val($(this).val());
            });
            $('#zipcode').change(function () {
                $('#payZipcode').val($(this).val());
            });

            $('#province').text(function () {
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


    
