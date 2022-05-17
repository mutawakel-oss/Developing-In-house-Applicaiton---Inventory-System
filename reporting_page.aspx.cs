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
                mFillCategoryDDL();
                mFillSubSubCategoryDDL();
                mFillRoomDDL();
                mFillPcTechDDL();
                productsGrid.Columns[5].Visible = false;
                productsGrid.Columns[6].Visible = false;
                productsGrid.Columns[7].Visible = false;
                productsGrid.Columns[8].Visible = false;
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
            if (null != Session["reporter"])
            {
                if (Session["reporter"].ToString() == "True")
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
    protected void mFillCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the category drop down list
        /// Author: mutawakelm
        /// Date :3/2/2009 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            int counter = 1;
            string strCategoryQuery = "SELECT id,category_name FROM t_category WHERE dept_id=" + Session["departmentId"].ToString() + " ORDER BY category_name";
            reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
            if (reader.HasRows)
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add("--Select--");
                ddlCategory.Items[0].Value = "0";
                while (reader.Read())
                {
                    ddlCategory.Items.Add(reader["category_name"].ToString());
                    ddlCategory.Items[counter].Value = reader["id"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();

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
    protected void mFillSubCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the category drop down list
        /// Author: mutawakelm
        /// Date :3/2/2009 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            string strSubCat = "";
            int counter = 1;
            string strsubCategoryQuery = "SELECT id,sub_cateogry_name FROM t_sub_catgory where category_id=" + ddlCategory.Items[ddlCategory.SelectedIndex].Value + " ORDER BY sub_cateogry_name";
            reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
            ddlSubCategory.Items.Clear();
            if (reader.HasRows)
            {
                ddlSubCategory.Items.Clear();
                ddlSubCategory.Items.Add("--Select--");
                ddlSubCategory.Items[0].Value = "0";
                while (reader.Read())
                {
                    if (reader["sub_cateogry_name"].ToString().Length > 80)
                        strSubCat = reader["sub_cateogry_name"].ToString().Substring(0, 80).ToString() + "...";
                    else
                        strSubCat = reader["sub_cateogry_name"].ToString();
                    ddlSubCategory.Items.Add(strSubCat);
                    ddlSubCategory.Items[counter].Value = reader["id"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();

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
    protected void mFillSubSubCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub sub category drop down list
        /// Author: mutawakelm
        /// Date :3/2/2009 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            int counter = 1;
            ddlSubSubCategory.Items.Clear();
            if (!string.IsNullOrEmpty(ddlSubCategory.Text))
            {
                string strsubCategoryQuery = "SELECT id,sub_sub_category_name FROM t_sut_sub_category WHERE sub_category_id=" + ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value + " ORDER BY sub_sub_category_name";
                reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
                if (reader.HasRows)
                {
                    ddlSubSubCategory.Items.Add("--Select--");
                    while (reader.Read())
                    {
                        ddlSubSubCategory.Items.Add(reader["sub_sub_category_name"].ToString());
                        ddlSubSubCategory.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else reader.Close();
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
    protected void mSubCategoryDDLselected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the filling drop down list function
        /// Author: mutawakelm
        /// Date :3/2/2009 03:44:13 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillSubCategoryDDL();
            mFillSubSubCategoryDDL();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubSubCategoryDDLselected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub sub category drop down list
        /// Author: mutawakelm
        /// Date :3/2/2009 03:44:16 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillSubSubCategoryDDL();

            mFillSubCategoryGrid(ddlSubCategory.SelectedItem.Value);//This call will used to fill the 
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillItemsGrid(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of items according to the selected sub-sub-category
        /// Author: mutawakelm
        /// Date :2/3/2009 9:29:01 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (ddlSubSubCategory.SelectedItem.Value != "--Select--")
            {
                mFillGridBySprNo(ddlSubSubCategory.Text,ddlSubSubCategory.SelectedItem.Text);
                lblitemCounterValidity.Text = "";
                lblItemsGrid.Text = "Statistics of Sub Sub Category (" + ddlSubSubCategory.SelectedItem.Text + ")";
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                tblStatistics.Visible = true;

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
    protected void mFillGridBySprNo(string spr_n,string strSubCatName)
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
                productTable.Columns.Add("serial");
                productTable.Columns.Add("actSerial");
                DataTable tblRooms = new DataTable();
                tblRooms.Columns.Add("id");
                tblRooms.Columns.Add("roomName");
                //The following code will fill the room datatable
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
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.serial_no,t_products.act_serial_no,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.allocated_room,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.sub_sub_cat=" + spr_n + " and t_products.inspected_by is not null and t_products.approved_by is not null and t_products.vendor=t_vendor.id AND t_products.status_id=8";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
				strAllocatedTo = "";
                        if (reader["inspected_by"].ToString() != "")
                            strInspectedBy =reader["inspected_by"].ToString();
                        if (reader["approved_by"].ToString() != "")
                            strApprovedBy = reader["approved_by"].ToString();
                        if (reader["allocated_to"].ToString() != "")
                            strAllocatedTo = reader["allocated_to"].ToString();
                        else
                            if (reader["allocated_room"].ToString() != "")
                            {
                                for (int i = 0; i < tblRooms.Rows.Count; i++)
                                {
                                    if (tblRooms.Rows[i][0].ToString() == reader["allocated_room"].ToString())
                                        strAllocatedTo = tblRooms.Rows[i][1].ToString();
                                }

                            }
                        if ((reader["allocated_to"].ToString() != "") || (reader["allocated_room"].ToString() != ""))
                            assignedItemsCounter++;
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(), reader["serial_no"].ToString(), reader["act_serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following code will display number of items in the data grid
                lblSprItemsTitle.Text = "Total Number of " + strSubCatName + ":";
                lblSprItemsCount.Text = productsGrid.Items.Count.ToString();
                lblAssignedItemsNoText.Text = "Total Assigned " + strSubCatName + ":";
                lblAssignedItemsNo.Text = assignedItemsCounter.ToString();
                lblUnassignedItemsNoText.Text = "Total Unassigned " + strSubCatName + ":";
                unAssignedItemsCounter=int.Parse(lblSprItemsCount.Text) - int.Parse(lblAssignedItemsNo.Text);
                lblUnassignedItemsNo.Text = unAssignedItemsCounter.ToString();
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";


            }
            mCheckGridStatus();

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
    protected void mGetUserFullName(DataGrid strGridName,int ColumnNo)
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
                string strUserQuery = "SELECT full_name FROM t_LdapUsers WHERE login_name='"+dg.Cells[ColumnNo].Text+"'";
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
            string strLdap = "";
            GeneralClass.Program.gRetrieveRecord("delete from t_ldapUsers");

            ///DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis
            for (int i = 0; i < 2; i++)
            {
                if (i == 0)
                    strLdap = "10.8.128.100";
                else
                    if (i == 1)
                        strLdap = "10.8.128.101";
                DirectoryEntry entry1 = new DirectoryEntry("LDAP://"+strLdap, username, pwd);//OU=staff,OU=collegeusers,OU=mis

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
            string strDepartmentsQuery = "SELECT DISTINCT(department_name)  FROM t_LdapUsers ORDER BY department_name";
            reader = GeneralClass.Program.gRetrieveRecord(strDepartmentsQuery);
            if (reader.HasRows)
            {
                ddlDepSearch.Items.Clear();
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
            string strPcTechQuery = "SELECT * FROM t_users WHERE (user_group='2' or user_group='2,6' or user_group='2,6,8') and dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechQuery);
            if (reader.HasRows)
            {
                ddlPcTechSearch.Items.Clear();
                ddlPcTechSearch.Items.Add("--Select A PC Tech--");
                ddlPcTechSearch.Items[0].Value = "0";
                while (reader.Read())
                {
                    ddlPcTechSearch.Items.Add(reader["full_name"].ToString());
                    ddlPcTechSearch.Items[counter].Value = reader["id"].ToString();
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
            int counter = 1;
            string strPcTechQuery = "SELECT * FROM t_storage_place WHERE dept_id="+Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechQuery);
            if (reader.HasRows)
            {
                ddlRoomSearch.Items.Clear();
                ddlRoomSearch.Items.Add("--Select A Room--");
                ddlRoomSearch.Items[0].Value = "0";
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
            mCheckGridStatus();
            tblUserItemsNavigation.Visible = false;
            lblItemsGrid.Text = "";
            if (ddlSearchCriteria.SelectedItem.Value == "1")
            {
                pnlCategorySearch.Visible = true;
                pnlPcTechSearch.Visible = false;
                pnlUserSearch.Visible = false;
                pnlRoomSearch.Visible = false;
                lblSprItemsTitle.Visible=true;
                lblAssignedItemsNoText.Visible=true;
                lblUnassignedItemsNoText.Visible = true;
                lblSprItemsCount.Visible = true;
                lblAssignedItemsNo.Visible = true;
                lblUnassignedItemsNo.Visible = true;

                productsGrid.DataSource=null;
                productsGrid.DataBind();
                usersItemsGrid.DataSource=null;
                usersItemsGrid.DataBind();
                subCategoryGrid.DataSource=null;
                subCategoryGrid.DataBind();
                pcTechListGrid.DataSource=null;
                pcTechListGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                tblUserItemsNavigation.Visible = false;
                tblSubCategoryNavigation.Visible = false;
                tblPcTechListNavigation.Visible = false;
                tblRoomStatisticsNavigation.Visible = false;
                lblSprItemsTitle.Text="";
                lblSprItemsCount.Text="";
                lblAssignedItemsNoText.Text="";
                lblAssignedItemsNo.Text="";
                lblUnassignedItemsNoText.Text="";
                lblUnassignedItemsNo.Text = "";
                mCheckGridStatus();
            }
            else
                if (ddlSearchCriteria.SelectedItem.Value == "2")
                {
                    pnlCategorySearch.Visible = false;
                    pnlPcTechSearch.Visible = false;
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
                    mFillUserItemsGrid();
                    tblUserItemsNavigation.Visible = false;
                    tblSubCategoryNavigation.Visible = false;
                    tblPcTechListNavigation.Visible = false;
                    tblRoomStatisticsNavigation.Visible = false;
                }
                else
                    if (ddlSearchCriteria.SelectedItem.Value == "3")
                    {
                        pnlCategorySearch.Visible = false;
                        pnlPcTechSearch.Visible = true;
                        pnlUserSearch.Visible = false;
                        pnlRoomSearch.Visible = false;
                        lblSprItemsCount.Visible = false;
                        lblAssignedItemsNo.Visible = false;
                        lblUnassignedItemsNo.Visible = false;
                        lblSprItemsTitle.Visible = false;
                        lblAssignedItemsNoText.Visible = false;
                        lblUnassignedItemsNoText.Visible = false;
                        mFillPcTechStatisticsGrid();
                        tblUserItemsNavigation.Visible = false;
                        tblSubCategoryNavigation.Visible = false;
                        tblPcTechListNavigation.Visible = false;
                        tblRoomStatisticsNavigation.Visible = false;

                    }
                    else
                        if (ddlSearchCriteria.SelectedItem.Value == "4")
                        {
                            pnlCategorySearch.Visible = false;
                            pnlPcTechSearch.Visible = false;
                            pnlUserSearch.Visible = false;
                            pnlRoomSearch.Visible = true;
                            lblSprItemsCount.Visible = false;
                            lblAssignedItemsNo.Visible = false;
                            lblUnassignedItemsNo.Visible = false;
                            lblSprItemsTitle.Visible = false;
                            lblAssignedItemsNoText.Visible = false;
                            lblUnassignedItemsNoText.Visible = false;
                            mFillRoomItemsGrid();
                            tblUserItemsNavigation.Visible = false;
                            tblSubCategoryNavigation.Visible = false;
                            tblPcTechListNavigation.Visible = false;
                            tblRoomStatisticsNavigation.Visible = false;
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
            usersItemsGrid.DataSource = null;
            usersItemsGrid.DataBind();
            lblItemsGrid.Text = "List of (" + ddlUserSearch.SelectedItem.Text + ") Items";

        }
        catch (Exception exp)
        {
        }
    }
    protected void mPcTechDDLSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the PC Tech products in the data grid
        /// Author: mutawakelm
        /// Date :2/8/2009 11:54:15 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillGridByPcTech(ddlPcTechSearch.SelectedItem.Value, ddlPcTechSearch.SelectedItem.Text);
           
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

            mFillGridByRoom(ddlRoomSearch.SelectedItem.Value, ddlRoomSearch.SelectedItem.Text);
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

            if ((productsGrid.Items.Count > 0) || (usersItemsGrid.Items.Count > 0) || (pcTechListGrid.Items.Count > 0) || (subCategoryGrid.Items.Count > 0) || (roomStatisticsGrid.Items.Count > 0))
                tblAssigning.Visible = true;
            else
                tblAssigning.Visible = false;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillGridByRoom(string roomNo,string roomName)
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
                productTable.Columns.Add("actSerial");
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.act_serial_no,t_products.spr_no,t_products.serial_no,t_products.delivery_date,t_products.recieved_by," +
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(), reader["serial_no"].ToString(), reader["act_serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                usersItemsGrid.DataSource = null;
                usersItemsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                pcTechListGrid.DataSource = null;
                pcTechListGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                lblItemsGrid.Text = "Statistics of Room (" + roomName + ")";
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();



            }
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mFillGridByPcTech(string pcTecID,string pcTechName)
    {
        try
        {
            if (pcTecID != "")
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
                productTable.Columns.Add("actSerial");
                string strProductQuery = "SELECT t_products.room,t_products.act_serial_no,t_vendor.supplier_name,t_products.spr_no,t_products.serial_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.maintained_by='" + pcTecID + "' and t_products.vendor=t_vendor.id";
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(), reader["serial_no"].ToString(), reader["act_serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                usersItemsGrid.DataSource = null;
                usersItemsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                pcTechListGrid.DataSource = null;
                pcTechListGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                lblItemsGrid.Text = "Statistics of PC Tech (" + pcTechName + ")";
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();



            }
            mCheckGridStatus();
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
                productTable.Columns.Add("actSerial");
                string strRoomName = "";
                DataTable tblRooms = new DataTable();
                tblRooms.Columns.Add("id");
                tblRooms.Columns.Add("roomName");
                //The following code will fill the room datatable
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
                string strProductQuery = "SELECT t_products.room,t_products.act_serial_no,t_products.allocated_room,t_vendor.supplier_name,t_products.spr_no,t_products.serial_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.allocated_to='" + userID + "' and t_products.vendor=t_vendor.id and t_products.dept_id=" + Session["departmentId"].ToString();
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
                        if (reader["allocated_room"].ToString() != "")
                        {
                            for (int i = 0; i < tblRooms.Rows.Count; i++)
                            {
                                if (tblRooms.Rows[i][0].ToString() == reader["allocated_room"].ToString())
                                    strRoomName = tblRooms.Rows[i][1].ToString();
                            }

                        }
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), strRoomName, reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(), reader["serial_no"].ToString(), reader["act_serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                usersItemsGrid.DataSource = null;
                usersItemsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                pcTechListGrid.DataSource = null;
                pcTechListGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = "0";
                lblAssignedItemsNo.Text = "0";
                lblUnassignedItemsNo.Text = "0";
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);

            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();



            }
            mCheckGridStatus();

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
            if (pnlPcTechSearch.Visible.ToString() == "True")
            {
                mFillGridByPcTech(ddlPcTechSearch.SelectedItem.Value, ddlPcTechSearch.SelectedItem.Text);
                
            }
            else
                if (pnlCategorySearch.Visible.ToString() == "True")
                {
                    mFillGridBySprNo(ddlSubSubCategory.SelectedItem.Value,ddlSubSubCategory.SelectedItem.Text);
                }
            else
                    if (pnlUserSearch.Visible.ToString() == "True")
                {
                    mFillGridByUser(ddlUserSearch.SelectedItem.Value);
                }
                    else
                        if (pnlRoomSearch.Visible.ToString() == "True")
                {

                    mFillGridByRoom(ddlRoomSearch.SelectedItem.Value, ddlRoomSearch.SelectedItem.Text);
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

            if (productsGrid.Items.Count > 0)
                GeneralClass.ResponseWriter.Write(Response, "Export.pdf", new GeneralClass.PdfDataGridExporter(productsGrid, Context, lblItemsGrid.Text));
            else
                if (subCategoryGrid.Items.Count > 0)
                    GeneralClass.ResponseWriter.Write(Response, "Export.pdf", new GeneralClass.PdfDataGridExporter(subCategoryGrid, Context, lblItemsGrid.Text));
                else
                    if (usersItemsGrid.Items.Count > 0)
                        GeneralClass.ResponseWriter.Write(Response, "Export.pdf", new GeneralClass.PdfDataGridExporter(usersItemsGrid, Context, lblItemsGrid.Text));
                    else
                        if (pcTechListGrid.Items.Count > 0)
                            GeneralClass.ResponseWriter.Write(Response, "Export.pdf", new GeneralClass.PdfDataGridExporter(pcTechListGrid, Context, lblItemsGrid.Text));
                        else
                            if (roomStatisticsGrid.Items.Count > 0)
                                GeneralClass.ResponseWriter.Write(Response, "Export.pdf", new GeneralClass.PdfDataGridExporter(roomStatisticsGrid, Context, lblItemsGrid.Text));
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSpecificationEditClicked(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when the edit link will be clicked in specification grid
        /// Author: mutawakelm
        /// Date :2/14/2009 10:26:07 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {


            
            //The following code will be used to add the properties to the table "t_item_upgrade"
            if (lblSpecifiedFromMaster.Text == "notUpgraded")
            {
                //The first step is to add the flag true to upgraded field in the table "t_item_upgrade"
                GeneralClass.Program.Add("upgraded", "True", "S");
                GeneralClass.Program.UpdateRecordStatement("t_products", "id", lblItemNumber.Text);
                foreach (DataGridItem dg in specificationDataGrid.Items)
                {
                    string thePropertyLabel = dg.Cells[1].Text;
                    string theDescriptionLabel = dg.Cells[2].Text;
                    //The following code will update the item 
                    GeneralClass.Program.Add("item_id", lblItemNumber.Text, "I");
                    GeneralClass.Program.Add("property", thePropertyLabel, "S");
                    GeneralClass.Program.Add("description", theDescriptionLabel, "S");
                    GeneralClass.Program.Add("upgradation_date", DateTime.Now.ToString(), "D");
                    GeneralClass.Program.InsertRecordStatement("t_item_upgrade");
                }
                lblSpecifiedFromMaster.Text = "upgraded";
                specificationDataGrid.EditItemIndex = e.Item.ItemIndex;
                mFillSpecificationGrid();
                
            }
            else
            {
                specificationDataGrid.EditItemIndex = e.Item.ItemIndex;
                mFillSpecificationGrid();
                
            }
            
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSpecificationCancelClicked(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to cancel the editing mode
        /// Author: mutawakelm
        /// Date :2/14/2009 10:31:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            specificationDataGrid.EditItemIndex = -1;
            mFillSpecificationGrid();
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSpecificationUpdateClicked(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update an item as the following steps
        /// 1-Put the flage of upgraded to "True" in the table "t_products".
        /// 2-Insert the updated records
        /// 3-Check the old properties then assign the updated once to last version flag.
        /// Author: mutawakelm
        /// Date :2/14/2009 10:31:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            System.Web.UI.WebControls.TextBox thePropertyTextBox =null;
            string thePropertyLabel = "";
            System.Web.UI.WebControls.TextBox theDescriptionTextBox = null;
            System.Web.UI.WebControls.TextBox selectedElement = null;
            string theDescriptionLabel = "";
            specificationDataGrid.EditItemIndex = -1;
            if (lblSpecifiedFromMaster.Text == "notUpgraded")
            {
                //The first step is to add the flag true to upgraded field in the table "t_item_upgrade"
                GeneralClass.Program.Add("upgraded", "True", "S");
                GeneralClass.Program.UpdateRecordStatement("t_products", "id", lblItemNumber.Text);
                thePropertyTextBox = (System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0];
                theDescriptionTextBox = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
                //The following code will update the item 
                GeneralClass.Program.Add("item_id", lblItemNumber.Text, "I");
                GeneralClass.Program.Add("property", thePropertyTextBox.Text, "S");
                GeneralClass.Program.Add("description", theDescriptionTextBox.Text, "S");
                GeneralClass.Program.Add("upgradation_date", DateTime.Now.ToString(), "D");
                GeneralClass.Program.InsertRecordStatement("t_item_upgrade");
                foreach (DataGridItem dg in specificationDataGrid.Items)
                {
                    if (dg.ItemIndex != e.Item.DataSetIndex)
                    {
                        thePropertyLabel = dg.Cells[1].Text;
                        theDescriptionLabel = dg.Cells[2].Text;
                        //The following code will update the item 
                        GeneralClass.Program.Add("item_id", lblItemNumber.Text, "I");
                        GeneralClass.Program.Add("property", thePropertyLabel, "S");
                        GeneralClass.Program.Add("description", theDescriptionLabel, "S");
                        GeneralClass.Program.Add("upgradation_date", DateTime.Now.ToString(), "D");
                        GeneralClass.Program.InsertRecordStatement("t_item_upgrade");
                    }
                }
            }
            else
                if (lblSpecifiedFromMaster.Text == "upgraded")
            {
                    //The first step is to assign the False 
                selectedElement = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
                GeneralClass.Program.Add("last_version", "False", "S");
                GeneralClass.Program.UpdateRecordStatement("t_item_upgrade", "id", selectedElement.Text);
                //The following code will assign the new upgrade record to the table "t_item_upgrade"
                thePropertyTextBox = (System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0];
                theDescriptionTextBox = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
                //The following code will update the item 
                GeneralClass.Program.Add("item_id", lblItemNumber.Text, "I");
                GeneralClass.Program.Add("property", thePropertyTextBox.Text, "S");
                GeneralClass.Program.Add("description", theDescriptionTextBox.Text, "S");
                GeneralClass.Program.Add("upgradation_date", DateTime.Now.ToString(), "D");
                GeneralClass.Program.InsertRecordStatement("t_item_upgrade");
            }

            mFillSpecificationGrid();
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
                mGetUserFullName(serviceHistoryGrid,2);
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mFillSubCategoryGrid(string subCatId)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub category grid
        /// Author: mutawakelm
        /// Date :2/17/2009 9:33:49 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblSubCategoryStatistics = new DataTable();//This datatable will hold the statistics of the selected sub category
            tblSubCategoryStatistics.Columns.Add("item_type");
            tblSubCategoryStatistics.Columns.Add("total");
            tblSubCategoryStatistics.Columns.Add("unassigned");
            tblSubCategoryStatistics.Columns.Add("assigned");
            tblSubCategoryStatistics.Columns.Add("minimum");
            tblSubCategoryStatistics.Columns.Add("sys_id");
            int intTotalItemNo = 0;
            int intUnassignedItemsNo = 0;
            int intAssignedItemsNo = 0;
            int intMinmumNo = 0;
            string strItemTypeName = "";
            string strMinmumNo = "";
            string strStatisticsQuery = "";//This query will be used to get item statistics
            string strSubCatID = "";
            for (int i = 1; i < ddlSubSubCategory.Items.Count; i++)
            {
                strStatisticsQuery = "SELECT pro.allocated_to,pro.allocated_room,pro.sub_sub_cat,sub.sub_sub_category_name,sub.minimum_number FROM t_products as pro join t_sut_sub_category as sub on pro.sub_sub_cat=sub.id  WHERE pro.sub_sub_cat=" + ddlSubSubCategory.Items[i].Value + " AND pro.status_id=8";
                reader = GeneralClass.Program.gRetrieveRecord(strStatisticsQuery);
                if (reader.HasRows)
                {
                    intTotalItemNo = 0;
                    intUnassignedItemsNo = 0;
                    intAssignedItemsNo = 0;
                    while (reader.Read())
                    {
                        intTotalItemNo++;
                        if ((reader["allocated_to"].ToString() != "") || (reader["allocated_room"].ToString() != ""))
                            intAssignedItemsNo++;
                        else
                            intUnassignedItemsNo++;
                        strItemTypeName = reader["sub_sub_category_name"].ToString();
                        strMinmumNo = reader["minimum_number"].ToString();
                        strSubCatID = reader["sub_sub_cat"].ToString();
                    }
                    reader.Close();
                    //The following segement will fill a row of the "tblSubCategoryStatistics" data table
                    tblSubCategoryStatistics.Rows.Add(strItemTypeName, intTotalItemNo.ToString(), intUnassignedItemsNo.ToString(), intAssignedItemsNo.ToString(), strMinmumNo, strSubCatID);
                }
                else reader.Close();
            }
            subCategoryGrid.DataSource = tblSubCategoryStatistics;
            subCategoryGrid.DataBind();
            productsGrid.DataSource = null;
            productsGrid.DataBind();
            lblItemsGrid.Text = "Statistics of Sub Category ("+ddlSubCategory.SelectedItem.Text+")";
            tblStatistics.Visible = false;
            mCheckGridStatus();

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mFillUserItemsGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the user items grid
        /// Author: mutawakelm
        /// Date :2/17/2009 10:32:54 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblUserItmes = new DataTable();//This datatable will hold the statistics of the selected sub category
            tblUserItmes.Columns.Add("user_name");
            tblUserItmes.Columns.Add("total");
            tblUserItmes.Columns.Add("userLogin");
            string strUsersItemsQuery = "SELECT distinct(allocated_to),count(allocated_to) as theCounter,ldap.full_name FROM t_products as pro,t_LdapUsers as ldap WHERE pro.allocated_to=ldap.login_name and pro.dept_id=" + Session["departmentId"].ToString() + " group by pro.allocated_to,ldap.full_name  order by theCounter desc";
            reader = GeneralClass.Program.gRetrieveRecord(strUsersItemsQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblUserItmes.Rows.Add(reader["full_name"].ToString(), reader["theCounter"].ToString(), reader["allocated_to"].ToString());
                }
                reader.Close();
                usersItemsGrid.DataSource = tblUserItmes;
                usersItemsGrid.DataBind();
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                pcTechListGrid.DataSource = null;
                pcTechListGrid.DataBind();
                lblItemsGrid.Text = "List of Users Items";
                mCheckGridStatus();
            }
            else reader.Close();
            tblUserItemsNavigation.Visible = false;
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mUsersItemsGridEdit(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the selected user info
        /// Author: mutawakelm
        /// Date :2/17/2009 11:07:40 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            mFillGridByUser(e.Item.Cells[3].Text);
            lblItemsGrid.Text = "List of (" + e.Item.Cells[1].Text + ") Items";
            usersItemsGrid.DataSource = null;
            usersItemsGrid.DataBind();
            tblUserItemsNavigation.Visible = true;
            
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGoUserListBtnClicked(object sender, EventArgs e)
    {
        
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the "mFillUserItemsGrid" function to refill the datagrid "usersItemsGrid"
        /// Author: mutawakelm
        /// Date :2/17/2009 11:17:11 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillUserItemsGrid();
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGoSubCategoryListBtnClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to return to the subcategory list
        /// Author: mutawakelm
        /// Date :2/17/2009 11:26:51 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillSubCategoryGrid(ddlSubCategory.SelectedItem.Value);
            tblSubCategoryNavigation.Visible = false;
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSubCategoryGridEdit(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the selected user info
        /// Author: mutawakelm
        /// Date :2/17/2009 11:07:40 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            mFillGridBySprNo(e.Item.Cells[6].Text, e.Item.Cells[1].Text);
            lblitemCounterValidity.Text = "";
            lblItemsGrid.Text = "Statistics of Sub Sub Category (" + e.Item.Cells[1].Text + ")";
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            tblStatistics.Visible = true;
            usersItemsGrid.DataSource = null;
            usersItemsGrid.DataBind();
            tblSubCategoryNavigation.Visible = true;


        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillPcTechStatisticsGrid()
    {
        
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the datagrid "pcTechListGrid"
        /// Author: mutawakelm
        /// Date :2/17/2009 10:32:54 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblPcTechStatistics = new DataTable();//This datatable will hold the statistics of PC Techs
            tblPcTechStatistics.Columns.Add("pc_tech_name");
            tblPcTechStatistics.Columns.Add("total");
            tblPcTechStatistics.Columns.Add("login");
            string strPcTechItemsQuery = "SELECT DISTINCT(maintained_by),COUNT(maintained_by) as itemsNo,ldap.full_name FROM t_products as pro,t_LdapUsers as ldap WHERE pro.maintained_by=ldap.login_name GROUP BY pro.maintained_by,ldap.full_name";
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechItemsQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblPcTechStatistics.Rows.Add(reader["full_name"].ToString(), reader["itemsNo"].ToString(), reader["maintained_by"].ToString());
                }
                reader.Close();

                pcTechListGrid.DataSource = tblPcTechStatistics;
                pcTechListGrid.DataBind();
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                usersItemsGrid.DataSource = null;
                usersItemsGrid.DataBind();
                roomStatisticsGrid.DataSource = null;
                roomStatisticsGrid.DataBind();
                lblItemsGrid.Text = "List of Pc Technicians Items";
            }
            else reader.Close();
            tblUserItemsNavigation.Visible = false;
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mPcTechStatisticsGridEdit(object sender, DataGridCommandEventArgs e)
    {
        
        //=====================================================//
        /// <summary>
        /// Description:This function will be issued when the ediing button of "pcTechListGrid" datagrid will be clicked
        /// in order to display the individual items list of the selected pc tech.
        /// Author: mutawakelm
        /// Date :2/17/2009 3:01:03 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillGridByPcTech(e.Item.Cells[3].Text, e.Item.Cells[1].Text);
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            tblStatistics.Visible = true;
            usersItemsGrid.DataSource = null;
            usersItemsGrid.DataBind();
            pcTechListGrid.DataSource = null;
            pcTechListGrid.DataBind();
            
            tblPcTechListNavigation.Visible = true;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGoPcTechListBtnClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to return to the pc techs list
        /// Author: mutawakelm
        /// Date :2/17/2009 11:26:51 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillPcTechStatisticsGrid();
            tblPcTechListNavigation.Visible = false;
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillRoomItemsGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the room statistics grid
        /// Author: mutawakelm
        /// Date :2/17/2009 10:32:54 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblRoomItmes = new DataTable();//This datatable will hold the statistics of the rooms
            tblRoomItmes.Columns.Add("room_name");
            tblRoomItmes.Columns.Add("total");
            tblRoomItmes.Columns.Add("sys_id");
            string strRoomsItemsQuery = "SELECT DISTINCT(allocated_room),COUNT(allocated_room) as no,sto.storage_place FROM t_products as pro join t_storage_place as sto on pro.allocated_room=sto.id and pro.dept_id=" + Session["departmentId"].ToString() + " GROUP BY allocated_room,storage_place ORDER BY no desc";
            reader = GeneralClass.Program.gRetrieveRecord(strRoomsItemsQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblRoomItmes.Rows.Add(reader["storage_place"].ToString(), reader["no"].ToString(), reader["allocated_room"].ToString());
                }
                reader.Close();

                roomStatisticsGrid.DataSource = tblRoomItmes;
                roomStatisticsGrid.DataBind();
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                subCategoryGrid.DataSource = null;
                subCategoryGrid.DataBind();
                pcTechListGrid.DataSource = null;
                pcTechListGrid.DataBind();
                usersItemsGrid.DataSource = null;
                usersItemsGrid.DataBind();
                lblItemsGrid.Text = "List of Room Items";
            }
            else reader.Close();
            tblRoomStatisticsNavigation.Visible = false;
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mRoomStatisticsGridEdit(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to display the room items list
        /// Author: mutawakelm
        /// Date :2/17/2009 4:35:04 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillGridByRoom(e.Item.Cells[3].Text, e.Item.Cells[1].Text);
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            subCategoryGrid.DataSource = null;
            subCategoryGrid.DataBind();
            tblStatistics.Visible = true;
            usersItemsGrid.DataSource = null;
            usersItemsGrid.DataBind();
            pcTechListGrid.DataSource = null;
            pcTechListGrid.DataBind();
            roomStatisticsGrid.DataSource = null;
            roomStatisticsGrid.DataBind();
            
            tblRoomStatisticsNavigation.Visible = true;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mGoRoomListBtnClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to return to the room list
        /// Author: mutawakelm
        /// Date :2/17/2009 11:26:51 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillRoomItemsGrid();
            tblRoomStatisticsNavigation.Visible = false;
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mShowItemsDetails(object sender, EventArgs e)
    {
        try
        {
            productsGrid.Columns[5].Visible = true;
            productsGrid.Columns[6].Visible = true;
            productsGrid.Columns[7].Visible = true;
            productsGrid.Columns[8].Visible = true;
        }
        catch (Exception exp)
        {
        }
    }
}
