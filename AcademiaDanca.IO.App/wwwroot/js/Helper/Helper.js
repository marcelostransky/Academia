var academia = academia || {};

// Common namespace
academia.helper = function () { };

// Rest namespace
academia.helper.rest = function () { };

// Utils class
academia.helper.rest.utils = new function () {

    this.GET = function (url, dataT, callback, callbackError, Loader) {
        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            type: "GET",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "json",
            beforeSend: function (xhr) {
                //xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));

                Loading(true, Loader);
            },
            complete: function () {

                Loading(false, Loader);
            },
            statusCode: {
                200: function (data) {
                    //if (typeof callback === "function")
                    callback(data);
                },
                401: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");
                },
                203: function (data) {
                    academia.cookie.Deletar("User");
                    window.location.href = "/Usuario/LogOff/?msg=Sua sessão expirou. Faça o login novamente";
                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");
                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (data) {
                if (typeof callbackError === "function") {
                    callbackError(data);
                }
                Loading(false);
            }
        });
    };

    this.POST = function (url, dataT, callback, callbackError, Loader) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        var ajax = $.ajax({
            type: "POST",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "text",
            beforeSend: function () {
                Loading(true, Loader);
                // xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
            },
            complete: function () {

                Loading(false, Loader);
            },
            statusCode: {
                200: function (data) {
                    if (callback !== null) {
                        callback(data);
                    }
                },
                401: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (data) {
                if (typeof callbackError === "function") {
                    callbackError(data);
                }
                Loading(false);
            }
        });

        //return ajax;
    };
    this.POSTFORM = function (url, dataT, callback, callbackError, Loader) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        var ajax = $.ajax({
            type: "POST",
            url: url,
            data: dataT,
            async: isAsync,
            cache: false,
        contentType: false,
        processData: false,
            beforeSend: function () {
                Loading(true, Loader);
                // xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
            },
            complete: function () {

                Loading(false, Loader);
            },
            statusCode: {
                200: function (data) {
                    if (callback !== null) {
                        callback(data);
                    }
                },
                401: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (data) {
                if (typeof callbackError === "function") {
                    callbackError(data);
                }
                Loading(false);
            }
        });

        //return ajax;
    };
    this.POSTSemLoader = function (url, dataT, callback, callbackError) {

        var isAsync = false;
        if (callback)
            isAsync = true;

        var ajax = $.ajax({
            type: "POST",
            url: url,
            data: dataT,
            async: isAsync,
            dataType: "text",
            beforeSend: function () {

                // xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
            },
            complete: function () {

            },
            statusCode: {
                200: function (data) {
                    //if (callback === "function") {
                    callback(data);
                    //}
                },
                401: function (data) {
                    //academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    //academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    //academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (data) {
                if (typeof callbackError === "function") {
                    callbackError(data);
                }
                Loading(false);
            }
        });

        //return ajax;
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
                Loading(true);
                //xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));


            },
            complete: function () {

                Loading(false);
            },
            statusCode: {
                200: function (data) {
                    //console.log("Recursos enviados para " + url + " .");
                    if (typeof callback === "function")
                        if (callback) callback(data);

                },
                401: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {
                Loading(false);
                if (typeof callbackError === 'function')
                    callbackError();

            },
        })

    };
    this.PUT = function (url, dataT, callback, callbackError, Loader) {
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
                //xhr.setRequestHeader("Authorization", "Basic " + utf8_to_b64(global.token));
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
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");
                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            }

            // TODO: (Comentado por LeanWork em 09/03) O tratamento de erro já está sendo realizado em statusCode acima.
            //error: function () {
            //    Modal();
            //    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Ocorreu um erro ao processar sua solicitação", "");                
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
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function () {

                Loading(false);
                if (typeof callbackError === 'function')
                    callbackError();
            },
        })
    };
    this.DELETE = function (url, dataT, callback, callbackError, Loader) {
        var isAsync = false;
        if (callback)
            isAsync = true;

        return $.ajax({
            type: "DELETE",
            url: url,
            async: isAsync,
            data: dataT,
            dataType: "json",
            beforeSend: function (xhr) {
                Loading(true, Loader);
            },
            complete: function (xhr, textStatus) {
                Loading(false, Loader);
            },
            statusCode: {
                200: function (data) {
                    if (callback !== null) {
                        callback(data);
                    }
                },
                401: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Acesso não autorizado!", "");

                },
                500: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                },
                501: function (data) {
                    academia.common.alerts.Message("Mensagem do Sistema", "Atenção!\n Servidor se comportou de maneira estranha e não conseguiu processar sua solicitação. \n Tente realizar esta operação novamente!", "");

                }
            },
            error: function (data) {
                if (typeof callbackError === "function") {
                    callbackError(data);
                }
                Loading(false, Loader);
            }
        });
    };
};
academia.helper.common = new function () {
    this.isEmpty = function (obj) {

        for (var prop in obj) {
            if (obj.hasOwnProperty(prop))
                return false;
        }
        return true;
    }
}
    
    /***
    * @name academia.Loading
    * @description Objeto que controla o componente de loading.
    **/
    ; (function (window, $, academia) {
        'use strict';
        var loaderDefault;

        $(document).ready(function () {
            loaderDefault = $('#loader');
        });

        academia.Loading = new function () {
            // Public API
            return {
                show: _show,
                hide: _hide,
                isLoading: _isLoading
            };

            // Private
            function _show(loader) {
                if (loader == 'undefined' || loader == null)
                    loader = loaderDefault;
                loader.show();
            }

            function _hide(loader) {
                if (loader == 'undefined' || loader == null)
                    loader = loaderDefault;
                loader.hide();
            }

            function _isLoading() {
                return loader.is(':visible');
            }
        };

    })(window, $, academia || {});

function utf8_to_b64(str) {
    return window.btoa(unescape(encodeURIComponent(str)));
}

function b64_to_utf8(str) {
    return decodeURIComponent(escape(window.atob(str)));
}


///**
//* @name Loading
//* @description Mostra ou oculta o componente de loading.
//*/
function Loading(showLoading, loader) {
    if (showLoading) {
        academia.Loading.show(loader);
    } else {
        academia.Loading.hide(loader);
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
    if (academia.Loading.isLoading()) {
        academia.Loading.hide();
    } else {
        academia.Loading.show();
    }
    return;
}