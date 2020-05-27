$(document).ready(function () {

    var usuarioId = $("#lblIdUsuarioL").val();

    if ($("#user").val() != null && $("#user").val() > 0) {
        $.ajax({
            type: "GET",
            url: '/Noticias/BuscarNoticiasPrivadasAsync/' + $("#user").val() +'',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (dados) {
                console.log(dados);
                console.log(dados.length);

                if (dados != null && dados != undefined) {
                    if (dados.length > 4) {
                        for (var i = 0; i < 2; i++) {
                            //As 4 de cima
                            var trHTMLGrande = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/?id=' + dados[i].ID + '&idUsuarioP=' + usuarioId + '"><img src="data:image/' + dados[i].Midia.Extensao + '; base64, ' + dados[i].Midia.ArquivoB64 + '" style="width:100%; height:250px" />' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[i].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[i].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsUm').append(trHTMLGrande);
                            $('#blocoUm').append('<div class="col-md-1 col-sm-1"></div>');
                        }
                        trHTMLGrande = ''
                        for (var c = 2; c < 4; c++) {
                            //As 4 de cima
                            //$('#newsDois').append('<div class="col-md-1 col-sm-1"></div>');
                            var trHTMLBaixo = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/?id=' + dados[c].ID + '&idUsuarioP=' + usuarioId + '"><img src="data:image/' + dados[c].Midia.Extensao + '; base64, ' + dados[c].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[c].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[c].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsDois').append(trHTMLBaixo);
                        }
                        for (var b = 4; b < dados.length; b++) {
                            //Listagem de baixo
                            var trHTML = "";
                            trHTML += '                           <hr class="hrTamanho"/>  ' +
                                '   <div class="row topp">  ' +
                                '                   <div>  <a href="/Noticias/Index/?id=' + dados[b].ID + '&idUsuarioP=' + usuarioId + '">' +
                                '                       <div class="col-md-1">  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-4">  ' +
                                '                           <img src="data:image/' + dados[b].Midia.Extensao + '; base64, ' + dados[b].Midia.ArquivoB64 + ' " style="width:100%;" class="img-responsive"/>  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-4">  ' +
                                '                           <p class="titulonew">' + dados[b].Nome + '</p>  ' +
                                '                           <p class="conteudoNew"><b>' + dados[b].Descricao + '</b></p>  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-2"></div>  ' +
                                '                   </div>  </a>' +
                                '              </div>  ';
                            $('#pai').append(trHTML);
                        }
                    }
                    else if (dados.length < 4) {
                        for (var a = 0; a < 2; a++) {
                            //As 4 de cima
                            trHTMLGrande = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/?id=' + dados[a].ID + '&idUsuarioP=' + usuarioId + '"><img src="data:image/' + dados[a].Midia.Extensao + '; base64, ' + dados[a].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[a].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[a].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsUm').append(trHTMLGrande);
                            $('#blocoUm').append('<div class="col-md-1 col-sm-1"></div>');
                        }
                        trHTMLGrande = '';
                        for (var o = 2; o < 4; o++) {
                            //As 4 de cima
                            //$('#newsDois').append('<div class="col-md-1 col-sm-1"></div>');
                            trHTMLBaixo = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/?id=' + dados[o].ID + '&idUsuarioP=' + usuarioId + '"><img src="data:image/' + dados[o].Midia.Extensao + '; base64, ' + dados[o].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[o].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[o].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsDois').append(trHTMLBaixo);
                        }
                    }

                }
            }    
        });//succes
        
    }
    else {
        $.ajax({
            type: "GET",
            url: '/Noticias/BuscarNoticiasPublicasAsync/',
            dataType: 'json',
            contentType: "application/json; charset=utf-8",
            success: function (dados) {
                console.log(dados);
                console.log(dados.length);

                if (dados != null && dados != undefined) {
                    if (dados.length > 4)
                    {
                        for (var i = 0; i < 2; i++) {
                            //As 4 de cima
                            var trHTMLGrande = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/' + dados[i].ID + '"><img src="data:image/' + dados[i].Midia.Extensao + '; base64, ' + dados[i].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[i].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[i].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsUm').append(trHTMLGrande);
                            $('#blocoUm').append('<div class="col-md-1 col-sm-1"></div>');
                        }
                        trHTMLGrande = ''
                        for (var m = 2; m < 4; m++) {
                            //As 4 de cima
                            //$('#newsDois').append('<div class="col-md-1 col-sm-1"></div>');
                            var trHTMLBaixo = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/' + dados[m].ID + '"><img src="data:image/' + dados[m].Midia.Extensao + '; base64, ' + dados[m].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[m].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[m].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsDois').append(trHTMLBaixo);
                        }
                        for (var g = 4; g < dados.length; g++) {
                            //Listagem de baixo
                            var trHTML = "";
                            trHTML += '                           <hr class="hrTamanho"/>  ' +
                                '   <div class="row topp">  ' +
                                '                   <div>  <a href="/Noticias/Index/' + dados[g].ID + '">' +
                                '                       <div class="col-md-1">  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-4">  ' +
                                '                           <img src="data:image/' + dados[g].Midia.Extensao + '; base64, ' + dados[g].Midia.ArquivoB64 + ' " style="width:100%;" class="img-responsive"/>  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-4">  ' +
                                '                           <p class="titulonew">' + dados[g].Nome + '</p>  ' +
                                '                           <p class="conteudoNew"><b>' + dados[g].Descricao + '</b></p>  ' +
                                '                       </div>  ' +
                                '                       <div class="col-md-2"></div>  ' +
                                '                   </div>  </a>' +
                                '              </div>  ';
                            $('#pai').append(trHTML);
                        }
                    }
                    else if (dados.length < 4)
                    {
                        for (var s = 0; s < 2; s++) {
                            //As 4 de cima
                            trHTMLGrande = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/' + dados[s].ID + '"><img src="data:image/' + dados[s].Midia.Extensao + '; base64, ' + dados[s].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[s].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[s].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsUm').append(trHTMLGrande);
                            $('#blocoUm').append('<div class="col-md-1 col-sm-1"></div>');
                        }
                        trHTMLGrande = ''
                        for (var x = 2; x < 4; x++) {
                            //As 4 de cima
                            //$('#newsDois').append('<div class="col-md-1 col-sm-1"></div>');
                            trHTMLBaixo = '   <div class="col-md-5 col-sm-5" style="margin-top:1%">  ' +
                                '                           <a href="/Noticias/Index/' + dados[x].ID + '"><img src="data:image/' + dados[x].Midia.Extensao + '; base64, ' + dados[x].Midia.ArquivoB64 + '" style="width:100%; height:250px" />  ' +
                                '                           <div class="row">  ' +
                                '                               <div class="col-md-12">  ' +
                                '                                   <div class="blocoDeLetras">  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-12 text-center">  ' +
                                '                                               <h3>' + dados[x].Nome + '</h3>  ' +
                                '                                           </div>  ' +
                                '                                       </div>  ' +
                                '                                       <div class="row">  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                           <div class="col-md-10">  ' +
                                '                                               <p class="text-justify">' + dados[x].Descricao + '</p>  ' +
                                '                                           </div>  ' +
                                '                                           <div class="col-md-1"></div>  ' +
                                '                                       </div>  ' +
                                '                                   </div>  ' +
                                '                               </div>  ' +
                                '                           </div>  </a>' +
                                '                      </div>  ';

                            $('#newsDois').append(trHTMLBaixo);
                        }
                    }
                }
            }    
            //    var primeiro = dados[0];
            //    var segundo = dados[1];
            //    var terceiro = dados[2];

            //    console.log(primeiro);

            //    if (dados != null || dados != undefined) {
            //        //html
            //        if (dados.length > 0) {
            //            var trHTMLGrande = '   <table style="width:100%;">  ' +
            //                '                       <tr>  ' +
            //                '                           <td class="linhaFotos">  ' +
            //                '                               <a href="/Noticias/Index/' + primeiro.ID + '"><img src="data:image/' + primeiro.Midia.Extensao + '; base64, ' + primeiro.Midia.ArquivoB64 + '" style="width: 640px; height: 468px" class="img-responsive" /></a>  ' +
            //                '                               <div id="textoFoto" class="fundoTextoFoto">  ' +
            //                '                                   <h1 class="text-center" style="margin:0;">' + primeiro.Nome + '</h1>  ' +
            //                '                                   <p class="pTexto">' + primeiro.Descricao + '</p>  ' +
            //                '                               </div>  ' +
            //                '                           </td>  ' +
            //                '                       </tr>  ' +
            //                '                  </table>  ';

            //            $('#grande').append(trHTMLGrande);
            //        }

            //        if (dados.length > 1) {
            //            var trHTMLPequenoUm = '   <table style="width:100%;">  ' +
            //                '                       <tr>  ' +
            //                '                           <td class="linhaFotos">  ' +
            //                '                               <a href="/Noticias/Index/' + segundo.ID + '"><img src="data:image/' + segundo.Midia.Extensao + '; base64, ' + segundo.Midia.ArquivoB64 + '" style="width:350px; height:214px; " class="img-responsive"/></a>  ' +
            //                '                               <div id="textoFoto" class="fundoTextoFoto">  ' +
            //                '                                   <h4 class="text-center distanciaTop" style="margin:0;">' + segundo.Nome + '</h4>  ' +
            //                '                                   <p class="pTexto">' + segundo.Descricao + '</p>  ' +
            //                '                               </div>  ' +
            //                '     ' +
            //                '                           </td>  ' +
            //                '                       </tr>  ' +
            //                '                   </table>  ' +
            //                '                  <br />  ';
            //            $('#pequeno').append(trHTMLPequenoUm);
            //        }

            //        if (dados.length > 2) {
            //            var trHTMLPequenoDois = '   <table style="width:100%;margin-top:5.8%;">  ' +
            //                '                       <tr>  ' +
            //                '                           <td class="linhaFotos">                             ' +
            //                '                               <a href="/Noticias/Index/' + terceiro.ID + '"><img src="data:image/' + terceiro.Midia.Extensao + '; base64, ' + terceiro.Midia.ArquivoB64 + '" style="width:350px; height:214px;" class="img-responsive"/></a> ' +
            //                '                               <div id="textoFoto" class="fundoTextoFoto">  ' +
            //                '                                   <h4 class="text-center distanciaTop" style="margin:0;">' + terceiro.Nome + '</h4>  ' +
            //                '                                   <p class="pTexto">' + terceiro.Descricao + '</p>  ' +
            //                '                               </div>  ' +
            //                '                           </td>  ' +
            //                '                       </tr>  ' +
            //                '                  </table>  ';
            //            $('#pequeno').append(trHTMLPequenoDois);
            //        }

            //        if (dados.length > 3) {
            //            for (var i = 3; i < dados.length; i++) {
            //                console.log(dados[i].Midia);

            //                var trHTML = "";
            //                trHTML += '                           <hr class="hrTamanho"/>  ' +
            //                    '   <div class="row topp">  ' +
            //                    '                   <div>  ' +
            //                    '                       <div class="col-md-1">  ' +
            //                    '                       </div>  ' +
            //                    '                       <div class="col-md-4">  ' +
            //                    '                           <img src="data:image/' + dados[i].Midia.Extensao + '; base64, ' + dados[i].Midia.ArquivoB64 + ' " style="width:100%; margin-left:3%;" class="img-responsive"/>  ' +
            //                    '                       </div>  ' +
            //                    '                       <div class="col-md-4">  ' +
            //                    '                           <p class="titulonew">' + dados[i].Nome + '</p>  ' +
            //                    '                           <a href="/Noticias/Index/' + dados[i].ID + '"><p class="conteudoNew"><b>' + dados[i].Descricao + '</b></p></a>  ' +
            //                    '                       </div>  ' +
            //                    '                       <div class="col-md-2"></div>  ' +
            //                    '                   </div>  ' +
            //                    '              </div>  ';
            //                $('#pai').append(trHTML);
            //            };
            //        }
            //    }
            //}
        });//succes
    }//else  
});//ready


//function getWhiteLabel(nome, id) {
//    window.location.href = '/Medico/WhiteLabel/' + id + '?nome=' + nome;
//}