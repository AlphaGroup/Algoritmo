/*
*   Useful functions in animation
*/
// Exchange two abherent and horizonal items.
function horiNextToExcg(item0, item1, callback, param) {
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
    itemLeft.animate({ transform: "...T0,-" + deltaY }, stepInterval, "ease-out", function () {
        this.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-out", function () {
            this.animate({ transform: "...T0," + deltaY }, stepInterval, "ease-in", function () {
                callback(param);
            });
        });
        itemRight.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in");
    });
}

// Exchange two casual and horizonal items.
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
    var absDeltaX = Math.abs(itemRight.getBBox().x - itemLeft.getBBox().x);
    var absDeltaY = itemRight.getBBox().y / 2;
    var interval = GetInterval();
    var stepInterval = interval / 3;
    // Push down left one
    itemLeft.animate({ transform: "...T0," + absDeltaY }, stepInterval, "ease-out", function () {
        // Move right
        this.animate({ transform: "...T" + absDeltaX + ",0" }, stepInterval, "ease-out", function () {
            // Lift up
            this.animate({ transform: "...T0,-" + absDeltaY }, stepInterval, "ease-in", function () {
                // Call the callback function
                callback(param);
            });
        });
    });
    // Lift up right one
    itemRight.animate({ transform: "...T0,-" + absDeltaY }, stepInterval, "ease-in", function () {
        // Move left
        this.animate({ transform: "...T-" + absDeltaX + ",0" }, stepInterval, "ease-out", function () {
            // Push down
            this.animate({ transform: "...T0," + absDeltaY }, stepInterval, "ease-out");
        });
    });
}

/*
Draw visual items on screen.
These functions return an object which contains 
*/
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

// Draw items as a tree
function binaryTreeDrawItmes(placeId, input) {
    var radius = 25;
    // Delta height between levels (calculate from center of the circle)
    var levelHeight = 100;
    var totalLevels = Math.floor(Math.log(input.length) / Math.log(2)) + 1;
    // The paper's height must bigger than levelHeight * (totalLevels - 1) + radius * 2 
    var paperHeight = levelHeight * (totalLevels - 1) + radius * 2 + 10 * radius;
    // The paper's width must bigger than 2*r*Math.pow(2,level-1)
    // So that there will not be overlap.
    var paperWidth = 2 * radius * Math.pow(2, totalLevels - 1) + radius;
    var paper = Raphael(placeId, paperWidth, paperHeight);
    // Add sets into setArr
    var items = [];
    var level = 0;
    for (var j = 0; j < input.length; ++j) {
        // level starts from zero
        level = Math.floor(Math.log(j + 1) / Math.log(2));
        // Calculate its x position
        // 1. Find the index and the x of the first node in that level
        var firstIndex = Math.pow(2, level) - 1;
        var firstX = 1 / Math.pow(2, level + 1);
        // 2. Calculate the delta propotion between each node in that level
        var deltaX = 1 / Math.pow(2, level);
        // 3. Calculate the final x, 1/2, 1/4, 1/8 ... 
        var posX = paperWidth * (firstX + deltaX * (j - firstIndex));
        var posY = 2 * radius + levelHeight * level;
        var square = paper.circle(posX, posY, radius).attr({ fill: "#487B7B" });
        var text = paper.text(posX, posY,
            input[j]).attr({ "font-family": "arial", "font-size": radius, fill: "white" });
        var card = paper.set();
        card.push(square);
        card.push(text);
        items.push(card);
    }
    // Draw links between each node
    for (var i = 0; i < input.length; i++) {
        // Do some calculation
        level = Math.floor(Math.log(i + 1) / Math.log(2));
        var theta = Math.atan((paperWidth * 1 / Math.pow(2, level + 2)) / levelHeight);
        var parentX = (items[i].getBBox().x + items[i].getBBox().x2) / 2;
        var parentY = (items[i].getBBox().y + items[i].getBBox().y2) / 2;
        var adjustX = radius * Math.sin(theta);
        var adjustY = radius * Math.cos(theta);
        // Get children's index
        var leftIndex = 2 * i + 1;
        var rightIndex = 2 * i + 2;
        // Link left one
        if (leftIndex < input.length) {
            var leftX = (items[leftIndex].getBBox().x + items[leftIndex].getBBox().x2) / 2;
            var leftY = (items[leftIndex].getBBox().y + items[leftIndex].getBBox().y2) / 2;
            var leftPath = paper.path("M" + (parentX - adjustX) + "," + (parentY + adjustY) +
                "L" + (leftX + adjustX) + "," + (leftY - adjustY)).attr({ "stroke-width": 5, "stroke-opacity": 0.8 });
        };
        // Link right one
        if (rightIndex < input.length) {
            var rightX = (items[rightIndex].getBBox().x + items[rightIndex].getBBox().x2) / 2;
            var rightY = (items[rightIndex].getBBox().y + items[rightIndex].getBBox().y2) / 2;
            var rightPath = paper.path("M" + (parentX + adjustX) + "," + (parentY + adjustY) +
                "L" + (rightX - adjustX) + "," + (rightY - adjustY)).attr({ "stroke-width": 5, "stroke-opacity": 0.8 });
        }
    }
    return {
        paper: paper,
        items: items
    };
}

