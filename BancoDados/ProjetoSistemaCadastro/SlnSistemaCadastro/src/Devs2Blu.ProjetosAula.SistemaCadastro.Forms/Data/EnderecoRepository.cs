using Devs2Blu.ProjetosAula.SistemaCadastro.Models.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Devs2Blu.ProjetosAula.SistemaCadastro.Forms.Data
{
    public class EnderecoRepository
    {

        public static void SaveEndereco(Endereco endereco, MySqlConnection conn, Int32 idpessoa)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL_INSERT_ENDERECO, conn);
                cmd.Parameters.Add("@idPessoa", MySqlDbType.Int32).Value = idpessoa;
                cmd.Parameters.Add("@CEP", MySqlDbType.VarChar, 50).Value = endereco.CEP;
                cmd.Parameters.Add("@rua", MySqlDbType.VarChar, 25).Value = endereco.Rua;
                cmd.Parameters.Add("@numero", MySqlDbType.Enum).Value = endereco.Numero;
                cmd.Parameters.Add("@bairro", MySqlDbType.Enum).Value = endereco.Bairro;
                cmd.Parameters.Add("@cidade", MySqlDbType.Enum).Value = endereco.Cidade;
                cmd.Parameters.Add("@UF", MySqlDbType.Enum).Value = endereco.UF;

                cmd.ExecuteNonQuery();            }
            catch (MySqlException myExc)
            {
                MessageBox.Show(myExc.Message, "Erro de MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        internal MySqlDataReader GetEnderecos()
        {
            MySqlConnection conn = ConnectionMySQL.GetConnection();

            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL_SELECT_ENDERECOS, conn);
                MySqlDataReader dataReader = cmd.ExecuteReader();

                return dataReader;
            }
            catch (MySqlException myExc)
            {
                MessageBox.Show(myExc.Message, "Erro de MySQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw;
            }
        }

        #region SQLS
        private const String SQL_INSERT_ENDERECO = @"INSERT INTO endereco
(id_pessoa,
CEP,
rua,
numero,
bairro,
cidade,
uf)
VALUES
(@idPessoa,
@CEP,
@rua,
@numero,
@bairro,
@cidade,
@uf)";
        private const String SQL_SELECT_ENDERECOS = @"SELECT id, id_pessoa, CEP, rua,numero, bairro, cidade, uf from endereco";
        #endregion
    }
}
