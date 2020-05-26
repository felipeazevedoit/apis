$(function () {
    $('#listagemTable').dataTable({
        "pagingType": "numbers",
        "columnDefs": [{
            "targets": "_all",
            "orderable": false,
        }],
        "dom": '<"top"f>rt' + "<'bottom col-sm-12'" +
            "<'row'" +
            "<'col-sm-6'l>" +
            "<'col-sm-6'p>" +
            ">" +
            ">" + '<"clear">',
        "oLanguage": {
            "sLengthMenu": "_MENU_",
            "sZeroRecords": "Nenhum registro encontrado.",
            "sInfo": "Mostrando página _PAGE_ de _PAGES_",
            "sInfoEmpty": "Nenhum dado para mostrar",
            "sInfoFiltered": "(Filtrado de _MAX_ registros)",
            "sSearch": "Pesquisar:"
        },
    });
    $(".ordenaLista").removeClass('sorting_desc');
    $(".ordenaLista").removeClass('sorting_asc');
});