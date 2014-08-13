// Set search button click handler
var paper;
$(function () {
    $("button#btnGo").click(function () {
        // Clear old paper
        try {
            paper.remove();
        } catch (ex) {
            // Do nothing.
        }
        // Get input and split it
        var input = $("input#inputArr").val();
        var arr = input.split(" ");
        // Create valid item array. This array is composed of numberic strings
        var validArr = [];
        for (var i = 0; i < arr.length; ++i) {
            // Is a valid item
            if (!isNaN(parseInt(arr[i]))) {
                validArr.push(parseInt(arr[i]).toString());
            }
        }
        // Create visual items on paper
        var squaEdge = 60;
        var margin = 20;
        var paperHeight = squaEdge * 5;
        var paperWidth = squaEdge * validArr.length + margin * (validArr.length + 1);
        paper = Raphael("paper", paperWidth, paperHeight);
        // Add sets into setArr
        var setArr = [];
        for (var j = 0; j < validArr.length; ++j) {
            var posX = margin * (j + 1) + squaEdge * j;
            var posY = paperHeight / 2;
            var square = paper.rect(posX, posY, squaEdge, squaEdge).attr({ fill: "#487B7B" });
            var text = paper.text(posX + squaEdge / 2, posY + squaEdge / 2,
                validArr[j]).attr({ "font-family": "arial", "font-size": squaEdge / 2, fill: "white" });
            var card = paper.set();
            card.push(text);
            card.push(square);
            setArr.push(card);
        }
        // Call jax function to get actions and play it.
        var url = "/Home/RequireActionsAjax";
        var sort = "SORT-BUBBLE";
        if ($.cookie("algorithm")) {
            sort = $.cookie("algorithm");
        }
        playActionsAjax(url, sort, validArr, setArr);
    });
});

// Set Enter press handler
$(function () {
    $("input#inputArr").keypress(function (event) {
        if (event.which == 13) {
            $("button#btnGo").click();
        }
    });
});

// Set side navbar
$(function () {
    $("li:contains('Home')").addClass("active");
});