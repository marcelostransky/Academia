(function ($) {
    removeLinha = function (item) {
        var tr = $("#" + item).closest('tr');
        tr.fadeOut(400, function () { tr.remove(); });
        return false;
    }
})(jQuery);