using System.Data;
using Microsoft.Data.SqlClient;

    public static class Database
    {

        public static string sqlDataSource = "Data Source=6LR48R3; Initial Catalog=Scheduling ;  Integrated Security = True; TrustServerCertificate = True";

         public static DataTable GetDataNoparam(string str)
         {
                DataTable resutdt = new DataTable();
                try
                {
                    SqlDataReader reader;
                    using (SqlConnection conn = new SqlConnection(sqlDataSource))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(str, conn))
                        {
                            reader = cmd.ExecuteReader();
                            resutdt.Load(reader);

                            reader.Close();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                return resutdt;
         }


         public static DataTable GetData(string str , SqlParameter param)
         {
                DataTable resutdt = new DataTable();
                try
                {
                    SqlDataReader reader;
                    using (SqlConnection conn = new SqlConnection(sqlDataSource))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(str, conn))
                        {
                            cmd.Parameters.Add(param);
                            reader = cmd.ExecuteReader();
                            resutdt.Load(reader);

                            reader.Close();
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                return resutdt;
         }

         public static int StoreData(string str, params IDataParameter[] sqlParams)
         {
                int rows = -1;
                try
                {         
                    using (SqlConnection conn = new SqlConnection(sqlDataSource))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(str, conn))
                        {
                            if (sqlParams != null)
                            {
                                foreach (IDataParameter para in sqlParams)
                                {
                                    cmd.Parameters.Add(para);
                                }
                                rows = cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                return rows;
         }
     }

