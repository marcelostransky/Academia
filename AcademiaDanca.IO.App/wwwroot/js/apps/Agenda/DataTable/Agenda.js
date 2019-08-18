
$(document).ready(function () {
    MontarAgendaDataTable();
});
function MontarAgendaDataTable() {
    var dNow = new Date();
    var data = dNow.getDate() + '/' + dNow.getMonth() + '/' + dNow.getFullYear();
    if ($("#inputData").val() !== '') {
        data = $("#inputData").val();
    }
    if (isDate(data)) {
        $('#agenda-datatable').DataTable(
            {
                "sEmptyTable": "Nenhum registro encontrado",
                "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                "sInfoPostFix": "",
                "sInfoThousands": ".",
                "sLengthMenu": "_MENU_ resultados por página",
                "sLoadingRecords": "Carregando...",
                "sProcessing": "Processando...",
                "sZeroRecords": "Nenhum registro encontrado",
                "sSearch": "Pesquisar",
                "oPaginate": {
                    "sNext": "Próximo",
                    "sPrevious": "Anterior",
                    "sFirst": "Primeiro",
                    "sLast": "Último"
                },
                "oAria": {
                    "sSortAscending": ": Ordenar colunas de forma ascendente",
                    "sSortDescending": ": Ordenar colunas de forma descendente"
                },
                "bDestroy": true,
                "bProcessing": true,
                "bServerSide": true,
                "sAjaxSource": "/Calendario/Agenda/DiaSemana?data=" + data,
                "aoColumns": [
                    {
                        "sTitle": "Data",
                        "mDataProp": "data"
                    },
                    {
                        "sTitle": "Codigo",
                        "mDataProp": "codTurma"
                    },
                    {
                        "sTitle": "Turma",
                        "mDataProp": "desTurma",
                        "bSortable": true
                    },
                    {
                        "sTitle": "Tipo",
                        "mDataProp": "tipoTurma"
                    },
                    {
                        "sTitle": "Dia",
                        "mDataProp": "siglaDia"
                    },
                    {
                        "sTitle": "Hora",
                        "mDataProp": "hora"
                    },
                    {
                        "sTitle": "Professor",
                        "mDataProp": "professor"
                    },
                    {
                        "sTitle": "Total de Alunos",
                        "mDataProp": "totalAluno"
                    }

                ],
                dom: 'rt<"dataTables_footer"ip>',

                language: {
                    lengthMenu: "Exibe _MENU_ Registros por pagina",
                    search: "Procurar",
                    paginate: { "previous": "Retornar", "next": "Avancar" },
                    zeroRecords: "Nenhum registro encontrado",
                    info: "Exibindo pagina _PAGE_ de _PAGES_",
                    infoEmpty: "Sem registros",
                    infoFiltered: "(filtrado de _MAX_ regitros totais)",
                    processing: "Aguarde Estamos Processando..."
                },

                "filter": true,            // habilita o filtro(search box)
                "lengthMenu": [[3, 5, 10, 25, 50, -1], [3, 5, 10, 25, 50, "Todos"]],

                pageLength: 10,
                scrollY: 'auto',
                scrollX: false,
                responsive: true,
                autoWidth: false,
                scrollCollapse: true,
                initComplete: function () {
                    var api = this.api(),
                        searchBox = $('#inputData');

                    // Bind an external input as a table wide search box
                    if (data.length >= 10) {
                        searchBox.on('keypress', function (event) {
                            api.search(event.target.value).draw();
                        });
                    }


                }
            }
        );
    }
    else {
        var msg = "Informe uma data valida. Ex.: " + dNow.getDate() + '/' + IncluirZero(dNow.getMonth())+ dNow.getMonth() + '/' + dNow.getFullYear();
        PNotify.error({
            title: msg
        });
    }
}
function IncluirZero(mes) {
    return mes <= 9 ? '0' : '';
}