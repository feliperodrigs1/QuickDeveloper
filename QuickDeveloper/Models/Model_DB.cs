using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace QuickDeveloper.Models
{
    public class Model_DB
    {
        private static readonly Lazy<Model_DB> lazy = new Lazy<Model_DB>(() => new Model_DB());
        public static Model_DB Instance { get { return lazy.Value; } }
        public SqlConnection sqlConnection { get; set; }
        public SqlDataAdapter adapter { get; set; }
        
        public Model_DB()
        {
            var builder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true);
            IConfiguration _configuration = builder.Build();
            sqlConnection = new SqlConnection(_configuration.GetConnectionString("Connection"));
        }

        public static void Register_Requisition(int IdUser, int IdDev, int idRequisition, string Description)
        {
            try
            {
                Model_DB.Instance.sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("spSLN_InsertUpdateRequisition", Model_DB.Instance.sqlConnection);
                var parameters = new DynamicParameters();

                parameters.Add("@IDDEV", IdUser);
                parameters.Add("@IDUSER", IdDev);
                parameters.Add("@IDREQUISITION", idRequisition);
                parameters.Add("@DESCRIPTION", Description);

                var result = Model_DB.Instance.sqlConnection.Query<Model_View_User>("spSLN_InsertUpdateRequisition", parameters, commandType: CommandType.StoredProcedure);

                Model_DB.Instance.sqlConnection.Close();
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static Model_View_User Data_User(string IdUser)
        {
            try
            {
                int id = Convert.ToInt32(IdUser);

                Model_DB.Instance.sqlConnection.Open();

                var parameters = new DynamicParameters();            

                parameters.Add("@IDUSER", IdUser);

                var user = Model_DB.Instance.sqlConnection.Query<Model_View_User>("spSLN_ShowUser", parameters, commandType: CommandType.StoredProcedure).ToList()[0];

                Model_DB.Instance.sqlConnection.Close();                

                return (Model_View_User)user;

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
