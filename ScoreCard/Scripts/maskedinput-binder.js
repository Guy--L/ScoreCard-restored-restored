$(function () {
    $.extend($.inputmask.defaults, {
        showMaskOnFocus: false,
        showMaskOnHover: false,
        clearMaskOnLostFocus: true
    });
    $('[mask]').each(function (e) {
        $(this).inputmask("decimal", { digits: $(this).attr('mask') });
    });
});