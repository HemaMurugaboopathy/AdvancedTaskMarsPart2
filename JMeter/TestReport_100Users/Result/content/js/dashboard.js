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

    var data = {"OkPercent": 99.41687068773648, "KoPercent": 0.583129312263521};
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
    createTable($("#apdexTable"), {"supportsControllersDiscrimination": true, "overall": {"data": [0.8934342310260405, 500, 1500, "Total"], "isController": false}, "titles": ["Apdex", "T (Toleration threshold)", "F (Frustration threshold)", "Label"], "items": [{"data": [0.9932236205227493, 500, 1500, "SignOut"], "isController": false}, {"data": [1.0, 500, 1500, "Add Education"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Education"], "isController": false}, {"data": [0.996014171833481, 500, 1500, "Add Certification"], "isController": false}, {"data": [0.999113475177305, 500, 1500, "Add Description"], "isController": false}, {"data": [0.9951284322409212, 500, 1500, "Update Education"], "isController": false}, {"data": [1.0, 500, 1500, "Add Language"], "isController": false}, {"data": [0.702846975088968, 500, 1500, "Enable/Disable ShareSkill"], "isController": false}, {"data": [0.982316534040672, 500, 1500, "Add Skill"], "isController": false}, {"data": [0.002234137622877569, 500, 1500, "SearchSkill"], "isController": false}, {"data": [0.887511071744907, 500, 1500, "Update Certification"], "isController": false}, {"data": [0.9982285208148804, 500, 1500, "Delete Language"], "isController": false}, {"data": [1.0, 500, 1500, "View ShareSkill"], "isController": false}, {"data": [0.9898499558693733, 500, 1500, "SignIn"], "isController": false}, {"data": [0.9969026548672566, 500, 1500, "Update Language"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Certification"], "isController": false}, {"data": [0.5807453416149069, 500, 1500, "Add ShareSkill"], "isController": false}, {"data": [0.7439893143365984, 500, 1500, "Delete ShareSkill"], "isController": false}, {"data": [1.0, 500, 1500, "Delete Skill"], "isController": false}, {"data": [0.9986737400530504, 500, 1500, "Update Skill"], "isController": false}]}, function(index, item){
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
    createTable($("#statisticsTable"), {"supportsControllersDiscrimination": true, "overall": {"data": ["Total", 22465, 131, 0.583129312263521, 590.063788114845, 1, 21172, 112.0, 890.9000000000015, 3643.9000000000015, 10259.960000000006, 98.44779834525312, 62.46780483238895, 64.51007745122529], "isController": false}, "titles": ["Label", "#Samples", "FAIL", "Error %", "Average", "Min", "Max", "Median", "90th pct", "95th pct", "99th pct", "Transactions/s", "Received", "Sent"], "items": [{"data": ["SignOut", 1033, 0, 0.0, 35.1055179090029, 7, 1406, 16.0, 31.0, 42.0, 1202.4199999999987, 5.271591581783666, 10.40418611990702, 1.0759400787038929], "isController": false}, {"data": ["Add Education", 1129, 0, 0.0, 69.63773250664302, 54, 471, 61.0, 75.0, 120.5, 234.0, 5.536865026041411, 1.1194984990019912, 3.8768869371793864], "isController": false}, {"data": ["Delete Education", 1129, 0, 0.0, 63.2400354295837, 4, 246, 59.0, 68.0, 95.5, 172.0, 5.543988529001592, 1.1802919555695233, 3.3130541226355796], "isController": false}, {"data": ["Add Certification", 1129, 0, 0.0, 70.04605845881304, 52, 666, 60.0, 72.0, 113.0, 239.90000000000123, 5.545649685877505, 1.1210444189225033, 3.623085585793018], "isController": false}, {"data": ["Add Description", 1128, 0, 0.0, 181.76329787234047, 156, 628, 173.0, 190.0, 248.0999999999999, 408.97000000000025, 5.5451774653426416, 1.191346721069708, 3.871876843476551], "isController": false}, {"data": ["Update Education", 1129, 3, 0.2657218777679362, 202.22497785651026, 4, 682, 223.0, 250.0, 293.5, 448.00000000000045, 5.540397298994974, 1.182364459013819, 4.192826735913454], "isController": false}, {"data": ["Add Language", 1130, 0, 0.0, 67.27522123893809, 53, 470, 61.0, 72.0, 114.80000000000018, 187.69000000000005, 5.53035085916203, 1.1182863437039647, 3.245840689801152], "isController": false}, {"data": ["Enable/Disable ShareSkill", 1124, 0, 0.0, 801.4466192170817, 154, 8860, 388.5, 2275.5, 3033.5, 4097.5, 5.635441108637667, 1.0456384869542548, 3.169935623608688], "isController": false}, {"data": ["Add Skill", 1131, 0, 0.0, 337.4456233421751, 60, 15205, 73.0, 102.60000000000014, 134.79999999999973, 15014.400000000012, 5.144042280298, 1.0401177236930674, 3.12962728576724], "isController": false}, {"data": ["SearchSkill", 1119, 0, 0.0, 7105.2529043789145, 1296, 21172, 6860.0, 11367.0, 12154.0, 13415.399999999994, 5.501529021917619, 38.39787882777854, 3.9972046799870204], "isController": false}, {"data": ["Update Certification", 1129, 126, 11.160318866253322, 121.66784765279016, 103, 580, 116.0, 133.0, 163.5, 222.3000000000004, 5.5472845820865455, 1.1403377551308698, 3.922103552178378], "isController": false}, {"data": ["Delete Language", 1129, 2, 0.1771479185119575, 64.97165633303803, 4, 337, 59.0, 69.0, 115.5, 186.0, 5.53344573400251, 1.0528995221950477, 3.3014649279892367], "isController": false}, {"data": ["View ShareSkill", 1124, 0, 0.0, 7.515124555160136, 1, 148, 4.0, 7.0, 29.0, 104.0, 5.638409406712917, 0.9691016167787827, 3.3753368811670104], "isController": false}, {"data": ["SignIn", 1133, 0, 0.0, 256.2797881729921, 202, 2474, 235.0, 270.0, 299.29999999999995, 796.6600000000001, 5.064139811379788, 2.433160924998883, 1.754220174708354], "isController": false}, {"data": ["Update Language", 1130, 0, 0.0, 237.45663716814155, 55, 719, 226.0, 255.89999999999998, 345.45000000000005, 471.66000000000076, 5.530107274293321, 1.161541414936673, 3.450687149474395], "isController": false}, {"data": ["Delete Certification", 1128, 0, 0.0, 64.44414893617021, 51, 444, 59.0, 71.0, 107.0, 160.97000000000025, 5.546104451633839, 1.1536330548808669, 3.336328459185981], "isController": false}, {"data": ["Add ShareSkill", 1127, 0, 0.0, 1118.770186335404, 110, 5190, 641.0, 3270.2, 3510.7999999999997, 4749.240000000002, 5.557336226238319, 1.2156672994896325, 9.106650573855372], "isController": false}, {"data": ["Delete ShareSkill", 1123, 0, 0.0, 708.2154942119319, 210, 10612, 392.0, 1732.2000000000005, 2506.6, 3582.199999999999, 5.635852654822845, 1.100752471645087, 3.318768702009937], "isController": false}, {"data": ["Delete Skill", 1130, 0, 0.0, 65.26637168141592, 2, 263, 58.0, 68.0, 103.45000000000005, 219.69000000000005, 5.526726368353867, 1.0206905290006407, 3.518731490968938], "isController": false}, {"data": ["Update Skill", 1131, 0, 0.0, 236.35985853227237, 58, 508, 225.0, 245.0, 329.99999999999864, 482.68000000000006, 5.520169851380042, 1.1174952183541988, 3.5145573278912563], "isController": false}]}, function(index, item){
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
    createTable($("#errorsTable"), {"supportsControllersDiscrimination": false, "titles": ["Type of error", "Number of errors", "% in errors", "% in all samples"], "items": [{"data": ["500/Internal Server Error", 131, 100.0, 0.583129312263521], "isController": false}]}, function(index, item){
        switch(index){
            case 2:
            case 3:
                item = item.toFixed(2) + '%';
                break;
        }
        return item;
    }, [[1, 1]]);

        // Create top5 errors by sampler
    createTable($("#top5ErrorsBySamplerTable"), {"supportsControllersDiscrimination": false, "overall": {"data": ["Total", 22465, 131, "500/Internal Server Error", 131, "", "", "", "", "", "", "", ""], "isController": false}, "titles": ["Sample", "#Samples", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors", "Error", "#Errors"], "items": [{"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Education", 1129, 3, "500/Internal Server Error", 3, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": ["Update Certification", 1129, 126, "500/Internal Server Error", 126, "", "", "", "", "", "", "", ""], "isController": false}, {"data": ["Delete Language", 1129, 2, "500/Internal Server Error", 2, "", "", "", "", "", "", "", ""], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}, {"data": [], "isController": false}]}, function(index, item){
        return item;
    }, [[0, 0]], 0);

});
