// Set search button click handler
$(function () {
    $("button#btnGo").click(function () {
        // Get input and split it
        var input = $("input#inputArr").val();
        var arr = input.split(",");
        // Create valid item array.
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
        // Set div height and width
        var paper = Raphael("paper", paperWidth, paperHeight);
        for (var j = 0; j < validArr.length; ++j) {
            var posX = margin * (j + 1) + squaEdge * j;
            var posY = paperHeight / 2;
            var square = paper.rect(posX, posY, squaEdge, squaEdge).attr({ fill: "blue" });
            var text = paper.text(posX + squaEdge / 2, posY + squaEdge / 2, validArr[j]).attr({ "font-family": "arial", "font-size": 32 });
            var card = paper.set();
            card.push(text);
            card.push(square);
        }
    });
});