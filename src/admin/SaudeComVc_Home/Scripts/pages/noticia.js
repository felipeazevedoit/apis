$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: '/Noticias/BuscarFotoAsync/' + $("#user").val() + '',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (dados) {
            console.log(dados.ProfileAvatar);
            $("#txtNome").text(dados.Nome);

            if (dados.ProfileAvatar == null) {
                $("#imgMedico").attr("src", "../../Img/generic-user-purple.png");                    
            }
            else {
                $("#imgMedico").attr("src", "data:image/" + dados.AvatarExtension + ";base64," + dados.ProfileAvatar);
            }
        }
    });


    if ($("#lblIdUsuario").val() != null && $("#lblIdUsuario").val() != "") {
        $.ajax({
            type: "GET",
            url: '/Noticias/BuscarFotoAsync/' + $("#lblIdUsuario").val() + '',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (pic) {
                console.log(pic);
                if (pic.ProfileAvatar != null) {
                $("#pcbMedico").attr("src", "data:image/png;base64," + pic.ProfileAvatar);
                }
            }
        });
    }
    

    

    if ($("#user").val() != 0) {

        var relacionadaMedico = getDataRelacionadaMedico();
        console.log(JSON.stringify(relacionadaMedico));

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: '/Noticias/BuscarNoticiaRelacionadaAsync/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(relacionadaMedico),
            success: function (relMed) {
                console.log(relMed);

                var trHTML = '';

                for (var i = 0; i < relMed.length; i++) {
                    console.log(relMed[i].Midia);

                    trHTML += '                           <hr />  ' +
                        '   <div class="row topp">  ' +
                        '                   <div>  ' +
                        '                       <div class="col-md-1">  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-4">  ' +
                        '                           <img src="data:image/' + relMed[i].Midia.Extensao + '; base64, ' + relMed[i].Midia.ArquivoB64 + ' " style="width:100%; margin-left:1%;" />  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-4">  ' +
                        '                           <p class="titulonew">' + relMed[i].Nome + '</p>  ' +
                        '                           <a href="/Noticias/Index/' + relMed[i].ID + '"><p class="conteudoNew"><b>' + relMed[i].Descricao + '</b></p></a>  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-2"></div>  ' +
                        '                   </div>  ' +
                        '              </div>  ';
                    $('#mae').append(trHTML);
                };
            }
        });
    }
    else {
        var relacionada = getDataRelacionada();
        console.log(JSON.stringify(relacionada));

        $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            url: '/Noticias/BuscarNoticiaRelacionadaAsync/',
            type: 'POST',
            contentType: 'application/json; charset=utf-8',
            data: JSON.stringify(relacionada),
            success: function (rel) {
                console.log(rel);

                var trHTML = '';

                for (var i = 0; i < rel.length; i++) {
                    console.log(rel[i].Midia);

                    trHTML += '                           <hr />  ' +
                        '   <div class="row topp">  ' +
                        '                   <div>  ' +
                        '                       <div class="col-md-1">  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-4">  ' +
                        '                           <img src="data:image/' + rel[i].Midia.Extensao + '; base64, ' + rel[i].Midia.ArquivoB64 + ' " style="width:100%; margin-left:1%;" />  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-4">  ' +
                        '                           <p class="titulonew">' + rel[i].Nome + '</p>  ' +
                        '                           <a href="/Noticias/Index/' + rel[i].ID + '"><p class="conteudoNew"><b>' + rel[i].Descricao + '</b></p></a>  ' +
                        '                       </div>  ' +
                        '                       <div class="col-md-2"></div>  ' +
                        '                   </div>  ' +
                        '              </div>  ';
                    $('#mae').append(trHTML);
                };
            }

        });
    }
    carregarComentarios();
    chamarIndex();
});

function chamarIndex() {

    var settings = {
        "data": { "idUsuarioP": $("#lblIdUsuarioL").val() },
        "url": "/Noticia/Index",
        "method": "POST",
    }

    $.ajax(settings).done(function (response) {
        
    });

}


function getDataRelacionadaMedico() {
    return {
        idCliente: 12,
        codigoExterno: $("#user").val(),
        NoticiaTags : $("#tags").val()
    }
}

function getDataRelacionada() {
    return {
        idCliente: 12,
        codigoExterno: 0,
        NoticiaTags : $("#tags").val()
    }
}

