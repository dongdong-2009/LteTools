function CoverageAnalysisSet() { }

CoverageAnalysisSet.prototype.getRsrpPercentage = function () {
    var dataRsrpPercentage = [];
    $.ajax({
        type: "GET", //提交的类型  
        dataType: "json",
        url: "/Coverage/CoverageIntervalPercentage", //提交地址  
        async: false,
        data: { fieldName: "信号RSRP" },
        success: function (data) {
            $(data).each(function (index, value) {
                dataRsrpPercentage.push([data[index].N, data[index].V]);
            });
        }
    });
    return dataRsrpPercentage;
};

CoverageAnalysisSet.prototype.getSinrPercentage = function () {
    var dataSinrPercentage = [];
    $.ajax({
        type: "GET", //提交的类型  
        dataType: "json",
        url: "/Coverage/CoverageIntervalPercentage", //提交地址  
        async: false,
        data: { fieldName: "标称SINR" },
        success: function (data) {
            $(data).each(function (index, value) {
                dataSinrPercentage.push([data[index].N, data[index].V]);
            });
        }
    });
    return dataSinrPercentage;
};
