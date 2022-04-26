// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    console.log("ready!");
});

function SubmitWithPopup() {
    console.log("inside SubmitWithPopup");

    var carnumber = document.getElementById("CarNumber").value;
    console.log(carnumber);

    jQuery.ajax({
        type: "POST",
        url: '/Home/CarFinder',
        dataType: 'JSON',
        data: { CarNumber: carnumber},
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            console.log("error!")
            console.log(data);
        }
    })

    //display popup
    $(".search-submit").click(function () {
        console.log("inside click")
        $('.bg-black-hidd').css("display", 'block');
        $('.bg-popup').css("display", 'block');
    });

    //close popup
    $(".back-btn").click(function () {
        $('.bg-black-hidd').css("display", 'none');
        $('.bg-popup').css("display", 'none');
    });
}
