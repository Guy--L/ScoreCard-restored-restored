$(function () {
    $.extend($.inputmask.defaults, {
        placeholder: ' ',
        clearMaskOnLostFocus: true
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