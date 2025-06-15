// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $('#SearchButton').click(function () {
        var query = $('#inputQuery').val();
        var type = $('#contentType').val();
        var genre = $('#contentGenre').val();
  
        var year = $('#contentReleaseYear').val();


       

        $.get('/Content/FetchData', {
            inputData: query,
            contentType: type,
            contentGenre: genre,

            contentReleaseYear: year
        }, function (result) {
            $('#partialPlaceHolder').html(result).fadeIn('slow');
        });
    });
});