// Recursively retrieve items from paraObj.data
/*
* Animation functions for sort algorithms
*/
// For merge sort
function mergeSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    var interval = GetInterval();
    var stepInterval = null;
    if (dataItem == null) {
        // End of actions
        return;
    }
    switch (dataItem.action) {
        case "DIVD":
            // Get parameters
            var divdIndexes = dataItem.param.split(',');
            var begin = parseInt(divdIndexes[0]);
            var mid = parseInt(divdIndexes[1]);
            var end = parseInt(divdIndexes[2]);
            // Create sub arrays
            paraObj.view.leftSub = [];
            for (var l = begin; l <= mid; l++) {
                var leftItem = paraObj.view.items[l].clone();
                // Change sub array' color
                leftItem[0].attr({ fill: "#511470" });
                paraObj.view.leftSub.push(leftItem);
            }
            paraObj.view.rightSub = [];
            for (var m = mid + 1; m <= end; m++) {
                var rightItem = paraObj.view.items[m].clone();
                // Change sub array' color
                rightItem[0].attr({ fill: "#E9C306" });
                paraObj.view.rightSub.push(rightItem);
            }
            // Hide original items whose indexes are from 'begin' to 'end'
            for (var i = begin; i <= end; i++) {
                paraObj.view.items[i].attr("opacity", 0);
            }
            // Lift up sub arrays
            var sets = paraObj.view.paper.set();
            var right = paraObj.view.rightSub;
            var left = paraObj.view.leftSub;
            for (var j = 0; j < right.length; j++) {
                sets.push(right[j]);
            }
            for (var k = 0; k < left.length; k++) {
                sets.push(left[k]);
            }
            var oldY = right[0].getBBox().y;
            stepInterval = interval / 2;
            sets.animate({ transform: "...T0,-" + oldY / 2 }, stepInterval, "ease-out", function () {
                mergeSortAnim(paraObj);
            });
            break;
        case "ASGN":
            // Get parameters
            var asgnIndexes = dataItem.param.split('=');
            var originIndex = parseInt(asgnIndexes[0]);
            var subIndex = parseInt(asgnIndexes[1]);
            if (subIndex < 0) {
                // For left sub array
                var leftIndex = Math.abs(subIndex) - 1;
                // Front animation
                stepInterval = interval / 3;
                paraObj.view.leftSub[leftIndex].animate({ opacity: 0 }, stepInterval, "ease-out", function () {
                    // Move to right place
                    var originX = paraObj.view.items[originIndex].getBBox().x;
                    var originY = paraObj.view.items[originIndex].getBBox().y;
                    var subX = paraObj.view.leftSub[leftIndex].getBBox().x;
                    var subY = paraObj.view.leftSub[leftIndex].getBBox().y;
                    var deltaX = originX - subX;
                    var deltaY = originY - subY;
                    paraObj.view.leftSub[leftIndex].transform("...T" + deltaX + "," + deltaY);
                    // Change to normal color
                    paraObj.view.leftSub[leftIndex][0].attr({ fill: "#487B7B" });
                    // Show it
                    paraObj.view.leftSub[leftIndex].animate({ opacity: 1 }, stepInterval, "ease-out", function () {
                        // Background manipulation: replace original items
                        paraObj.view.items[originIndex] = paraObj.view.leftSub[leftIndex];
                        mergeSortAnim(paraObj);
                    });
                });
            } else {
                // For right sub array
                var rightIndex = subIndex;
                // Front animation
                stepInterval = interval / 3;
                paraObj.view.rightSub[rightIndex].animate({ opacity: 0 }, stepInterval, "ease-out", function () {
                    // Move to right place
                    var originX = paraObj.view.items[originIndex].getBBox().x;
                    var originY = paraObj.view.items[originIndex].getBBox().y;
                    var subX = paraObj.view.rightSub[rightIndex].getBBox().x;
                    var subY = paraObj.view.rightSub[rightIndex].getBBox().y;
                    var deltaX = originX - subX;
                    var deltaY = originY - subY;
                    paraObj.view.rightSub[rightIndex].transform("...T" + deltaX + "," + deltaY);
                    // Change to normal color
                    paraObj.view.rightSub[rightIndex][0].attr({ fill: "#487B7B" });
                    // Show it
                    paraObj.view.rightSub[rightIndex].animate({ opacity: 1 }, stepInterval, "ease-out", function () {
                        // Background manipulation: replace original items
                        paraObj.view.items[originIndex] = paraObj.view.rightSub[rightIndex];
                        mergeSortAnim(paraObj);
                    });
                });
            }
            break;
        default:
            break;
    }
}

