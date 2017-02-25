using System;   // Necessário para : EventArgs, Array, Exception
using System.IO;    // Necessário para : StreamReader
using System.Collections.Generic;   // Necessário para : List
using System.Linq;  // Necessário para o mpetodo de extensão : Count [Array]
using System.Web.Configuration; // Necessário para : WebConfigurationManager
using MarcoCarvalho.Exames.Data.Common;

namespace MarcoCarvalho.Capacitacao.Biblioteca
{
    public partial class Default1 : System.Web.UI.Page
    {
        protected string sqlCode;
        protected List<string> tables;
        protected string[] confirmTables = { "pacientes", "exames"};
        
        // Método utilizado para Habilitar ou Desabilitar dos controles do formulário
        protected void enableDisableControls()
        {
            btnExecute.Enabled = !btnExecute.Enabled;
            btnExecute.CssClass = (btnExecute.CssClass.Equals("button")) ? "button disabled" : "button";
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var appDataPath = Server.MapPath("~/App_Data/");
            // Verifica se o diretório "App_Data" existe
            if (!Directory.Exists(appDataPath))
            {
                // Cria o diretório caso não exista
                Directory.CreateDirectory(appDataPath);
            }

            // Lê o arquivo com script SQL e armazena na propriedade sqlCode
            using (StreamReader sr = new StreamReader(Path.Combine(appDataPath, "schema.sql")))
            {
                this.sqlCode = sr.ReadToEnd();
            }
            // Verifica se a base de dados e suas tabelas já foram criadas
            try
            {
                this.tables = ConnectionFactory.GetTables();
                int verificadas = 0;
                if (this.tables.Count > 0)
                {
                    foreach (var table in this.tables)
                        if (Array.Exists(this.confirmTables, element => element.Equals(table)))
                            verificadas++;
                }
                if (verificadas == confirmTables.Count())
                    lblMessage.Text = "Base de Dados Já Criada";
                else
                    this.enableDisableControls();
            }
            catch (Exception ex)
            {
                this.enableDisableControls();
            }
        }

        // Manipulador do evento Click do botão btnExecutar
        protected void btnExecute_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectionFactory.executeCreationScript(this.sqlCode);
                lblMessage.Text = "Base de Dados Criada com Sucesso";
            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                this.enableDisableControls();
            }
        }
    }
}