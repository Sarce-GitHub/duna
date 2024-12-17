// WAITING

function wait()
{
    $("#modal").css("display", "block");
}

$(".chk-wait").change(function () {
    if ($(this).val().trim() != "") wait();
});

$(".modal-check").click(function () {
    wait();
});

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}

function AlertErr(_t, _c) {
    $.alert({
        type: 'red',
        title: _t,
        content: _c
    });
}

function AlertOk(_t, _c) {
    $.alert({
        type: 'green',
        title: _t,
        content: _c
    });
}

$(() => {
    if (window.matchMedia("(max-width: 768px)").matches) {
        $('#CPH_Bottom_btn_Indietro').parent().removeClass('text-center');
    }
});
