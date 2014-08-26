
// Query the JSON by ajax
function queryAjax(url, servInput, callback, visualObj) {
    $.get(url, {
        queryType: GetQueryType(),
        algorithm: GetAlgo(),
        input: servInput
    }, function(data, textStatus) {
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