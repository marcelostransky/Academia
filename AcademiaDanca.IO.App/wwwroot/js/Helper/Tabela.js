﻿(function ($) {
    removeLinha = function (item) {
        var tr = $("#" + item).closest('tr');
        tr.fadeOut(400, function () { tr.remove(); });
        return false;
    };
    existeLinha = function (item) { return $("#" + item + " td:contains-ci('" + $("#Turmas").val() + "')").parent("tr").length };
})(jQuery);
$.extend($.expr[":"], {
    "contains-ci": function (elem, i, match, array) {
        return (elem.textContent || elem.innerText || $(elem).text() || "").toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
    }
});