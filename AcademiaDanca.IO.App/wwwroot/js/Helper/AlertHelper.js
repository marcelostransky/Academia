var academia = avalia || {};

academia.common = function () { };

academia.common.alerts = new function () {
    function IncluirDivModalBootstrap(mensagem) {
        var htmlM = "<div class=\"modal fade\" id=\"myModal\"><div class=\"modal-dialog\"><div class=\"modal-content\"><div class=\"modal-header\"><button type=\"button\" class=\"close\" data-dismiss=\"modal\" aria-hidden=\"true\">&times;</button><h4 class=\"modal-title\">Mensagem do Sistema</h4></div><div class=\"modal-body\"><p><label id=\"modalMsg\"></label></p></div><div class=\"modal-footer\"><button type=\"button\" class=\"btn btn-default\" data-dismiss=\"modal\">Fechar</button></div></div><!-- /.modal-content --></div><!-- /.modal-dialog --></div><!-- /.modal -->";
        return htmlM;

    }

    this.Message = function (title, html, callback) {
        $('#myModal').remove();
        $("body").append(IncluirDivModalBootstrap());

        $("#modalMsg").html(html);
        $('#myModal').modal({
            keyboard: false,
            show: true
        });
    };

    this.Confirm = function (title, html, callback) {
        $("body").append("<div id=\"dialog-message\"></div>");
        $("#dialog-message").html(html);
        $("#dialog-message").dialog({
            modal: true,
            resizable: false,
            draggable: true,
            closeOnEscape: true,
            title: title,
            buttons: {
                "Confirmar": function () {
                    $(this).dialog("close");
                    if (callback) callback();
                },
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    };
};