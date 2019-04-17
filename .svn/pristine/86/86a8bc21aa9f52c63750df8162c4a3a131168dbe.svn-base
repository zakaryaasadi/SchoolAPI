using SchoolAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace SchoolAPI.ModelView
{
    public class BufferDb
    {
        public static void InsertFrame(AttachmentClass attach, int frame)
        {
            string pathDb = HostingEnvironment.MapPath("~/App_Data/BufferDb.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDb + ";Version=3;"))
            {
                connection.Open();
                string sql = string.Format("INSERT INTO 'BUFFER' VALUES({0},{1},'{2}');", attach.id, frame, attach.attach);
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public static byte[] GetBuffer(int attachId)
        {
            string pathDb = HostingEnvironment.MapPath("~/App_Data/BufferDb.db");
            using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDb + ";Version=3;"))
            {
                connection.Open();

                string sql = string.Format("SELECT COUNT(*) FROM BUFFER WHERE ATTACH_ID = {0} ORDER BY FRAME;", attachId);
                SQLiteCommand command = new SQLiteCommand(sql, connection);
                int count = Convert.ToInt32(command.ExecuteScalar());

                command.CommandText = string.Format("SELECT * FROM BUFFER WHERE ATTACH_ID = {0} ORDER BY FRAME;", attachId);
                SQLiteDataReader reader = command.ExecuteReader();

                byte[] buffer = new byte[count];
                while (reader.Read())
                {
                    int frame = int.Parse(reader["FRAME"].ToString());
                    byte data = byte.Parse(reader["DATA"].ToString());
                    buffer[frame] = data;
                }
                reader.Close();
                connection.Close();
                return buffer;
            }
        }

        //public static List<int> GetFramesFailed(int attachId)
        //{
        //    string pathDb = HostingEnvironment.MapPath("~/App_Data/BufferDb.db");
        //    using (SQLiteConnection connection = new SQLiteConnection("Data Source=" + pathDb + ";Version=3;"))
        //    {
        //        connection.Open();
        //        SQLiteCommand command = new SQLiteCommand(connection);
        //        command.CommandText = string.Format("SELECT * FROM BUFFER WHERE DATA IS NULL AND ATTACH_ID = {0} ORDER BY FRAME;", attachId);
        //        command.CommandType = System.Data.CommandType.Text;
        //        SQLiteDataReader reader = command.ExecuteReader();
        //        List<int> buffer = new List<int>();
        //        while (reader.Read())
        //        {
        //            int frame = int.Parse(reader["FRAME"].ToString());
        //            buffer.Add(frame);
        //        }
        //        reader.Close();
        //        connection.Close();

        //        return buffer;
        //    }
        //}

    }
}