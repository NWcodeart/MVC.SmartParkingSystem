// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//const { valHooks } = require("jquery");

// Write your JavaScript code.

function SubmitWithPopup() {

    var carnumber = document.getElementById("CarNumber").value;

    //pass carNumber to ther controller and call carFinder partial view
    $.get('/Home/CarFinder', { CarNumber: carnumber }, function (content) {
        $("#partialContainer").html(content);
    });

    //display popup
    $('.bg-black-hidd').css("display", 'block');
    $('.bg-popup').css("display", 'block');
}
//close popup
$(".back-btn, .close-btn").click(function () {
    $('.bg-black-hidd').css("display", 'none');
    $('.bg-popup').css("display", 'none');
});

function test() {
    alert("loaded");
}
//disply car
function CarStatus(isVacant, CarId) {
    console.log("inside CarStatus");
    if (isVacant == 'True') {
        $(CarId).css("display", 'none');
    } else if (isVacant == 'False') {
        $(CarId).css("display", 'block');
    }
}
$(document).ready(function () {
    console.log("document is ready!");

    for (let j = 1; j < 10; j++) {
        if (j == 4) { continue; }
        let value = document.getElementById(j).value;

        if (value != null) {
            let ImgId = '#S' + j;
            console.log("ImgId = " + ImgId);
            console.log(value);
            CarStatus(value, ImgId);
        }
    }
})