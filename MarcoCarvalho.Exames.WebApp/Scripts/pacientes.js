function loadTable() {
    $.ajax({
        type: "POST",
        url: "Pacientes.aspx/GetPacientes",
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#table_body").empty();
            $(data.d).each(function () {
                var row = "<tr><td><a onclick='setId(" + this.Id + ")'>" + this.Id + "</a></td><td>" + this.Nome + "</td><td>" + this.Idade + "</td><td><a onclick='deletePaciente(" + this.Id + ")'> Excluir </a></td></tr>";
                $("#table_body").append(row);
            });
        }
    });
}

function loadTableExames(id) {
    $.ajax({
        type: "POST",
        url: "Pacientes.aspx/GetExames",
        data: JSON.stringify({ id: id }),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (data) {
            $("#table_body_exames").empty();
            $(data.d).each(function () {
                var row = "<tr><td>" + this.Afericao + "</td><td>" + this.Data + ' ' + this.Hora + "</td><td><a onclick='deleteExame(" + this.Id + ")'> Excluir </a></td></tr>";
                $("#table_body_exames").append(row);
            });
            $("#btnNovaAfericao").prop('disabled', false);
        }
    });
}

function insertExame() {
    $.ajax({
        type: 'POST',
        url: 'Pacientes.aspx/AddExame',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ afericao: $("#cp_principal_txtAfericao").val(), data: $("#cp_principal_txtData").val(), hora: $("#cp_principal_txtHora").val(), id_paciente : $("#cp_principal_txtId").val() }),
        dataType: 'json',
        success: function () {
            loadTableExames($("#cp_principal_txtId").val());
        }
    });
    cleanExameForm();
}

function deleteExame(id) {
    $.ajax({
        type: 'POST',
        url: 'Pacientes.aspx/DeleteExame',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: id }),
        dataType: 'json',
        success: function () {
            loadTableExames($("#cp_principal_txtId").val());
        }
    });
}

function deletePaciente(id)
{
    $.ajax({
        type: 'POST',
        url: 'Pacientes.aspx/DeletePaciente',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: id }),
        dataType: 'json',
        success: function () {
            loadTable();
        }
    });
}

function loadPaciente(id)
{
    $.ajax({
        type: 'POST',
        url: 'Pacientes.aspx/GetPaciente',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: id}),
        dataType: 'json',
        success: function (result) {
            $("#cp_principal_txtNome").val(result["d"].Nome);
            $("#cp_principal_txtIdade").val(result["d"].Idade);
            loadTableExames(id);
        },
        error: function () {
            console.log('Erro');
        }
    });
}


function updatePaciente()
{
    if ($("#cp_principal_txtNome").val() != "" && $("#cp_principal_txtIdade").val() != "")
    $.ajax({
        type: 'POST',
        url: 'Pacientes.aspx/UpdatePaciente',
        contentType: 'application/json; charset=utf-8',
        data: JSON.stringify({ id: $("#cp_principal_txtId").val(), nome: $("#cp_principal_txtNome").val(), idade: $("#cp_principal_txtIdade").val() }),
        dataType: 'json'
    });
}

function cleanExameForm()
{
    $("#cp_principal_txtAfericao").val("");
    $("#cp_principal_txtData").val("");
    $("#cp_principal_txtHora").val("");
}

function cleanForms() {
    $("#cp_principal_txtId").val("0");
    $("#cp_principal_txtNome").val("");
    $("#cp_principal_txtIdade").val("");
    $("#table_body_exames").empty();
    $("#btnNovaAfericao").prop('disabled', true);
}

function setId(id) {
    $("#cp_principal_txtId").val(id);
    loadPaciente(id);
}
$(document).ready(function () {
    $("#cp_principal_txtData").mask('00/00/0000');
    $("#cp_principal_txtHora").mask('00:00');
    $("#cp_principal_txtAfericao").mask('##0');
    $("#cp_principal_txtIdade").mask('###');

    loadTable();
    $("#cp_principal_btnSalvar").click(function () { updatePaciente(); });
});                                                                