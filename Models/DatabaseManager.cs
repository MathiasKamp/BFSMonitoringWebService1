using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BFSMonitoringWebService1.Models
{
    public class DatabaseManager
    {

        private static SqlDataReader rdr = null;
        public string Con()
        {
            //return ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

            return
                @"Data Source=192.168.1.253;Initial Catalog=bfsWebService;Persist Security Info=True;User ID=sa;Password=Kode1234!";
        }

        public List<StatusMessage> GetLastStatusMessage(string agntName)
        {
            string con = Con();

            List<StatusMessage> statusMessages = new List<StatusMessage>();

            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    SqlCommand sql =
                        new SqlCommand(
                            "select top 1 * from BfsAgentMessage where agentName = @agentName order by messageNumber desc", connection);
                    
                    sql.Parameters.Add(new SqlParameter("@agentName", agntName));

                    rdr = sql.ExecuteReader();

                    while (rdr.Read())
                    {
                        string agentName = (string) rdr["agentName"];
                        string directory = (string) rdr["directory"];
                        string fileName = (string) rdr["fileName"];
                        DateTime dateChecked = (DateTime)rdr["dateChecked"];
                        string status = (string) rdr["status"];
                        DateTime lastModifiedDate= (DateTime) rdr["lastModifiedDate"];

                        StatusMessage statusMessage = new StatusMessage(agentName, directory, fileName, dateChecked,
                            status, lastModifiedDate);
                        
                        statusMessages.Add(statusMessage);
                        
                    }
                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }

                finally
                {
                    connection?.Close();

                    rdr?.Close();
                }
            }

            return statusMessages;
        }
        
        public void InsertStatusMessage(StatusMessage statusMessage)
        {
            string con = Con();
            
            using (SqlConnection connection = new SqlConnection(con))
            {
                try
                {
                    connection.Open();

                    SqlCommand sql = new SqlCommand("sp_InsertStatusMessage", connection)
                    {
                        CommandType =  CommandType.StoredProcedure
                    };

                    sql.Parameters.Add(new SqlParameter("@AgentName", statusMessage.AgentName));
                    sql.Parameters.Add(new SqlParameter("@directory", statusMessage.Directory));
                    sql.Parameters.Add(new SqlParameter("@fileName", statusMessage.FileName));
                    sql.Parameters.Add(new SqlParameter("@dateChecked", statusMessage.DateChecked));
                    sql.Parameters.Add(new SqlParameter("@status", statusMessage.Status));
                    sql.Parameters.Add(new SqlParameter("@lastModifiedDate", statusMessage.LastModifiedDate));


                    sql.ExecuteNonQuery();
                    

                }
                finally
                {
                    connection?.Close();
                    
                }
                
            }
        }
    }
}