using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conversion
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connection"].ConnectionString))
            {
                conn.Open();
                var cmd = new SqlCommand("", conn);
                cmd.CommandText = "Delete from playlists";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete from songs";
                cmd.ExecuteNonQuery();
                ConvertFile(cmd, @"c:\users\david\Downloads\songs2016.csv");
                ConvertFile(cmd, @"c:\users\david\Downloads\songs2015.csv");
                ConvertFile(cmd, @"c:\users\david\Downloads\songs2014.csv");
                ConvertFile(cmd, @"c:\users\david\Downloads\songs2013.csv");

            }
        }

        private static void ConvertFile(SqlCommand cmd,string file)
        {
            var reader = new TextFieldParser(file);
            reader.TextFieldType = FieldType.Delimited;
            reader.SetDelimiters(",");
            var fields = reader.ReadFields();
            int? playListid = null;
            var index = 0;
            var description = "";
            while (fields != null)
            {
                if (fields[0] == "")
                {
                    playListid = null;
                    index = 0;
                }
                else {
                    if (!playListid.HasValue)
                    {
                        var date = fields[2];
                        description = fields[0];
                        var datetime = DateTime.Parse(date);
                        cmd.CommandText = "INSERT PlayLists Select '" + datetime.ToShortDateString() +
                            "','" + description + "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "SELECT @@IDENTITY";
                        playListid = int.Parse(cmd.ExecuteScalar().ToString());
                        Console.WriteLine(datetime.ToShortDateString());
                    }
                    else
                    {
                        var name = fields[0];
                        cmd.CommandText = "if not exists (Select * FROM SONGS Where name='" +
                            name.Replace("'", "''") + "') insert songs select '" + name.Replace("'", "''") + "','','" + fields[1] +
                            "'";
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Select id FROM songs where name='" + name.Replace("'", "''") + "'";
                        var songid = cmd.ExecuteScalar().ToString();
                        cmd.CommandText = "INSERT SONGPlaylists select " + playListid.Value +
                            "," + songid + ",''," + index;
                        cmd.ExecuteNonQuery();
                        index++;
                    }
                }

                fields = reader.ReadFields();
            }
        }
    }
}
