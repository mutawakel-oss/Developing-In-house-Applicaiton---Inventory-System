using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.Odbc;

public partial class dataImportExport : System.Web.UI.Page
{
    OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void mAppend(object sender, EventArgs e)
    {
        //This function will be used to append the sub-sub-category table
        try
        {
            DataTable tblSubSubCategories = new DataTable();
            string strSubQuery = "SELECT DISTINCT(EQUIPMENT) FROM SPR_bms";
            tblSubSubCategories.Columns.Add("subSubCategoryName");
            reader = GeneralClass.Program.gRetrieveRecord(strSubQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblSubSubCategories.Rows.Add(reader["EQUIPMENT"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            //The following query will be used to append the subSubCategories to the table "SubSubCategoryId"
            for (int i = 0; i < tblSubSubCategories.Rows.Count; i++)
            {
                GeneralClass.Program.Add("sub_sub_category_name", tblSubSubCategories.Rows[i][0].ToString(), "S");
                GeneralClass.Program.InsertRecordStatement("t_sut_sub_category");
            }

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mAddSprs(object sender, EventArgs e)
    {
        //This query will add the sprs 
        try
        {
            DataTable tblOldSprs = new DataTable();
            tblOldSprs.Columns.Add("ordered");
            tblOldSprs.Columns.Add("SprNo");
            tblOldSprs.Columns.Add("equipment");
            tblOldSprs.Columns.Add("comments");
            tblOldSprs.Columns.Add("quantity");
            tblOldSprs.Columns.Add("date");
            tblOldSprs.Columns.Add("status");
            tblOldSprs.Columns.Add("recieved");
            tblOldSprs.Columns.Add("signatory");
            tblOldSprs.Columns.Add("date_recieved");
            string sprsQuery = "SELECT * FROM SPR_bms";
             reader = GeneralClass.Program.gRetrieveRecord(sprsQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblOldSprs.Rows.Add(reader["ordered"].ToString(), reader["spr"].ToString(), reader["equipment"].ToString(), reader["additional_info"].ToString(), reader["quantity"].ToString(), reader["date"].ToString(), reader["status"].ToString(), reader["recieved"].ToString(), reader["signatory"].ToString(), reader["recieving_date"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            //The following code will be used to add the SPRs to the system
            int sys_id = 12;
            int j=0;//This index will take the index
            int sub_sub_cat_id = 0;
            for (int i = 0; i < tblOldSprs.Rows.Count; i++)
            {
                if (tblOldSprs.Rows[i][1].ToString() != "")
                {
                    GeneralClass.Program.Add("spr_no", sys_id.ToString(), "S");
                    GeneralClass.Program.Add("requester", tblOldSprs.Rows[i][8].ToString(), "S");
                    GeneralClass.Program.Add("actual_spr_no", tblOldSprs.Rows[i][1].ToString(), "S");
                    GeneralClass.Program.Add("justification", "Old Spr", "S");
                    GeneralClass.Program.Add("request_date", tblOldSprs.Rows[i][0].ToString(), "D");
                    int returnedValue=GeneralClass.Program.InsertRecordStatement("t_spr_data");
                    if (returnedValue != 0)
                    {
                        //Here the first product of the spr should be entered
                        reader = GeneralClass.Program.gRetrieveRecord("select id from t_sut_sub_category where sub_sub_category_name='" + tblOldSprs.Rows[i][2].ToString() + "'");
                        if (reader.HasRows)
                        {
                            reader.Read();
                            sub_sub_cat_id = int.Parse(reader["id"].ToString());
                            reader.Close();
                        }
                        else reader.Close();
                        //The following code will add the product to the table "spr products"
                        GeneralClass.Program.Add("spr_no", sys_id.ToString(), "I");
                        GeneralClass.Program.Add("sub_sub_category_id", sub_sub_cat_id.ToString(), "I");
                        GeneralClass.Program.Add("cat_no", tblOldSprs.Rows[i][4].ToString(), "I");
                        GeneralClass.Program.Add("date_time", DateTime.Now.ToString(), "D");
                        GeneralClass.Program.InsertRecordStatement("t_spr_products");
                        //End of first product entry

                        j=i+1;
                        if (tblOldSprs.Rows[j][1].ToString() == "")
                        {
                            while (tblOldSprs.Rows[j][1].ToString() == "")
                            {
                                //Query the sub_sub_category of the item
                                reader = GeneralClass.Program.gRetrieveRecord("select id from t_sut_sub_category where sub_sub_category_name='" + tblOldSprs.Rows[j][2].ToString() + "'");
                                if (reader.HasRows)
                                {
                                    reader.Read();
                                    sub_sub_cat_id = int.Parse(reader["id"].ToString());
                                    reader.Close();
                                }
                                else reader.Close();
                                //The following code will add the product to the table "spr products"
                                GeneralClass.Program.Add("spr_no", sys_id.ToString(), "I");
                                GeneralClass.Program.Add("sub_sub_category_id", sub_sub_cat_id.ToString(), "I");
                                GeneralClass.Program.Add("cat_no", tblOldSprs.Rows[j][4].ToString(), "I");
                                GeneralClass.Program.Add("date_time", DateTime.Now.ToString(), "D");
                                GeneralClass.Program.InsertRecordStatement("t_spr_products");

                                j++;
                            }
                            i = j - 1;
                        }
                    }
                    sys_id++;
                }
            }

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
}
