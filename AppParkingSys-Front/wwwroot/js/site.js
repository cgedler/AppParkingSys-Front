// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function delete_item() {
    var x = confirm("Do you want to delete?");
    if (x)
        return true;
    else
        return false;
}