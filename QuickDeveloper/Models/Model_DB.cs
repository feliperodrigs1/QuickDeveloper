using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuickDeveloper.Models
{
    public class Model_DB
    {
        SqlConnection sqlConnection;

        public Model_DB()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection"));
        }

        public void Register_Requisition(int IdUser, int IdDev, int idRequisition, string Description)
        {
            sqlConnection.Open();

            using (SqlCommand sqlCommand = new SqlCommand("spSLN_InsertUpdateRequisition", sqlConnection))
            {

                sqlCommand.Parameters.Clear();
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@IDDEV", IdUser);
                sqlCommand.Parameters.AddWithValue("@IDUSER", IdDev);
                sqlCommand.Parameters.AddWithValue("@IDREQUISITION", idRequisition);
                sqlCommand.Parameters.AddWithValue("@DESCRIPTION", Description);

                var resultado = sqlCommand.ExecuteScalar();
            }

            sqlConnection.Close();
         
        }

    }
}
