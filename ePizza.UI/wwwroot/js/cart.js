

function AddToCart(ItemId, UnitPrice, Quantity) {

    $.ajax({
        type: "GET",
        url: "Cart/AddToCart/" + ItemId + "/" + UnitPrice + "/" + Quantity,
        success: function (response) {
            $("#cartCounter").text(response.count)
        },
        error: function (event) {
            alert("Error in adding item in cart.")
        }
    })
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "Cart/GetCartCount",
        success: function (response) {
            $("#cartCounter").text(response.count)
        },
        error: function (event) {
            alert("Error in fetching cart item.")
        }
    })
})