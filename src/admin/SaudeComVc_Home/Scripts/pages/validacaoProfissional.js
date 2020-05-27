function cadastrarProfissional() {
    if ($("#nome").val() == "" || $("#sobrenome").val() == "" || $("#telefone").val() == "" || $("#local").val() == "" || $("#numero").val() == "" || $("#cep").val() == "" || $("#email").val() == "" || $("#email").val() == "" || $("#senha").val() == "" || $("#confirmarSenha").val() == "" || $("#registro").val() == "" || $("#cpf").val() == "" || $("#dataNascimento").val() == "") {
        if ($("#nome").val() == "") {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("ativo");
        }
        else {
            $("#lblNome").removeClass();
            $("#lblNome").addClass("off");
        }
        //-----------------------------------------------
        if ($("#sobrenome").val() == "") {
            $("#lblSobrenome").removeClass();
            $("#lblSobrenome").addClass("ativo");
        }
        else {
            $("#lblSobrenome").removeClass();
            $("#lblSobrenome").addClass("off");
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
            $("#lblConfrimarSenha").removeClass();
            $("#lblConfrimarSenha").addClass("ativo");
        }
        else {
            $("#lblConfrimarSenha").removeClass();
            $("#lblConfrimarSenha").addClass("off");
        }
        //-----------------------------------------------
        if ($("#registro").val() == "") {
            $("#lblRegistro").removeClass();
            $("#lblRegistro").addClass("ativo");
        }
        else {
            $("#lblRegistro").removeClass();
            $("#lblRegistro").addClass("off");
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
        if ($("#dataNascimento").val() == "") {
            $("#lblData").removeClass();
            $("#lblData").addClass("ativo");
        }
        else {
            $("#lblData").removeClass();
            $("#lblData").addClass("off");
        }
    }
    else {
        $("#lblNome").removeClass();
        $("#lblNome").addClass("off");
        $("#lblSobrenome").removeClass();
        $("#lblSobrenome").addClass("off");
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
        $("#lblConfrimarSenha").removeClass();
        $("#lblConfrimarSenha").addClass("off");
        cadProfissional();
    }
}