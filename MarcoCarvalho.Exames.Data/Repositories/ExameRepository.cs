using MarcoCarvalho.Exames.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarcoCarvalho.Exames.Data.Repositories
{
    public class ExameRepository : BaseRepository<Exame>
    {
        public override void DeleteOneById(int id)
        {
            try
            {
                using (this._connection)
                {
                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "DELETE FROM exames WHERE id = @id";

                    DbParameter parId = command.CreateParameter();
                    parId.ParameterName = "@id";
                    parId.Value = id;
                    command.Parameters.Add(parId);

                    command.ExecuteNonQuery();

                    this._connection.Close();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override IEnumerable<Exame> FetchAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Exame> FetchByPacienteId(int id)
        {
            List<Exame> exames = new List<Exame>();
            try
            {
                using (_connection)
                {

                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "SELECT * FROM exames WHERE id_paciente = @id ORDER BY data_hora DESC";

                    DbParameter parId = command.CreateParameter();
                    parId.ParameterName = "@id";
                    parId.Value = id;
                    command.Parameters.Add(parId);

                    DbDataReader dados = command.ExecuteReader();
                    if (dados.HasRows)
                    {
                        while (dados.Read())
                        {
                            exames.Add(new Exame()
                            {
                                Id = (int)dados["id"],
                                IdPaciente = (int)dados["id_paciente"],
                                Afericao = (int)dados["afericao"],
                                DataHora = (DateTime)dados["data_hora"]
                            });
                        }
                    }
                    dados.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return exames;
        }

        public override Exame FetchOneById(int id)
        {
            throw new NotImplementedException();
        }

        public override Exame Insert(Exame exame)
        {
            try
            {
                using (this._connection)
                {
                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = (exame.Id > 0) ?
                        "INSERT INTO exames(id, afericao, data_hora, id_paciente) VALUES(@id, @afericao, @data_hora, @id_paciente);" :
                        "INSERT INTO exames(afericao, data_hora, id_paciente) VALUES(@afericao, @data_hora, @id_paciente);";
                    command.CommandText += "SELECT LAST_INSERT_ID();";

                    if (exame.Id > 0)
                    {
                        DbParameter parId = command.CreateParameter();
                        parId.ParameterName = "@id";
                        parId.Value = exame.Id;
                        command.Parameters.Add(parId);
                    }
                    DbParameter parAfericao = command.CreateParameter();
                    parAfericao.ParameterName = "@afericao";
                    parAfericao.Value = exame.Afericao;
                    command.Parameters.Add(parAfericao);
                    DbParameter parDataHora = command.CreateParameter();
                    parDataHora.ParameterName = "@data_hora";
                    parDataHora.Value = exame.DataHora;
                    command.Parameters.Add(parDataHora);
                    DbParameter parIdPaciente = command.CreateParameter();
                    parIdPaciente.ParameterName = "@id_paciente";
                    parIdPaciente.Value = exame.IdPaciente;
                    command.Parameters.Add(parIdPaciente);

                    exame.Id = Int32.Parse(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return exame;
        }

        public override Exame Update(Exame entity)
        {
            throw new NotImplementedException();
        }
    }
}
