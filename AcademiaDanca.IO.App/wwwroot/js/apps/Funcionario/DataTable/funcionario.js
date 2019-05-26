
$(document).ready(function () {
    $('#funcionario-datatable').DataTable(
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
            "sAjaxSource": "/Funcionario/ObterFuncionario?nome",
            "aoColumns": [
                {
                    "sTitle": "Id",
                    "mDataProp": "idUsuario"

                },
                {
                    "sTitle": "Foto",
                    "mDataProp": "foto"
                },
                {
                    "sTitle": "Nome",
                    "mDataProp": "nomeFuncionario",
                    "bSortable": true
                },
                {
                    "sTitle": "Email",
                    "mDataProp": "email"
                },
                {
                    "sTitle": "Data Nascimento",
                    "mDataProp": "dataNascimento"
                },
                {
                    "sTitle": "Perfil",
                    "mDataProp": "perfil"
                },
                {
                    "sTitle": "Status",
                    "mDataProp": "status"
                },
                {
                    "sTitle": "Acao",
                    "mDataProp": "acao",
                    "className": 'details-control pointer',
                    "bSortable": false
                }

            ],
            dom: 'rt<"dataTables_footer"ip>',

            //columnDefs: [
            //    {
            //        // Target the id column
            //        targets: 0,
            //        width: '72px'
            //    },
            //    {
            //        // Target the image column
            //        targets: 1,
            //        filterable: false,
            //        sortable: false,
            //        width: '80px'
            //    },
            //    //{
            //    //    // Target the price column
            //    //    targets: 4,
            //    //    render: function (data, type) {
            //    //        if (type === 'display') {
            //    //            return '<div class="layout-align-start-start layout-row">' + '<i class="s-4 icon-currency-usd text-muted"></i>' + '<span>' + data + '</span>' + '</div>';
            //    //        }

            //    //        return data;
            //    //    }
            //    //},
            //    //{
            //    //    // Target the quantity column
            //    //    targets: 5,
            //    //    render: function (data, type) {
            //    //        if (type === 'display') {
            //    //            if (parseInt(data) <= 5) {
            //    //                return '<i class="quantity-indicator icon-circle s-3 text-danger mr-1"></i><span>' + data + '</span>';
            //    //            }
            //    //            else if (parseInt(data) > 5 && parseInt(data) <= 25) {
            //    //                return '<i class="quantity-indicator icon-circle s-3 text-info mr-1"></i><span>' + data + '</span>';
            //    //            }
            //    //            else {
            //    //                return '<i class="quantity-indicator icon-circle s-3 text-success mr-1"></i><span>' + data + '</span>';
            //    //            }
            //    //        }

            //    //        return data;
            //    //    }
            //    //},
            //    {
            //        // Target the status column
            //        targets: 6,
            //        filterable: false,
            //        render: function (data, type) {
            //            if (type === 'display') {
            //                if (data === 'true') {
            //                    return '<i class="icon-checkbox-marked-circle text-success"></i>';
            //                }

            //                return '<i class="icon-cancel text-danger"></i>';
            //            }

            //            if (type === 'filter') {
            //                if (data) {
            //                    return '1';
            //                }

            //                return '0';
            //            }

            //            return data;
            //        }
            //    },
            //    {
            //        // Target the actions column
            //        targets: 7,
            //        responsivePriority: 1,
            //        filterable: false,
            //        sortable: false
            //    }
            //],
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
            //"processing": true,
            //"serverSide": true,
            //"ajax": {
            //    "url": "/Funcionario/ObterFuncionario?nome=m",
            //    "type": 'GET',
            //    "dataType": "json"
            //},
            //columns: [
            //    { "data": "idUsuario", "title": "Id", "autowidth": true },
            //    {
            //        "data": "foto", "title": "Foto", "width": "50px", "render": function (data) {
            //            return '<img class="rounded img-thumbnail" style="width:90px; height:50px;" src="/Imagens/Clientes/' + data + '"/>';
            //        }
            //    },


            //    { "data": "nomeFuncionario", "title": "Nome", "autowidth": true },
            //    { "data": "email", "title": "Email", "autowidth": true },
            //    { "data": "cpf", "title": "Cpf", "autowidth": true },
            //    { "data": "dataNascimento", "title": "Data Nascimento", "autowidth": true },
            //    { "data": "perfil", "title": "Perfil", "autowidth": true },
            //    { "data": "status", "title": "Status", "autowidth": true }
            //],


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
                    searchBox.on('keyup', function (event) {
                        api.search(event.target.value).draw();
                    });
                }
            }
        }
    );

});