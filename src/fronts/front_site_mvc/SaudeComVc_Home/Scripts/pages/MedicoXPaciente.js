function PesquisarPac() {
    var idPac = $("#inlineFormCustomSelectPref").val();

    $.ajax({
        type: "POST",
        url: '@Url.Action("Visualizar", " MedicoXPaciente")',
        data: { id: idPac },
        dataType: "json",
        success: function (response) {

        }
    });
}