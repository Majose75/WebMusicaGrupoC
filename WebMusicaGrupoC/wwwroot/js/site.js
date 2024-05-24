// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('th').click(function () {
    var table = $(this).parents('table').eq(0) 
    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()))
    this.asc = !this.asc
    if (!this.asc) { rows = rows.reverse() }
    for (var i = 0; i < rows.length; i++) { table.append(rows[i]) }
})
function comparer(index) {
    return function (a, b) {
        var valA = getCellValue(a, index), valB = getCellValue(b, index)
        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.toString().localeCompare(valB)
    }
}
function getCellValue(row, index) { return $(row).children('td').eq(index).text() }



/*El evento onclick que escucha al clickar sobre el head de alguna columna*/
////$('th').click(function () {
////    /*$(".ordenable").on("click", function () {*/
    
////    var table = $(this).parents('table').eq(0)
   
////    var rows = table.find('tr:gt(0)').toArray().sort(comparer($(this).index()))
   
////    this.asc = !this.asc
////    if (!this.asc) {
////        rows = rows.reverse()
////    }
////    for (var i = 0; i < rows.length; i++) {
////        table.append(rows[i])
////    }
////});

/////*El comparador, usa una regex para corrobar si el valor de la celda es una fecha, ya
////que requeire de un comparador custom*/
////function comparer(index) {
////    var dateFormat = /(^\d{1,4}[\.|\\/|-]\d{1,2}[\.|\\/|-]\d{1,4})(\s*(?:0?[1-9]:[0-5]|1(?=[012])\d:[0-5])\d\s*[ap]m)?$/
////    return function (a, b) {
////        var valA = getCellValue(a, index), valB = getCellValue(b, index)
////        if (dateFormat.test(valA) && dateFormat.test(valB)) {
////            return compareDate(valA, valB);
////        }
////        return $.isNumeric(valA) && $.isNumeric(valB) ? valA - valB : valA.localeCompare(valB)
////    }
////}

////function getCellValue(row, index) {
////    return $(row).children('td').eq(index).text()
////}

/////*Esta función se puede obviar si usamos momentjs ex: moment(valA,'dd/MM/yyyy').isAfter(...)*/
////function compareDate(valA, valB) {
////    var dateA = new Date(valA.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"));
////    var dateB = new Date(valB.replace(/(\d{2})\/(\d{2})\/(\d{4})/, "$2/$1/$3"));
////    return dateA > dateB ? 1 : -1;
////}


/// Para añadir inputbox y filtrar elementos
$("#searchInput").keyup(function () {
    //split the current value of searchInput
    var data = this.value.toUpperCase().split(" ");
    //create a jquery object of the rows
    var jo = $("tbody").find("tr");
    if (this.value == "") {
        jo.show();
        return;
    }
    //hide all the rows
    jo.hide();

    //Recusively filter the jquery object to get results.
    jo.filter(function (i, v) {
        var $t = $(this);
        for (var d = 0; d < data.length; ++d) {
            if ($t.text().toUpperCase().indexOf(data[d]) > -1) {
                return true;
            }
        }
        return false;
    })
        //show the rows that match.
        .show();
}).focus(function () {
    this.value = "";
    $(this).css({
        "color": "black"
    });
    $(this).unbind('focus');
}).css({
    "color": "#C0C0C0"
});