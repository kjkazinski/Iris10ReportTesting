using System.Data.SqlClient;
using System.Data;
using System;
using System.Linq;
using System.Configuration;
using System.Reflection;
using System.Collections.Generic;

namespace IrisWeb
{
    public static class Adocls
    {

        //5 minute timeout, 300 seconds
        private const int MintCommandTimeout = 300;
        
        private static string ConnectionStringLookup(string name)
        {
            if (name == "CountyDatabase")
            {
                return "Initial Catalog=Jefferson;Data Source=localhost;User ID=developer;Password=aociris;";
                //return "Initial Catalog=A_Wallowa9;Data Source=10.0.0.40;User ID=developer;Password=aociris;";
                // return "Initial Catalog=Z_Marion;Data Source=192.168.104.202;User ID=developer;Password=aociris;";
            }
            else
            {
                return "Initial Catalog=IRISSystemData;Data Source=10.0.0.40;User ID=developer;Password=aociris;";
                //return "Initial Catalog="+name+";Data Source=192.168.104.202;User ID=developer;Password=aociris;";
            }
        }

        public static bool AddConnectionString(string connectionName, string connectionString)
        {
            try
            {
                ConfigurationManager.ConnectionStrings.Add(new ConnectionStringSettings(connectionName, connectionString));
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddCountySpecificConnectionString(string databaseName)
        {
            try
            {
                return AddConnectionString("CountyDatabase", "Initial Catalog=" + databaseName + ";Data Source=192.168.104.202;User ID=developer;Password=aociris;");
            }
            catch
            {
                return false;
            }
        }

    #region  Fetch Data Methods ****************************************************************************

        public static DataSet FetchDataSet(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            DataSet tempFetchDataSet = null;
            //Changed the function to use the command object so that the connection timeout can be set for this command.
            SqlDataAdapter objDA = null;
            DataSet objDS = new DataSet();
            SqlCommand objCommand = new SqlCommand();
            SqlConnection objCon = new SqlConnection(ConnectionStringLookup(connectionName));

            try
            {
                objCommand.CommandText = gen.SqlString;

                foreach(SqlParameter param in gen.SqlVariables)
                {
                    objCommand.Parameters.Add(param);
                }

                objCommand.Connection = objCon;
                objCommand.CommandTimeout = MintCommandTimeout;
                objCommand.CommandType = CommandType.Text;

                objDA = new SqlDataAdapter(objCommand);
                objDA.Fill(objDS);

                tempFetchDataSet = objDS;

            }
            catch (Exception )
            {
                
                ////ProcessError(ex, strSQL);
            }
            finally
            {
                objCon.Close();
            }
            return tempFetchDataSet;
        }


        public static DataTable FetchDataTable(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            try
            {
                SqlDataAdapter objDA = null;
                DataTable objDT = new DataTable();

                objDA = new SqlDataAdapter(gen.SqlString, ConnectionStringLookup(connectionName));

                objDA.SelectCommand.CommandTimeout = MintCommandTimeout;

                foreach (SqlParameter param in gen.SqlVariables)
                {
                    objDA.SelectCommand.Parameters.Add(param);
                }

                objDA.Fill(objDT);

                return objDT;
            }
            catch (Exception ex)
            {
                ////ProcessError(ex, strSQL);
            }
            return null;
        }

        public static DataTable FetchDataTable(string strSql, string strConnectionString)
        {
            try
            {
                SqlDataAdapter objDA = null;
                DataTable objDT = new DataTable();

                objDA = new SqlDataAdapter(strSql, strConnectionString);
                objDA.SelectCommand.CommandTimeout = MintCommandTimeout;
                objDA.Fill(objDT);

                return objDT;
            }
            catch (Exception)
            {
                //ProcessError(ex, strSQL);
            }
            return null;
        }

        public static DataView FetchDataView(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            try
            {
                DataTable objDT = null;
                objDT = FetchDataTable(gen);
                DataView objDV = new DataView(objDT);
                return objDV;
            }
            catch (Exception)
            {
                //ProcessError(ex, strSQL);
            }
            return null;
        }

        public static SqlDataReader FetchDataReader(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            SqlCommand objCommand = null;
            SqlConnection conDataReader = null;

            try
            {
                conDataReader = new SqlConnection(ConnectionStringLookup(connectionName));

                try
                {
                    //Fetch datareader is first called when starting app.  If tcpip is not enabled or it cannot connect to server or database, we need to show a messagebox with error.
                    conDataReader.Open();
                }
                catch (Exception)
                {
                    //MessageBox.Show(ex.Message + Environment.NewLine + Environment.NewLine + "Also make sure TCP/IP is enabled on sql server", "", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    //if (MessageBox.Show("Would you like to see the connection string?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                    //    string strLogin = Microsoft.VisualBasic.Interaction.InputBox("Enter Login Name used for connection", "", "", -1, -1);
                    //    string strPassword = Microsoft.VisualBasic.Interaction.InputBox("Enter Password used for connection", "", "", -1, -1);

                    //    if (strLogin == "developer" && strPassword == "aociris")
                    //    {
                    //        MessageBox.Show("Connection string = " + Environment.NewLine + Environment.NewLine + DBCONNECTSTRING);
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Invalid login name or password.  Application closing.");
                    //        System.Environment.Exit(1);
                    //    }
                    //}

                    //System.Environment.Exit(1);
                }

                objCommand = new SqlCommand(gen.SqlString, conDataReader);
                foreach (SqlParameter param in gen.SqlVariables)
                {
                    objCommand.Parameters.Add(param);
                }
                objCommand.CommandTimeout = MintCommandTimeout;

                return objCommand.ExecuteReader(CommandBehavior.CloseConnection);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //ProcessError(ex, strSQL);
            }
            return null;
        }

        public static SqlDataReader FetchDataReaderSproc(string sprocName, string strParam1)
        {
            SqlGenerator gen = new SqlGenerator(SqlGenerator.SqlTypes.Sproc, sprocName);

            //Determine if a param was sent.
            if (strParam1.Length > 0)
            {
                gen.AddField("PARAMETER", sprocName, strParam1);
            }

            return FetchDataReader(gen);

        }

        public static SqlDataReader FetchDataReaderSproc1(string sprocName, string param1, string param2)
        {
            SqlGenerator gen = new SqlGenerator(SqlGenerator.SqlTypes.Sproc, sprocName);

            //Determine which parameters where sent.
            if (param1.Length > 0)
            {
                gen.AddField("PARAMETER", sprocName, param1);
            }
            if (param2.Length > 0)
            {
                gen.AddField("PARAMETER2", sprocName, param2);
            }

            return FetchDataReader(gen);

        }

        public static SqlDataReader FetchDataReaderSproc2(string sprocName, string param1, string param2, string param3)
        {
            SqlGenerator gen = new SqlGenerator(SqlGenerator.SqlTypes.Sproc, sprocName);

            //Determine which parameters where sent.
            if (param1.Length > 0)
            {
                gen.AddField("PARAMETER", sprocName, param1);
            }
            if (param2.Length > 0)
            {
                gen.AddField("PARAMETER2", sprocName, param2);
            }
            if (param3.Length > 0)
            {
                gen.AddField("PARAMETER3", sprocName, param3);
            }

            return FetchDataReader(gen);

        }
        #endregion

    #region  Single Value Return Functions ***************************************************************** 

        public static string FetchValueString(SqlGenerator gen, string connectionName = "CountyDatabase")
        {
            string returnValue = null;
            SqlDataReader myReader = null;

            try
            {
                myReader = FetchDataReader(gen, connectionName);

                while (myReader.Read())
                {
                    returnValue = myReader[0].ToString();
                }

            }
            catch (Exception)
            {

            }
            finally
            {
                if (!((myReader == null)))
                {
                    if (!myReader.IsClosed)
                    {
                        myReader.Close();
                    }
                }
            }

            return returnValue ?? "";
        }

        public static string GetUniqueKey(string connectionName = "CountyDatabase")
	    {
		    string tempGetUniqueKey = null;
		    SqlConnection conConnection = null;
		    SqlCommand comCommand = null;
		    SqlDataReader myReader = null;

		    try
		    {
			    conConnection = new SqlConnection(ConnectionStringLookup(connectionName));
			    comCommand = new SqlCommand("GetnextKeyUSP", conConnection);
			    comCommand.CommandType = CommandType.StoredProcedure;
			    comCommand.CommandTimeout = MintCommandTimeout;

			    conConnection.Open();
			    myReader = comCommand.ExecuteReader();

			    while (myReader.Read())
			    {
				    tempGetUniqueKey = myReader.GetString(0).ToString();
			    }

		    }
		    catch (Exception)
		    {
		    }
		    finally
		    {
			    if (!((myReader == null)))
			    {
				    if (!myReader.IsClosed)
				    {
					    myReader.Close();
				    }
			    }

			    if (!((conConnection == null)))
			    {
				    if (conConnection.State == ConnectionState.Open)
				    {
					    conConnection.Close();
				    }
			    }
		    }

		    return tempGetUniqueKey;
	    }

        public static string GetNextSysKey(string strTable, string strKeyFieldName, string connectionName = "CountyDatabase")
	    {
		    string tempGetNextSysKey = null;
		    SqlDataReader objDR = null;
		    tempGetNextSysKey = "";

		    try
		    {
			    objDR = FetchDataReaderSproc1("GetNextSysKeyUSP", strTable, strKeyFieldName);

			    if (objDR.Read() == true)
			    {
				    tempGetNextSysKey = objDR.GetString(0);
			    }
		    }
		    catch (Exception)
		    {
                //Microsoft.VisualBasic.Information.Err().Raise(pErrorNum.SQLError,, ex.Message);
		    }
		    finally
		    {
			    if (!((objDR == null)))
			    {
				    if (!objDR.IsClosed)
				    {
					    objDR.Close();
				    }
			    }
		    }

		    return tempGetNextSysKey;
	    }

    #endregion

    #region  Execute Methods ******************************************************************************* 

        public static int ExecuteSql(SqlGenerator gen, bool blnReturnLastAutoKey = false, string connectionName = "CountyDatabase")
	    {
		    int tempExecuteSql = 0;
            string sql = gen.SqlString;
            if (gen.IsAutonumberKey) blnReturnLastAutoKey = true;

		    try
		    {
			    using (SqlConnection connection = new SqlConnection(ConnectionStringLookup(connectionName)))
			    {

                    SqlCommand objCommand = new SqlCommand(sql, connection);

                    foreach (SqlParameter param in gen.SqlVariables)
                    {
                        objCommand.Parameters.Add(param);
                    }

				    objCommand.CommandTimeout = MintCommandTimeout;

				    if (connection.State != ConnectionState.Open)
				    {
					    connection.Open();
				    }

                    if (sql.StartsWith("INSERT ") && blnReturnLastAutoKey)
				    {
					    tempExecuteSql = Convert.ToInt32(objCommand.ExecuteScalar());
				    }
				    else
				    {
					    tempExecuteSql = objCommand.ExecuteNonQuery();
				    }
				    //Execute the sql and return the number of rows affected.
				    connection.Close();
			    }
		    }
		    catch (Exception ex)
		    {
                Console.WriteLine(ex.Message);
            }
		    return tempExecuteSql;
	    }

    #endregion

 
    }
}