// Base namespace
var avalia = avalia || {};

// Common namespace
avalia.helper = function () { };

// Rest namespace
avalia.helper.rest = function () { };

// Utils class
avalia.helper.rest.utils = new function () {

    this.GET = function (url, callback) {
        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            type: "GET",
            url: url,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true);
            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    if (typeof callback === 'function')
                        callback(data);
                },
                401: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");
                },
                203: function (data) {
                    avalia.cookie.Deletar("User");
                    window.location.href = "/Usuario/LogOff/?msg=Sua sessão expirou. Faça o login novamente";
                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");
                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                //var msg = "[";
                //for (i in XMLHttpRequest) {
                //    if (i != "channel")
                //        if (!avalia.helper.common.isEmpty(XMLHttpRequest[i].ExceptionMessage))
                //            msg += XMLHttpRequest[i].ExceptionMessage + "<br>";
                //}

                //avalia.common.alerts.Message("Mensagem do Sistema", "Ocorreu(am) algum(s) erro(s) ao obter recursos solicitado.", "");
                //console.log("Ocorreu(am) algum(s) erro(s) ao obter recursos de " + url + " .");
            }
        });
    };

    this.POST = function (url, dataT, callback, callbackError) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        var ajax = $.ajax({
            type: "POST",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "text",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true);
            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    if (callback === "function") {
                        callback(data);
                    }
                },
                401: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {
                if (typeof callbackError === "function") {
                    callbackError();
                }
                Loading(false);
            }
        })

        return ajax;
    };
    this.POSTLIST = function (url, dataT, callback, callbackError) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        var ajax = $.ajax({
            contentType: "application/json; charset=utf-8",
            type: "POST",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true);
            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    //console.log("Recursos enviados para " + url + " .");
                    if (callback) callback(data);

                },
                401: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {
                Loading(false);
                if (typeof callbackError === 'function')
                    callbackError();

            },
        })

    };
    this.PUT = function (url, dataT, callback) {
        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            type: "PUT",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
                Loading(true);
            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    if (typeof callback === "function")
                        callback(data);
                },
                401: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");
                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            }

            // TODO: (Comentado por LeanWork em 09/03) O tratamento de erro já está sendo realizado em statusCode acima.
            //error: function () {
            //    Modal();
            //    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Ocorreu um erro ao processar sua solicitação", "");                
            //}
        })
    };
    this.PUTLIST = function (url, dataT, callback, callbackError) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            contentType: "application/json; charset=utf-8",
            type: "PUT",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true);
            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    if (typeof callback === 'function')
                        callback(data);
                },
                401: function (data) {
                    alert('Atenção!\n Acesso não autorizado!');

                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {

                Loading(false);
                if (typeof callbackError === 'function')
                    callbackError();
            },
        })
    };
    this.DELETE = function (url, callback) {
        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            type: "DELETE",
            url: url,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true);
            },
            complete: function (xhr, textStatus) {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    //console.log("Recursos deletados de " + url + " .");
                    if (callback) callback(data);
                },
                401: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    avalia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {

                Loading(false);
            }
        })
    };
    this.ProcessarSolicitacao = function (url, DivLoad, callback, dataT) {

        var iconCarregando = "<div ID=\"ICONLOAD\"\><img src=\"/Imagens/loading.gif\" style=\"height:30px\" /> Aguarde...</div>";
        var url = url;
        $.ajax({
            url: url,
            type: "GET",
            dataType: "json",
            data: dataT,
            beforeSend: function () {
                $("#" + DivLoad).html(iconCarregando);
                $("#EnviarForm").hide();
            },
            complete: function () { $('#ICONLOAD').remove(); },
            success: function (data) {
                try {
                    $("#EnviarForm").show();
                    callback(data);

                } catch (e) {


                }
            },
            error: function (xmlHttpRequest, textStatus, errorThrown) {

                Loading(false);
                $("#" + DivLoad).html("Ocorreu um erro ao tentar carregar as informações");
            }
        });

    }

}
avalia.helper.common = new function () {
    this.isEmpty = function (obj) {

        for (var prop in obj) {
            if (obj.hasOwnProperty(prop))
                return false;
        }
        return true;
    }
}

avalia.helper.ajax = new function () {
    ///
    // Public API
    ///
    return {
        GET: doGet,
        POST: doPost,
        PUT: doPut
    };

    ///
    // Private API
    ///

    /**
    * @name doGet
    * @description Realiza uma chamada GET assíncrona.
    * @params @url URL destino da requisição.
    * @params @beforeSend Função que será executada antes de a requisição ser efetuada.
    * @returns Retorna um promise da requisição.
    */
    function doGet(url, beforeSend) {
        var jqxhr = $.ajax({
            type: "GET",
            url: url,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
                if (typeof beforeSend === 'function') {
                    beforeSend();
                }
            }
        })

        return jqxhr;
    }

    /**
    * @name doPost
    * @description Realiza uma chamada POST assíncrona.
    * @params @url URL destino da requisição.
    * @params @dataT Objeto contendo os dados que serão enviados por POST.
    * @params @beforeSend Função que será executada antes de a requisição ser efetuada.
    * @returns Retorna um promise da requisição.
    */
    function doPost(url, dataT, beforeSend) {

        return createRequest("POST", url, dataT, beforeSend);
    }

    /**
    * @name doPut
    * @description Realiza uma chamada PUT assíncrona.
    * @params @url URL destino da requisição.
    * @params @dataT Objeto contendo os dados que serão enviados na requisição.
    * @params @beforeSend Função que será executada antes de a requisição ser efetuada.
    * @returns Retorna um promise da requisição.
    */
    function doPut(url, dataT, beforeSend) {

        return createRequest("PUT", url, dataT, beforeSend);
    }

    /**
    * @name createRequest
    * @description Cria uma requisição xhr.
    * @params @type Tipo de requisição REST (GET, POST, PUT, DELETE).
    * @params @url URL destino da requisição.
    * @params @dataT Objeto contendo os dados que serão enviados na requisição.
    * @params @beforeSend Função que será executada antes de a requisição ser efetuada.
    * @returns Retorna um promise da requisição.
    */
    function createRequest(type, url, dataT, beforeSend) {
        var jqxhr = $.ajax({
            type: type,
            url: url,
            data: dataT,
            beforeSend: function (xhr) {
                xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
                if (typeof beforeSend === 'function') {
                    beforeSend();
                }
            }
        })
        return jqxhr;
    }
};

/***
* @name avalia.Loading
* @description Objeto que controla o componente de loading.
**/
; (function (window, $, avalia) {
    'use strict';
    var loader;

    $(document).ready(function () {
        loader = $('#loader');
    });

    avalia.Loading = new function () {
        // Public API
        return {
            show: _show,
            hide: _hide,
            isLoading: _isLoading
        };

        // Private
        function _show() {
            $('#loader').show();
        }

        function _hide() {
            $('#loader').hide();
        }

        function _isLoading() {
            return $('#loader').is(':visible');
        }
    }

})(window, $, avalia || {});

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}


/**
* @name Loading
* @description Mostra ou oculta o componente de loading.
*/
function Loading(showLoading) {
    if (showLoading) {
        avalia.Loading.show();
    } else {
        avalia.Loading.hide();
    }
}

//Cortina
// [Obsoleto]

/**
* @name Modal
* @description Apresenta e/ou oculta loading na tela.
* @deprecated Função mantida apenas para compatibilidade do legado. Favor utilizar a função Loading().
*/
function Modal() {
    if (avalia.Loading.isLoading()) {
        avalia.Loading.hide();
    } else {
        avalia.Loading.show();
    }
    return;
}