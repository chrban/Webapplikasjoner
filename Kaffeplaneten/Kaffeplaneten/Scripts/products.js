

function getAllProductsInitial() {
    $.getJSON("/Product/GetAllProducts", function (data) {
        $('#productCanvas').empty();
        for (var i in data) {
            var template = $('#productTemplate').html();
            var html = Mustache.to_html(template, data[i]);
            $('#productCanvas').append(html);

            //TODO- Christer: fikse dette
            //var template2 = $('#categoryTemplate').html();
            //var html2 = Mustache.to_html(template2, data[i]);
            //$('#categoryCanvas').append(html2);
        }
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

