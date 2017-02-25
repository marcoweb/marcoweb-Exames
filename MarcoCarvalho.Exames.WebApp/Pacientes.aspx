<%@ Page Title="" Language="C#" MasterPageFile="~/Default.Master" AutoEventWireup="true" CodeBehind="Pacientes.aspx.cs" Inherits="MarcoCarvalho.Exames.WebApp.Pacientes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cp_menu" runat="server">
    <div class="top-bar callout">
        <div class="top-bar-left">
            <ul class="menu">
                <li>
                    <asp:LinkButton ID="btnConfiguracao" Text="Configuração" PostBackUrl="~/Default.aspx" runat="server"></asp:LinkButton>
                </li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cp_principal" runat="server">
    <div class="row">
        <div class="large-12 column">
            <div class="row">
                <div class="large-6 columns">
                    <table>
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>Nome</th>
                                <th>Idade</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody id="table_body">
                        </tbody>
                    </table>
                </div>
                <div class="large-6 columns">
                    <div class="row">
                        <div class="large-6 columns">
                            <h3>Paciente</h3>
                        </div>
                        <div class="large-6 columns" style="text-align: right;">
                            <button type="reset" class="button" onclick="cleanForms()">Limpar Formulário</button>
                            <asp:LinkButton ID="btnSalvar" Text="Salvar" runat="server" CssClass="button"></asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <div class="large-2 columns">
                            <label>
                                Id
                                <asp:TextBox ID="txtId" runat="server" ReadOnly="true" Text="0"></asp:TextBox>
                            </label>
                        </div>
                        <div class="large-8 columns">
                            <label>
                                Nome
                                <asp:TextBox ID="txtNome" runat="server"></asp:TextBox>
                            </label>
                        </div>
                        <div class="large-2 columns">
                            <label>
                                Idade
                                <asp:TextBox ID="txtIdade" runat="server"></asp:TextBox>
                            </label>
                        </div>
                    </div>
                    <div class="row">
                        <div class="large-6 columns">
                            <h3>Exames</h3>
                        </div>
                        <div class="large-6 columns" style="text-align: right;">
                            <button id="btnNovaAfericao" type="button" disabled="true" data-open="novaAfericao" class="button">Nova Aferição</button>
                        </div>
                    </div>
                    <div class="row">
                        <table>
                            <thead>
                                <tr>
                                    <th>Aferição</th>
                                    <th>Data e Hora</th>
                                    <th></th>
                                </tr>
                            </thead>
                            <tbody id="table_body_exames">
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="reveal" id="novaAfericao" data-reveal>
        <h3>Nova Aferição</h3>
        <div class="row">
            <div class="small-12 columns">
                <label>
                    Valor Aferido
                    <asp:TextBox ID="txtAfericao" runat="server"></asp:TextBox>
                </label>
            </div>
        </div>
        <div class="row">
            <div class="small-6 columns">
                <label>
                    Data
                    <asp:TextBox ID="txtData" runat="server"></asp:TextBox>
                </label>
            </div>
            <div class="small-6 columns">
                <label>
                    Hora
                    <asp:TextBox ID="txtHora" runat="server"></asp:TextBox>
                </label>
            </div>
        </div>
        <a id="btnAddExame" data-close aria-label="Close modal" class="button" onclick="insertExame()">Salvar Aferição</a>
        <button class="close-button" data-close aria-label="Close modal" type="button">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
</asp:Content>
<asp:Content ID="Content6" ContentPlaceHolderID="cp_script" runat="server">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.mask/1.14.3/jquery.mask.js"></script>
    <script src="Scripts/pacientes.js"></script>
</asp:Content>
