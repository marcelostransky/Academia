
$(document).ready(function () {

    MontarDataTableTurma();
});
function MontarDataTableTurma() {
    $('#turma-datatable').DataTable(
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
            "sAjaxSource": Url(),
            "aoColumns": [
                {
                    "sTitle": "Ano",
                    "mDataProp": "ano",
                    "bSortable": true

                },
                {
                    "sTitle": "Id",
                    "mDataProp": "idTurma",
                    "bSortable": true

                },
                {
                    "sTitle": "Codigo",
                    "mDataProp": "codTurma",
                    "bSortable": true
                },
                {
                    "sTitle": "Turma",
                    "mDataProp": "desTurma",
                    "bSortable": true
                },
                {
                    "sTitle": "Tipo",
                    "mDataProp": "desTurmaTipo"
                },
                {
                    "sTitle": "Valor",
                    "mDataProp": "valor"
                },
                {
                    "sTitle": "Status",
                    "mDataProp": "status"
                },
                {
                    "sTitle": "Professor",
                    "mDataProp": "nomeProfessor"
                },
                {
                    "sTitle": "Foto",
                    "mDataProp": "foto"
                },
                {
                    "sTitle": "Acao",
                    "mDataProp": "acao",
                    "className": 'details-control pointer',
                    "bSortable": false
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
                    searchBox = $('#products-search-input');

                // Bind an external input as a table wide search box
                if (searchBox.length > 0) {
                    searchBox.on('keypress', function (event) {
                        api.search(event.target.value).draw();
                    });
                }
            }
        }
    );
}

function Url() {

    var ano = $("#inputFiltroAno").val() === '' ? 0 : $("#inputFiltroAno").val();
    var turmaDesc = $("#inputSearch").val();
    var status = $("#status").val() === '' ? null : $("#status").val() === '1' ? true : false;
    var base = `/Turma/ListarTurmas?ano=${ano}&turmaDesc=${turmaDesc}&status=${status}`;
    return base;

}