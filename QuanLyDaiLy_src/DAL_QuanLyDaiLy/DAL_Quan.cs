﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLyDaiLy;

namespace DAL_QuanLyDaiLy
{
    public class DAL_Quan
    {
        private static SqlConnection conn = DBUtils.GetDBConnection();
        public static ArrayList DSQuan()
        {
            ArrayList arrList = new ArrayList();
            DataTable dt = new DataTable();
            dt = ResultQuery.GetTableResult(conn, "SELECT * FROM Quan ");
            int id;
            string ten;
            foreach (DataRow r in dt.Rows)
            {
                id = (int)r["IdQuan"];
                ten = r["TenQuan"].ToString();
                DTO_Quan q = new DTO_Quan(id, ten);
                arrList.Add(q);
            }

            return arrList;
        }
        /*
         * ThemQuan trả về 
         * 1:thành công
         * 0:thất bại
         */
        public static int ThemQuan(string tenQuan)
        {
            int kq = 0;
            SqlCommand cmd = new SqlCommand("PR_InsertQuan", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@TenQuan", SqlDbType.NVarChar).Value = tenQuan;
            try
            {
                conn.Open();
                kq = cmd.ExecuteNonQuery();
                return kq;
            }
            finally
            {
                conn.Close();
            }
        }
        /*
         * XoaLoaiDaiLy trả về 
         * 1:thành công
         * 0:thất bại
         */
        public static int XoaLoaiDaiLy(int id)
        {
            string query = "delete Quan where IdQuan=" + id;
            int result = ResultQuery.GetResultQuery(conn, query);
            return result;
        }
        /*
         * CapNhatLoaiDaiLy trả về 
         * 1:thành công
         * 0:thất bại
         */
        public static int CapNhatLoaiDaiLy(DTO_Quan quan)
        {
            string query = "update Quan set TenQuan =N'" + quan.TenQuan + "' where IdQuan=" + quan.IdQuan;
            int result = ResultQuery.GetResultQuery(conn, query);
            return result;
        }
    }
}