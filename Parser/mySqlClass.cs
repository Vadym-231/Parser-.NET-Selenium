using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class mySqlClass
    {
        int id;
        MySqlConnection conn = new MySqlConnection("server=localhost;port=3306;username=root;password=root;database=parsedate;");
       public mySqlClass()
        {
            MySqlCommand commandMaxId = new MySqlCommand("select max(id) from parsedate.datafromitworld;");
            commandMaxId.Connection = this.conn;
            this.openConn();
            using (DbDataReader reader = commandMaxId.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    //

                    while (reader.Read())
                    {
                       
                        this.id = Convert.ToInt32(reader.GetValue(0));
                      
                       
                        
                       

                    }
                    
                }
            }
        }
        public void openConn()
        {
            if(conn.State == System.Data.ConnectionState.Closed)
            {
                conn.Open();
            }
        }
        public void queryData(string title,string img, string content,string date)
        {
            this.id+=1;
            MySqlDataAdapter adapter = new MySqlDataAdapter();
            MySqlCommand commandMaxId = new MySqlCommand("select max(id) from parsedate.datafromitworld;");
            commandMaxId.Connection = this.conn;
          
            MySqlCommand query = new MySqlCommand("insert into parsedate.datafromitworld (id,Title,ImgSrc,Content,date) values(@id,@title,@img,@content,@date);");
                        query.Connection = this.conn;
            Console.WriteLine("ID:" + id);    
            query.Parameters.Add("@id", MySqlDbType.Int32).Value = this.id;
                        query.Parameters.Add("@title", MySqlDbType.LongText).Value = title;
                        query.Parameters.Add("@img", MySqlDbType.VarChar).Value = img;
                        query.Parameters.Add("@date", MySqlDbType.VarChar).Value = date;
                        query.Parameters.Add("@content", MySqlDbType.LongText).Value = content;
                        query.ExecuteScalar();
        }
        public MySqlConnection getConn()
        {
            return conn;
        }
        public void closeConn()
        {
            if (conn.State == System.Data.ConnectionState.Open)
            {
                conn.Close();
            }
        }
    }
}
