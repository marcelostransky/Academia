
$(document).ready(function () {
    ValidaDadosBase();
});
var turmas = [];
function ValidaDadosBase() {
    if ($("#hiddenIdAluno").val().length !== 0) {
        $(".base").removeClass('visible');
        $(".base").addClass("invisible");
        $(".form").removeClass('invisible');
        $(".base").hide();
        $(".form").show();
        $("#formAluno :input").prop("disabled", true);
        $(".form").addClass("visible");
        //$(".formReset").trigger("reset");

    }
    else {
        $(".base").removeClass('invisible');
        $(".base").addClass("visible");

    }
}
function ObeterValorCursoSelecionado(alunoId) {
    var dataT = {
        alunoId: alunoId
    };
    var callback = function (data) {
        if (parseFloat(data.valor) > 0) {
            $("#tb_turma tfoot").html('');
            document.getElementById("inputValorParcela").value = data.valor;
            var tabela = $("#tb_turma");
            var linha = "<tfoot><tr><td></td><td></td><td></td><td>Total</td><td>" + data.valor + "</td><td></td ></tr > </tfoot>";
            tabela.append(linha);
            return false;
        }
        else {
            $("#tb_turma tfoot").html('');
        }
    };
    var callbackErro = function (data) {
        PNotify.error({
            text: 'Não localizado'
        });

    };

    academia.helper.rest.utils.GET("/Aluno/TotalTurmas", dataT, callback, callbackErro, $('#loader'));
}
//Mascaras de campos Inicio
$("#inputData").mask("00/00/0000");
$("#inputDescontoParcela").mask("%99");
$("#inputDescontoMatricula").mask("%99");
$("#inputVigencia").mask("9999");
$("#inputValorMatricula").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
$("#inputValorParcela").maskMoney({ showSymbol: true, symbol: "R$", decimal: ",", thousands: "." });
jQuery("#inputTelefone")
    .mask("(99) 9999-9999?9")
    .focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-9999");
        } else {
            element.mask("(99) 9999-99999");
        }
    });
jQuery(".telefone")
    .mask("(99) 9999-9999?9")
    .focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-9999");
        } else {
            element.mask("(99) 9999-99999");
        }
    });
$("#inputData").mask("00/00/0000");
jQuery("#inputCelular")
    .mask("(99) 9999-9999?9")
    .focusout(function (event) {
        var target, phone, element;
        target = (event.currentTarget) ? event.currentTarget : event.srcElement;
        phone = target.value.replace(/\D/g, '');
        element = $(target);
        element.unmask();
        if (phone.length > 10) {
            element.mask("(99) 99999-9999");
        } else {
            element.mask("(99) 9999-99999");
        }
    });
//Mascaras de campos Fim

