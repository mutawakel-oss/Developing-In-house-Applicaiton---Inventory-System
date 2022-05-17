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
                mFillSubCategoryDDL();
                mFillSubSubCategoryDDL();
                mFillRoomDDL();
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
            if (null != Session["assignmenter"])
            {
                if (Session["assignmenter"].ToString() == "True")
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
            int counter = 0;
            string strCategoryQuery = "SELECT id,category_name FROM t_category WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
            if (reader.HasRows)
            {
                ddlCategory.Items.Clear();
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
            int counter = 0;
            string strsubCategoryQuery = "SELECT id,sub_cateogry_name FROM t_sub_catgory where category_id=" + ddlCategory.Items[ddlCategory.SelectedIndex].Value;
            reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
            ddlSubCategory.Items.Clear();
            if (reader.HasRows)
            {
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
            string strSubSubCat = "";
            int counter = 1;
            ddlSubSubCategory.Items.Clear();
            if (!string.IsNullOrEmpty(ddlSubCategory.Text))
            {
                string strsubCategoryQuery = "SELECT id,sub_sub_category_name FROM t_sut_sub_category WHERE sub_category_id=" + ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value;
                reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
                if (reader.HasRows)
                {
                    ddlSubSubCategory.Items.Add("--Select--");
                    while (reader.Read())
                    {
                        if (reader["sub_sub_category_name"].ToString().Length > 80)
                            strSubSubCat = reader["sub_sub_category_name"].ToString().Substring(0, 80).ToString() + "...";
                        else
                            strSubSubCat = reader["sub_sub_category_name"].ToString();
                        ddlSubSubCategory.Items.Add(strSubSubCat);
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
                mFillGridBySprNo(ddlSubSubCategory.Text);
                lblitemCounterValidity.Text = "";
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
                productTable.Columns.Add("serial");
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
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.serial_no,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
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
                        if (reader["inspected_by"] != null)
                            strInspectedBy = reader["inspected_by"].ToString();
                        if (reader["approved_by"] != null)
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(),reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                   
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following call will fill the productsGrid use full names
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
                //The following code will display number of items in the data grid
                lblSprItemsTitle.Text = "Total Number of "+ddlSubSubCategory.SelectedItem.Text+":";
                lblSprItemsCount.Text = productsGrid.Items.Count.ToString();
                lblAssignedItemsNoText.Text = "Total Assigned " + ddlSubSubCategory.SelectedItem.Text + ":";
                lblAssignedItemsNo.Text = assignedItemsCounter.ToString();
                lblUnassignedItemsNoText.Text = "Total Unassigned " + ddlSubSubCategory.SelectedItem.Text + ":";
                unAssignedItemsCounter=int.Parse(lblSprItemsCount.Text) - int.Parse(lblAssignedItemsNo.Text);
                lblUnassignedItemsNo.Text = unAssignedItemsCounter.ToString();
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
    protected void mAssignLinkClicked(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to delete a row in the dataGrid "productsGrid"
        /// Author: mutawakelm
        /// Date :3/2/2009 10:41:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TableCell itemCell = e.Item.Cells[10];
            TableCell assignedToCell = e.Item.Cells[9];
            string item = itemCell.Text;
            string assignedToText = assignedToCell.Text;
            lblItemNumber.Text = item;
            lblAssigned_to.Text = assignedToText;
            AssignementExtender.Show();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
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
                DirectoryEntry entry1 = new DirectoryEntry("LDAP://" + strLdap, username, pwd);//OU=staff,OU=collegeusers,OU=mis

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
                ddlDepartment.Items.Clear();
                while (reader.Read())
                {
                    ddlDepartment.Items.Add(reader["department_name"].ToString());
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
    protected void mDpartmentDDlSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will fill the users drop down list with the employees
        /// Author: mutawakelm
        /// Date :2/3/2009 11:24:14 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int counter = 1;
            string strUsersQuery = "SELECT full_name,department_name,login_name  FROM t_LdapUsers where department_name='" + ddlDepartment.SelectedItem.Text + "'";
            reader = GeneralClass.Program.gRetrieveRecord(strUsersQuery);
            if (reader.HasRows)
            {
                ddlUsers.Items.Clear();
                ddlUsers.Items.Add("Select A User");
                while (reader.Read())
                {
                    ddlUsers.Items.Add(reader["full_name"].ToString());
                    ddlUsers.Items[counter].Value = reader["login_name"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
            AssignementExtender.Show();
        }
        catch (Exception exp)
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
            DateTime starDate = new DateTime();//This variable will hold the start date
            DateTime endDate = new DateTime();//This variable will hold the end date

            //The following code will be used to assign/update the t_products to a specified end-user
            if (ddlSelectionType.SelectedItem.Value == "1")
            {
                foreach (DataGridItem dg in productsGrid.Items)
                {
                    CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                    if (chk != null)
                    {
                        if (chk.Checked == true)
                        {
                            if (dg.Cells[9].Text == "&nbsp;")
                            {
                                if (ddlUsers.SelectedItem != null)
                                GeneralClass.Program.Add("allocated_to", ddlUsers.SelectedItem.Value, "S");
                                else
                                GeneralClass.Program.Add("allocated_to", string.Empty, "S");
                                int returnId = GeneralClass.Program.UpdateRecordStatement("t_products", "id", dg.Cells[10].Text);
                                if (returnId != 0)
                                {
                                    mInsertAssignmentHistory(dg.Cells[10].Text);
                                }
                                
                            }
                            else
                            {
                                if (ddlUsers.SelectedItem!=null)
                                GeneralClass.Program.Add("allocated_to", ddlUsers.SelectedItem.Value, "S");
                                else
                                GeneralClass.Program.Add("allocated_to", string.Empty, "S");
                                int returnId = GeneralClass.Program.UpdateRecordStatement("t_products", "id", dg.Cells[10].Text);
                                if (returnId != 0)
                                    mInsertAssignmentHistory(dg.Cells[10].Text);

                            }
                            
                        }
                    }
                }
                mFillAfaterModification();
                if (pnlCategorySearch.Visible.ToString() == "True")
                    mCheckAssigningValidityByCategory();
                else
                    if (pnlSprSearch.Visible.ToString() == "True")
                    mCheckAssignVlidityBySpr();
               
            }
            else
            //The following code will be used to assign/update the t_products to a pc technician
                if (ddlSelectionType.SelectedItem.Value == "2")
                {
                    lblPcTechError.Text = "";
                    foreach (DataGridItem dg in productsGrid.Items)
                    {
                        CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                        if (chk != null)
                        {
                            if (chk.Checked == true)
                            {
                                GeneralClass.Program.Add("maintained_by", ddlPcTech.SelectedItem.Value, "S");
                                int returnId = GeneralClass.Program.UpdateRecordStatement("t_products", "id", dg.Cells[10].Text);
                            }
                        }
                    }
                    //The following code will add a prefentive maintainance sheet for the PC Tech
                    if ((txtStartingDate.Text != "") && (txtEndingDate.Text != ""))
                    {
                        starDate = DateTime.Parse(txtStartingDate.Text);
                        endDate = DateTime.Parse(txtEndingDate.Text);
                        if (starDate < endDate)
                            mAssignItemsToMaintainnce();
                        else
                        {
                            lblPcTechError.Text = "Starting date should be before ending date , please verify the dates.";
                            AssignementExtender.Show();
                        }
                    }
                    mFillAfaterModification();

                }
                else
                    //The following code will be used to assign/update the t_products to a pc technician
                    if (ddlSelectionType.SelectedItem.Value == "3")
                    {
                        foreach (DataGridItem dg in productsGrid.Items)
                        {
                            CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                            if (chk != null)
                            {
                                if (chk.Checked == true)
                                {
                                    if (ddlRoom.SelectedItem.Text != "--Select A room--")
                                    GeneralClass.Program.Add("allocated_room", ddlRoom.SelectedItem.Value, "S");
                                    else
                                    GeneralClass.Program.Add("allocated_room", string.Empty, "S");
                                    int returnId = GeneralClass.Program.UpdateRecordStatement("t_products", "id", dg.Cells[10].Text);
                                }
                            }
                        }
                        mFillAfaterModification();

                    }


        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mCheckAssignVlidityBySpr()
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to check if the assignment of items is valid when the search
        /// is by spr no.
        /// Author: mutawakelm
        /// Date :3/11/2009 11:46:49 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strUnassignedQuery = "";
            string strassignedQuery = "";
            string strSystemAdministratorsQuery = "";
            double dblRemainedPercentage = 0.0;
            string itemReport = "";
            string strToMail = "";//This variable will hold the mail of the requester
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("inventory@ksau-hs.edu.sa", "Inventory System");
            //The following code will create a data table for the categories of the spr items
            DataTable tblSprCategoryValidation = new DataTable();
            tblSprCategoryValidation.Columns.Add("sub_sub_cat_id");
            tblSprCategoryValidation.Columns.Add("sub_sub_cat_name");
            tblSprCategoryValidation.Columns.Add("sub_sub_cat_min");
            tblSprCategoryValidation.Columns.Add("sub_sub_cat_unassign");
            tblSprCategoryValidation.Columns.Add("sub_sub_cat_assign");
            //The following query will look for spr sub_sub_category ids
            string strSprSubSubCatIds = "SELECT DISTINCT(pro.sub_sub_cat),sub.minimum_number,sub.sub_sub_category_name FROM t_products as pro JOIN t_sut_sub_category as sub ON pro.sub_sub_cat=sub.id  WHERE spr_no=" + lblSprHiddenNo.Text+" AND status_id=8";
            reader = GeneralClass.Program.gRetrieveRecord(strSprSubSubCatIds);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    tblSprCategoryValidation.Rows.Add(reader["sub_sub_cat"].ToString(), reader["sub_sub_category_name"].ToString(), reader["minimum_number"].ToString());
                }
                reader.Close();
            }
            else
                reader.Close();
            //The following code will iterate the table to find the unassigned and assigned number of items
            for (int i = 0; i < tblSprCategoryValidation.Rows.Count; i++)
            {
                //The followint query to get the number of unassigned items
                strUnassignedQuery = "SELECT COUNT(id) AS unass FROM t_products WHERE sub_sub_cat="+tblSprCategoryValidation.Rows[i][0].ToString()+" AND allocated_to IS NULL AND status_id=8";
                reader = GeneralClass.Program.gRetrieveRecord(strUnassignedQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    tblSprCategoryValidation.Rows[i][3] = reader["unass"].ToString();
                    reader.Close();
                }
                else
                    reader.Close();
                //The followint query to get the number of assigned items
                strassignedQuery = "SELECT COUNT(id) AS unass FROM t_products WHERE sub_sub_cat=" + tblSprCategoryValidation.Rows[i][0].ToString() + " AND allocated_to IS NOT NULL AND status_id=8";
                reader = GeneralClass.Program.gRetrieveRecord(strassignedQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    tblSprCategoryValidation.Rows[i][4] = reader["unass"].ToString();
                    reader.Close();
                }
                else
                    reader.Close();

            }
            //The following code will compare the percentage and send emails
            for (int i = 0; i < tblSprCategoryValidation.Rows.Count; i++)
            {
                dblRemainedPercentage = double.Parse(tblSprCategoryValidation.Rows[i][3].ToString()) / (double.Parse(tblSprCategoryValidation.Rows[i][4].ToString()) + double.Parse(tblSprCategoryValidation.Rows[i][3].ToString())) * 100;
                if (dblRemainedPercentage <= double.Parse(tblSprCategoryValidation.Rows[i][2].ToString()))
                {
                    itemReport += "\n\n* (" + tblSprCategoryValidation.Rows[i][1].ToString() + ") Minimum Percentage:" + tblSprCategoryValidation.Rows[i][2].ToString() + "% ,the remained items percentage is:" + dblRemainedPercentage.ToString() + "% ,The number of remained items is:" + tblSprCategoryValidation.Rows[i][3].ToString();
                }

            }
            //The following code will send the emial
            if (itemReport != "")
            {
                strSystemAdministratorsQuery = "SELECT id FROM t_users where user_group='1,3,4,5,6,7,8,9,10' and dept_id=" + Session["departmentId"].ToString();
                reader = GeneralClass.Program.gRetrieveRecord(strSystemAdministratorsQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //The following code will be used to send e-mail for the system admiinistrators
                        strToMail = reader["id"].ToString() + "@ksau-hs.edu.sa";
                        //The following code will send an e-mail to the user ask about service evaluation
                        smtpClient.Host = "mail1.ksuhs.edu.sa";
                        message.From = fromAddress;
                        message.To.Add(strToMail);
                        message.Subject = "Minimum Number of Item Has Reacheed";//This should have a distinguished name
                        message.IsBodyHtml = false;
                        string strMessage = "Dear, administrator \n\nThis email has sent to inform you that the minimum percentage number of the following itmes which belong to SPR No:"+lblSprHiddenNo.Text+" has been reached:" + itemReport + ".\nPlease contact the vendor.\nWith regards ";
                        message.Body = strMessage;
                        smtpClient.Send(message);
                        message.Dispose();

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

        }
    }
    protected void mCheckAssigningValidityByCategory()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check if the minimum items number of the item is reached
        /// Author: mutawakelm
        /// Date :2/3/2009 3:15:29 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strToMail = "";//This variable will hold the mail of the requester
            int intRemainedItems=int.Parse(lblUnassignedItemsNo.Text);
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("inventory@ksau-hs.edu.sa", "Inventory System");
            double dblRemainedPercentage = 0.0;
            int itemMinimumNo = 0;//This varaible will hold the minum number of items 
            string strSystemAdministratorsQuery = "";
            //The following query will get the item minium number from the table "t_sut_sub_category"
            string strMinumNoQuery = "SELECT minimum_number FROM t_sut_sub_category WHERE id="+ddlSubSubCategory.SelectedItem.Value;
            reader = GeneralClass.Program.gRetrieveRecord(strMinumNoQuery);
            if (reader.HasRows)
            {
                reader.Read();
                itemMinimumNo = int.Parse(reader["minimum_number"].ToString());
                reader.Close();
            }
            else reader.Close();
            //The following condition will identify the validity
            dblRemainedPercentage = double.Parse(lblUnassignedItemsNo.Text) / double.Parse(lblSprItemsCount.Text)*100;
            if (dblRemainedPercentage <= double.Parse(itemMinimumNo.ToString()))
            {
                //lblitemCounterValidity.Text = "You have reached the minium number("+itemMinimumNo.ToString()+") of this item please contact the vendor";
                //The following code will be used to retrieve the emails of the system administrators
                strSystemAdministratorsQuery = "SELECT id FROM t_users where user_group='1,3,4,5,6,7,8,9,10' and dept_id=" + Session["departmentId"].ToString();
                reader = GeneralClass.Program.gRetrieveRecord(strSystemAdministratorsQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        //The following code will be used to send e-mail for the system admiinistrators
                        strToMail = reader["id"].ToString()+"@ksau-hs.edu.sa";
                        //The following code will send an e-mail to the user ask about service evaluation
                        smtpClient.Host = "mail1.ksuhs.edu.sa";
                        message.From = fromAddress;
                        message.To.Add(strToMail);
                        message.Subject = "Minimum Number of "+ddlSubSubCategory.SelectedItem.Text+" Has Reacheed";//This should have a distinguished name
                        message.IsBodyHtml = false;
                        string strMessage = "Dear, administrator \n\nThis email has sent to inform you that the minimum percentage of (" + ddlSubSubCategory.SelectedItem.Text + ") which is:" + itemMinimumNo.ToString() + "% has reached, The percentage of the remained items is:"+dblRemainedPercentage.ToString()+"%,the remained total items number is:" + intRemainedItems.ToString()+ ".\nPlease contact the vendor.\nWith regards ";
                        message.Body = strMessage;
                        smtpClient.Send(message);
                        message.Dispose();
                    }
                    reader.Close();
                }
                else reader.Close();

            }
        }catch(Exception exp)
        {
            if (reader != null)
                reader.Close();

        }
    }
    protected void mInsertAssignmentHistory(string strItemNo)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to insert an assignment hisotry for an item
        /// Author: mutawakelm
        /// Date :2/3/2009 11:51:34 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("item_no", strItemNo, "I");
            if (ddlUsers.SelectedItem != null)
            GeneralClass.Program.Add("assigned_to", ddlUsers.SelectedItem.Value, "S");
            else
            GeneralClass.Program.Add("assigned_to", string.Empty, "S");
            GeneralClass.Program.Add("assignment_date",DateTime.Now.ToString(), "S");
            GeneralClass.Program.Add("assigned_by",    Session["UserID"].ToString(), "S");
            GeneralClass.Program.InsertRecordStatement("t_assignment_history");
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
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
            lblPcTechError.Text = "";
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSelectionTypeDDLSelected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to determine the assignment mechnism 
        /// Author: mutawakelm
        /// Date :2/8/2009 10:01:32 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            AssignementExtender.Show();
            if (ddlSelectionType.SelectedItem.Value == "1")
            {
              
                pnlAssignUser.Visible = true;
                pnlAssignToRoom.Visible = false;
                pnlAssignToPctech.Visible = false;
               

            }
            else
                if (ddlSelectionType.SelectedItem.Value == "2")
                {
                    mFillPcTechDDL();
                    pnlAssignUser.Visible = false;
                    pnlAssignToRoom.Visible = false;
                    pnlAssignToPctech.Visible =true;
                
                }
                else
                    if (ddlSelectionType.SelectedItem.Value == "3")
                    {
                        pnlAssignUser.Visible = false;
                        pnlAssignToRoom.Visible = true;
                        pnlAssignToPctech.Visible = false;
                    }
                    else
                        if (ddlSelectionType.SelectedItem.Value == "0")
                        {
                            pnlAssignUser.Visible = false;
                            pnlAssignToRoom.Visible = false;
                            pnlAssignToPctech.Visible = false;
                        }
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
                ddlPcTech.Items.Clear();
                ddlPcTechSearch.Items.Clear();
                ddlPcTech.Items.Add("--Select A PC Tech--");
                ddlPcTechSearch.Items.Add("--Select A PC Tech--");
                ddlPcTech.Items[0].Value = "0";
                ddlPcTechSearch.Items[0].Value = "0";
                while (reader.Read())
                {
                    ddlPcTech.Items.Add(reader["full_name"].ToString());
                    ddlPcTech.Items[counter].Value = reader["id"].ToString();
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
            string strPcTechQuery = "SELECT * FROM t_storage_place WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strPcTechQuery);
            if (reader.HasRows)
            {
                ddlRoom.Items.Clear();
                ddlRoomSearch.Items.Clear();
                ddlRoomSearch.Items.Add("--Select A room--");
                ddlRoomSearch.Items[0].Value = "0";
                ddlRoom.Items.Add("--Select A room--");
                ddlRoom.Items[0].Value = "0";
                while (reader.Read())
                {
                    ddlRoom.Items.Add(reader["storage_place"].ToString());
                    ddlRoom.Items[counter].Value = reader["id"].ToString();
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
            if (ddlSearchCriteria.SelectedItem.Value == "1")
            {
                pnlCategorySearch.Visible = true;
                pnlPcTechSearch.Visible = false;
                pnlUserSearch.Visible = false;
                pnlRoomSearch.Visible = false;
                lblSprItemsTitle.Visible=true;
                lblSprItemsTitle.Text = "";
                lblAssignedItemsNoText.Visible=true;
                lblAssignedItemsNoText.Text = "";
                lblUnassignedItemsNoText.Visible = true;
                lblUnassignedItemsNoText.Text = "";
                lblSprItemsCount.Visible = true;
                lblSprItemsCount.Text = "";
                lblAssignedItemsNo.Visible = true;
                lblAssignedItemsNo.Text = "";
                lblUnassignedItemsNo.Visible = true;
                lblUnassignedItemsNo.Text = "";
                pnlSprSearch.Visible = false;
                ddlSubSubCategory.SelectedIndex = 0;
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
                    pnlSprSearch.Visible = false;

                    pullusers("", "wstaff", "test123");
                    DisplayDepartmentList();
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
                        pnlSprSearch.Visible = false;

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
                            pnlSprSearch.Visible = false;

                        }
                        else
                            if (ddlSearchCriteria.SelectedItem.Value == "5")
                            {
                                pnlCategorySearch.Visible = false;
                                pnlPcTechSearch.Visible = false;
                                pnlUserSearch.Visible = false;
                                pnlRoomSearch.Visible = false;
                                lblSprItemsCount.Visible = false;
                                lblAssignedItemsNo.Visible = false;
                                lblUnassignedItemsNo.Visible = false;
                                lblSprItemsTitle.Visible = false;
                                lblAssignedItemsNoText.Visible = false;
                                lblUnassignedItemsNoText.Visible = false;
                                pnlSprSearch.Visible = true;
                                
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
            mFillGridByPcTech(ddlPcTechSearch.SelectedItem.Value);
           
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
                string strProductQuery = "SELECT t_products.serial_no,t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
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
                //The following call will fill the productsGrid use full names
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
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
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillGridByPcTech(string pcTecID)
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
                string strProductQuery = "SELECT t_products.serial_no,t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(),reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following call will fill the productsGrid use full names
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
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
                string strProductQuery = "SELECT t_products.serial_no,t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(),reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following call will fill the productsGrid use full names
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
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
                mFillGridByPcTech(ddlPcTechSearch.SelectedItem.Value);
                
            }
            else
                if (pnlCategorySearch.Visible.ToString() == "True")
                {
                    mFillGridBySprNo(ddlSubSubCategory.SelectedItem.Value);
                }
            else
                    if (pnlUserSearch.Visible.ToString() == "True")
                {
                    mFillGridByUser(ddlUserSearch.SelectedItem.Value);
                }
                    else
                        if (pnlRoomSearch.Visible.ToString() == "True")
                {
                            
                    mFillGridByRoom(ddlRoomSearch.SelectedItem.Value);
                }
                        else
                    if (pnlSprSearch.Visible.ToString() == "True")
                    {
                        mFillGridBySpr(txtSprNo.Text);
                    }
        }
        catch (Exception exp)
        {
        }
    }
    protected void mAssignItemsToMaintainnce()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to assign the selected items in the grid for maintaince by the followin steps
        /// 1- add the selected items into the table "t_preventive_maintainance"
        /// Author: mutawakelm
        /// Date :2/9/2009 9:25:01 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strToMail = "";//This variable will hold the mail of the requester
            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();
            MailAddress fromAddress = new MailAddress("inventory@ksau-hs.edu.sa", "Inventory System");
            foreach (DataGridItem dg in productsGrid.Items)
            {
                CheckBox chk = (CheckBox)dg.FindControl("chkSelect");
                if (chk != null)
                {
                    if (chk.Checked == true)
                    {
                        GeneralClass.Program.Add("item_id", dg.Cells[10].Text, "I");
                        GeneralClass.Program.Add("starting_date", txtStartingDate.Text, "D");
                        GeneralClass.Program.Add("ending_date", txtEndingDate.Text, "D");
                        GeneralClass.Program.Add("maintained_by", ddlPcTech.SelectedItem.Value, "S");
                        GeneralClass.Program.Add("windows_update","False", "S");
                        GeneralClass.Program.Add("trend_update", "False", "S");
                        GeneralClass.Program.Add("virus_def", "False", "S");
                        GeneralClass.Program.Add("windows_firewall", "False", "S");
                        GeneralClass.Program.Add("pf", "False", "S");
                        GeneralClass.Program.Add("internet_cache", "False", "S");
                        GeneralClass.Program.Add("temp_files", "False", "S");
                        GeneralClass.Program.Add("scan_disk", "False", "S");
                        GeneralClass.Program.Add("event_log", "False", "S");
                        GeneralClass.Program.Add("hd_gb", "False", "S");
                        int returnID = GeneralClass.Program.InsertRecordStatement("t_preventive_maintainance");
                        if (returnID != 0)
                        {
                            lblPcTechError.Text = "Items preventive maintainance was added successfully.";

                        }
                            
                    }
                }
            }
            AssignementExtender.Show();
            //The following code will send an email to a pc tech to alaram him with the maintainance sheet
            strToMail = ddlPcTech.SelectedItem.Value + "@ksau-hs.edu.sa";
            //The following code will send an e-mail to the user ask about service evaluation
            smtpClient.Host = "mail1.ksuhs.edu.sa";
            message.From = fromAddress;
            message.To.Add(strToMail);
            message.Subject = "A New Preventive Maintainance Task.";//This should have a distinguished name
            message.IsBodyHtml = false;
            string strMessage = "Dear, " + ddlPcTech.SelectedItem.Text + " \n\nThis email has sent to inform you that a new prevetive maintainance task which is needed to start from:" + txtStartingDate.Text + " to:" + txtEndingDate.Text + " has been assigned for you, \nPlease check inventory system.\nWith regards. ";
            message.Body = strMessage;
            smtpClient.Send(message);
            message.Dispose();
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
    protected void mSprSearchBtnClicked(object sender, EventArgs e)
    {
        try
        {
            mFillGridBySpr(txtSprNo.Text);
            lblSprHiddenNo.Text = txtSprNo.Text;
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillGridBySpr(string sprNo)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid according to the entered spr no
        /// Author: mutawakelm
        /// Date :3/11/2009 8:28:11 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (sprNo != "")
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
                DataTable tblRooms = new DataTable();
                tblRooms.Columns.Add("id");
                tblRooms.Columns.Add("roomName");
                //The following code will fill the room datatable
                string strRoomsQury="SELECT * FROM t_storage_place";
                reader = GeneralClass.Program.gRetrieveRecord(strRoomsQury);
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        tblRooms.Rows.Add(reader["id"].ToString(),reader["storage_place"].ToString());
                    }
                    reader.Close();
                }
                else
                    reader.Close();
                //End of filling the rooms data table

                string strProductQuery = "SELECT t_products.serial_no,t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.allocated_room,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.spr_no='" + sprNo + "' and t_products.vendor=t_vendor.id AND t_products.status_id=8 and t_products.dept_id=" + Session["departmentId"].ToString();
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
				strAllocatedTo = "";
                        if (reader["inspected_by"] != null)
                            strInspectedBy = reader["inspected_by"].ToString();
                        if (reader["approved_by"] != null)
                            strApprovedBy = reader["approved_by"].ToString();
                        if (reader["allocated_to"].ToString() != "")
                            strAllocatedTo = reader["allocated_to"].ToString();
                        else
                            if (reader["allocated_room"].ToString() != "")
                            {
                                for(int i=0;i<tblRooms.Rows.Count;i++)
                                {
                                    if (tblRooms.Rows[i][0].ToString() == reader["allocated_room"].ToString())
                                        strAllocatedTo = tblRooms.Rows[i][1].ToString();
                                }
                              
                            }
                        if (reader["allocated_to"].ToString() != "")
                            assignedItemsCounter++;
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString(), reader["serial_no"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following call will fill the productsGrid use full names
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
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
            mCheckGridStatus();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mRefreshUses(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to refresh the list of users
        /// Author: mutawakelm
        /// Date :4/8/2009 2:43:11 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            pullusers("", "wstaff", "test123");
            DisplayDepartmentList();
            AssignementExtender.Show();
            txtUserId.Text = "";
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSearchUser(object sender, EventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search the selected user by his user id
        /// Author: mutawakelm
        /// Date :4/8/2009 2:43:11 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strUserQuery = "SELECT department_name,full_name,login_name FROM t_LdapUsers WHERE login_name='" + txtUserId.Text+ "'";
            reader = GeneralClass.Program.gRetrieveRecord(strUserQuery);
            if (reader.HasRows)
            {
                reader.Read();
                ddlDepartment.Items.Clear();
                ddlDepartment.Items.Add(reader["department_name"].ToString());
                ddlUsers.Items.Clear();
                ddlUsers.Items.Add(reader["full_name"].ToString());
                ddlUsers.Items[0].Value=reader["login_name"].ToString();
                reader.Close();
                AssignementExtender.Show();
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
