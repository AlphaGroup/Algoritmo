
// Exchange two items horizonally
function horiExcg(item0, item1, callback, param) {
    var oldOneX = item0.getBBox().x;
    var oldTwoX = item1.getBBox().x;
    var itemLeft = item0;
    var itemRight = item1;
    // If item1 is the left one
    if (oldTwoX < oldOneX) {
        itemLeft = item1;
        itemRight = item0;
    }
    var deltaX = itemRight.getBBox().x - itemLeft.getBBox().x;
    var deltaY = itemRight.getBBox().y / 2;
    var interval = GetInterval();
    var stepInterval = interval / 3;
    itemLeft.animate({ transform: "...T0,-" + deltaY }, stepInterval, "ease-out", function() {
        this.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-out", function() {
            this.animate({ transform: "...T0," + deltaY }, stepInterval, "ease-in", function() {
                callback(param);
            });
        });
        itemRight.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in");
    });
}

function drawItems(queryType, algorithm, input, placeId) {
    if (queryType == "SORT") {
        if (algorithm == "BUBBLE") {
            var squaEdge = 60;
            var margin = 20;
            var paperHeight = squaEdge * 5;
            var paperWidth = squaEdge * input.length + margin * (input.length + 1);
            var paper = Raphael(placeId, paperWidth, paperHeight);
            // Add sets into setArr
            var items = [];
            for (var j = 0; j < input.length; ++j) {
                var posX = margin * (j + 1) + squaEdge * j;
                var posY = paperHeight / 2;
                var square = paper.rect(posX, posY, squaEdge, squaEdge).attr({ fill: "#487B7B" });
                var text = paper.text(posX + squaEdge / 2, posY + squaEdge / 2,
                    input[j]).attr({ "font-family": "arial", "font-size": squaEdge / 2, fill: "white" });
                var card = paper.set();
                card.push(text);
                card.push(square);
                items.push(card);
            }
            return {
                paper: paper,
                visualObj: {
                    items: items
                }
            };
        }
    }
}

// Recursively retrieve items from paraObj.data
function bubbleSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    if (dataItem == null) {
        // End of actions
        return;
    }
    if (dataItem.action == "EXCG") {
        var indexes = dataItem.param.split(",");
        var index0 = parseInt(indexes[0]);
        var index1 = parseInt(indexes[1]);
        var items = paraObj.view.items;
        // Exchange two items in array
        var temp = items[index0];
        items[index0] = items[index1];
        items[index1] = temp;
        // Exchange two items on screen visually
        horiExcg(items[index0],
            items[index1], bubbleSortAnim, paraObj);
    }
}