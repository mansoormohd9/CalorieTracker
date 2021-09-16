$(document).ready(function () {
    $("#nameAutoComplete").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "https://api.nutritionix.com/v1_1/search",
                type: "POST",
                dataType: "json",
                data: {
                    "appId": "f1413ae5",
                    "appKey": "45eefd244070bfd3054e6fa9ebaccd26",
                    "query": request.term,
                    "fields": ["item_name", "brand_name", "upc", "nf_calories"],
                },
                success: function (data) {
                    console.log(data);
                    response($.map(data.hits, function (item) {
                        return { label: item.fields.item_name, value: item.fields.item_name, id: item.fields.item_name, calorieValue: item.fields.nf_calories };
                    }));
                }
            });
        },
        minLength: 2,
        select: function (event, ui) {
            $('#calorieAutoComplete').val(ui.item.calorieValue);
            console.log("Selected: " + ui.item.value + " aka " + ui.item.id);
        }
    });
});