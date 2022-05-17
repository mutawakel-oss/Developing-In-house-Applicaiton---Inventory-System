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
using System.Net.Mail;

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
                mFillPcTechDDL();                    
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
            if (null != Session["maintainance_manager"])
            {
                if (Session["maintainance_manager"].ToString() == "True")
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
    protected void mDisplaySpecification(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the specfication panel
        /// Author: mutawakelm
        /// Date :2/3/2009 1:22:07 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TableCell itemCell = e.Item.Cells[9];
            string assignedToText = itemCell.Text;
            lblItemNumber.Text = assignedToText;
            mFillSpecificationGrid();
            mFillAssignHistoryGrid();
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillSpecificationGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the specification data grid
        /// Author: mutawakelm
        /// Date :2/2/2009 9:09:57 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblItemSpecification = new DataTable();
            tblItemSpecification.Columns.Add("property");
            tblItemSpecification.Columns.Add("description");
          
                string strSpecificationQuery = "SELECT t_delivery_item_details.property,t_delivery_item_details.description FROM t_delivery_item_details join t_products on t_delivery_item_details.date_time=t_products.date_time where t_products.id=" +lblItemNumber.Text;
                reader = GeneralClass.Program.gRetrieveRecord(strSpecificationQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tblItemSpecification.Rows.Add(reader["property"].ToString(), reader["description"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                specificationDataGrid.DataSource = tblItemSpecification;
                specificationDataGrid.DataBind();

            
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillAssignHistoryGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the assignement history data grid
        /// Author: mutawakelm
        /// Date :2/2/2009 9:09:57 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblItemAssHistory = new DataTable();//This table will hold the assignment history of the item
            tblItemAssHistory.Columns.Add("assigned_to");
            tblItemAssHistory.Columns.Add("assignment_date");
            tblItemAssHistory.Columns.Add("assigned_by");
            string strSpecificationQuery = "SELECT assigned_to,assignment_date,assigned_by FROM t_assignment_history WHERE item_no=" + lblItemNumber.Text;
            reader = GeneralClass.Program.gRetrieveRecord(strSpecificationQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblItemAssHistory.Rows.Add(reader["assigned_to"].ToString(), reader["assignment_date"].ToString(), reader["assigned_by"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            assignmentHistoryGrid.DataSource = tblItemAssHistory;
            assignmentHistoryGrid.DataBind();


        }
        catch (Exception exp)
        {
        }
    }
    protected void mAssignProduct(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to assign a product
        /// Author: mutawakelm
        /// Date :2/8/2009 9:25:09 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            AssignementExtender.Show();
            txtStartingDate.Text = lblStartHiddenDate.Text;
            txtEndingDate.Text = lblEndHiddenDate.Text;
            lblPcTechError.Text = "";
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillPcTechDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the PC Techinicans drop down list
        /// Author: mutawakelm
        /// Date :2/8/2009 10:46:51 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int counter = 1;
            string strPcTechQuery = "SELECT * FROM t_users WHERE (user_group='2' or user_group='2,6' or user_group='2,6,8')";
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechQuery);
            if (reader.HasRows)
            {               
                ddlPcTechName.Items.Clear();
                ddlPcTechName.Items.Add("--Select A PC Tech--");
                ddlPcTechName.Items[0].Value = "0";
                while (reader.Read())
                {
                    ddlPcTechName.Items.Add(reader["full_name"].ToString());
                    ddlPcTechName.Items[counter].Value = reader["id"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mClearBtnClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to clear the check box in the data grid
        /// Author: mutawakelm
        /// Date :2/8/2009 11:05:22 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            foreach (DataGridItem dg in productsGrid.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                chk.Checked = false;   
            }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSelectAll(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to select all the items in the data grid
        /// Author: mutawakelm
        /// Date :2/8/2009 1:09:24 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            foreach (DataGridItem dg in productsGrid.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                chk.Checked = true;
            }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mCheckGridStatus()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check if the data grid is empty
        /// Author: mutawakelm
        /// Date :2/8/2009 2:39:46 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            if (productsGrid.Items.Count > 0)
                tblAssigning.Visible = true;
            else
                tblAssigning.Visible = false;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mPcTechDDLSelectd(object sender, EventArgs e)
    {
        try
        {
            mGetPcDates();
            productsGrid.DataSource = null;
            productsGrid.DataBind();
            tblAssigning.Visible = false;
            lblShowStartDateText.Text = "";
            lblShowEndDateText.Text = "";
        }
        catch (Exception exp)
        {
        }

       
    }
    protected void mGetPcDates()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get the assignment dates for the selected PC Tech
        /// Author: mutawakelm
        /// Date :2/9/2009 12:00:44 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strDatesQuery = "SELECT DISTINCT(ending_date) FROM t_preventive_maintainance WHERE maintained_by='" + ddlPcTechName.SelectedItem.Value + "' order by ending_date";//This query will hold the dates query
            reader = GeneralClass.Program.gRetrieveRecord(strDatesQuery);
            if (reader.HasRows)
            {
                ddlStartingEndingDates.Items.Clear();
                ddlStartingEndingDates.Items.Add("--Select An Ending Date--");
                while (reader.Read())
                {
                    ddlStartingEndingDates.Items.Add(reader["ending_date"].ToString());
                }
                reader.Close();
            }
            else
            {
                reader.Close();
                ddlStartingEndingDates.Items.Clear();
            }
        }
        catch (Exception exp)
        {

        }
    }
    protected void mDatesDDLSelected(object sender , EventArgs e)
    {
        try
        {
            mRetrieveItems();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mRetrieveItems()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to retrieve items for the seleced PC Tech
        /// Author: mutawakelm
        /// Date :2/9/2009 12:11:35 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            //The following data table will hold the items retrieved for the PC Tech
            if (ddlStartingEndingDates.Text != "--Select An Ending Date--")
            {
                DataTable tblMaintainance = new DataTable();
                tblMaintainance.Columns.Add("item");
                tblMaintainance.Columns.Add("assigned_to");
                tblMaintainance.Columns.Add("w_update");
                tblMaintainance.Columns.Add("trend_update");
                tblMaintainance.Columns.Add("virus_def");
                tblMaintainance.Columns.Add("windows_firewall");
                tblMaintainance.Columns.Add("sygate_pf");
                tblMaintainance.Columns.Add("internet_cache");
                tblMaintainance.Columns.Add("temp_files");
                tblMaintainance.Columns.Add("scan_disk");
                tblMaintainance.Columns.Add("event_log");
                tblMaintainance.Columns.Add("hd_gb");
                tblMaintainance.Columns.Add("hidden");
                string allocated_to = "";
                //The following code will fill the room datatable
                DataTable tblRooms = new DataTable();
                tblRooms.Columns.Add("id");
                tblRooms.Columns.Add("roomName");
                string strRoomsQury = "SELECT * FROM t_storage_place";
                reader = GeneralClass.Program.gRetrieveRecord(strRoomsQury);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tblRooms.Rows.Add(reader["id"].ToString(), reader["storage_place"].ToString());
                    }
                    reader.Close();
                }
                else
                    reader.Close();
                //End of filling the rooms data table
                string strMaintainedItemsQuery = "SELECT t_preventive_maintainance.*,t_products.serial_no,t_sut_sub_category.sub_sub_category_name,t_products.allocated_to,t_products.allocated_room" +
                " FROM t_preventive_maintainance left join" +
                " t_products on t_preventive_maintainance.item_id=t_products.id left join t_sut_sub_category on" +
                " t_products.sub_sub_cat=t_sut_sub_category.id WHERE ending_date='" + ddlStartingEndingDates.Text + "' and t_preventive_maintainance.maintained_by='" + ddlPcTechName.SelectedItem.Value + "' ";
                reader = GeneralClass.Program.gRetrieveRecord(strMaintainedItemsQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        allocated_to = "";
                        if (reader["allocated_to"].ToString() != "")
                            allocated_to = reader["allocated_to"].ToString();
                        else
                            if (reader["allocated_room"].ToString() != "")
                            {
                                for (int i = 0; i < tblRooms.Rows.Count; i++)
                                {
                                    if (tblRooms.Rows[i][0].ToString() == reader["allocated_room"].ToString())
                                        allocated_to = tblRooms.Rows[i][1].ToString();
                                }

                            }
                        tblMaintainance.Rows.Add(reader["serial_no"].ToString(), allocated_to, reader["windows_update"].ToString(), reader["trend_update"].ToString(), reader["virus_def"].ToString(), reader["windows_firewall"].ToString(), reader["pf"].ToString(), reader["internet_cache"].ToString(), reader["temp_files"].ToString(), reader["scan_disk"].ToString(), reader["event_log"].ToString(), reader["hd_gb"].ToString(), reader["id"].ToString());
                        lblStartHiddenDate.Text = reader["starting_date"].ToString();
                        lblEndHiddenDate.Text = reader["ending_date"].ToString();
                        lblShowStartDateText.Text = reader["starting_date"].ToString();
                        lblShowEndDateText.Text = reader["ending_date"].ToString();
                    }
                    reader.Close();
                    productsGrid.DataSource = tblMaintainance;
                    productsGrid.DataBind();
                    mGetUserFullName(productsGrid, 1);
                }
                else
                {
                    reader.Close();
                    
                }
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                lblShowStartDateText.Text = "";
                lblShowEndDateText.Text = "";
            }
            mCheckGridStatus();

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mGetUserFullName(DataGrid strGridName, int ColumnNo)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get the user full name from the table "t_LdapUsers"
        /// Author: mutawakelm
        /// Date :2/17/2009 3:23:28 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            foreach (DataGridItem dg in strGridName.Items)
            {
                string strUserQuery = "SELECT full_name FROM t_LdapUsers WHERE login_name='" + dg.Cells[ColumnNo].Text + "'";
                reader = GeneralClass.Program.gRetrieveRecord(strUserQuery);
                if (reader.HasRows)
                {
                    reader.Read();

                    dg.Cells[ColumnNo].Text = reader["full_name"].ToString();
                    reader.Close();

                }
                else
                {
                    reader.Close();

                }
            }
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();

        }
    }
    protected void mBtnAssignClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to assign an item to a user
        /// Author: mutawakelm
        /// Date :2/3/2009 11:36:46 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DateTime starDate = new DateTime();//This variable will hold the start date
            DateTime endDate = new DateTime();//This variable will hold the end date

        
           
          
                //The following code will be used to assign/update the t_products to a pc technician
               
                   lblPcTechError.Text = "";
                   foreach (DataGridItem dg in productsGrid.Items)
                   {
                       CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                       if (chk != null)
                       {
                           if (chk.Checked == true)
                           {
                               starDate = DateTime.Parse(txtStartingDate.Text);
                               endDate = DateTime.Parse(txtEndingDate.Text);
                               if (starDate < endDate)
                               {
                                   GeneralClass.Program.Add("starting_date", txtStartingDate.Text, "D");
                                   GeneralClass.Program.Add("ending_date", txtEndingDate.Text, "D");
                                   int returnId = GeneralClass.Program.UpdateRecordStatement("t_preventive_maintainance", "id", dg.Cells[13].Text);
                               }
                               else
                               {
                                   lblPcTechError.Text = "Starting date should be before ending date , please verify the dates.";
                                   AssignementExtender.Show();
                               }

                           }
                       }
                   }
                   mGetPcDates();
                   mRetrieveItems();
                   
            
               


        }
        catch (Exception exp)
        {
        }
    }
    protected void mBtnDeleteClick(object sender, EventArgs e)
    {
        try
        {
            DeleteExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mBtnAcceptDeleteClick(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to delete the selected items
        /// Author: mutawakelm
        /// Date :2/9/2009 2:52:47 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strDeleteQuery = "";
            foreach (DataGridItem dg in productsGrid.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                if (chk != null)
                {
                    if (chk.Checked == true)
                    {
                        strDeleteQuery="Delete  From t_preventive_maintainance Where  id=" + dg.Cells[13].Text;
                        GeneralClass.Program.gDeleteRecord(strDeleteQuery); 
                    }
                }
            }
            mGetPcDates();
            mRetrieveItems();
        }
        catch (Exception exp)
        {
        }
    }
}

