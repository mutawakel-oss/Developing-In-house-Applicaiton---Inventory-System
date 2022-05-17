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
    #region
    //in function mBtnAddClicked we should but the logged pc tech user name
    #endregion
    System.Data.Odbc.OdbcDataReader reader;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session.Count == 0)
            {
                Response.Redirect("error_page.aspx?error=Session Expired");
            }
            if (!IsPostBack)
            {
              
                mFillRoomDDL();
            }
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/pctech_default_page.aspx";
        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mFillGridBySprNo(string spr_n)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of the new spr product
        /// Author: mutawakelm
        /// Date :1/2/2009 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            if ((spr_n != "") && (spr_n != " "))
            {
                DataTable productTable = new DataTable();
                int assignedItemsCounter = 0;
                int unAssignedItemsCounter = 0;
                string strInspectedBy = "";//This variable will hold the name of the inspecter
                string strApprovedBy = "";//This variable will hold the name of the approval responsible
                string strAllocatedTo = "";//This variable will hold the name of the allocating employee
                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("vendor");//This column will hold the vendor
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("rec_date");//This column will hold the date of reciving the item
                productTable.Columns.Add("rec_by");//This column will hold the reciver of the item
                productTable.Columns.Add("insp_by");//This column will hold the person who inspect
                productTable.Columns.Add("appro_by");//This column will hold the pseron who approved
                productTable.Columns.Add("assigned_to");//This column will hold the pseron who have the item
                productTable.Columns.Add("hidden");
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.sub_sub_cat=" + spr_n + " and t_products.inspected_by is not null and t_products.approved_by is not null and t_products.vendor=t_vendor.id";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["inspected_by"] != null)
                            strInspectedBy = reader["inspected_by"].ToString();
                        if (reader["approved_by"] != null)
                            strApprovedBy = reader["approved_by"].ToString();
                        if (reader["allocated_to"] != null)
                            strAllocatedTo = reader["allocated_to"].ToString();
                        if (reader["allocated_to"].ToString() != "")
                            assignedItemsCounter++;
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following code will display number of items in the data grid
                
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";


            }
            

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
        finally
        {
            if (reader != null)
                reader.Close();
        }
    }
    private void pullusers(string domain, string username, string pwd)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:3/2/2009
        /// 	Author:Mutawakelm
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>

        try
        {
            GeneralClass.Program.gRetrieveRecord("delete from t_ldapUsers");

            ///DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

            DirectoryEntry entry1 = new DirectoryEntry("LDAP://DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();
            string strFullName;
            string strEMail;
            string strBadgeNo;
            string strTitle;
            string strDepartment;
            string strPager;
            string strTele;
            string strMobile;
            string strLoginName;


            DirectorySearcher dSearch = new DirectorySearcher(entry1);
            dSearch.Filter = "(&(objectCategory=user)(cn=*))";


            foreach (SearchResult sResultSet in dSearch.FindAll())
            {
                strFullName = GetProperty(sResultSet, "Name");
                strBadgeNo = GetProperty(sResultSet, "employeeid");
                strEMail = GetProperty(sResultSet, "mail");
                strTele = GetProperty(sResultSet, "telephonenumber");
                strDepartment = GetProperty(sResultSet, "department");
                strTitle = GetProperty(sResultSet, "title");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");

                if (GetProperty(sResultSet, "sAMAccountName").Trim() == "wtest")
                {
                    string str_qwe = "";
                }

                strPager = GetProperty(sResultSet, "pager");

                if ("" == strBadgeNo)
                    strBadgeNo = "0";

                if ("" == strTele)
                    strTele = "0";

                strMobile = GetProperty(sResultSet, "mobile");

                if ("" == strMobile)
                    strMobile = "0";

                
                    if ("" != strLoginName.Trim())
                        if (strFullName != string.Empty)
                        {
                            GeneralClass.Program.Add("full_name", strFullName.Trim(), "S");
                            GeneralClass.Program.Add("badge_number", strBadgeNo, "I");
                            GeneralClass.Program.Add("email_address", strEMail, "S");
                            GeneralClass.Program.Add("phone_ext", strTele, "I");
                            GeneralClass.Program.Add("mobile", strMobile, "S");
                            GeneralClass.Program.Add("department_name", strDepartment, "S");
                            GeneralClass.Program.Add("job_title", strTitle, "S");
                            GeneralClass.Program.Add("login_name", strLoginName, "S");

                            int intReturnID = GeneralClass.Program.InsertRecordStatement("t_LdapUsers");
                        }
            }


        }
        catch (Exception ex)
        {

        }
    }
    protected void DisplayDepartmentList()
    {
        /// <summary>
        /// 	Description: Populate data from t_users into gridview  
        ///	
        ///
        /// 	Date:3/2/2009
        /// 	Author:mutawakelm
        /// 	Parameter:
        ///		input: 
        ///		output: userlist in the gridview
        /// 	Example:  DisplayUserList();
        /// </summary>
        try
        {
            string strDepartmentsQuery = "SELECT DISTINCT(department_name)  FROM t_LdapUsers";
            reader = GeneralClass.Program.gRetrieveRecord(strDepartmentsQuery);
            if (reader.HasRows)
            {
                
                while (reader.Read())
                {
                
                    ddlDepSearch.Items.Add(reader["department_name"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
        }
        catch (Exception ex)
        {
            if (reader != null)
                reader.Close();
        }

    }
    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        /// <summary>
        /// 	Description: Returns property for the LDAP data fetching
        ///	
        ///
        /// 	Date:3/2/2009
        /// 	Author:mutawakelm  
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  GetProperty(sResultSet, "sAMAccountName");
        /// </summary>
        //HttpContext.Current.Response.Write(searchResult.Path);
        try
        {
            if (searchResult.Properties.Contains(PropertyName))
                return searchResult.Properties[PropertyName][0].ToString();
            else
                return string.Empty;
        }
        catch (Exception ex)
        {
            return string.Empty;
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
            //DateTime starDate = new DateTime();//This variable will hold the start date
            //DateTime endDate = new DateTime();//This variable will hold the end date

            ////The following code will be used to assign/update the t_products to a specified end-user
           
            ////The following code will be used to assign/update the t_products to a pc technician
            //    if (ddlSelectionType.SelectedItem.Value == "2")
            //    {
            //        lblPcTechError.Text = "";
            //        foreach (DataGridItem dg in productsGrid.Items)
            //        {
            //            CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
            //            if (chk != null)
            //            {
            //                if (chk.Checked == true)
            //                {
            //                    GeneralClass.Program.Add("maintained_by", ddlPcTech.SelectedItem.Value, "S");
            //                    int returnId = GeneralClass.Program.UpdateRecordStatement("t_products", "id", dg.Cells[10].Text);
            //                }
            //            }
            //        }
            //        //The following code will add a prefentive maintainance sheet for the PC Tech
            //        if ((txtStartingDate.Text != "") && (txtEndingDate.Text != ""))
            //        {
            //            starDate = DateTime.Parse(txtStartingDate.Text);
            //            endDate = DateTime.Parse(txtEndingDate.Text);
            //            if (starDate < endDate)
                            
            //            else
            //            {
            //                lblPcTechError.Text = "Starting date should be before ending date , please verify the dates.";
            //                AssignementExtender.Show();
            //            }
            //        }
            //        mFillAfaterModification();

            //    }
               

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
            TableCell itemCell = e.Item.Cells[10];
            string assignedToText = itemCell.Text;
            lblItemNumber.Text = assignedToText;
            mFillSpecificationGrid();
            mFillAssignHistoryGrid();
            mGetUpgaradeHistory();
            mGetPreventiveMaintainance();
            mGetItemServiceHistory();
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
            tblItemSpecification.Columns.Add("sys_id");
            string upgraded ="";//This variable will hold the upgradation status
          //The following segment will determine if the specification data retrieved from the master table
            string strUpgradeStatus = "SELECT upgraded FROM t_products WHERE id=" + lblItemNumber.Text;
            reader = GeneralClass.Program.gRetrieveRecord(strUpgradeStatus);
            if (reader.HasRows)
            {
                reader.Read();
                upgraded = reader["upgraded"].ToString();
                reader.Close();
            }
            else reader.Close();
            //The following segment will put the specification according to the upgrade status
            if (upgraded == "False")
            {
                string strSpecificationQuery = "SELECT t_delivery_item_details.property,t_delivery_item_details.description FROM t_delivery_item_details join t_products on t_delivery_item_details.date_time=t_products.date_time where t_products.id=" + lblItemNumber.Text;
                reader = GeneralClass.Program.gRetrieveRecord(strSpecificationQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tblItemSpecification.Rows.Add(reader["property"].ToString(), reader["description"].ToString(), " ");
                    }
                    reader.Close();
                }
                else reader.Close();
                specificationDataGrid.DataSource = tblItemSpecification;
                specificationDataGrid.DataBind();
                lblSpecifiedFromMaster.Text = "notUpgraded";
            }
            else
            {
                //The following code will retrieve the specifcation from the t_item_upgrades
                string strSpecificationQuery = "SELECT id,property,description  FROM t_item_upgrade WHERE item_id=" + lblItemNumber.Text+" AND last_version='True'";
                reader = GeneralClass.Program.gRetrieveRecord(strSpecificationQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        tblItemSpecification.Rows.Add(reader["property"].ToString(), reader["description"].ToString(), reader["id"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                specificationDataGrid.DataSource = tblItemSpecification;
                specificationDataGrid.DataBind();
                lblSpecifiedFromMaster.Text = "upgraded";
            }

            
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
            mGetUserFullName(assignmentHistoryGrid, 0);
            mGetUserFullName(assignmentHistoryGrid, 2);


        }
        catch (Exception exp)
        {
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
    protected void mFillRoomDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the room Drop Down List
        /// Author: mutawakelm
        /// Date :2/8/2009 10:59:57 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int counter = 0;
            string strPcTechQuery = "SELECT * FROM t_storage_place ";
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechQuery);
            if (reader.HasRows)
            {
                ddlRoomSearch.Items.Clear();
                while (reader.Read())
                {
                    ddlRoomSearch.Items.Add(reader["storage_place"].ToString());
                    ddlRoomSearch.Items[counter].Value = reader["id"].ToString();
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
    protected void mDdlSearchSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the selected search panel
        /// Author: mutawakelm
        /// Date :2/8/2009 11:23:34 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            productsGrid.DataSource = null;
            productsGrid.DataBind();
            
                if (ddlSearchCriteria.SelectedItem.Value == "2")
                {
                 
                    pnlUserSearch.Visible = true;
                    pnlRoomSearch.Visible = false;
                    lblSprItemsCount.Visible = false;
                    lblAssignedItemsNo.Visible = false;
                    lblUnassignedItemsNo.Visible = false;
                    lblSprItemsTitle.Visible = false;
                    lblAssignedItemsNoText.Visible = false;
                    lblUnassignedItemsNoText.Visible = false;
                    pullusers("", "wstaff", "test123");
                    DisplayDepartmentList();
                }
                    else
                        if (ddlSearchCriteria.SelectedItem.Value == "4")
                        {
                  
                            pnlUserSearch.Visible = false;
                            pnlRoomSearch.Visible = true;
                            lblSprItemsCount.Visible = false;
                            lblAssignedItemsNo.Visible = false;
                            lblUnassignedItemsNo.Visible = false;
                            lblSprItemsTitle.Visible = false;
                            lblAssignedItemsNoText.Visible = false;
                            lblUnassignedItemsNoText.Visible = false;

                        }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mDepartmentSearchDDLSelected(object sender, EventArgs e)
    {
       
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to pull the users of the selected department 
        /// Author: mutawakelm
        /// Date :2/8/2009 11:33:56 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int counter = 1;
            string strUsersQuery = "SELECT full_name,department_name,login_name  FROM t_LdapUsers where department_name='" + ddlDepSearch.SelectedItem.Text + "'";
            reader = GeneralClass.Program.gRetrieveRecord(strUsersQuery);
            if (reader.HasRows)
            {
                ddlUserSearch.Items.Clear();
                ddlUserSearch.Items.Add("Select A User");
                while (reader.Read())
                {
                    ddlUserSearch.Items.Add(reader["full_name"].ToString());
                    ddlUserSearch.Items[counter].Value = reader["login_name"].ToString();
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
    protected void mUserDDLSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the selected user assigned products
        /// Author: mutawakelm
        /// Date :2/8/2009 11:39:50 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillGridByUser(ddlUserSearch.SelectedItem.Value);
        }
        catch (Exception exp)
        {
        }
    }
    protected void mRoomDDLSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the selected room products
        /// Author: mutawakelm
        /// Date :2/8/2009 12:11:29 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            mFillGridByRoom(ddlRoomSearch.SelectedItem.Value);
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
    protected void mFillGridByRoom(string roomNo)
    {
        try
        {
            if (roomNo != "")
            {
                DataTable productTable = new DataTable();
                int assignedItemsCounter = 0;
                int unAssignedItemsCounter = 0;
                string strInspectedBy = "";//This variable will hold the name of the inspecter
                string strApprovedBy = "";//This variable will hold the name of the approval responsible
                string strAllocatedTo = "";//This variable will hold the name of the allocating employee
                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("vendor");//This column will hold the vendor
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("rec_date");//This column will hold the date of reciving the item
                productTable.Columns.Add("rec_by");//This column will hold the reciver of the item
                productTable.Columns.Add("insp_by");//This column will hold the person who inspect
                productTable.Columns.Add("appro_by");//This column will hold the pseron who approved
                productTable.Columns.Add("assigned_to");//This column will hold the pseron who have the item
                productTable.Columns.Add("hidden");
                productTable.Columns.Add("serial");
                string strProductQuery = "SELECT t_products.room,t_products.serial_no,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.allocated_room='" + roomNo + "' and t_products.vendor=t_vendor.id";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        if (reader["inspected_by"] != null)
                            strInspectedBy = reader["inspected_by"].ToString();
                        if (reader["approved_by"] != null)
                            strApprovedBy = reader["approved_by"].ToString();
                        if (reader["allocated_to"] != null)
                            strAllocatedTo = reader["allocated_to"].ToString();
                        if (reader["allocated_to"].ToString() != "")
                            assignedItemsCounter++;
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(),reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();



            }
            
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillGridByUser(string userID)
    {
        try
        {
            if (userID != "")
            {
                DataTable productTable = new DataTable();
                int assignedItemsCounter = 0;
                int unAssignedItemsCounter = 0;
                string strInspectedBy = "";//This variable will hold the name of the inspecter
                string strApprovedBy = "";//This variable will hold the name of the approval responsible
                string strAllocatedTo = "";//This variable will hold the name of the allocating employee
                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("vendor");//This column will hold the vendor
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("rec_date");//This column will hold the date of reciving the item
                productTable.Columns.Add("rec_by");//This column will hold the reciver of the item
                productTable.Columns.Add("insp_by");//This column will hold the person who inspect
                productTable.Columns.Add("appro_by");//This column will hold the pseron who approved
                productTable.Columns.Add("assigned_to");//This column will hold the pseron who have the item
                productTable.Columns.Add("hidden");
                productTable.Columns.Add("serial");
                string strProductQuery = "SELECT t_products.serial_no,t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.allocated_to='" + userID + "' and t_products.vendor=t_vendor.id";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["inspected_by"] != null)
                            strInspectedBy = reader["inspected_by"].ToString();
                        if (reader["approved_by"] != null)
                            strApprovedBy = reader["approved_by"].ToString();
                        if (reader["allocated_to"] != null)
                            strAllocatedTo = reader["allocated_to"].ToString();
                        if (reader["allocated_to"].ToString() != "")
                            assignedItemsCounter++;
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(),reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();



            }
            

        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillAfaterModification()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid regarding the chosed 
        /// Author: mutawakelm
        /// Date :2/8/2009 4:28:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
          
                    if (pnlUserSearch.Visible.ToString() == "True")
                {
                    mFillGridByUser(ddlUserSearch.SelectedItem.Value);
                }
                    else
                        if (pnlRoomSearch.Visible.ToString() == "True")
                {
                            
                    mFillGridByRoom(ddlRoomSearch.SelectedItem.Value);
                }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mExportToExl(object sender, EventArgs e)
    {
        try
        {
            Response.Clear();
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("content-disposition", "attachment;filename=Report.xlsx");
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            System.IO.StringWriter stringWrite = new System.IO.StringWriter();
            System.Web.UI.HtmlTextWriter  htmlWrite  = new System.Web.UI.HtmlTextWriter(stringWrite);
            productsGrid.RenderControl(htmlWrite);
            Response.Write(stringWrite.ToString());
            Response.End();


        }
        catch (Exception exp)
        {
        }
    }
    protected void mGetUpgaradeHistory()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to list the upgradation history
        /// Author: mutawakelm
        /// Date :2/14/2009 1:38:19 PM
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
            tblItemSpecification.Columns.Add("upgrade_date");
            string strSpecificationQuery = "SELECT property,description,upgradation_date  FROM t_item_upgrade WHERE item_id=" + lblItemNumber.Text + " AND last_version='False'";
            reader = GeneralClass.Program.gRetrieveRecord(strSpecificationQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblItemSpecification.Rows.Add(reader["property"].ToString(), reader["description"].ToString(), reader["upgradation_date"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            upgradationHistoryGrid.DataSource = tblItemSpecification;
            upgradationHistoryGrid.DataBind();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGetPreventiveMaintainance()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to retrieve the preventive maintainance for a selected item
        /// Author: mutawakelm
        /// Date :2/14/2009 3:59:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblPreventiveMaintaince = new DataTable();
            tblPreventiveMaintaince.Columns.Add("action");
            tblPreventiveMaintaince.Columns.Add("status");
            tblPreventiveMaintaince.Columns.Add("start_date");
            tblPreventiveMaintaince.Columns.Add("end_date");
            tblPreventiveMaintaince.Columns.Add("maintained_by");
            


            string strPrevQuery = "SELECT *   FROM t_preventive_maintainance WHERE item_id=" + lblItemNumber.Text+" order by ending_date desc";
            reader = GeneralClass.Program.gRetrieveRecord(strPrevQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    tblPreventiveMaintaince.Rows.Add("Windows Update", reader["windows_update"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Trend AV Update", reader["trend_update"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Verify Virus Definition", reader["virus_def"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Enabling Windows Firewall", reader["windows_firewall"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Uninstalling Sygate PF", reader["pf"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Clear Internet Cache", reader["internet_cache"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Delete Temp Files", reader["temp_files"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Run Scan Disk", reader["scan_disk"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Review Event Log", reader["event_log"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    tblPreventiveMaintaince.Rows.Add("Available HD/GB", reader["hd_gb"].ToString(), reader["starting_date"].ToString(), reader["ending_date"].ToString(), reader["maintained_by"].ToString());
                    

                }

                reader.Close();
                preventiveGrid.DataSource = tblPreventiveMaintaince;
                preventiveGrid.DataBind();
                mGetUserFullName(preventiveGrid, 4);
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mAddServiceHisotoryClicked(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to insert a history report for the selected item in the
        /// data grid "product_grid"
        /// Author: mutawakelm
        /// Date :2/15/2009 12:15:12 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            txtServiceReport.Text = "";
            addServiceHistoryExtender.Show();
            lblItemNumber.Text = e.Item.Cells[10].Text;
            
        }
        catch (Exception exp)
        {
        }
    }
    protected void mBtnAddClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a service report for the selected item
        /// Author: mutawakelm
        /// Date :2/15/2009 12:45:13 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("item_id", lblItemNumber.Text, "I");
            GeneralClass.Program.Add("service", txtServiceReport.Text, "S");
            GeneralClass.Program.Add("date", DateTime.Now.ToString(), "D");
            GeneralClass.Program.Add("maintained_by", Session["UserID"].ToString(), "S");
            int returnId = GeneralClass.Program.InsertRecordStatement("t_item_service_history");
           
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGetItemServiceHistory()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to retrieve the item Service history.
        /// Author: mutawakelm
        /// Date :2/14/2009 3:59:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblServieHistory = new DataTable();
            tblServieHistory.Columns.Add("date");
            tblServieHistory.Columns.Add("service_report");
            tblServieHistory.Columns.Add("done_by");
            string strPrevQuery = "SELECT *   FROM t_item_service_history WHERE item_id=" + lblItemNumber.Text + " order by date desc";
            reader = GeneralClass.Program.gRetrieveRecord(strPrevQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    tblServieHistory.Rows.Add(reader["date"].ToString(), reader["service"].ToString(), reader["maintained_by"].ToString());
                }

                reader.Close();
                serviceHistoryGrid.DataSource = tblServieHistory;
                serviceHistoryGrid.DataBind();
                mGetUserFullName(serviceHistoryGrid, 2);
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
}
