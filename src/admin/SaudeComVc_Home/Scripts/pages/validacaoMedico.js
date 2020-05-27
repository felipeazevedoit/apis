function cadastrarMedico() {
    if ($("#nome").val() == "" || $("#cpf").val() == "" || $("#telefone").val() == "" || $("#crm").val() == "" || $("#uf-crm").val() == "" || $("#local").val() == "" || $("#numero").val() == "" || $("#cep").val() == "" ||$("#email").val() == "" || $("#senha").val() == "" || $("#confirmarSenha").val() == "") {
        if ($("#nome").val() == "") {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("ativo");
        }
        else {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("off");
        }
        //---------------------------------------------
        if ($("#cpf").val() == "") {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("ativo");
        }
        else {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("off");
        }
        //---------------------------------------------
        if ($("#crm").val() == "") {
            $("#lblCrm").removeClass();
            $("#lblCrm").addClass("ativo");
        }
        else {
            $("#lblCrm").removeClass();
            $("#lblCrm").addClass("off");
        }
        //---------------------------------------------
        if ($("#uf-crm").val() == "") {
            $("#lblUfCrm").removeClass();
            $("#lblUfCrm").addClass("ativo");
        }
        else {
            $("#lblUfCrm").removeClass();
            $("#lblUfCrm").addClass("off");
        }
        //---------------------------------------------
        if ($("#email").val() == "") {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("ativo");
        }
        else {
            $("#lblEmail").removeClass();
            $("#lblEmail").addClass("off");
        }
        //---------------------------------------------
        if ($("#senha").val() == "") {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("ativo");
        }
        else {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("off");
        }
        //---------------------------------------------
        if ($("#confirmarSenha").val() == "") {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("ativo");
        }
        else {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("off");
        }
        //---------------------------------------------
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
        if ($("#telefone").val() == "") {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("ativo");
        }
        else {
            $("#lblTelefone").removeClass();
            $("#lblTelefone").addClass("off");
        }
        //-----------------------------------------------
    }
    else {
        $("#lblLocal").removeClass();
        $("#lblLocal").addClass("off");
        $("#lblNumero").removeClass();
        $("#lblNumero").addClass("off");
        $("#lblCep").removeClass();
        $("#lblCep").addClass("off");
        $("#lblNome").removeClass();
        $("#lblNome").addClass("off");
        $("#lblCpf").removeClass();
        $("#lblCpf").addClass("off");
        $("#lblCrm").removeClass();
        $("#lblCrm").addClass("off");
        $("#lblEmail").removeClass();
        $("#lblEmail").addClass("off");
        $("#lblSenha").removeClass();
        $("#lblSenha").addClass("off");
        $("#lblConfirmarSenha").removeClass();
        $("#lblConfirmarSenha").addClass("off");
        $("#lblTelefone").removeClass();
        $("#lblTelefone").addClass("off");
        cadMedico();
    }
}