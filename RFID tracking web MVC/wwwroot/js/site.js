// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    setInterval(function () { $('#tabelsIn').load('/Panel/HomeIn'); }, 500);
    setInterval(function () { $('#tabelsOut').load('/Panel/HomeOut'); }, 500);
});
