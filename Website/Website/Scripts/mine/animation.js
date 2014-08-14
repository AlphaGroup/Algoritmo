//// Exchange two items horizonally.
//function HorizonExchangeItems(itemOne, itemTwo) {
//    var oldOneX = itemOne.attr("x");
//    var oldTwoX = itemTwo.attr("x");
//    // Find which one is at left
//    var leftSqua = itemOne;
//    var rightSqua = itemTwo;
//    if (oldOneX > oldTwoX) {
//        leftSqua = itemTwo;
//        rightSqua = itemOne;
//    }
//    var leftX = leftSqua.attr("x");
//    var rightX = rightSqua.attr("x");
//    var leftY = leftSqua.attr("y");
//    leftSqua.animate({ y: leftY - 120 }, 500, function() {
//        this.animate({ x: rightX }, 500, "ease-out", function() {
//            rightSqua.animate({ x: leftX }, 500, "ease-out");
//            var anim = Raphael.animation({ y: leftY }, 500, "ease-out");
//            this.animate(anim.delay(500));
//        });
//    });
//}

// Exchange two sets horizonally
function HorizonExchangeSets(setOne, setTwo, interval, callback, param) {
    var oldOneX = setOne.getBBox().x;
    var oldTwoX = setTwo.getBBox().x;
    var setLeft = setOne;
    var setRight = setTwo;
    // If setTwo is the left one
    if (oldTwoX < oldOneX) {
        setLeft = setTwo;
        setRight = setOne;
    }
    var deltaX = setRight.getBBox().x - setLeft.getBBox().x;
    var deltaY = setRight.getBBox().y / 2;
    var stepInterval = interval / 3;
    setLeft.animate({ transform: "...T0,-" + deltaY }, stepInterval, "ease-out", function() {
        this.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-out", function() {
            this.animate({ transform: "...T0," + deltaY }, stepInterval, "ease-in", function() {
                callback(param, interval);
            });
        });
        setRight.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in");
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

function bubbleSortAnim(paraObj) {
    alert("bubbleSortAnim" + paraObj.view.items.length);
}