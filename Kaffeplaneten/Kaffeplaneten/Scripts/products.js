

function getAllProductsInitial() {
    $.getJSON("/Product/GetAllProducts", function (data) {
        $('#productCanvas').empty();
        for (var i in data) {
            var template = $('#productTemplate').html();
            var html = Mustache.to_html(template, data[i]);
            $('#productCanvas').append(html);
            


        }
    });
}

function testfunc(id) {
    window.alert("motatt id før " + id);
    $.getJSON("/Product/GetProductById", { id},
        function (data) {
            
                var template = $('#testTemplate').html();
                var html = Mustache.to_html(template, data);
                $('#testCanvas').append(html);
            
        });
   
}





//TODO - Christer: Finne en bedre måte å gjøre dette på..
//sjekk .txt på desk
function hideUnwanted(id) {
    switch (id) {

        case ("Arabica"): $('.cat-' + 'Robusta').hide();
            $('.cat-' + 'Arabica').show();
            break;
        case ("Robusta"): $('.cat-' + 'Arabica').hide();
            $('.cat-' + 'Robusta').show();
            break;
    }
}

