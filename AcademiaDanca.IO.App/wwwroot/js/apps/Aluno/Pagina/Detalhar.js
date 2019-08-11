function BuscarAluno(cep) {
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
        }
        var callbackErro = function (data) {
            PNotify.error({
                title: 'Ops! :-(',
                text: 'CEP não localizado'
            });

        }

        academia.helper.rest.utils.GET("/Aluno/ObterLogradouroPor", dataT, callback, callbackErro, $('#loader'));

    }

}