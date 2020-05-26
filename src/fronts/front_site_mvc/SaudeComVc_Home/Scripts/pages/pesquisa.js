function pesquisarNM() {
    var texto = $("#basics").val();
    window.location.href = '/Pesquisa/Index?texto=' + texto;
}