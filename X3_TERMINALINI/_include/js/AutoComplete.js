//****************************************************
// Funzione generale AUTOCOMPLETE
// Campi gestiti data-autocomplete-data (Campo descrizione principale)
// Campi extra testo: data-autocomplete-str1 edata-autocomplete-str2"
// Campi extra numerico: data-autocomplete-dec1 e data-autocomplete-dec2
//****************************************************

// FUNZIONE AUTOCOMPLETE
// MODIFICA IL SOURCE DEL CAMPO AUTOCOMPLETE
$(".form-autocomplete").focus(function (e) {
    $(this).autocomplete("option", "source", "AutoCompleteClienti-Data.aspx");
    //
    $(this).val('');    
});

// FUNZIONE AUTOCOMPLETE 
// ESEGUE LA QUERY IN BASE AI PARAMATRI
$(".form-autocomplete").autocomplete({
    source: '',
    minLength: 3,
    delay: 500,
    select: function (event, ui) {
        // prevent autocomplete from updating the textbox
        event.preventDefault();
        // 
        $(this).val(ui.item.label);
        //
        $("#CPH_Main_txtAutoCodiceCliente").val(ui.item.value);
        __doPostBack("txtAutoCodiceCliente", "TextChanged");
        $("#CPH_Main_txtAutoCodiceCliente").hide();
        //
        //
    }
});

$(".form-autocomplete-sped").focus(function (e) {
    $(this).autocomplete("option", "source", "AutoCompleteClienti-Data.aspx?SPED=true");
    //
    $(this).val('');
});

// FUNZIONE AUTOCOMPLETE 
// ESEGUE LA QUERY IN BASE AI PARAMATRI
$(".form-autocomplete-sped").autocomplete({
    source: '',
    minLength: 3,
    delay: 500,
    select: function (event, ui) {
        // prevent autocomplete from updating the textbox
        event.preventDefault();
        // 
        $(this).val(ui.item.label);
        //
        $("#CPH_Main_txtAutoCodiceClienteSped").val(ui.item.value);
        __doPostBack("txtAutoCodiceClienteSped", "TextChanged");
        $("#CPH_Main_txtAutoCodiceClienteSped").hide();
        //
        //
    }
});