function HabilitarTab(atual, prox) {
    $(`#${atual}`).removeClass('active');
    $(`#${prox}`).addClass('active');
    $(`#${atual}-pane`).removeClass('show');
    $(`#${prox}-pane`).addClass('show');
    $(`#${atual}-pane`).removeClass('active');
    $(`#${prox}-pane`).addClass('active');
}
function BindItemMatricula(id) {
    var dataT = {

        id: id
    };
    var callback = function (data) {
        if (data.success) {
            PNotify.success({
                title: 'Atualizado com sucesso.'
            });
            if (document.getElementById("results").innerHTML !== '') {
                document.getElementById("results").innerHTML = '';
                desligar_camera();
            }

            document.getElementById("formAlunoFoto").reset();
            HabilitarTab(tabAtual, tabProx);


            return false;
        } else {
            var msg = '';
            $.each(data.data, function (index, item) {
                msg = msg + item.property + ' > ' + item.message + '\n';
            });

            PNotify.error({
                title: 'Ops! ' + data.message + ' :-( ',
                text: msg
            });
        }
    };
    var callbackErro = function (data) {
        PNotify.error({
            text: 'Ocorreu um erro ao processar sua solicitação'
        });

    };
    academia.helper.rest.utils.GET("/Financeiro/Matricula/Item", dataT, callback, callbackErro, $('#loader'));

    return false;

}
function BuscaCep(cep) {
    if (cep.value !== '') {
        var dataT = {
            cep: cep.value
        };
        var callback = function (data) {
            if (data.success) {
                document.getElementById('inputRua').value = data.cep.result.rua;
                //document.getElementById('inputUF').value = data.cep.result.estado;
                document.getElementById('inputBairro').value = data.cep.result.bairro;
                $('#Estados  option[value="' + data.cep.result.estado + '"]').prop("selected", true);
                document.getElementById('inputComplemento').value = data.cep.result.complemento;
                document.getElementById('inputCidade').value = data.cep.result.cidade;
                return false;
            }
        };
        var callbackErro = function (data) {
            PNotify.error({
                title: 'Ops! :-(',
                text: 'CEP não localizado'
            });

        };

        academia.helper.rest.utils.GET("/Aluno/ObterLogradouroPor", dataT, callback, callbackErro, $('#loader'));

    }

}
$(function () {
    $.validator.addMethod("dateFormat",
        function (value, element) {
            return value.match(/^dd?-mm?-yyyy$/);
        });
    var jvalidate = $("#formAluno").validate({
        ignore: [],
        rules: {

            inputNome: {
                required: true,
                minlength: 6,
                maxlength: 300

            },


            inputData: {
                required: true
                //dateFormat: true

            }
        },
        messages: {


            inputNome: {
                required: "Informe o nome.",
                minlength: $.validator.format("O nome precisa ter pelo menos {0} characters"),
                maxlength: $.validator.format("O nome não pode serr superior há {0} characters")

            },
            inputData: {
                required: "Informe a data de nascimento.",

            }

        },
        submitHandler: function (form) {
            const tabAtual = 'basic-tab';
            const tabProx = 'foto-tab';
            var dataT = {

                Id: $("#hiddenIdAluno").val().length <= 0 ? 0 : $("#hiddenIdAluno").val(),
                UifId: $("#formAluno #inputId").val(),
                Nome: $("#formAluno #inputNome").val(),
                DataNascimento: $("#formAluno #inputData").val(),
                Telefone: $("#formAluno #inputTelefone").val(),
                Email: $("#formAluno #inputAddress").val(),
                Celular: $("#formAluno #inputCelular").val(),
                Cpf: $("#formAluno #inputCpf").val(),
                Foto: 'profile.jpg'
            };
            var callback = function (data) {
                if (JSON.parse(data).success) {
                    PNotify.success({
                        text: 'Cadastro realizado com sucesso.'
                    });
                    document.getElementById('hiddenIdAluno').value = JSON.parse(data).data.id;
                    document.getElementById("formAluno").reset();
                    ValidaDadosBase();
                    //$("#basic-tab-pane").removeClass('show');
                    //$("#foto-tab-pane").addClass('show');
                    HabilitarTab(tabAtual, tabProx);
                    return false;
                } else {
                    var msg = '';
                    $.each(JSON.parse(data).data, function (index, item) {
                        msg = msg + item.property + ' > ' + item.message + '\n';
                    });

                    PNotify.error({
                        title: msg
                    });


                }
            };
            var callbackErro = function (data) {
                PNotify.error({
                    title: 'Ocorreu um erro ao processar sua solicitação'
                });

            };
            academia.helper.rest.utils.POST("/Aluno/Novo", dataT, callback, callbackErro, $('#loader'));
        }
    });
});
function AdicionarLinhaTabelaTurma(turma) {
    var dados = turma.text().split("|");
    var dataT = {

        IdMatriculaGuid: $("#hiddenHashAluno").val(),
        IdTurma: turma.val(),
        Valor: dados[4]
    };
    var callback = function (data) {
        const resultado = JSON.parse(data);
        if (resultado.success) {
            $("#tb_turma tbody").html('');
            $.each(resultado.data.itens, function (index, item) {
                var tabela = $("#tb_turma tbody");
                var linha = "<tr id=\"" + item.idTurma + "\"><td>" + item.idTurma +
                    "</td><td>" + item.idTurma +
                    "</td><td>" + item.valor +
                    "</td><td>" + item.valorDesconto +
                    "%</td><td>" + item.valorCalculado +
                    "</td><td><a href=\"#\" onclick=\"javascript:DeletarTurmaAluno('" + item.idTurma + "','" + document.getElementById('hiddenIdAluno').value + "')\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir\"> <i class=\"icon-delete-forever \"></i> </a></td ></tr> ";
                tabela.append(linha);
            });
            $("#tb_turma tfoot").html('');
            if (parseFloat(resultado.data.totalDesconto) > 0) {
                $("#tb_turma tfoot").html('');
                document.getElementById("inputValorParcela").value = resultado.data.totalDesconto;
                var tabela = $("#tb_turma");
                var linha = "<tfoot><tr><td></td><td></td><td></td><td>Total</td><td>" + resultado.data.totalDesconto + "</td><td></td ></tr > </tfoot>";
                tabela.append(linha);
                return false;
            }
            return false;
        } else {
            var msg = '';
            $.each(JSON.parse(data).data, function (index, item) {
                msg = msg + item.property + ' - ' + item.message + '\n';
            });

            PNotify.error({
                
                text: msg
            });
        }
    };
    var callbackErro = function (data) {
        PNotify.error({
            text: 'Ocorreu um erro ao processar sua solicitação'
        });

    };
    academia.helper.rest.utils.POST("/Matricula/Item/Add", dataT, callback, callbackErro, $('#loader'));

    return false;
    //if (turma.val().length > 0) {
    //    $("#dadosTurmas").show();
    //    $("#dadosTurmas").removeClass('invisible');
    //    $("#dadosTurmas").addClass("visible");
    //    var dados = turma.text().split("|");
    //    var tabela = $("#tb_turma tbody");
    //    var linha = "<tr id=\"" + turma.val() + "\"><td>" + turma.val() + "</td><td>" + dados[1] + "</td><td>" + dados[2] + "</td><td>" + dados[4] + "</td><td><a href=\"#\" onclick=\"javascript:DeletarTurmaAluno('" + turma.val() + "','" + document.getElementById('hiddenIdAluno').value + "')\" class=\"btn btn-icon fuse-ripple-ready\" title=\"Excluir\"> <i class=\"icon-delete-forever \"></i> </a></td ></tr> ";
    //    tabela.append(linha);
    //    SetarArrayTurmas(turma.val(), dados[4]);
    //}
    //else {
    //    PNotify.error({
    //        title: 'Ops! Informe uma turma. :-( ',
    //        text: ''
    //    });

    //}

}
function DeletarTurmaAluno(turmaId, alunoId) {
    var dataT = {
        IdTurma: turmaId,
        IdAluno: alunoId
    };

    var callback = function (data) {

        if (data.success) {
            removeLinha(turmaId);
            ObeterValorCursoSelecionado($("#hiddenHashAluno").val());
            PNotify.success({
                text: 'Deletado com sucesso.'
            });

        } else {
            var msg = '';
            $.each(JSON.parse(data).data, function (index, item) {
                msg = msg + item.property + ' : ' + item.message + '\n';
            });

            PNotify.error({
                title: msg
            });


        }
    };
    var callbackErro = function (data) {
        PNotify.error({
            text: 'Ocorreu um erro ao processar sua solicitação'
        });

    };

    academia.helper.rest.utils.DELETE("/Matricula/Item/Del", dataT, callback, callbackErro, $('#loader'));

}
$.extend($.expr[":"], {
    "contains-ci": function (elem, i, match, array) {
        return (elem.textContent || elem.innerText || $(elem).text() || "").toLowerCase().indexOf((match[3] || "").toLowerCase()) >= 0;
    }
});
function Turma() {
    $("#dadosTurmas").show();
    $("#dadosTurmas").removeClass('invisible');
    $("#dadosTurmas").addClass("visible");
    AdicionarLinhaTabelaTurma($('#Turmas option:selected'));
    //if (existeLinha("tb_turma") <= 0) {
    //    var dataT = {
    //        IdAluno: $("#hiddenIdAluno").val(),
    //        IdTurma: $("#Turmas").val()
    //    };
    //    var callback = function (data) {
    //        if (JSON.parse(data).success) {
    //            AdicionarLinhaTabelaTurma($('#Turmas option:selected'));
    //            PNotify.success({
    //                text: 'Atualizado com sucesso.'
    //            });
    //            //document.getElementById("formTurma").reset();
    //            ObeterValorCursoSelecionado($("#hiddenHashAluno").val());
    //            return false;
    //        } else {
    //            var msg = '';
    //            $.each(JSON.parse(data).data, function (index, item) {
    //                msg = msg + item.property + ' > ' + item.message + '\n';
    //            });

    //            PNotify.error({
    //                title: 'Ops! ' + JSON.parse(data).message + ' :-( ',
    //                text: msg
    //            });
    //        }
    //    };
    //    var callbackErro = function (data) {
    //        PNotify.error({
    //            text: 'Ocorreu um erro ao processar sua solicitação'
    //        });

    //    };
    //    academia.helper.rest.utils.POST("/Aluno/Turma/Novo", dataT, callback, callbackErro, $('#loader'));
    //}
    //else {
    //    PNotify.info({

    //        text: 'A turma informada já esta vinculada para este aluno.'
    //    });
    //}


    //return false;
}
$("#formAlunoFoto").submit(function (event) {

    event.preventDefault();
    const tabAtual = 'foto-tab';
    const tabProx = 'logradouro-tab';
    var input = document.getElementById("file");
    var files = input.files;

    var formData = new FormData();
    formData.append('Id', $("#hiddenIdAluno").val());
    formData.append('Foto', '');
    if (document.getElementById("results").innerHTML !== '') {
        var file = document.getElementById("base64image").src;
        formData.append("base64image", file);
    }

    for (var i = 0; i !== files.length; i++) {
        formData.append("file", files[i]);
    }
    var callback = function (data) {
        if (data.success) {
            PNotify.success({
                title: 'Atualizado com sucesso.'
            });
            if (document.getElementById("results").innerHTML !== '') {
                document.getElementById("results").innerHTML = '';
                desligar_camera();
            }

            document.getElementById("formAlunoFoto").reset();
            HabilitarTab(tabAtual, tabProx);


            return false;
        } else {
            var msg = '';
            $.each(data.data, function (index, item) {
                msg = msg + item.property + ' > ' + item.message + '\n';
            });

            PNotify.error({
                title: 'Ops! ' + data.message + ' :-( ',
                text: msg
            });
        }
    };
    var callbackErro = function (data) {
        PNotify.error({
            text: 'Ocorreu um erro ao processar sua solicitação'
        });

    };
    academia.helper.rest.utils.POSTFORM("/Aluno/Editar/Foto", formData, callback, callbackErro, $('#loader'));

    return false;

});
$(function () {

    $("#formLogradouro").validate({
        ignore: [],
        rules: {

            inputRua: {
                required: true,
                minlength: 3,
                maxlength: 300

            },
            inputCep: {
                required: true
                //dateFormat: true

            },
            inputNumero: {
                required: true
                //dateFormat: true

            },
            inputBairro: {
                required: true
                //dateFormat: true

            },
            Estados: {
                required: true
                //dateFormat: true

            },
            inputCidade: {
                required: true
                //dateFormat: true

            },


        },
        messages: {


            inputRua: {
                required: "Informe o logradouro.",
                minlength: $.validator.format("O logradouro precisa ter pelo menos {0} characters"),
                maxlength: $.validator.format("O logradouro não pode serr superior há {0} characters")

            },
            inputNumero: {
                required: "Informe o número."

            },
            inputCep: {
                required: "Informe o cep."

            },
            inputBairro: {
                required: "Informe o bairro.",
                minlength: $.validator.format("O bairro precisa ter pelo menos {0} characters"),
                maxlength: $.validator.format("O bairro não pode serr superior há {0} characters")

            },
            inputCidade: {
                required: "Informe a cidade.",

            },
            Estados: {
                required: "Informe o estado.",

            }

        },
        submitHandler: function (form) {
            const tabAtual = 'logradouro-tab';
            const tabProx = 'filiacao-tab';
            var dataT = {

                idAluno: $("#hiddenIdAluno").val().length <= 0 ? 0 : $("#hiddenIdAluno").val(),
                Logradouro: $("#formLogradouro #inputRua").val(),
                Complemento: $("#formLogradouro #inputComplemento").val(),
                Numero: $("#formLogradouro #inputNumero").val(),
                Bairro: $("#formLogradouro #inputBairro").val(),
                Cidade: $("#formLogradouro #inputCidade").val(),
                Uf: $("#formLogradouro #Estados").val(),
                Cep: $("#formLogradouro #inputCep").val()
            };
            var callback = function (data) {
                if (JSON.parse(data).success) {
                    PNotify.success({
                        text: 'Cadastro realizado com sucesso.'
                    });
                    HabilitarTab(tabAtual, tabProx);
                    return false;
                } else {
                    var msg = '';
                    $.each(JSON.parse(data).data, function (index, item) {
                        msg = msg + item.property + ' > ' + item.message + '\n';
                    });

                    PNotify.error({
                        title: 'Ops! ' + JSON.parse(data).message + ' :-( ',
                        text: msg
                    });


                }
            };
            var callbackErro = function (data) {
                PNotify.error({
                    title: 'Ops! :-(',
                    text: 'Ocorreu um erro ao processar sua solicitação'
                });

            };

            academia.helper.rest.utils.POST("/Logradouro/Novo", dataT, callback, callbackErro, $('#loader'));

        }
    });
});
$(function () {
    $("#formResponsavel").validate({
        ignore: [],
        rules: {

            inputResponsavel: {
                required: true,
                minlength: 3,
                maxlength: 300

            },
            inputEmailResponsavel: {
                required: true,
                email: true
            },
            TipoFiliacao: {
                required: true
            }
        },
        messages: {


            inputRua: {
                required: "Informe o nome do responsavel.",
                minlength: $.validator.format("O nome do responsavel precisa ter pelo menos {0} characters"),
                maxlength: $.validator.format("O logradouro não pode serr superior há {0} characters")

            },
            inputEmailResponsavel: {
                required: "Informe o email do responsavel.",
                email: "Email informado não é valido"
            },
            TipoFiliacao: {
                required: "Informe o tipo de responsavel."
            }

        },
        submitHandler: function (form) {

            var dataT = {

                idAluno: $("#hiddenIdAluno").val().length <= 0 ? 0 : $("#hiddenIdAluno").val(),
                Nome: $("#formResponsavel #inputResponsavel").val(),
                Telefone: $("#formResponsavel #inputTelefoneResponsavel").val(),
                Email: $("#formResponsavel #inputEmailResponsavel").val(),
                Docmento: $("#formResponsavel #inputDocumentoResponsavel").val(),
                TipoFiliacao: $("#formResponsavel #TipoFiliacao").val()

            };
            var callback = function (data) {
                if (JSON.parse(data).success) {
                    PNotify.success({
                        title: 'Cadastro realizado com sucesso.'
                    });
                    document.getElementById("formResponsavel").reset();
                    return false;
                } else {
                    var msg = '';
                    $.each(JSON.parse(data).data, function (index, item) {
                        msg = msg + item.property + ' - ' + item.message + '\n';
                    });

                    PNotify.error({
                        title: msg
                    });
                }
            };
            var callbackErro = function (data) {
                PNotify.error({

                    title: 'Ocorreu um erro ao processar sua solicitação. [' + data + ']'
                });

            };
            academia.helper.rest.utils.POST("/Aluno/Responsavel/Novo", dataT, callback, callbackErro, $('#loader'));
        }
    });
});
$(function () {
    $("#formMatricula").validate({

        ignore: [],
        rules: {

            inputValorMatricula: {
                required: true


            },
            inputDescontoMatricula: {
                required: true


            },
            inputValorParcela: {
                required: true


            },
            inputDescontoParcela: {
                required: true


            },
            inputDiaVencimento: {
                required: true


            },
            inputTotalParcelas: {
                required: true


            },
            TipoFiliacao: {
                required: true
            },
            inputVigencia: {
                required: true
            },
            inputMesVencimento: {
                required: true
            }

        },
        messages: {


            inputValorMatricula: {
                required: "Campo Obrigatório."

            },
            inputDescontoMatricula: {
                required: "Campo Obrigatório."

            },
            inputValorParcela: {
                required: "Campo Obrigatório."

            },
            inputDescontoParcela: {
                required: "Campo Obrigatório."

            },
            inputDiaVencimento: {
                required: "Campo Obrigatório."

            },
            inputTotalParcelas: {
                required: "Campo Obrigatório."

            },
            TipoFiliacao: {
                required: "Informe o tipo de responsavel."
            }

        },
        submitHandler: function (form) {

            var dataT = {

                IdAluno: $("#hiddenIdAluno").val(),
                PercentualDesconto: $("#formMatricula #inputDescontoParcela").val().replace('%', ''),
                ValorMatricula: $("#formMatricula #inputValorMatricula").val().replace('R$', '').replace('.', ','),
                ValorContrato: $("#formMatricula #inputValorParcela").val().replace('R$', '').replace('.', ','),
                DiaVencimento: $("#formMatricula #inputDiaVencimento").val(),
                TotalParcelas: $("#formMatricula #inputTotalParcelas").val(),
                DataIncialPagamento: $("#formMatricula #inputDiaVencimento").val() + "/" + $("#formMatricula #Mes").val() + "/" + $("#formMatricula #inputVigencia").val(),
                DataContrato: $("#formMatricula #inputDiaVencimento").val() + "/" + $("#formMatricula #Mes").val() + "/" + $("#formMatricula #inputVigencia").val(),
                Ano: $("#formMatricula #inputVigencia").val(),
                Turmas: turmas
            };
            var callback = function (data) {
                if (JSON.parse(data).success) {
                    PNotify.success({
                        title: 'Matriculado com sucesso.'
                    });
                    document.getElementById("formResponsavel").reset();
                    return false;
                } else {
                    var msg = '';
                    $.each(JSON.parse(data).data, function (index, item) {
                        msg = msg + item.property + ' > ' + item.message + '\n';
                    });

                    PNotify.info({
                        title: msg
                    });


                }
            };
            var callbackErro = function (data) {
                PNotify.error({
                    title: 'Ocorreu um erro ao processar sua solicitação'
                });

            };

            academia.helper.rest.utils.POST("/Aluno/Matricular/", dataT, callback, callbackErro, $('#loader'));
            return false;
        }
    });
});
function SetarArrayTurmas(idTurma, valor) {
    // Percorrer todas as linhas do corpo da tabela (
    //// você também pode fazer um for ao invés de usar o método each() do JQuery)
    //$('#tb_turma tbody tr').each(function () {
    //    // Recuperar todas as colunas da linha percorida
    //    var colunas = $(this).children();

    // Criar objeto para armazenar os dados (com JSON essa tarefa fica mais simples)
    var ItemMatricula = {
        'IdMatricula': 0, // valor da coluna Produto
        'IdTurma': idTurma, // Valor da coluna Quantidade
        'Valor': valor // Valor da coluna Quantidade
    };

    // Adicionar o objeto pedido no array
    turmas.push(ItemMatricula);
    //});

}

