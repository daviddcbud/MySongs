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
                ConvertFile2(cmd, @"c:\users\david\Downloads\songs2014.csv");
                ConvertFile2(cmd, @"c:\users\david\Downloads\songs2013.csv");

                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2012.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2011.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2010.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2009.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2008.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2007.csv");
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2006.csv", false);
                ConvertFile3(cmd, @"c:\users\david\Downloads\songs2007.csv", false);

            }
        }

        private static void ConvertFile3(SqlCommand cmd, string file, bool usedescription = true)
        {
            var reader = new TextFieldParser(file);
            reader.TextFieldType = FieldType.Delimited;
            reader.SetDelimiters(",");
            var fields = reader.ReadFields();
            int? playListid = null;
            var index = 0;
            string description = null;
            string prevDate = null;
            while (fields != null)
            {
                if (fields[0] == "" || (prevDate!=null && fields[2] != prevDate))
                {
                    playListid = null;
                    description = null;
                    index = 0;
                }
                else {
                    if (description == null)
                    {

                        description = fields[0];
                        if (!usedescription) description = "";
                        if (!playListid.HasValue)
                        {
                            var date = fields[2];
                            prevDate = date;
                            DateTime datetime;
                            if (DateTime.TryParse(date, out datetime))
                            {
                                cmd.CommandText = "INSERT PlayLists Select '" + datetime.ToShortDateString() +
                                    "','" + description + "'";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "SELECT @@IDENTITY";
                                playListid = int.Parse(cmd.ExecuteScalar().ToString());
                                Console.WriteLine(datetime.ToShortDateString());
                            }
                        }

                    }
                    else
                    {
                        if (!playListid.HasValue)
                        {
                            var date = fields[2];
                            var datetime = DateTime.Parse(date);
                            cmd.CommandText = "INSERT PlayLists Select '" + datetime.ToShortDateString() +
                                "','" + description + "'";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "SELECT @@IDENTITY";
                            playListid = int.Parse(cmd.ExecuteScalar().ToString());
                            Console.WriteLine(datetime.ToShortDateString());
                        }
                        var name = fields[0];
                        cmd.CommandText = "if not exists (Select * FROM SONGS Where name='" +
                            name.Replace("'", "''") + "') insert songs select '" + name.Replace("'", "''") + "','','" + "" +
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
        private static void ConvertFile2(SqlCommand cmd, string file)
        {
            var reader = new TextFieldParser(file);
            reader.TextFieldType = FieldType.Delimited;
            reader.SetDelimiters(",");
            var fields = reader.ReadFields();
            int? playListid = null;
            var index = 0;
            string description = null;
            while (fields != null)
            {
                if (fields[0] == "")
                {
                    playListid = null;
                    description = null;
                    index = 0;
                }
                else {
                    if (description==null)
                    {
                       
                        description = fields[0];
                        if (!playListid.HasValue)
                        {
                            var date = fields[2];
                            DateTime datetime;
                            if (DateTime.TryParse(date, out datetime))
                            {
                                cmd.CommandText = "INSERT PlayLists Select '" + datetime.ToShortDateString() +
                                    "','" + description + "'";
                                cmd.ExecuteNonQuery();
                                cmd.CommandText = "SELECT @@IDENTITY";
                                playListid = int.Parse(cmd.ExecuteScalar().ToString());
                                Console.WriteLine(datetime.ToShortDateString());
                            }
                        }

                    }
                    else
                    {
                        if(!playListid.HasValue)
                        {
                            var date = fields[2];
                            var datetime = DateTime.Parse(date);
                            cmd.CommandText = "INSERT PlayLists Select '" + datetime.ToShortDateString() +
                                "','" + description + "'";
                            cmd.ExecuteNonQuery();
                            cmd.CommandText = "SELECT @@IDENTITY";
                            playListid = int.Parse(cmd.ExecuteScalar().ToString());
                            Console.WriteLine(datetime.ToShortDateString());
                        }
                        var name = fields[0];
                        string keyValue = fields[1];
                        if (keyValue.Contains(":")) keyValue = "";
                        cmd.CommandText = "if not exists (Select * FROM SONGS Where name='" +
                            name.Replace("'", "''") + "') insert songs select '" + name.Replace("'", "''") + "','','" + keyValue +
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