function carregarComentarios() {
    var coment = "";
    $.ajax({
        type: "GET",
        url: '/Noticias/BuscarComentariosAsync/' + $("#noticia").val() + '',
        dataType: 'json',
        contentType: "application/json; charset=utf-8",
        success: function (obj) {
            console.log(obj);
            $("#lblNumero").text(obj.length);

            

            $.each(obj, function (i, item) {

                var src = '';
                if (item.profileAvatar == undefined || item.AvatarExtension == undefined) {
                    src = "src='../../Img/generic-user-purple.png'";
                }
                else {
                    src = 'src = "data:image/' + item.AvatarExtension + ';base64,' + item.profileAvatar + '"';
                }

                coment += '   <div style="width:50%; margin-left:25%;">  ' +
                    '                               <div class="row" style="background-color:#F5F5F5; border-radius:24px; border-bottom-left-radius:0px;">  ' +
                    '                                   <div class="col-md-2 col-sm-3">  ' +
                    '                                       <img ' + src + ' id="imgMedico" class="img-circle img-responsive pull-left" style="width:100 %; margin-top:10%" " />  ' +
                    '                                   </div>  ' +
                    '                                   <div class="col-md-9 col-sm-9">  ' +
                    '                                       <table id="tblComentarios" class="pull-left" style="margin-right:70%; width:100%;">  ' +
                    '                                           <tr>  ' +
                    '                                               <td>  ' +
                    '                                                   <h5 class="pull-left"><b>' + item.Nome + '</h3></b>  ' +                 
                    '                                               </td>  ' +   
                    '                                           </tr>  ' +
                    '                                           <tr>  ' +
                    '                                               <td><p class="pull-left" style="color:gray;">' + item.Descricao + '</p></td>  ' +
                    '                                           </tr>  ' +
                    '                                           <tr style="bottom:50%">' +
                    '                                               <td>  ' +
                    '                                                   <h6 class="pull-left">Às:' + item.ComentarioHora + '</h6>  ' +
                    '                                               </td>  ' +
                    '                                           </tr>'+
                    '                                       </table>  ' +
                    '                                   </div>  ' +
                    '                               </div>  ' +
                    '                          </div>  ' +
                    '                          <br/>  ';

            });
            $("#pai").empty();
            $("#pai").append(coment);
        }
    });
}

function getWhiteLabel(nome, id) {
    window.location.href = '/Medico/WhiteLabel/' + id + '?nome=' + nome;
}


//setTimeout("alert('ok');", 2000);

var intervalo = setInterval(function () {   
    carregarComentarios();
}, 60000);

setTimeout(function () {
    clearInterval(intervalo);
}, 1200000000);


var d = new Date();

$("#btnComentario").click(function () {
    
    function getDataComentario() {
        return {
            Nome: $("#lblNomeUser").val(),
            Descricao: $("#lblDescricaoComent").val(),
            DataCriacao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            DataEdicao: d.getFullYear() + "-" + (d.getMonth() + 1) + "-" + d.getDate(),
            UsuarioCriacao: $("#lblIdUsuario").val(),
            UsuarioEdicao: $("#lblIdUsuario").val(),
            Ativo: true,
            IdCliente: 12,
            Status: 1,
            NoticiaId: $("#noticia").val()
        }
    }

    var url = "http://187.84.232.164:5300/api/Seguranca/WpNoticias/SalvarComentario/12/999";

    var comentario = { 'comentario': getDataComentario()};
    console.log(JSON.stringify(comentario));

    $.ajax({
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        url: url,
        type: 'POST',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify(comentario),
        success: function (coment) {
            console.log(coment);
            $(':input', '#form1')
                .not(':button, :submit, :reset, :hidden')
                .val('')
                .removeAttr('checked')
                .removeAttr('selected');
            $('#form1')[0].reset();
            carregarComentarios();
        }
    });
});

function LoadingInit(elemento) {
    $(elemento).loading({
        theme: 'dark',
        message: 'Aguarde...',
        onStart: function (loading) {
            loading.overlay.slideDown(400);
        },
        onStop: function (loading) {
            loading.overlay.slideUp(400);
        }
    });
}

function LoadingStop(elemento) {
    $(elemento).loading('stop');
}