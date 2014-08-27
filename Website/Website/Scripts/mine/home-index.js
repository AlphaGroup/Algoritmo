/*
This file is made for home-index.js
*/
// The global paper variable
var paper;
// Set search button click handler
$(function () {
    $("button#btnGo").click(function () {
        // Clear old paper
        try {
            paper.remove();
        } catch (ex) {
            // Do nothing.
        }
        // Create valid item array. This array is composed of numberic strings
        var input = $("input#inputArr").val();
        var arr = input.split(" ");
        var validArr = [];
        for (var i = 0; i < arr.length; ++i) {
            // Is a valid item
            if (!isNaN(parseInt(arr[i]))) {
                validArr.push(parseInt(arr[i]).toString());
            }
        }
        // Draw visual items on paper
        //var visualObj = drawItems(GetQueryType(), GetAlgo(), validArr, "paper");
        var visualObj = (GetDrawItemsFunc())("paper", validArr);
        paper = visualObj.paper;
        // Decide which callback function should be used
        var callback = GetSortAnimFunc();
        // Call jax function to get actions and play it.
        var url = "/Home/RequireActionsAjax";
        queryAjax(url, validArr.join(" "), callback, visualObj);
    });
});

// Set inputArr's event handler
$(function () {
    // Set Enter press handler
    $("input#inputArr").keypress(function (event) {
        if (event.which == 13) {
            $("button#btnGo").click();
        }
    });
    // Set change handler
    $("input#inputArr").change(function () {
        $.cookie("input", $(this).val(), { path: '/' });
    });
    // Set value when loaded
    $("input#inputArr").val(GetInput());
});

// Set side navbar
$(function () {
    $("li:contains('Home')").addClass("active");
});