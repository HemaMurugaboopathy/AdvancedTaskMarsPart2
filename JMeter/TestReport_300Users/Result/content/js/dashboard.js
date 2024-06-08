/*
   Licensed to the Apache Software Foundation (ASF) under one or more
   contributor license agreements.  See the NOTICE file distributed with
   this work for additional information regarding copyright ownership.
   The ASF licenses this file to You under the Apache License, Version 2.0
   (the "License"); you may not use this file except in compliance with
   the License.  You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/
var showControllersOnly = false;
var seriesFilter = "";
var filtersOnlySampleSeries = true;

/*
 * Add header in statistics table to group metrics by category
 * format
 *
 */
function summaryTableHeader(header) {
    var newRow = header.insertRow(-1);
    newRow.className = "tablesorter-no-sort";
    var cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Requests";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 3;
    cell.innerHTML = "Executions";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 7;
    cell.innerHTML = "Response Times (ms)";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 1;
    cell.innerHTML = "Throughput";
    newRow.appendChild(cell);

    cell = document.createElement('th');
    cell.setAttribute("data-sorter", false);
    cell.colSpan = 2;
    cell.innerHTML = "Network (KB/sec)";
    newRow.appendChild(cell);
}

/*
 * Populates the table identified by id parameter with the specified data and
 * format
 *
 */
function createTable(table, info, formatter, defaultSorts, seriesIndex, headerCreator) {
    var tableRef = table[0];

    // Create header and populate it with data.titles array
    var header = tableRef.createTHead();

    // Call callback is available
    if(headerCreator) {
        headerCreator(header);
    }

    var newRow = header.insertRow(-1);
    for (var index = 0; index < info.titles.length; index++) {
        var cell = document.createElement('th');
        cell.innerHTML = info.titles[index];
        newRow.appendChild(cell);
    }

    var tBody;

    // Create overall body if defined
    if(info.overall){
        tBody = document.createElement('tbody');
        tBody.className = "tablesorter-no-sort";
        tableRef.appendChild(tBody);
        var newRow = tBody.insertRow(-1);
        var data = info.overall.data;
        for(var index=0;index < data.length; index++){
            var cell = newRow.insertCell(-1);
            cell.innerHTML = formatter ? formatter(index, data[index]): data[index];
        }
    }

    // Create regular body
    tBody = document.createElement('tbody');
    tableRef.appendChild(tBody);

    var regexp;
    if(seriesFilter) {
        regexp = new RegExp(seriesFilter, 'i');
    }
    // Populate body with data.items array
    for(var index=0; index < info.items.length; index++){
        var item = info.items[index];
        if((!regexp || filtersOnlySampleSeries && !info.supportsControllersDiscrimination || regexp.test(item.data[seriesIndex]))
                &&
                (!showControllersOnly || !info.supportsControllersDiscrimination || item.isController)){
            if(item.data.length > 0) {
                var newRow = tBody.insertRow(-1);
                for(var col=0; col < item.data.length; col++){
                    var cell = newRow.insertCell(-1);
                    cell.innerHTML = formatter ? formatter(col, item.data[col]) : item.data[col];
                }
            }
        }
    }

    // Add support of columns sort
    table.tablesorter({sortList : defaultSorts});
}

