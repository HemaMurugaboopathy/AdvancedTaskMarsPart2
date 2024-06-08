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

    var data = {"OkPercent": 99.04184866845146, "KoPercent": 0.9581513315485416};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.7832323516979005, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [1.0, 500, 1500, "SignOut"], "isController": false}, {"data": [0.9816197587593337, 500, 1500, "Add Education"], "isController": false}, {"data": [0.9808473592571096, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.9784256559766764, 500, 1500, "Add Certification"], "isController": false}, {"data": [0.9656601539372409, 500, 1500, "Add Description"], "isController": false}, {"data": [0.9697406340057637, 500, 1500, "Update Education"], "isController": false}, {"data": [0.9841179807146909, 500, 1500, "Add Language"], "isController": false}, {"data": [0.208557749850389, 500, 1500, "Enable/Disable ShareSkill"], "isController": false}, {"data": [0.7858129894058038, 500, 1500, "Add Skill"], "isController": false}, {"data": [0.0038898862956313583, 500, 1500, "SearchSkill"], "isController": false}, {"data": [0.8405627198124267, 500, 1500, "Update Certification"], "isController": false}, {"data": [0.9768967484312607, 500, 1500, "Delete Language"], "isController": false}, {"data": [0.9856545128511656, 500, 1500, "View ShareSkill"], "isController": false}, {"data": [0.913634269921695, 500, 1500, "SignIn"], "isController": false}, {"data": [0.94968732234224, 500, 1500, "Update Language"], "isController": false}, {"data": [0.9755162241887906, 500, 1500, "Delete Certification"], "isController": false}, {"data": [0.10609683203825464, 500, 1500, "Add ShareSkill"], "isController": false}, {"data": [0.16906044284859365, 500, 1500, "Delete ShareSkill"], "isController": false}, {"data": [0.9772599663110612, 500, 1500, "Delete Skill"], "isController": false}, {"data": [0.7949342425718461, 500, 1500, "Update Skill"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 35485, 340, 0.9581513315485416, 6783.467634211644, 1, 323374, 121.0, 23791.500000000007, 50503.0, 248000.3400000001, 45.20508855025236, 28.28400699406544, 29.232277001314053], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["SignOut", 1671, 0, 0.0, 13.684619988031121, 4, 182, 8.0, 24.0, 43.0, 104.83999999999992, 2.300062491225076, 4.539478803482303, 0.4694463483066805], "isController": false}, {"data": ["Add Education", 1741, 0, 0.0, 1211.072946582424, 53, 288464, 64.0, 129.0, 198.49999999999932, 4825.939999999986, 2.2246983687249067, 0.449799163390103, 1.5577233695075763], "isController": false}, {"data": ["Delete Education", 1723, 2, 0.11607661056297155, 1201.2222867092282, 3, 300343, 60.0, 90.0, 146.39999999999986, 44023.159999999756, 2.2003422470819602, 0.4731188781830256, 1.3134021099915716], "isController": false}, {"data": ["Add Certification", 1715, 0, 0.0, 1714.6239067055392, 53, 276959, 62.0, 117.0, 185.0, 51747.03999999939, 2.1931815711364515, 0.4435880070853192, 1.4328500694241075], "isController": false}, {"data": ["Add Description", 1689, 0, 0.0, 3348.7122557726466, 155, 308556, 177.0, 268.0, 488.0, 247594.2, 2.157446319312274, 0.4635138576647464, 1.5064200374104257], "isController": false}, {"data": ["Update Education", 1735, 4, 0.23054755043227665, 2575.397694524496, 5, 309250, 127.0, 278.0, 380.59999999999945, 54855.639999999265, 2.214122913811278, 0.47669244811805933, 1.675608152008152], "isController": false}, {"data": ["Add Language", 1763, 0, 0.0, 367.6018150879184, 52, 67105, 63.0, 157.60000000000014, 245.0, 4043.0399999997685, 2.250216980650431, 0.4550959740516669, 1.3206839896200286], "isController": false}, {"data": ["Enable/Disable ShareSkill", 1671, 0, 0.0, 8840.268701376424, 160, 114163, 2866.0, 25342.399999999965, 51348.99999999999, 66295.56, 2.299467448292945, 0.42665899919497996, 1.2934504396647812], "isController": false}, {"data": ["Add Skill", 2171, 65, 2.9940119760479043, 31656.37171810228, 55, 299344, 72.0, 172588.8, 249890.20000000007, 257445.52000000002, 2.770114913196261, 0.7137166862101613, 1.6348744955162673], "isController": false}, {"data": ["SearchSkill", 1671, 0, 0.0, 36163.62417713952, 1305, 180434, 30097.0, 95931.0, 102925.2, 141084.75999999998, 2.2957837814073114, 16.023404966521536, 1.6680304036787497], "isController": false}, {"data": ["Update Certification", 1706, 230, 13.481828839390387, 2199.6787807737396, 4, 309902, 118.0, 196.5999999999999, 292.64999999999986, 13750.780000000079, 2.178262573848336, 0.44224761249781347, 1.5397406035293983], "isController": false}, {"data": ["Delete Language", 1753, 11, 0.6274957216200798, 1853.093553907587, 6, 309191, 60.0, 96.60000000000014, 134.29999999999995, 26971.720000001747, 2.2377933034705584, 0.42743129644250866, 1.334334521376479], "isController": false}, {"data": ["View ShareSkill", 1673, 0, 0.0, 545.5570830842797, 2, 192107, 9.0, 27.0, 51.0, 3603.7799999999997, 2.2069289389987956, 0.379315911390418, 1.32114007774049], "isController": false}, {"data": ["SignIn", 2171, 0, 0.0, 417.75863657300783, 190, 4674, 254.0, 596.5999999999999, 1369.2000000000012, 3169.5600000000054, 2.968807690729826, 1.4264193201553461, 1.0285041883469854], "isController": false}, {"data": ["Update Language", 1759, 0, 0.0, 2006.064809550881, 57, 308595, 232.0, 452.0, 763.0, 17562.60000000112, 2.2437455115522917, 0.47144894825226386, 1.4000003049433833], "isController": false}, {"data": ["Delete Certification", 1695, 12, 0.7079646017699115, 682.474336283186, 5, 283730, 60.0, 85.0, 148.59999999999945, 4760.079999999997, 2.1657411690146966, 0.44896380425213567, 1.3024693122781552], "isController": false}, {"data": ["Add ShareSkill", 1673, 0, 0.0, 9166.54632396892, 116, 73957, 4583.0, 25043.20000000002, 44802.1, 60118.0, 2.2887897186416657, 0.5006727509528643, 3.750575339727261], "isController": false}, {"data": ["Delete ShareSkill", 1671, 0, 0.0, 9623.50029922205, 217, 138662, 4433.0, 20942.19999999993, 55116.99999999995, 66971.64, 2.299280769564182, 0.44907827530550437, 1.3539710000460956], "isController": false}, {"data": ["Delete Skill", 1781, 3, 0.16844469399213924, 1257.6047164514318, 1, 288614, 60.0, 131.79999999999995, 222.89999999999986, 63531.9, 2.2724691825873484, 0.42808039952266663, 1.4441126572450425], "isController": false}, {"data": ["Update Skill", 2053, 13, 0.6332196785192401, 15444.3765221627, 54, 323374, 239.0, 50487.2, 67605.9, 301153.04000000004, 2.6172704151554362, 0.5727544882745521, 1.6525824609895399], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 84, 24.705882352941178, 0.23671974073552204], "isController": false}, {"data": ["500/Internal Server Error", 256, 75.29411764705883, 0.7214315908130196], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 35485, 340, "500/Internal Server Error", 256, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 84, "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Delete Education", 1723, 2, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Education", 1735, 4, "500/Internal Server Error", 4, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Add Skill", 2171, 65, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 65, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Certification", 1706, 230, "500/Internal Server Error", 230, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Delete Language", 1753, 11, "500/Internal Server Error", 10, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 1, "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Delete Certification", 1695, 12, "500/Internal Server Error", 12, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Delete Skill", 1781, 3, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Update Skill", 2053, 13, "Non HTTP response code: org.apache.http.NoHttpResponseException/Non HTTP response message: localhost:60190 failed to respond", 13, "", "", "", "", "", "", "", ""], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
