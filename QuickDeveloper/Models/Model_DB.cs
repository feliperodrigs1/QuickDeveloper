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

        public static bool Register_Requisition(Model_Requisition requisition)
        {
            try
            {
                if (Instance.sqlConnection.State != ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }
                
                var parameters = new DynamicParameters();

                parameters.Add("@IDDEV", requisition.idDeveloper);
                parameters.Add("@IDUSER", requisition.idUser);
                parameters.Add("@IDREQUISITION", requisition.idRequisition);
                parameters.Add("@DESCRIPTION", requisition.description);

                var result = Instance.sqlConnection.Query<int>("spSLN_InsertUpdateRequisition", parameters, commandType: CommandType.StoredProcedure).ToList()[0];

                Convert.ToInt32(result);

                return true;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);                
            }      
            finally
            {
                Instance.sqlConnection.Close();
            }      
        }
        
        public static Model_View_User UserRequisition(string email)
        {
            try
            {                
                if (Instance.sqlConnection.State != ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }

                var parameters = new DynamicParameters();

                parameters.Add("@EMAIL", email);

                var user = Instance.sqlConnection.Query<Model_View_User>("spSLN_FindUserByEmail", parameters, commandType: CommandType.StoredProcedure).ToList()[0];

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Instance.sqlConnection.Close();
            }
        }

        public static Model_View_User Data_User(string IdUser)
        {
            try
            {
                int id = Convert.ToInt32(IdUser);

                if (Instance.sqlConnection.State != ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }

                var parameters = new DynamicParameters();            

                parameters.Add("@IDUSER", IdUser);

                var user = Instance.sqlConnection.Query<Model_View_User>("spSLN_ShowUser", parameters, commandType: CommandType.StoredProcedure).ToList()[0];

                return user;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Instance.sqlConnection.Close();
            }
        }

        public static void UpdateData_User(Model_View_User datauser)
        {
            try
            {
                if (Instance.sqlConnection.State != ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }
                var parameters = new DynamicParameters();
                
                parameters.Add("@USERNAME", datauser.Username);
                parameters.Add("@EMAIL", datauser.Email);
                parameters.Add("@COMPETENCES", datauser.Competences);
                parameters.Add("@ADITIONALINFO", datauser.AditionalInfo);
                parameters.Add("@BIRTHDATE", datauser.Birthdate);


                var result = Instance.sqlConnection.Query<int>("spSLN_AlterUser", parameters, commandType: CommandType.StoredProcedure).ToList()[0];

                Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Instance.sqlConnection.Close();
            }
        }

        public static List<Model_Requisition> Requisitions(string IdUser, string role)
        {
            try
            {
                int id = Convert.ToInt32(IdUser);

                if (Instance.sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }
                    
                var parameters = new DynamicParameters();

                parameters.Add("@IDUSER", id);
                parameters.Add("@ROLE", role.ToUpper());

                var result = Instance.sqlConnection.Query<Model_Requisition>("spSLN_ShowRequisition", parameters, commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Instance.sqlConnection.Close();
            }            
        }

        public static List<Model_View_User_Competences> UserByCompetence(string competences)
        {
            try
            {
                if (Instance.sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    Instance.sqlConnection.Open();
                }

                var parameters = new DynamicParameters();

                parameters.Add("@COMPETENCES", competences.Replace('e',',').Replace('.',' ').ToUpper().Trim());

                var result = Instance.sqlConnection.Query<Model_View_User_Competences>("spSLN_FindUser_by_Competence", parameters, commandType: CommandType.StoredProcedure).ToList();

                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                Instance.sqlConnection.Close();
            }
        }
    }
}
