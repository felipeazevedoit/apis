function cadastrarClinica() {
    if ($("#nome").val() == "" || $("#cnpj").val() == "" || $("#telefone").val() == "" || $("#local").val() == "" || $("#numero").val() == "" || $("#cep").val() == "" || $("#email").val() == "" || $("#crm").val() == "" || $("#responsavel").val() == "") {
        if ($("#nome").val() == "") {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("ativo");
        }
        else {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("off");
        }
        //-----------------------------------------------
        if ($("#cnpj").val() == "") {
            $("#lblCnpj").removeClass();
            $("#lblCnpj").addClass("ativo");
        }
        else {
            $("#lblCnpj").removeClass();
            $("#lblCnpj").addClass("off");
        }
        //-----------------------------------------------
        if ($("#telefone").val() == "") {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("ativo");
        }
        else {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("off");
        }
        //-----------------------------------------------
        if ($("#local").val() == "") {
            $("#lblLocal").removeClass();
            $("#lblLocal").addClass("ativo");
        }
        else {
            $("#lblLocal").removeClass();
            $("#lblLocal").addClass("off");
        }
        //-----------------------------------------------
        if ($("#numero").val() == "") {
            $("#lblNumero").removeClass();
            $("#lblNumero").addClass("ativo");
        }
        else {
            $("#lblNumero").removeClass();
            $("#lblNumero").addClass("off");
        }
        //-----------------------------------------------
        if ($("#cep").val() == "") {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("ativo");
        }
        else {
            $("#lblCep").removeClass();
            $("#lblCep").addClass("off");
        }
        //-----------------------------------------------
        if ($("#email").val() == "") {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("ativo");
        }
        else {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("off");
        }
        //-----------------------------------------------
        if ($("#crm").val() == "") {
            $("#lblCrm").removeClass();
            $("#lblCrm").addClass("ativo");
        }
        else {
            $("#lblCrm").removeClass();
            $("#lblCrm").addClass("off");
        }
        //-----------------------------------------------
        if ($("#responsavel").val() == "") {
            $("#lblResponsavel").removeClass();
            $("#lblResponsavel").addClass("ativo");
        }
        else {
            $("#lblResponsavel").removeClass();
            $("#lblResponsavel").addClass("off");
        }
    }
    else {
        $("#lblNome").removeClass();
        $("#lblNome").addClass("off");
        $("#lblCnpj").removeClass();
        $("#lblCnpj").addClass("off");
        $("#lblTelefone").removeClass();
        $("#lblTelefone").addClass("off");
        $("#lblLocal").removeClass();
        $("#lblLocal").addClass("off");
        $("#lblNumero").removeClass();
        $("#lblNumero").addClass("off");
        $("#lblCep").removeClass();
        $("#lblCep").addClass("off");
        $("#lblEmail").removeClass();
        $("#lblEmail").addClass("off");
        cadClinica();
    }
}