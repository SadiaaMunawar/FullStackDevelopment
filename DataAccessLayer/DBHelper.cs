using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;
using ProductManagementApp.Models;



namespace DataAccessLayer
{
    public class DBHelper
    {
        public static NpgsqlConnection GetConnection()
        {
            
            return new NpgsqlConnection("Host=aws-0-ap-northeast-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.zdytoqcvvruxllxdndsm;Password=Supa@7560@Sadia;SSL Mode=Require;Trust Server Certificate=true");
            return new NpgsqlConnection("Host=aws-0-ap-northeast-1.pooler.supabase.com;Port=6543;Database=postgres;Username=postgres.zdytoqcvvruxllxdndsm;Password=Supa@7560@Sadia;SSL Mode=Require;Trust Server Certificate=true");
        }
    }
}
