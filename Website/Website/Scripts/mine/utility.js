// Global variables 
var global_sortAnimFunc = null;
var global_drawItemsFunc = null;

// Query the JSON by ajax
function queryAjax(url, servInput, callback, visualObj) {
    $.get(url, {
        queryType: GetQueryType(),
        algorithm: GetAlgo(),
        input: servInput
    }, function (data, textStatus) {
        var paraObj = {
            data: data,
            view: visualObj
        };
        callback(paraObj);
    });
}

// Get query type
function GetQueryType() {
    var result = $.cookie("queryType");
    if (result) {
        return result;
    } else {
        // Default value
        return "SORT";
    }
}

// Get Algorithm
function GetAlgo() {
    var result = $.cookie("algorithm");
    if (result) {
        return result;
    } else {
        // Default value
        return "BUBBLE";
    }
}

// Get interval
function GetInterval() {
    var result = $.cookie("interval");
    if (result) {
        return result;
    } else {
        return 1500;
    }
}

// Get input
function GetInput() {
    var result = $.cookie("input");
    if (result) {
        return result;
    } else {
        return "";
    }
}

// Get drawItems function
function GetDrawItemsFunc() {
    RefreshGlobalVar();
    if (global_drawItemsFunc) {
        return global_drawItemsFunc;
    } else {
        // Default value
        return horiDrawItems;
    }
}

// Get animation function for sort algorithm
function GetSortAnimFunc() {
    RefreshGlobalVar();
    if (global_sortAnimFunc) {
        return global_sortAnimFunc;
    } else {
        // Default value
        return bubbleSortAnim;
    }
}

function RefreshGlobalVar() {
    // Decide which animation function should be used
    if (GetQueryType() == "SORT") {
        switch (GetAlgo()) {
            case "BUBBLE":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = bubbleSortAnim;
                break;
            case "HEAP":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = heapSortAnim;
                break;
            case "INSERTION":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = insertionSortAnim;
                break;
            case "MERGE":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = mergeSortAnim;
                break;
            case "QUICK":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = quickSortAnim;
                break;
            case "SELECTION":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = selectionSortAnim;
                break;
            case "SHELL":
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = shellSortAnim;
                break;
            default:
                global_drawItemsFunc = horiDrawItems;
                global_sortAnimFunc = bubbleSortAnim;
                break;
        }
    } else if (GetQueryType() == "ANLY") {
        // TODO
    }
}
