// Exchange two items horizonally.
function HorizonExchangeItems(itemOne, itemTwo) {
    var oldOneX = itemOne.attr("x");
    var oldTwoX = itemTwo.attr("x");
    // Find which one is at left
    var leftSqua = itemOne;
    var rightSqua = itemTwo;
    if (oldOneX > oldTwoX) {
        leftSqua = itemTwo;
        rightSqua = itemOne;
    }
    var leftX = leftSqua.attr("x");
    var rightX = rightSqua.attr("x");
    var leftY = leftSqua.attr("y");
    leftSqua.animate({ y: leftY - 120 }, 500, function () {
        this.animate({ x: rightX }, 500, "ease-out", function () {
            rightSqua.animate({ x: leftX }, 500, "ease-out");
            var anim = Raphael.animation({ y: leftY }, 500, "ease-out");
            this.animate(anim.delay(500));
        });
    });
}

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
    setLeft.animate({ transform: "...T0,-" + deltaY }, stepInterval, "ease-out", function () {
        this.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-out", function () {
            this.animate({ transform: "...T0," + deltaY }, stepInterval, "ease-in", function () {
                callback(param, interval);
            });
        });
        setRight.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in");
    });
}