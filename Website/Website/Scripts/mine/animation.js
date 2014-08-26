
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

// Draw items on the screen. Return an object contain paper and visualObj
function drawItems(queryType, algorithm, input, placeId) {
    var result = null;
    if (queryType == "SORT") {
        switch (algorithm) {
        case "BUBBLE":
            result = horiDrawItems(placeId, input);
            break;
        case "INSERTION":
            result = horiDrawItems(placeId, input);
            break;
        case "MERGE":
            result = horiDrawItems(placeId, input);
            break;
        default:
            break;
        }
    }
    return result;
}

// Draw items horizontally
function horiDrawItems(placeId, input) {
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
        card.push(square);
        card.push(text);
        items.push(card);
    }
    return {
        paper: paper,
        items: items
    };
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

// For insertion sort
function insertionSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    var viewItems = paraObj.view.items;
    if (dataItem == null) {
        // End of actions
        return;
    }
    var interval = GetInterval();
    // Deal with the data returned from server.
    switch (dataItem.action) {
    case "ASGN":
        // Assignment
        {
            var indexes = dataItem.param.split("=");
            var indexLhr = parseInt(indexes[0]);
            var indexRhr = parseInt(indexes[1]);
            var deltaX = null;
            var deltaY = null;
            var stepInterval = null;
            var key = null;
            var lhr = null;
            var rhr = null;
            if (indexLhr < 0) {
                // left hand reference is the key(assign a key)
                key = paraObj.view.items[indexRhr].clone();
                paraObj.key = key;
                stepInterval = interval / 2;
                var oldY = key.getBBox().y;
                // lift up this item
                key.animate({ transform: "...T0,-" + oldY / 2 }, stepInterval, "ease-in", function() {
                    insertionSortAnim(paraObj);
                });
            } else if (indexRhr < 0) {
                // right hand reference is the key(assign an item with key's value)
                key = paraObj.key;
                lhr = viewItems[indexLhr];
                stepInterval = interval / 3;
                deltaX = key.getBBox().x - lhr.getBBox().x;
                deltaY = key.getBBox().y - lhr.getBBox().y;
                // visual animation
                // let lhr fade away
                lhr.animate({ opacity: 0 }, stepInterval, "ease-in", function() {
                    // move key to lhr's position
                    key.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in", function() {
                        this.animate({ transform: "...T0," + (-deltaY) }, stepInterval, "ease-in", function() {
                            // background assignment
                            viewItems[indexLhr] = key;
                            // recursively call itself
                            insertionSortAnim(paraObj);
                        });
                    });
                });
            } else {
                // Both operands are normal items in viewItems
                stepInterval = interval / 3;
                deltaX = viewItems[indexLhr].getBBox().x - viewItems[indexRhr].getBBox().x;
                var cloned = viewItems[indexRhr].clone();
                // Visual animation
                viewItems[indexLhr].animate({ opacity: 0 }, stepInterval, "ease-in", function() {
                    this.clear();
                    cloned.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-in", function() {
                        insertionSortAnim(paraObj);
                    });
                });
                // Background assignment
                viewItems[indexLhr] = cloned;
            }
        }
        break;
    default:
        break;
    }
    return;
}