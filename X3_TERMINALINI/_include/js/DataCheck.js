////////////////////////////////////////
// CONTROLLI CSS SU MODIFICA
////////////////////////////////////////

// CONTROLLO DATA
$(".date-validator").change(function () {
    $(this).removeClass("ui-state-error");
    if (!func_check_date($(this))) {
        $(this).addClass("ui-state-error");
        AlertErr($(this).val(), "Data non valida");
        $(this).val("");
        $(this).focus();
    }
});

// CONTROLLO EURO
$(".euro-validator").change(function () {
    $(this).removeClass("ui-state-error");
    var xd = func_check_decimal($(this).val());
    if (xd < 0) {
        $(this).addClass("ui-state-error");
        AlertErr("Dato Numerico €", $(this).val() + " - Dato non valida");
        $(this).val("");
        $(this).focus();
    }
    else {
        $(this).val(xd.toFixed(2).replace('.', ',').toString());
    }
});

////////////////////////////////////////
// CONTROLLI FORM
////////////////////////////////////////

// CONTROLLO CAMPO TESTO
function CheckField(id) {
    $("#" + id).removeClass("ui-state-error");
    //
    if ($("#" + id).val().trim() == "") {
        $("#" + id).addClass("ui-state-error");
        return $("#" + id).attr("data-label") + "|";
    }
    return "";
    // 
}

// CONTROLLO PIU' CAMPI TESTI
function CheckFieldArr(id) {

    var ok = false;
    for (var i = 0; i < id.length; i++) {
        $("#" + id[i]).removeClass("ui-state-error");
        if ($("#" + id[i]).val().trim() != "") ok = true;
    }
    //
    var txt = "";
    if (!ok) {

        for (var i = 0; i < id.length; i++) {
            $("#" + id[i]).addClass("ui-state-error");
            txt = txt + $("#" + id[i]).attr("data-label") + "|";
        }

    }
    return txt;
    // 
}

// CONTROLLO CAMPO DATA
function CheckFieldDate(id) {
    $("#" + id).removeClass("ui-state-error");
    //
    if ($("#" + id).val().trim() == "") {
        $("#" + id).addClass("ui-state-error");
        return $("#" + id).attr("data-label") + "|";
    }
    else {
        if (!func_check_date(id)) {
            $("#" + id).addClass("ui-state-error");
            return $("#" + id).attr("data-label") + "|";
        }
    }
    return "";
    // 
}



////////////////////////////////////////
// CONTROLLI DATI
////////////////////////////////////////

// CONTROLLO DECIMALE
function func_check_decimal(xVal) {
    if (xVal == "") return -1;
    var tempVal = xVal.toString().replace(',', '.');
    if ($.isNumeric(tempVal)) {
        var xv = parseFloat(tempVal);
        if (xv >= 0)
            return xv
        else
            return -1;
    }
    else {
        return -1;
    }
}


// CONTROLLO DATA
function func_check_date(xfld) {

    var dtfield = $("#" + xfld).val().trim();
    var currVal = dtfield.substr(8, 2) + "/" + dtfield.substr(5, 2) + "/" + dtfield.substr(0, 4);
    if (currVal == '') return false;

    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$/;
    var dtArray = currVal.match(rxDatePattern);
    if (dtArray == null) return false;

    dtDay = dtArray[1];
    dtMonth = dtArray[3];
    dtYear = dtArray[5];

    if (dtMonth < 1 || dtMonth > 12) return false;
    else if (dtDay < 1 || dtDay > 31) return false;
    else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31) return false;
    else if (dtMonth == 2) {
        var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
        if (dtDay > 29 || (dtDay == 29 && !isleap))
            return false;
    }
    //
    var dt = new Date(dtYear + '-' + dtMonth + '-' + dtDay);
    var month = ('0' + (dt.getMonth() + 1)).slice(-2);
    var day = ('0' + dt.getDate()).slice(-2);
    var year = dt.getFullYear();
    $("#" + xfld).val(year + "-" + month + "-" + day);
    return true;
}



////////////////////////////////////////
// CALCOLI
////////////////////////////////////////

// AGGIUNTA DU UN MESE
function calc_AddMonthToDate(dtfield, mm) {

    var currVal = dtfield.val().trim();
    if (currVal == '') return "";
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$/;
    var dtArray = currVal.match(rxDatePattern);
    if (dtArray == null) return "";
    //
    dtDay = parseInt(dtArray[1]);
    dtMonth = parseInt(dtArray[3]);
    dtYear = parseInt(dtArray[5]);
    //
    dtMonth = dtMonth + mm;
    if (dtMonth > 12) {
        dtYear++;
        dtMonth = dtMonth - 12;
    }
    //
    var dt = new Date(dtYear.toString() + '-' + dtMonth.toString() + '-' + dtDay.toString());
    var month = ('0' + (dt.getMonth() + 1)).slice(-2);
    var day = ('0' + dt.getDate()).slice(-2);
    var year = dt.getFullYear();
    return day + "/" + month + "/" + year;

}

// FINE ANNO
function calc_EndYearDate(dtfield) {

    var currVal = dtfield.val().trim();
    if (currVal == '') return "";
    var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{2,4})$/;
    var dtArray = currVal.match(rxDatePattern);
    if (dtArray == null) return "";
    //
    dtYear = parseInt(dtArray[5]);
    return "31/12/" + dtYear;

}