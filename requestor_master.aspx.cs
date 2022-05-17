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
using System.Data.Odbc;

public partial class Default2 : System.Web.UI.Page
{
    System.Data.Odbc.OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
            {
                Response.Redirect("error_page.aspx?error=Session Expired");
            }
            mCheckAuthority();
            if (!IsPostBack)
            {
                mFillUsersGrid();
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
    protected void mCheckAuthority()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check the authority
        /// Author: mutawakelm
        /// Date :3/14/2009 3:21:46 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["user_management"])
            {
                if (Session["user_management"].ToString() == "True")
                {

                }
            }
            else
                Response.Redirect("~/error_page.aspx?error=You do not have the privileges to access this page");


        }
        catch (Exception exp)
        {

        }
    }
    protected void mFillUsersGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the users Grid
        /// Author: mutawakelm
        /// Date :2/28/2009 1:37:17 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblUsersPrivileges=new DataTable();
            tblUsersPrivileges.Columns.Add("user_id");
            tblUsersPrivileges.Columns.Add("name");
            tblUsersPrivileges.Columns.Add("position");
            string strUsersQuery = "SELECT * FROM t_requestors_table WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strUsersQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    tblUsersPrivileges.Rows.Add(reader["id"].ToString(), reader["name"].ToString(), reader["position"].ToString());
                    
                }
                reader.Close();
                RequestorsGrid.DataSource = tblUsersPrivileges;
                RequestorsGrid.DataBind();
            }
            else reader.Close();

        }
        catch (Exception mFillUsersGrid_Exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mEditRoles(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            TableCell itemCell = e.Item.Cells[1];
            lblSelectedId.Text = itemCell.Text;
            RequestorsGrid.EditItemIndex = e.Item.ItemIndex;
            mFillUsersGrid();
        }
        catch (Exception exp)
        {
        }
    }
    protected void userGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update the contents of the datagrid "categoryGrid" after editing
        /// Author: mutawakelm
        /// Date :18/08/2008 10:41:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            
            System.Web.UI.WebControls.TextBox txtName = new System.Web.UI.WebControls.TextBox();
            txtName = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox txtPosition = new System.Web.UI.WebControls.TextBox();
            txtPosition = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
            System.Web.UI.WebControls.TextBox txtID = new System.Web.UI.WebControls.TextBox();
            txtID = (System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0];

            //The following code will update the user info
            GeneralClass.Program.Add("name", txtName.Text, "S");
            GeneralClass.Program.Add("position", txtPosition.Text, "S");
            GeneralClass.Program.UpdateRecordStatement("t_requestors_table", "id", txtID.Text);
            RequestorsGrid.EditItemIndex = -1;
            mFillUsersGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void userGrid_cancelCoomand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            RequestorsGrid.EditItemIndex = -1;
            mFillUsersGrid();

        }
        catch (Exception exp)
        {
        }
    }
  
}
