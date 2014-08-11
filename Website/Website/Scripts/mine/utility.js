// Query actions and then play them
function playActionsAjax(url, type, inputArr, setArr) {
    $.get(url, {
        type: type,
        input: inputArr.join()
    }, function (data, textStatus) {
        var param = {
            data: data,
            index: 0,
            setArr: setArr,
        };
        playActionImpl(param);
    });
}

function playActionImpl(param) {
    var data = param.data;
    var index = param.index;
    var setArr = param.setArr;
    // If index is out of range, then return.
    if (index >= data.length)
        return;
    // Call functions according to data
    var item = data[index];
    if (item.action == "EXCG") {
        var indexes = item.param.split(",");
        var newParam = {
            data: data,
            index: ++index,
            setArr: setArr
        };
        var indexOne = parseInt(indexes[0]);
        var indexTwo = parseInt(indexes[1]);
        // Exchange two items in array
        var temp = setArr[indexOne];
        setArr[indexOne] = setArr[indexTwo];
        setArr[indexTwo] = temp;
        // Exchange two items on screen visually
        HorizonExchangeSets(setArr[indexOne],
            setArr[indexTwo], playActionImpl, newParam);
    }
}