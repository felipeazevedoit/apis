function cadastrarFornecedor() {
    if ($("#nome").val() == "" || $("#cnpj").val() == "" || $("#emailResp").val() == "" || $("#cpf").val() == "" || $("#nomeResp").val() == "" || $("#telefone").val() == "" || $("#local").val() == "" || $("#numero").val() == "" || $("#cep").val() == "" || $("#email").val() == "" || $("#email").val() == "" || $("#senha").val() == "" || $("#confirmarSenha").val() == "") {
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
        if ($("#senha").val() == "") {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("ativo");
        }
        else {
            $("#lblSenha").removeClass();
            $("#lblSenha").addClass("off");
        }
        //-----------------------------------------------
        if ($("#confirmarSenha").val() == "") {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("ativo");
        }
        else {
            $("#lblConfirmarSenha").removeClass();
            $("#lblConfirmarSenha").addClass("off");
        }
        //-----------------------------------------------
        if ($("#emailResp").val() == "") {
            $("#lblEmailResp").removeClass();
            $("#lblEmailResp").addClass("ativo");
        }          
        else {     
            $("#lblEmailResp").removeClass();
            $("#lblEmailResp").addClass("off");
        }
        //-----------------------------------------------
        if ($("#cpf").val() == "") {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("ativo");
        }
        else {
            $("#lblCpf").removeClass();
            $("#lblCpf").addClass("off");
        }
        //-----------------------------------------------
        if ($("#nomeResp").val() == "") {
            $("#lblnomeResp").removeClass();
            $("#lblnomeResp").addClass("ativo");
        }
        else {
            $("#lblnomeResp").removeClass();
            $("#lblnomeResp").addClass("off");
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
        $("#lblSenha").removeClass();
        $("#lblSenha").addClass("off");
        $("#lblConfirmarSenha").removeClass();
        $("#lblConfirmarSenha").addClass("off");
        $("#lblnomeResp").removeClass();
        $("#lblnomeResp").addClass("off");
        $("#lblCpf").removeClass();
        $("#lblCpf").addClass("off");
        $("#lblEmailResp").removeClass();
        $("#lblEmailResp").addClass("off");
        cadFornecedor();
    }
}