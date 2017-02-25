using MarcoCarvalho.Exames.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace MarcoCarvalho.Exames.Data.Repositories
{
    public class PacienteRepository : BaseRepository<Paciente>
    {
        public override void DeleteOneById(int id)
        {
            try
            {
                using (this._connection)
                {
                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "DELETE FROM pacientes WHERE id = @id";

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

        public override IEnumerable<Paciente> FetchAll()
        {
            List<Paciente> pacientes = new List<Paciente>();
            try
            {
                using (_connection)
                {

                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "SELECT * FROM pacientes";
                    DbDataReader dados = command.ExecuteReader();
                    if (dados.HasRows)
                    {
                        while (dados.Read())
                        {
                            pacientes.Add(new Paciente()
                            {
                                Id = (int)dados["id"],
                                Nome = (string)dados["nome"],
                                Idade = (int)dados["idade"]
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
            return pacientes;
        }

        public override Paciente FetchOneById(int id)
        {
            Paciente paciente = new Paciente();
            try
            {
                using (_connection)
                {

                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "SELECT * FROM pacientes WHERE id = @id";

                    DbParameter parId = command.CreateParameter();
                    parId.ParameterName = "@id";
                    parId.Value = id;
                    command.Parameters.Add(parId);

                    DbDataReader dados = command.ExecuteReader();
                    if (dados.HasRows)
                    {
                        dados.Read();
                        paciente.Id = (int)dados["id"];
                        paciente.Nome = (string)dados["nome"];
                        paciente.Idade = (int)dados["idade"];
                    }else
                    {
                        dados.Close();
                        throw new RowNotInTableException();
                    }
                    dados.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return paciente;
        }

        public override Paciente Insert(Paciente paciente)
        {
            try
            {
                using (this._connection)
                {
                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = (paciente.Id > 0) ?
                        "INSERT INTO pacientes(id, nome, idade) VALUES(@id, @nome, @idade);" :
                        "INSERT INTO pacientes(nome, idade) VALUES(@nome, @idade);";
                    command.CommandText += "SELECT LAST_INSERT_ID();";

                    if (paciente.Id > 0)
                    {
                        DbParameter parId = command.CreateParameter();
                        parId.ParameterName = "@id";
                        parId.Value = paciente.Id;
                        command.Parameters.Add(parId);
                    }
                    DbParameter parNome = command.CreateParameter();
                    parNome.ParameterName = "@nome";
                    parNome.Value = paciente.Nome;
                    command.Parameters.Add(parNome);
                    DbParameter parIdade = command.CreateParameter();
                    parIdade.ParameterName = "@idade";
                    parIdade.Value = paciente.Idade;
                    command.Parameters.Add(parIdade);

                    paciente.Id = Int32.Parse(command.ExecuteScalar().ToString());
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return paciente;
        }

        public override Paciente Update(Paciente paciente)
        {
            try
            {
                this.FetchOneById(paciente.Id);
                using (this._connection)
                {
                    this._connection.Open();
                    DbCommand command = this._connection.CreateCommand();
                    command.CommandText = "UPDATE pacientes SET nome = @nome, idade = @idade WHERE id = @id;";

                    DbParameter parId = command.CreateParameter();
                    parId.ParameterName = "@id";
                    parId.Value = paciente.Id;
                    command.Parameters.Add(parId);

                    DbParameter parNome = command.CreateParameter();
                    parNome.ParameterName = "@nome";
                    parNome.Value = paciente.Nome;
                    command.Parameters.Add(parNome);
                    DbParameter parIdade = command.CreateParameter();
                    parIdade.ParameterName = "@idade";
                    parIdade.Value = paciente.Idade;
                    command.Parameters.Add(parIdade);

                    command.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return paciente;
        }
    }
}