// For bubble sort
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
        horiNextToExcg(items[index0],
            items[index1], bubbleSortAnim, paraObj);
    }
}

// For heap sort
function heapSortAnim(paraObj) {

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
                    // Change color
                    key[0].animate({ fill: "#DDD914" }, stepInterval, "ease-in");
                    // lift up this item
                    key.animate({ transform: "...T0,-" + oldY / 2 }, stepInterval, "ease-in", function () {
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
                    lhr.animate({ opacity: 0 }, stepInterval, "ease-in", function () {
                        // Change key's color
                        key[0].animate({ fill: "#487B7B" }, 2 * stepInterval, "ease-in");
                        // move key to lhr's position
                        key.animate({ transform: "...T-" + deltaX + ",0" }, stepInterval, "ease-in", function () {
                            this.animate({ transform: "...T0," + (-deltaY) }, stepInterval, "ease-in", function () {
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
                    viewItems[indexLhr].animate({ opacity: 0 }, stepInterval, "ease-in", function () {
                        this.remove();
                        cloned.animate({ transform: "...T" + deltaX + ",0" }, stepInterval, "ease-in", function () {
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

// For Quick sort
function quickSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    if (dataItem == null) {
        // End of actions
        return;
    }
    var indexes, index0, index1;
    var oldY, oldX, delatX, deltaY, absDeltaX, absDeltaY;
    var interval = GetInterval();
    var items = paraObj.view.items;
    var stepInterval;
    var temp;
    switch (dataItem.action) {
        case "ASGN":
            // Assignment
            // Get parameters
            indexes = dataItem.param.split('=');
            index0 = parseInt(indexes[0]);
            index1 = parseInt(indexes[1]);
            if (index0 < 0 && index1 >= 0) {
                // index0 refers to the pivot, which means this action is to set the pivot element
                // Create the pivot element
                paraObj.view.pivot = items[index1].clone();
                stepInterval = interval / 2;
                // Change color
                paraObj.view.pivot[0].animate({ fill: "#DDD914" }, stepInterval, "ease-out");
                // Hide the original one
                items[index1].attr("opacity", 0);
                // Lift up the pivot element
                oldY = paraObj.view.pivot.getBBox().y;
                paraObj.view.pivot.animate({ transform: "...T0,-" + oldY / 2 }, stepInterval, "ease-out", function () {
                    quickSortAnim(paraObj);
                });
            } else if (index0 >= 0 && index1 < 0) {
                // index1 refers to the pivot, which means this action is to set an element with pivot element
                // Usually this is used to move the pivot to the same horizon.
                // 1. Visual movement
                // Calculate deltaX and deltaY
                deltaY = items[index0].getBBox().y - paraObj.view.pivot.getBBox().y;
                stepInterval = interval / 2;
                // Push down and change color
                paraObj.view.pivot[0].animate({ fill: "#487B7B" }, stepInterval, "ease-out");
                paraObj.view.pivot.animate({ transform: "...T0," + deltaY }, stepInterval, "ease-out", function () {
                    // 2. Background array manipulation
                    items[index0] = paraObj.view.pivot;
                    paraObj.view.pivot = null;
                    // 3. Recursively call itself
                    quickSortAnim(paraObj);
                });
            } else {
                // Error
                console.log("Error in quickSortAnim!");
                return;
            }
            break;
        case "EXCG":
            // Exchange
            indexes = dataItem.param.split(',');
            index0 = parseInt(indexes[0]);
            index1 = parseInt(indexes[1]);
            stepInterval = interval / 3;
            if (index0 >= 0 && index1 >= 0) {
                // Exchange two normal elements
                // 1. Background array manipulation
                temp = items[index0];
                items[index0] = items[index1];
                items[index1] = temp;
                // 2. Visual exchange
                horiExcg(items[index0], items[index1], quickSortAnim, paraObj);
            } else if (index0 >= 0 && index1 < 0) {
                // Exchange an element with pivot
                // 1. Visual Exchange
                absDeltaY = Math.abs(items[index0].getBBox().y - paraObj.view.pivot.getBBox().y);
                absDeltaX = Math.abs(items[index0].getBBox().x - paraObj.view.pivot.getBBox().x);
                // Push down
                items[index0].animate({ transform: "...T0," + absDeltaY }, interval / 3, "ease-out", function () {
                    // Move right
                    this.animate({ transform: "...T" + absDeltaX + ",0" }, interval / 3, "ease-out", function () {
                        // Lift up to be the pivot
                        this.animate({ transform: "...T0,-" + 2 * absDeltaY }, interval / 3, "ease-out", function () {
                            // 2. Background manipulation
                            temp = items[index0];
                            items[index0] = paraObj.view.pivot;
                            paraObj.view.pivot = temp;
                            // 3. Recursively call itself
                            quickSortAnim(paraObj);
                        });
                    });
                    // For pivot
                    // Move left
                    paraObj.view.pivot.animate({ transform: "...T-" + absDeltaX + ",0" }, interval / 3, "ease-out", function () {
                        // Push down and change color to normal element's one
                        this.animate({ transform: "...T0," + absDeltaY }, interval / 3, "ease-out");
                        this[0].animate({ fill: "#487B7B" }, interval / 3, "ease-out");
                    });
                });
            } else {
                // Error
                console.log("Error in quickSortAnim!");
                return;
            }
            break;
        default:
            break;
    }
}

// For selection sort
function selectionSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    if (dataItem == null) {
        // There is no more actions
        return;
    }
    var indexes, index0, index1;
    var interval = GetInterval();
    var items = paraObj.view.items;
    switch (dataItem.action) {
        case "MARK":
            index0 = parseInt(dataItem.param);
            if (index0 >= 0) {
                // Mark the element
                // Change the element's color
                items[index0][0].animate({ fill: "#DDD914" }, interval / 4, "ease-out", function () {
                    // Recursively call itself
                    selectionSortAnim(paraObj);
                });
            } else {
                // Unmark the element
                // Change the element's color
                items[Math.abs(index0) - 1][0].animate({ fill: "#487B7B" }, interval / 4, "ease-out", function () {
                    // Recursively call itself
                    selectionSortAnim(paraObj);
                });
            }
            break;
        case "EXCG":
            // Exchange
            indexes = dataItem.param.split(',');
            index0 = parseInt(indexes[0]);
            index1 = parseInt(indexes[1]);
            stepInterval = interval / 3;
            // Exchange two normal elements
            // 1. Background array manipulation
            temp = items[index0];
            items[index0] = items[index1];
            items[index1] = temp;
            // 2. Visual exchange
            horiExcg(items[index0], items[index1], selectionSortAnim, paraObj);
            break;
        default:
            break;
    }
}

// For shell sort
function shellSortAnim(paraObj) {
    var dataItem = paraObj.data.shift();
    if (dataItem == null) {
        return;
    }
    var indexes, index0, index1, temp;
    var deltaX;
    var absDeltaY;
    var items = paraObj.view.items;
    var interval = GetInterval();
    switch (dataItem.action) {
        case "ASGN":
            indexes = dataItem.param.split("=");
            index0 = parseInt(indexes[0]);
            index1 = parseInt(indexes[1]);
            if (index0 >= 0 && index1 >= 0) {
                // Normal assignment
                // 1. Visual animation
                deltaX = items[index0].getBBox().x - items[index1].getBBox().x;
                absDeltaY = items[index0].getBBox().y / 2;
                // Hide the destination item
                items[index0].animate({ opacity: 0 }, interval / 3, "ease-out");
                // Clone the item
                temp = items[index1].clone();
                // Push down
                temp.animate({ transform: "...T0," + absDeltaY }, interval / 3, "ease-out", function () {
                    // Move right
                    this.animate({ transform: "...T" + deltaX + ",0" }, interval / 3, "ease-out", function () {
                        // Lift up
                        this.animate({ transform: "...T0,-" + absDeltaY }, interval / 3, "ease-out", function () {
                            // 2. Background manipulation
                            items[index0] = temp;
                            // 3. Recursively call itself
                            shellSortAnim(paraObj);
                        });
                    });
                });
            } else if (index0 == -1 && index1 >= 0) {
                // Assign the key with value of the index1
                // Clone
                temp = items[index1].clone();
                // 1. Background manipulation
                paraObj.view.key = temp;
                // 2. Visual animation
                // Change color
                temp[0].animate({ fill: "#DDD914" }, interval / 3, "ease-out");
                // Lift up
                absDeltaY = items[index1].getBBox().y / 2;
                temp.animate({ transform: "...T0,-" + absDeltaY }, interval / 3, "ease-out", function () {
                    // 3. Recursively call itselft
                    shellSortAnim(paraObj);
                });
            } else if (index0 >= 0 && index1 == -1) {
                // Assign the index0 with value of key
                // 1. Visual animation
                // Hide index0
                items[index0].animate({ opacity: 0 }, interval / 3, "ease-out");
                // Change color
                paraObj.view.key[0].animate({ fill: "#487B7B" }, interval / 3, "ease-out");
                // Move key to the index0
                deltaX = items[index0].getBBox().x - paraObj.view.key.getBBox().x;
                deltaY = items[index0].getBBox().y - paraObj.view.key.getBBox().y;
                // Move left
                paraObj.view.key.animate({ transform: "...T" + deltaX + ",0" }, interval / 3, "ease-out", function () {
                    // Push down
                    this.animate({ transform: "...T0," + deltaY }, interval / 3, "ease-out", function () {
                        // 2. Background manipulation
                        items[index0] = paraObj.view.key;
                        paraObj.view.key = null;
                        // 3. Recursively call itself
                        shellSortAnim(paraObj);
                    });
                });
            } else {
                // Error
                console.log("Error in shellSortAnim");
                return;
            }
            break;
        default:
            break;
    }
}