using System.Data;
using System.Data.SqlClient;

namespace SalesDataServices
{
   
    public static class DataBaseHelper
    {
        public static DataSet GetDataFromDB(string conString,string sp_name, params string[] parameters)
        {
            DataSet resultsDataSet = new DataSet();
            using (SqlConnection conn = new SqlConnection(conString))
            {
                SqlCommand command = new SqlCommand(sp_name);
                command.Connection = conn;
                command.CommandType = CommandType.StoredProcedure;
                foreach (string param in parameters)
                {
                    string parameterName = param.Split(':')[0];
                    string parameterValue = param.Split(':')[1];
                    SqlParameter p = new SqlParameter(parameterName, parameterValue);
                    command.Parameters.Add(p);
                }
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(resultsDataSet);
            }
            return resultsDataSet;
        }

    }
}
