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
using System.DirectoryServices;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Reporting;
using CrystalDecisions.ReportSource;
using CrystalDecisions.Web;
using CrystalDecisions.Shared;


public partial class Default2 : System.Web.UI.Page
{
    System.Data.Odbc.OdbcDataReader reader;
    public static ReportDocument sprReportRpt = null;
    ReportDocument rpt = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
                if (Session.Count == 0)
                {
                    Response.Redirect("error_page.aspx?error=Session Expired");
                }
                if (Request.QueryString["spr_no"] != null)
                {
                    mDisplaySprForm(Request.QueryString["spr_no"].ToString());
                }
            
            

            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/admin_default_page.aspx";
            
        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mDisplaySprForm(string spr)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the spr form report
        /// Author: mutawakelm
        /// Date :2/24/2009 4:20:35 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblSprData = new DataTable();
            tblSprData.Columns.Add("department_name");
            tblSprData.Columns.Add("cost_code");
            tblSprData.Columns.Add("request_date");
            tblSprData.Columns.Add("justification");
            tblSprData.Columns.Add("requestor");
            tblSprData.Columns.Add("spr_sys_id");
            DataTable tblSprProducts = new DataTable();
            tblSprProducts.Columns.Add("item_no");
            tblSprProducts.Columns.Add("item_name");
            tblSprProducts.Columns.Add("qty");
            tblSprProducts.Columns.Add("price", Type.GetType("System.Double"));
            tblSprProducts.Columns.Add("total", Type.GetType("System.Double"));
            tblSprProducts.Columns.Add("cat_no");
            tblSprProducts.Columns.Add("unit_of_order");
            int counter = 1;
            double totalPrice = 0.0;//This variable will hold the total spr price
            sprReportRpt = CreateCrystalReportDocument("rpt_spr_form.rpt");
            TextObject txtSprTotal;
            txtSprTotal = (TextObject)sprReportRpt.ReportDefinition.ReportObjects["txtTotal"];
            //Tje following code will fill the spr data table
            string sprDataQuery = "SELECT t_spr_data.*,t_requestors_table.position,t_requestors_table.name FROM t_spr_data,t_requestors_table WHERE spr_no=" + Request.QueryString["spr_no"].ToString() + " AND t_spr_data.requester=t_requestors_table.user_id";
            reader = GeneralClass.Program.gRetrieveRecord(sprDataQuery);
            if (reader.HasRows)
            {
                reader.Read();
                tblSprData.Rows.Add(reader["department"].ToString(), reader["cost_code"].ToString(), reader["request_date"].ToString(), reader["justification"].ToString(), reader["name"].ToString() + ", " + reader["position"].ToString(),reader["spr_no"].ToString());
                reader.Close();
            }
            else reader.Close();
            //Tje following code will fill the spr product table
            string strProductQuery = "SELECT pro.*,sub.sub_sub_category_name FROM t_spr_products as pro join t_sut_sub_category as sub on pro.sub_sub_category_id=sub.id  where spr_no=" + Request.QueryString["spr_no"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblSprProducts.Rows.Add(counter.ToString(), reader["sub_sub_category_name"].ToString(), reader["cat_no"].ToString(), double.Parse(reader["unit_order"].ToString()), double.Parse(reader["total"].ToString()),reader["cat"].ToString(),reader["unit_of_order"].ToString());
                    totalPrice += double.Parse(reader["total"].ToString());
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
            //The following code will assign the data tables to the report data tables
            txtSprTotal.Text ="SR "+ totalPrice.ToString();
            sprReportRpt.SetDatabaseLogon("sa", "dbadmin");
            sprReportRpt.Database.Tables["spr_data_table"].SetDataSource((DataTable)tblSprData);
            sprReportRpt.Database.Tables["spr_products"].SetDataSource((DataTable)tblSprProducts);
            //overallTranscriptRpt.Refresh();
            this.sprRequestFormViwer.ReportSource = sprReportRpt;//to assign the transcript report file to the form report viewer

        }
        catch(Exception exp)
        {
        }
    }
    private ReportDocument CreateCrystalReportDocument(string ReportName)
    {

        try
        {
            //TextObject txtHandle;
            ReportDocument rpt = new ReportDocument();
            string reportPath = Server.MapPath(ReportName);
            rpt.Load(reportPath);
            ////txtHandle = (TextObject)rpt.ReportDefinition.ReportObjects["txtHandled"];
            ////txtHandle.Text = "test";

            ConnectionInfo connectionInfo = new ConnectionInfo();
            connectionInfo.DatabaseName = "inventory";
            connectionInfo.UserID = "sa";
            connectionInfo.Password = "dbadmin";
            Tables tables = rpt.Database.Tables;

            foreach (CrystalDecisions.CrystalReports.Engine.Table table in tables)
            {
                TableLogOnInfo tableLogonInfo = table.LogOnInfo;
                tableLogonInfo.ConnectionInfo = connectionInfo;
                table.ApplyLogOnInfo(tableLogonInfo);
            }
            return rpt;
        }
        catch (Exception exp)
        {
            rpt.Close();
            rpt.Dispose();
            rpt = null;
            mRestartApplication();
            return null;

        }
    }
    protected void mRestartApplication()
    {
        System.Web.HttpRuntime.UnloadAppDomain();
        this.sprRequestFormViwer.Dispose();


        try
        {
            System.Threading.Thread.Sleep(1000);
            Response.Redirect("http://registration.ksau-hs.edu.sa/inventory/product_request_page.aspx");
        }
        catch
        {
        }
        finally
        {

        }


    }
}
