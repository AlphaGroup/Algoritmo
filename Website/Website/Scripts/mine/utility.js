//// Query actions and then play them
//function playActionsAjax(url, type, inputArr, setArr, interval) {
//    $.get(url, {
//        type: type,
//        input: inputArr.join(" ")
//    }, function(data, textStatus) {
//        var param = {
//            data: data,
//            index: 0,
//            setArr: setArr,
//        };
//        playActionImpl(param, interval);
//    });
//}

//function playActionImpl(param, interval) {
//    var data = param.data;
//    var index = param.index;
//    var setArr = param.setArr;
//    // If index is out of range, then return.
//    if (index >= data.length)
//        return;
//    // Call functions according to data
//    var item = data[index];
//    if (item.action == "EXCG") {
//        var indexes = item.param.split(",");
//        var newParam = {
//            data: data,
//            index: ++index,
//            setArr: setArr
//        };
//        var indexOne = parseInt(indexes[0]);
//        var indexTwo = parseInt(indexes[1]);
//        // Exchange two items in array
//        var temp = setArr[indexOne];
//        setArr[indexOne] = setArr[indexTwo];
//        setArr[indexTwo] = temp;
//        // Exchange two items on screen visually
//        // And recursively call playActionImpl itself
//        HorizonExchangeSets(setArr[indexOne],
//            setArr[indexTwo], interval, playActionImpl, newParam);
//    }
//}

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