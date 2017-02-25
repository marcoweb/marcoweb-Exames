using MarcoCarvalho.Exames.Data.Entities;
using MarcoCarvalho.Exames.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MarcoCarvalho.Exames.WebApp
{
    public partial class Pacientes : System.Web.UI.Page
    {
        protected static PacienteRepository _repository = new PacienteRepository();
        protected static ExameRepository _repository_ex = new ExameRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static IEnumerable<Paciente> GetPacientes()
        {
            return _repository.FetchAll();
        }

        [WebMethod]
        public static Paciente GetPaciente(string id)
        {
            return _repository.FetchOneById(Int32.Parse(id));
        }

        [WebMethod]
        public static void DeletePaciente(string id)
        {
            _repository.DeleteOneById(Int32.Parse(id));
        }

        [WebMethod]
        public static Paciente UpdatePaciente(string id, string nome, string idade)
        {
            Paciente paciente = new Paciente()
            {
                Id = Int32.Parse(id),
                Nome = nome,
                Idade = Int32.Parse(idade)
            };
            if (paciente.Id > 0)
                 paciente = _repository.Update(paciente);
            else
                paciente = _repository.Insert(paciente);
            return paciente;
        }

        [WebMethod]
        public static Exame AddExame(string afericao, string data, string hora, string id_paciente)
        {
            Exame exame = new Exame()
            {
                Afericao = int.Parse(afericao),
                DataHora = DateTime.Parse(data + " " + hora),
                IdPaciente = int.Parse(id_paciente)
            };
            exame = _repository_ex.Insert(exame);
            return exame;
        }

        [WebMethod]
        public static IEnumerable<Exame> GetExames(string id)
        {
            return _repository_ex.FetchByPacienteId(Int32.Parse(id));
        }

        [WebMethod]
        public static void DeleteExame(string id)
        {
            _repository_ex.DeleteOneById(Int32.Parse(id));
        }

        /*
        protected void btnSalvar_Click(object sender, EventArgs e)
        {
           if(Int32.Parse(txtId.Text) > 0)
            {
                _repository.Update(new Paciente() {
                    Id = Int32.Parse(txtId.Text),
                    Nome = txtNome.Text,
                    Idade = Int32.Parse(txtIdade.Text)});
            }else
            {
                Paciente paciente = _repository.Insert(new Paciente() {
                    Nome = txtNome.Text,
                    Idade = Int32.Parse(txtIdade.Text) });
                txtId.Text = paciente.Id.ToString();
            }
        }*/
    }
}