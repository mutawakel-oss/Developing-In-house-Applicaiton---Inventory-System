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
            tblUsersPrivileges.Columns.Add("spr_creator");
            tblUsersPrivileges.Columns.Add("pc_tech");
            tblUsersPrivileges.Columns.Add("item_reciever");
            tblUsersPrivileges.Columns.Add("inspector");
            tblUsersPrivileges.Columns.Add("approval_supervisor");
            tblUsersPrivileges.Columns.Add("assignment_supervisor");
            tblUsersPrivileges.Columns.Add("maintainance_supervisor");
            tblUsersPrivileges.Columns.Add("reporting_supervisor");
            tblUsersPrivileges.Columns.Add("users_supervisor");
            bool pc_tech_status = false;
            bool spr_creator_status = false;
            bool item_reciever_status = false;
            bool item_inspector_status = false;
            bool approval_status = false;
            bool assignment_status = false;
            bool maintainance_status = false;
            bool reporting_status = false;
            bool user_management_status = false;
            string strUsersQuery = "SELECT no,full_name,user_group FROM t_users WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strUsersQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    pc_tech_status = false;
                    spr_creator_status = false;
                    item_reciever_status = false;
                    item_inspector_status = false;
                    approval_status = false;
                    assignment_status = false;
                    maintainance_status = false;
                    reporting_status = false;
                    user_management_status = false;
                    string[] grps = reader["user_group"].ToString().Split(',');
                    for (int i = 0; i <= grps.Length - 1; i++)
                    {
                       
                        if (grps[i] == "1")
                        {
                            spr_creator_status = true;
                        }

                        if (grps[i] == "2")
                        {
                            pc_tech_status = true;
                        }
                        if (grps[i] == "3")
                        {
                            item_reciever_status = true;
                        }
                        if (grps[i] == "4")
                        {
                            item_inspector_status = true;
                        }
                        if (grps[i] == "5")
                        {
                            approval_status = true;
                        }
                        if (grps[i] == "6")
                        {
                            assignment_status = true;
                        }
                        if (grps[i] == "7")
                        {
                            maintainance_status = true;
                        }
                        if (grps[i] == "8")
                        {
                            reporting_status = true;
                        }
                        if (grps[i] == "9")
                        {
                            user_management_status = true;
                        }
                    }
                    tblUsersPrivileges.Rows.Add( reader["no"].ToString(),reader["full_name"].ToString(), spr_creator_status, pc_tech_status, item_reciever_status, item_inspector_status, approval_status, assignment_status, maintainance_status, reporting_status, user_management_status);
                    
                }
                reader.Close();
                UsersGrid.DataSource = tblUsersPrivileges;
                UsersGrid.DataBind();
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
            UsersGrid.EditItemIndex = e.Item.ItemIndex;
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
            string str_user_privilges="";//This variable will hold the user privilges
            string checkd=e.Item.Cells[3].Text;
            System.Web.UI.WebControls.CheckBox spr_creator = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox pc_tech = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox reciever = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox inspector = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox approval = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox assignment = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox maintainance = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox reporter = new System.Web.UI.WebControls.CheckBox();
            System.Web.UI.WebControls.CheckBox user_supervisor = new System.Web.UI.WebControls.CheckBox();
             spr_creator = (CheckBox)e.Item.FindControl("spr_creator");
             pc_tech = (CheckBox)e.Item.FindControl("pc_tech");
             reciever = (CheckBox)e.Item.FindControl("item_reciever");
             inspector = (CheckBox)e.Item.FindControl("inspector");
             approval = (CheckBox)e.Item.FindControl("approval_supervisor");
             assignment = (CheckBox)e.Item.FindControl("assignment_supervisor");
             maintainance = (CheckBox)e.Item.FindControl("maintainance_supervisor");
             reporter = (CheckBox)e.Item.FindControl("reporting_supervisor");
             user_supervisor = (CheckBox)e.Item.FindControl("users_supervisor");
            //The following code will take the privileges
             if (spr_creator.Checked == true)
             {
                 if(str_user_privilges!="")
                 str_user_privilges = str_user_privilges + ",1";
                 else
                 str_user_privilges = str_user_privilges + "1";
             }
             if (pc_tech.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",2";
                 else
                 str_user_privilges = str_user_privilges + "2";
             }
             if (reciever.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",3";
                 else
                 str_user_privilges = str_user_privilges + "3";
             }
             if (inspector.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",4";
                 else
                 str_user_privilges = str_user_privilges + "4";
             }
             if (approval.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",5";
                 else
                 str_user_privilges = str_user_privilges + "5";
             }
             if (assignment.Checked == true)
             {
                 if (str_user_privilges != "")
                     str_user_privilges = str_user_privilges + ",6";
                 else
                     str_user_privilges = str_user_privilges + "6";
             }
             if (maintainance.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",7";
                 else
                 str_user_privilges = str_user_privilges + "7";
             }
             if (reporter.Checked == true)
             {
                 if (str_user_privilges != "")
                     str_user_privilges = str_user_privilges + ",8";
                 else
                     str_user_privilges = str_user_privilges + "8";
             }
             if (user_supervisor.Checked == true)
             {
                 if (str_user_privilges != "")
                 str_user_privilges = str_user_privilges + ",9";
                 else
                 str_user_privilges = str_user_privilges + "9";
             }

       
       
            //The following code will update the user info
            GeneralClass.Program.Add("user_group",str_user_privilges, "S");
            GeneralClass.Program.UpdateRecordStatement("t_users","no",lblSelectedId.Text);
            UsersGrid.EditItemIndex = -1;
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
            UsersGrid.EditItemIndex = -1;
            mFillUsersGrid();

        }
        catch (Exception exp)
        {
        }
    }
  
}
