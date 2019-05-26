var academia = academia || {};

academia.cookie = new function () {
    this.Gravar = function (nomeCookie, valorCookie, data) {
        var dataExpiracao = "";
        if (data) {
            data = data.toGMTString();
            dataExpiracao = "expires=" + data + ";";
        }
        
        document.cookie = nomeCookie + "=" + valorCookie + ";path=/" + dataExpiracao;
    }

    this.Ler = function (nomeCookie) {
        var nomeCookie = nomeCookie + "=";

        var cookies = document.cookie;

        if (cookies.indexOf(nomeCookie) == -1) {
            return false;
        }

        cookies = cookies.substr(cookies.indexOf(nomeCookie), cookies.length);

        if (cookies.indexOf(';') != -1) {
            cookies = cookies.substr(0, cookies.indexOf(';'));
        }

        cookies = cookies.split('=')[1];

        return cookies;
    }

    this.Deletar = function (nomeCookie) {
        document.cookie = nomeCookie + "=; expires=Thu, 01 Jan 1970 00:00:01 GMT;path=/;";
    }
};
