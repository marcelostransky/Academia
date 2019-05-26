

function LerCookie() {
    var token = avalia.cookie.Ler("_tk");
    var cookieUsuario = avalia.cookie.Ler("User");
    var usuario = JSON.parse(cookieUsuario);

    if (cookieUsuario === null || usuario.Token !== token) {
        GerarCookieUser(token);
        cookieUsuario = avalia.cookie.Ler("User");
    }

    if (cookieUsuario) {
        CarregarVariaveis(cookieUsuario, token);
        MudarMenuPrincial(global.role);
        AdicionarNomeUsuario(global.nomeUsuario);
        $(window).trigger("CarregarPagina", cookieUsuario);
        VerificarDadosUsuario(usuario.Id);
    }
}
function ValidarSessionAtiva() {
    var jsonSessionAtiva = avalia.helper.rest.utils.GET("/Usuario/ValidarSessionAtiva/?token=" + global.token);

    if (!jsonSessionAtiva.responseJSON.session) {
        avalia.cookie.Deletar("User");
        window.location.href = "/Usuario/LogOff/?msg=Sua sessão expirou. Faça o login novamente";
    }
}
function AtualizarVariavelUrl(variavel, valor) {
    var ancoras = $(" ul > li > a");
    $.each(ancoras, function (index, link) {
        if ($(link).attr("href").indexOf(variavel) > -1) {
            var url = $(link).attr("href");
            $(link).attr("href", url.replace(variavel, valor));
        }
    });
}

function CarregarVariaveis(cookie, token) {
    var valores = JSON.parse(cookie);

    global.idUsuario = valores.Id;
    global.idEscola = valores.IdEscola;
    global.nomeUsuario = valores.Nome;
    global.role = valores.Funcao;
    global.inep = valores.Ineps;
    global.idAutor = valores.IdAutor;
    global.token = token;

}

function MudarMenuPrincial(role) {
    //if (role != "RedeGestora")
    $("#menuPrincipal").load(virtualPath2 + "Shared/_MenuPrincipal_" + role);
    //$("#menuPrincipal").load("/Views/Shared/_MenuPrincipal_" + role);
}

function AdicionarNomeUsuario(nomeUsuario) {
    $("#nomeUsuario").html(nomeUsuario);
}

function ValidarUrl() {
    var ultimoCaracteres = location.pathname.substring(location.pathname.length - 1);

    if (ultimoCaracteres === "/" && location.pathname !== virtualPath) {
        location.href = location.pathname.substring(0, location.pathname.length - 1);
    }
}

function GerarCookieUser(token) {
    var jsonInformacaoUsuario = avalia.helper.rest.utils.GET(apiEscolaUri + "InformacaoUsuario/" + token);

    if (jsonInformacaoUsuario) {
        CarregarVariaveis(jsonInformacaoUsuario.responseText, token);
        avalia.cookie.Gravar("User", JSON.stringify(CriarCookieUser(jsonInformacaoUsuario.responseJSON)));
    }
};


function VerificarEmailUnico(email) {
    var jsonEmail = {
        "email": email
    };

    return avalia.helper.rest.utils.PUT("/Usuario/VerificarEmailUnico", jsonEmail).responseJSON.status;
}


function CriarCookieUser(json) {
    return {
        "Funcao": json.Funcao,
        "Id": json.Id,
        "IdAutor": json.IdAutor,
        "IdEscola": json.IdEscola,
        "Ineps": json.Ineps,
        "Nome": json.Nome,
        "Token": global.token,
        "IdProfessor": json.idProfessor
    };
};