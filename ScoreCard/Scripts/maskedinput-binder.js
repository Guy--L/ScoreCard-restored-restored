$(function () {
    $.extend($.inputmask.defaults, {
        showMaskOnFocus: true,
        showMaskOnHover: true,
        clearMaskOnLostFocus: false
    });
    $('[mask]').each(function (e) {
        $(this).inputmask({
            alias: "numeric",
            autoGroup: !0,
            digits: $(this).attr('mask'),
            digitsOptional: !1,
            clearMaskOnLostFocus: !1
        });
    });
});