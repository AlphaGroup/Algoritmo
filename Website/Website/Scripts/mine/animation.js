// Exchange two item horizonally.
function HorizonalExchange(squareOne, squareTwo) {
    var oldOneX = squareOne.attr("x");
    var oldTwoX = squareTwo.attr("x");
    // Find which one is at left
    var leftSqua = squareOne;
    var rightSqua = squareTwo;
    if (oldOneX > oldTwoX) {
        leftSqua = squareTwo;
        rightSqua = squareOne;
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
