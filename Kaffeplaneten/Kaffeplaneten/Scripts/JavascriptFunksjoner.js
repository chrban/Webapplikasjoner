
function createShoppingCart()
{
    $.ajax({
        url: "/ShoppingCart/createCart",
        datatype: "text",
        type: "POST",
        success: function (data) {
        },
        error: function () {
            alert("NOE SKJEDDE!");
        }
    });
}

function showLoginBox()
{
    if (document.getElementById('LogInBox'))
    {
        $('#LoggInBar').hide();
        $('#LoggOutBar').show();
    }
    
}
    
function showLogOutBox()
{
    if (document.getElementById('LogOut'))
    {
        $('#LoggOutBar').hide();
        $('#LoggInBar').show();
    }
}
