using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDNS
{
    class SQLiteHelper
    {
        public static DataTable lodaData(string dbPath, string tableName, List<string> param)
        {
            DataTable dbTable = new DataTable();
            SQLiteConnection sqlCon = OpenDb(dbPath);
            string loadSql = "select * from " + tableName;
            SQLiteCommand scmd = new SQLiteCommand(loadSql, sqlCon);
            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();
            for (int i = 0; i < param.Count; i++)
            {
                List<string> list = new List<string>();
                dic.Add(param[i].ToString(), list);
            }
            SQLiteDataAdapter da = new SQLiteDataAdapter(scmd);
            try
            {
                da.Fill(dbTable);
            }
            catch (Exception e)
            {
                if (e.ToString().Contains("tabel"))
                {
                }
            }
            sqlCon.Close();
            //每次拿出一行
            foreach (DataRow drs in dbTable.Rows)
            {
                int i = 0;
                //第m行，第n个key为第n列名，第n个value为第n列的值
                foreach (KeyValuePair<string, List<string>> kvp in dic)
                {
                    kvp.Value.Add(drs[param[i]].ToString());
                    i++;
                }
            }
            DataTable tempTable = tempDataTable(param, tableName);
            KeyValuePair<string, List<string>> pair = dic.First();
            int rowNum = pair.Value.Count();

            for (int i = 0; i < rowNum; i++)
            {
                DataRow dr = tempTable.NewRow();
                for (int s = 0; s < param.Count; s++)
                {
                    dr[param[s]] = dic[param[s]][i];
                }
                tempTable.Rows.Add(dr);
            }
            return tempTable;
        }
        /// <summary>
        /// 中间表
        /// </summary>
        /// <param name="param">列名集合</param>
        /// <param name="tempTableName">表名</param>
        /// <returns>DataTable</returns>
        private static DataTable tempDataTable(List<string> param, string tempTableName)
        {
            DataTable dt = new DataTable(tempTableName);
            foreach (string columName in param)
            {
                dt.Columns.Add(columName, typeof(String));
            }
            return dt;
        }
        public static SQLiteConnection OpenDb(string dbPath)
        {
            string dbName = dbPath;
            try
            {
                string sqliteconString = "Data Source = " + dbPath;
                SQLiteConnection SQLiteconnect = new SQLiteConnection(sqliteconString);
                string SQLiteConnString = sqliteconString;
                SQLiteconnect.Open();
                return SQLiteconnect;
            }
            catch (Exception e)
            {
                throw new Exception("打开数据库:" + dbName + "失败" + e.ToString());
            }
        }

        public static void deleteData(string tableName,string dbPath)
        {
            SQLiteConnection sqlCon = OpenDb(dbPath);
            string delSql = "delete from ddns_info";
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = sqlCon;
            cmd.Transaction = sqlCon.BeginTransaction();
            cmd.CommandText = delSql;
            cmd.ExecuteNonQuery();
            cmd.Transaction.Commit();
        }

        public static bool addData(string tableName, string dbPath, List<string> columnValues)
        {
            SQLiteConnection sqlCon = OpenDb(dbPath);
            SQLiteCommand cmd = new SQLiteCommand();
            cmd.Connection = sqlCon;
            cmd.Transaction = sqlCon.BeginTransaction();
            string insertSql = string.Format("insert into {0} values('{1}',", tableName, uuid());
            foreach (string para in columnValues)
            {
                insertSql += "'" + para + "',";
            }
            insertSql = insertSql.Substring(0, insertSql.Length - 1) + ")";
            try
            {
                cmd.CommandText = insertSql;
                cmd.ExecuteNonQuery();
                cmd.Transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "错误提示");
                cmd.Transaction.Rollback();
                return false;
            }
        }

        public static string uuid()
        {
            string uuid = Guid.NewGuid().ToString();
            return uuid;
        }
    }
}