$(document).ready(function() {

    // Customize table sorter default options
    $.extend( $.tablesorter.defaults, {
        theme: 'blue',
        cssInfoBlock: "tablesorter-no-sort",
        widthFixed: true,
        widgets: ['zebra']
    });

    var data = {"OkPercent": 99.26246640788295, "KoPercent": 0.7375335921170498};
    var dataset = [
        {
            "label" : "FAIL",
            "data" : data.KoPercent,
            "color" : "#FF6347"
        },
        {
            "label" : "PASS",
            "data" : data.OkPercent,
            "color" : "#9ACD32"
        }];
    $.plot($("#flot-requests-summary"), dataset, {
        series : {
            pie : {
                show : true,
                radius : 1,
                label : {
                    show : true,
                    radius : 3 / 4,
                    formatter : function(label, series) {
                        return '<div style="font-size:8pt;text-align:center;padding:2px;color:white;">'
                            + label
                            + '<br/>'
                            + Math.round10(series.percent, -2)
                            + '%</div>';
                    },
                    background : {
                        opacity : 0.5,
                        color : '#000'
                    }
                }
            }
        },
        legend : {
            show : true
        }
    });

    // Creates APDEX table
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.8484771573604061, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "SignOut"], "isController": false}, {"data": [0.9769503546099291, 500, 1500, "Add Education"], "isController": false}, {"data": [0.9725177304964538, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.9571513002364066, 500, 1500, "Add Certification"], "isController": false}, {"data": [0.9104609929078015, 500, 1500, "Add Description"], "isController": false}, {"data": [0.943853427895981, 500, 1500, "Update Education"], "isController": false}, {"data": [0.9692671394799054, 500, 1500, "Add Language"], "isController": false}, {"data": [0.6130177514792899, 500, 1500, "Enable/Disable ShareSkill"], "isController": false}, {"data": [0.908096926713948, 500, 1500, "Add Skill"], "isController": false}, {"data": [0.0024271844660194173, 500, 1500, "SearchSkill"], "isController": false}, {"data": [0.8073286052009456, 500, 1500, "Update Certification"], "isController": false}, {"data": [0.973404255319149, 500, 1500, "Delete Language"], "isController": false}, {"data": [0.9964497041420118, 500, 1500, "View ShareSkill"], "isController": false}, {"data": [0.9899527186761229, 500, 1500, "SignIn"], "isController": false}, {"data": [0.9143026004728132, 500, 1500, "Update Language"], "isController": false}, {"data": [0.9630614657210402, 500, 1500, "Delete Certification"], "isController": false}, {"data": [0.541371158392435, 500, 1500, "Add ShareSkill"], "isController": false}, {"data": [0.636094674556213, 500, 1500, "Delete ShareSkill"], "isController": false}, {"data": [0.982565011820331, 500, 1500, "Delete Skill"], "isController": false}, {"data": [0.9157801418439716, 500, 1500, "Update Skill"], "isController": false}]}, function(index, item){
        switch(index){
            case 0:
                item = item.toFixed(3);
                break;
            case 1:
            case 2:
                item = formatDuration(item);
                break;
        }
        return item;
    }, [[0, 0]], 3);

    // Create statistics table
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 33490, 247, 0.7375335921170498, 3556.964138548818, 1, 242901, 163.0, 7656.8000000000175, 29453.9, 145373.7700000002, 46.6935943750183, 29.06865427351306, 30.697305946740858], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["SignOut", 1392, 0, 0.0, 11.860632183908056, 3, 90, 9.0, 21.0, 27.0, 56.069999999999936, 2.120848766113956, 4.185776715152642, 0.43286854699005545], "isController": false}, {"data": ["Add Education", 1692, 0, 0.0, 593.3930260047284, 53, 167505, 62.0, 223.4000000000001, 385.0, 1991.7799999999902, 2.5615715816947553, 0.5179123134836767, 1.7936004141358783], "isController": false}, {"data": ["Delete Education", 1692, 0, 0.0, 546.427895981087, 3, 167104, 59.0, 147.70000000000005, 272.849999999999, 1740.0799999999972, 2.5616026064155126, 0.5453530076121382, 1.530815874971992], "isController": false}, {"data": ["Add Certification", 1692, 0, 0.0, 481.53782505910146, 52, 167284, 61.0, 230.7000000000005, 809.3999999999996, 1609.4899999999996, 2.5615909720571604, 0.5180108553473212, 1.6735394143615627], "isController": false}, {"data": ["Add Description", 1692, 0, 0.0, 860.1761229314432, 154, 166269, 174.0, 703.0000000000009, 1729.6999999999998, 2739.9799999999927, 2.5610675352903605, 0.5502293532850383, 1.7882453981763746], "isController": false}, {"data": ["Update Education", 1692, 4, 0.2364066193853428, 427.4775413711586, 6, 154519, 214.0, 395.70000000000005, 928.8999999999987, 2435.21, 2.561412591795305, 0.5493381196135779, 1.9384271779954676], "isController": false}, {"data": ["Add Language", 1692, 0, 0.0, 623.6979905437358, 53, 167311, 61.0, 221.70000000000005, 542.6999999999998, 4482.689999999875, 2.5615056566583254, 0.5179595992310941, 1.5033836910660678], "isController": false}, {"data": ["Enable/Disable ShareSkill", 1690, 0, 0.0, 11655.39704142013, 153, 172618, 493.5, 28889.400000000172, 127098.4, 154432.9, 2.5018912225069543, 0.464218097926095, 1.4073138126601616], "isController": false}, {"data": ["Add Skill", 1692, 0, 0.0, 6429.79905437352, 55, 167137, 68.0, 476.5000000000007, 51902.199999999975, 157966.49, 2.5609667498123168, 0.5178240216070218, 1.558088169075267], "isController": false}, {"data": ["SearchSkill", 1648, 0, 0.0, 20774.458131067968, 1256, 242901, 10074.5, 69606.40000000001, 98978.29999999999, 112661.18999999997, 2.3072701410122867, 16.10357392364728, 1.6763759618292395], "isController": false}, {"data": ["Update Certification", 1692, 230, 13.59338061465721, 439.3067375886526, 5, 59773, 116.0, 549.7, 1314.199999999999, 2932.259999999999, 2.561381571677054, 0.519718863016042, 1.8106929733068313], "isController": false}, {"data": ["Delete Language", 1692, 3, 0.1773049645390071, 1180.156619385344, 5, 167592, 59.0, 105.70000000000005, 202.0, 47680.16999999963, 2.5615521916259043, 0.4873964053111332, 1.5283197137329172], "isController": false}, {"data": ["View ShareSkill", 1690, 0, 0.0, 85.19940828402368, 1, 46818, 4.0, 18.0, 34.899999999999636, 328.4499999999996, 2.5826683089788225, 0.44389611560573505, 1.5460699935586113], "isController": false}, {"data": ["SignIn", 1692, 0, 0.0, 256.40661938534294, 193, 2084, 231.0, 309.70000000000005, 361.3499999999999, 726.4899999999996, 2.5594055886405958, 1.2297144039171612, 0.8866557799909544], "isController": false}, {"data": ["Update Language", 1692, 0, 0.0, 1338.0786052009435, 56, 158593, 227.0, 1103.9000000000008, 1592.0999999999995, 10449.43999999997, 2.5608892279191413, 0.5380355240136339, 1.5979484791314773], "isController": false}, {"data": ["Delete Certification", 1692, 10, 0.5910165484633569, 325.51595744680816, 6, 158193, 59.0, 178.10000000000014, 535.0, 1400.3499999999997, 2.561610362713127, 0.5313269277934723, 1.540684867423038], "isController": false}, {"data": ["Add ShareSkill", 1692, 0, 0.0, 10397.320330969264, 108, 176126, 674.5, 22630.300000000003, 96890.29999999996, 145260.61, 2.5056235848483817, 0.5481051591855836, 4.10589489782772], "isController": false}, {"data": ["Delete ShareSkill", 1690, 0, 0.0, 10198.25680473373, 210, 175798, 433.5, 46037.1, 63785.9, 153958.18, 2.497923324100378, 0.4878756492383551, 1.4709450824536405], "isController": false}, {"data": ["Delete Skill", 1692, 0, 0.0, 349.2742316784868, 5, 165363, 59.0, 125.0, 201.0, 1982.1899999999735, 2.5614823898085697, 0.4730618925562595, 1.63083298561437], "isController": false}, {"data": ["Update Skill", 1692, 0, 0.0, 3998.0330969267166, 56, 168630, 227.0, 899.9000000000003, 1553.349999999998, 155145.98, 2.5607535758986475, 0.5189219158394414, 1.6303689676810802], "isController": false}]}, function(index, item){
        switch(index){
            // Errors pct
            case 3:
                item = item.toFixed(2) + '%';
                break;
            // Mean
            case 4:
            // Mean
            case 7:
            // Median
            case 8:
            // Percentile 1
            case 9:
            // Percentile 2
            case 10:
            // Percentile 3
            case 11:
            // Throughput
            case 12:
            // Kbytes/s
            case 13:
            // Sent Kbytes/s
                item = item.toFixed(2);
                break;
        }
        return item;
    }, [[0, 0]], 0, summaryTableHeader);

    // Create error table
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["500/Internal Server Error", 247, 100.0, 0.7375335921170498], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 33490, 247, "500/Internal Server Error", 247, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Education", 1692, 4, "500/Internal Server Error", 4, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Certification", 1692, 230, "500/Internal Server Error", 230, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Delete Language", 1692, 3, "500/Internal Server Error", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Delete Certification", 1692, 10, "500/Internal Server Error", 10, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
