using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Mvc.Services
{
    /// <summary>
    /// This class demostrated how to query a SQL store procedure into a POCO with parameters
    /// </summary>
    public class SqlRepositorySvc
    {
        // private static BusinessObject.QueryClauses _queryClauses = new BusinessObject.QueryClauses();

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="paginator"></param>
        ///// <returns></returns>
        //public static List<BusinessObject.GetItemModel> uspGetItem(BusinessObject.PaginatorModel paginator)
        //{

        //    string WhereClause = _queryClauses.GenerateWhereClause(paginator);
        //    string OrderByClause = _queryClauses.GenerateOrderByClause(paginator);
        //    string OffsetClause = _queryClauses.GenerateOffsetClause(paginator);
        //    //make sure not null, want to send at least a blank string
        //    if (WhereClause == null) WhereClause = "";
        //    if (OrderByClause == null) OrderByClause = "";
        //    if (OffsetClause == null) OffsetClause = "";


        //    System.Data.DataTable dt = new System.Data.DataTable("Query1");

        //    string ConStr = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        //    using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConStr))
        //    {
        //        con.Open();

        //        string StoredProcedureName = "dbo.GetItem";

        //        System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(StoredProcedureName, con);
        //        cmd.CommandTimeout = 60;
        //        cmd.CommandType = System.Data.CommandType.StoredProcedure;
        //        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@WhereClause", WhereClause));
        //        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OrderByClause", OrderByClause));
        //        cmd.Parameters.Add(new System.Data.SqlClient.SqlParameter("@OffsetClause", OffsetClause));

        //        System.Data.SqlClient.SqlParameter ReturnValue = cmd.Parameters.Add("@Return_Value", System.Data.SqlDbType.Int);
        //        ReturnValue.Direction = System.Data.ParameterDirection.ReturnValue;

        //        using (System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd))
        //        {
        //            adp.Fill(dt);
        //        }

        //        con.Close();
        //    }

        //    /* == using custom extension to convert DataTable to List :)
        //     * IMPORTANT!!! Class Model MUST match the field names from Stored Procedure.
        //     * EX: [CreatedOn] will NOT map to [createdon]
        //     */
        //    return dt.ToList<BusinessObject.GetItemModel>();
        //}
    }
}