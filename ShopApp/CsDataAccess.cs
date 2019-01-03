using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp
{
    public class MsSqlDataAccess
    {
        public static string ConnStr;


        private static SqlConnection SqlConn = null;
        private static SqlCommand SqlComm = null;
        private static SqlDataAdapter SqlDA = null;


        public static SqlParameterCollection Parameters
        {
            get
            {
                return SqlComm.Parameters;
            }
        }

        public MsSqlDataAccess(string conn)
        {
            ConnStr = conn;
            SqlConn = new SqlConnection(conn);
        }


        public static int NonQuery(string CommandText)
        {
            var res = 0;

            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                res = SqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }

        public static int NonQuery(string CommandText, CommandType CommandType, SqlParameter[] Parameters)
        {
            var res = 0;

            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                SqlComm.CommandType = CommandType;
                if (Parameters != null)
                {
                    for (var i = 0; i <= Parameters.Length - 1; i++)
                    {
                        SqlComm.Parameters.Add(Parameters[i]);
                    }
                }
                res = SqlComm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }



        public static SqlDataReader GetSqlDataReader(string sql)
        {
            SqlDataReader res = null;
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(sql, SqlConn);
                SqlComm.CommandType = CommandType.Text;

                res = SqlComm.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }


        public static SqlDataReader GetSqlDataReader(string CommandText, CommandType CommandType, SqlParameter[] Parameters)
        {
            SqlDataReader res = null;
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                SqlComm.CommandType = CommandType;
                if (Parameters != null)
                {
                    for (var i = 0; i <= Parameters.Length - 1; i++)
                    {
                        SqlComm.Parameters.Add(Parameters[i]);
                    }
                }
                res = SqlComm.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }


        public static DataSet GetDataset(string sql)
        {
            var res = new DataSet();
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(sql, SqlConn);
                SqlComm.CommandType = CommandType.Text;

                SqlDA = new SqlDataAdapter(SqlComm);
                SqlDA.Fill(res);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }

        public static DataSet GetDataset(string CommandText, CommandType CommandType, SqlParameter[] Parameters)
        {
            var res = new DataSet();
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                SqlComm.CommandType = CommandType;
                if (Parameters != null)
                {
                    for (var i = 0; i <= Parameters.Length - 1; i++)
                    {
                        SqlComm.Parameters.Add(Parameters[i]);
                    }
                }
                SqlDA = new SqlDataAdapter(SqlComm);
                SqlDA.Fill(res);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlDA.Dispose();
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res;
        }


        public static DataTable GetDataTable(string sql)
        {
            var res = new DataSet();
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(sql, SqlConn);
                SqlComm.CommandType = CommandType.Text;

                SqlDA = new SqlDataAdapter(SqlComm);
                SqlDA.Fill(res);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlDA.Dispose();
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res.Tables[0];
        }


        public static DataTable GetDataTable(string CommandText, CommandType CommandType, SqlParameter[] Parameters)
        {
            var res = new DataSet();
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                SqlComm.CommandType = CommandType;
                if (Parameters != null)
                {
                    for (var i = 0; i <= Parameters.Length - 1; i++)
                    {
                        SqlComm.Parameters.Add(Parameters[i]);
                    }
                }
                SqlDA = new SqlDataAdapter(SqlComm);
                SqlDA.Fill(res);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlDA.Dispose();
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res.Tables[0];
        }


        public static DataTable GetColoumnName(string tblName)
        {
            var res = new DataSet();
            var sql = "select COLUMN_NAME from INFORMATION_SCHEMA.COLUMNS where TABLE_NAME = '" + tblName + "'";
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(sql, SqlConn);
                SqlComm.CommandType = CommandType.Text;

                SqlDA = new SqlDataAdapter(SqlComm);
                SqlDA.Fill(res);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlDA.Dispose();
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }
            return res.Tables[0];
        }


        public static DataTable GetPivotTable(DataTable source)
        {
            var Rotated = new DataTable("Pivot" + source.TableName);
            Rotated.Columns.Add(" ");

            foreach (DataRow r in source.Rows)
            {
                Rotated.Columns.Add(r[0].ToString());
            }
            for (var i = 0; i < source.Columns.Count - 1; i++)
            {
                Rotated.Rows.Add(Rotated.NewRow());
            }

            for (var r = 0; r < Rotated.Rows.Count; r++)
            {
                for (var c = 0; c < Rotated.Columns.Count; c++)
                {
                    if (c == 0)
                    {
                        Rotated.Rows[r][0] = source.Columns[r + 1].ColumnName;
                    }
                    else
                    {
                        Rotated.Rows[r][c] = source.Rows[c - 1][r + 1];
                    }
                }
            }

            Rotated.AcceptChanges();
            return Rotated;
        }


        public static object ExecuteScalar(string sqlstr)
        {
            object result;
            try
            {
                SqlConn.Open();
                var cmd = SqlConn.CreateCommand();
                cmd.CommandText = sqlstr;

                if (SqlConn.State == ConnectionState.Closed)
                {
                    SqlConn.Open();
                }
                result = cmd.ExecuteScalar();
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlConn.Close();
                SqlConn.Dispose();
            }


            return result;
        }


        public static object ExecuteScalar(string CommandText, CommandType CommandType, SqlParameter[] Parameters)
        {
            object result;
            try
            {
                SqlConn.Open();
                SqlComm = new SqlCommand(CommandText, SqlConn);
                SqlComm.CommandType = CommandType;
                if (Parameters != null)
                {
                    for (var i = 0; i <= Parameters.Length - 1; i++)
                    {
                        SqlComm.Parameters.Add(Parameters[i]);
                    }
                }
                result = SqlComm.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw (ex);
            }
            finally
            {
                SqlComm.Dispose();
                SqlConn.Close();
                SqlConn.Dispose();
            }

            return result;
        }
    }
}
