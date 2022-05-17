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
                mFillDepartmentDDL();//This call will fill the department drop downn list.
                mFillCategoryDDL();
                mLastSprReferenceNo();
                mFillRequesterDefaultValues();
                btnPrintSpr.Visible = false;
                btnAddDeliveryForSpr.Visible = false;
            }
            MyAccordion.Width = 800;
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
            if (null != Session["spr_creatror"])
            {
                if (Session["spr_creatror"].ToString() == "True")
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
    protected void mFillRequesterDefaultValues()
    {
        try
        {
            //=====================================================//
            /// <summary>
            /// Description:This function will be used to fill the requestors of spr for the user department
            /// Author: mutawakelm
            /// Date :09/08/2008 11:00:00 AM
            /// Parameter:
            /// input:
            /// output:
            /// Example:
            /// <summary>
            //=====================================================//
            int counter=0;
            string strReqQuery = "SELECT * FROM t_requestors_table WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strReqQuery);
            if (reader.HasRows)
            {
                 ddlRequesterName.Items.Clear();
                while (reader.Read())
                {
                    ddlRequesterName.Items.Add(reader["name"].ToString());
                    ddlRequesterName.Items[counter].Value = reader["user_id"].ToString();
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
    protected void mFillGrid()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of the new spr product
        /// Author: mutawakelm
        /// Date :09/08/2008 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable productTable = new DataTable();
            int counter = 1;
            productTable.Columns.Add("spr_no");//This column will hold the number of spr request
            productTable.Columns.Add("item_no");//This column will hold the name number of item
            productTable.Columns.Add("description");//This column will hold the description
            productTable.Columns.Add("cat_no");//This column will hold the number of the catalog
            productTable.Columns.Add("unit_order");//This column will hold the number of the order
            productTable.Columns.Add("total");//This column will hold the total number
            productTable.Columns.Add("hidden");
            string strProductQuery = "SELECT pro.*,sub.sub_sub_category_name FROM t_spr_products as pro join t_sut_sub_category as sub on pro.sub_sub_category_id=sub.id  where spr_no=" + this.txtSprNo.Text;
            reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productTable.Rows.Add(this.txtSprNo.Text, counter.ToString(), reader["sub_sub_category_name"].ToString(), reader["cat_no"].ToString(), reader["unit_order"].ToString(), reader["total"].ToString(), reader["id"].ToString());
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
            productsGrid.DataSource = productTable;
            productsGrid.DataBind();

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
    protected void mFillGridBySprNo(string spr_n)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of the new spr product
        /// Author: mutawakelm
        /// Date :09/08/2008 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable productTable = new DataTable();
            int counter = 1;
            productTable.Columns.Add("spr_no");//This column will hold the number of spr request
            productTable.Columns.Add("item_no");//This column will hold the name number of item
            productTable.Columns.Add("description");//This column will hold the description
            productTable.Columns.Add("cat_no");//This column will hold the number of the catalog
            productTable.Columns.Add("unit_order");//This column will hold the number of the order
            productTable.Columns.Add("total");//This column will hold the total number
            productTable.Columns.Add("hidden");
            string strProductQuery = "SELECT pro.*,sub.sub_sub_category_name FROM t_spr_products as pro join t_sut_sub_category as sub on pro.sub_sub_category_id=sub.id  where spr_no=" + spr_n;
            reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productTable.Rows.Add(this.txtSprNo.Text, counter.ToString(), reader["sub_sub_category_name"].ToString(), reader["cat_no"].ToString(), reader["unit_order"].ToString(), reader["total"].ToString(), reader["id"].ToString());
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
            productsGrid.DataSource = productTable;
            productsGrid.DataBind();

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
    protected void mFillDepartmentDDL()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to departments drop down list.
        /// Author: mutawakelm
        /// Date :09/08/2008 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strDepartmentQuery = "SELECT * FROM t_department_master";
            reader = GeneralClass.Program.gRetrieveRecord(strDepartmentQuery);
            ddlDepartments.Items.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    ddlDepartments.Items.Add(reader["department_name"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            //Response.Write(exp.Message.ToString());
        }
    }
    protected void mGetLdapUsers()
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to get all the users of the LDAP.
        /// Author: mutawakelm
        /// Date :09/08/2008 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", "wstaff", "test123");//OU=staff,OU=collegeusers,OU=mis                                
            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();

            string strFullName;
            string strLoginName;

            DirectorySearcher dSearch = new DirectorySearcher(entry1);

            dSearch.Filter = "(&(objectCategory=user)(cn=*))";

            foreach (SearchResult sResultSet in dSearch.FindAll())
            {

                strFullName = GetProperty(sResultSet, "Name");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                if ("" != strLoginName.Trim())
                    if (strFullName != string.Empty)
                    {
                        ListItem li = new ListItem();
                        li.Value = strLoginName;
                        li.Text = strFullName;
                        ddlRequesterName.Items.Add(li);
                        
                    }
            }

        }
        catch (Exception exp)
        {
            
        }
    }
    protected void mCheckBoxChanged(object sender, EventArgs e)
    {
        try
        {
            if (requesterChkBox.Checked == true)
            {
                mGetLdapUsers();
            }
            else
            {
            }
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected string GetProperty(SearchResult searchResult, string PropertyName)
    {
        try
        {
            if (searchResult.Properties.Contains(PropertyName))
                return searchResult.Properties[PropertyName][0].ToString();
            else
                return string.Empty;
        }
        catch (Exception exp)
        {
            return string.Empty;
        }
    }
    protected void mAddProduct(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a new product to the grid "productsGrid"
        /// Author: mutawakelm
        /// Date :09/08/2008 11:00:00 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mSaveRequest(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to save the information of the request to the table ""
        /// Author: mutawakelm
        /// Date :10/08/2008 10:50:30 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            //The following code will be used to check the availability of the spr no in the database
            //if the spr is available then it will updte the spr data
            //if the spr is not available then it will insert it from the begining
            string strSprExQuery = "SELECT spr_no FROM t_spr_data WHERE spr_no="+this.txtSprNo.Text;
            reader = GeneralClass.Program.gRetrieveRecord(strSprExQuery);
            if (reader.HasRows)
            {
                reader.Close();
                GeneralClass.Program.Add("spr_no", txtSprNo.Text, "S");
                GeneralClass.Program.Add("department", ddlDepartments.Text, "S");
                GeneralClass.Program.Add("delivary_date", txtDeliveryDate.Text, "S");
                GeneralClass.Program.Add("delivary_date", txtDeliveryDate.Text, "S");
                GeneralClass.Program.Add("request_date", txtRequestDate.Text, "D");
                if(txtPoNo.Text!="")
                GeneralClass.Program.Add("po_no", txtPoNo.Text, "S");
                GeneralClass.Program.Add("cost_code", txtCostCode.Text, "S");
                GeneralClass.Program.Add("justification", txtJustification.Text, "S");
                if(txtActualSpr.Text!="")
                GeneralClass.Program.Add("actual_spr_no", txtActualSpr.Text, "S");
                GeneralClass.Program.UpdateRecordStatement("t_spr_data", "spr_no", txtSprNo.Text.ToString());
                btnPrintSpr.Visible = true;
                btnAddDeliveryForSpr.Visible =false;
                mSearchByHiddenSpr();
            }
            else
            {
                reader.Close();
                GeneralClass.Program.Add("spr_no", txtSprNo.Text, "S");
                GeneralClass.Program.Add("department", ddlDepartments.Text, "S");
                GeneralClass.Program.Add("delivary_date", txtDeliveryDate.Text, "S");
                GeneralClass.Program.Add("request_date", txtRequestDate.Text, "D");
                GeneralClass.Program.Add("requester", ddlRequesterName.SelectedItem.Value.ToString().Trim(), "S");
                if(txtPoNo.Text!="")
                GeneralClass.Program.Add("po_no", txtPoNo.Text, "S");
                GeneralClass.Program.Add("cost_code", txtCostCode.Text, "S");
                GeneralClass.Program.Add("justification", txtJustification.Text, "S");
                if(txtActualSpr.Text!="")
                GeneralClass.Program.Add("actual_spr_no", txtActualSpr.Text, "S");
                GeneralClass.Program.Add("dept_id", Session["departmentId"].ToString(), "I");
                GeneralClass.Program.InsertRecordStatement("t_spr_data");
                btnPrintSpr.Visible = true;
                btnAddDeliveryForSpr.Visible =false;
                mSearchByHiddenSpr();
            }

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSearchBtnClicked(object sender, EventArgs e)
    {
        try
        {
            mSearch();
        }
        catch (Exception exp)
        {
        }
       
    }
    protected void mSearch()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search about a spr information
        /// and display the data on the fields
        /// Author: mutawakelm
        /// Date :10/08/2008 03:13:50 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strSprNo = "";//This variable will hold the number of the spr
            //The following code will be used to search about the information 
            if (!string.IsNullOrEmpty(txtSearchSpr.SelectedItem.Value))
            {
                string strSearchQuery = "SELECT spr.*,us.full_name from t_spr_data as spr,t_users as us WHERE spr_no=" + txtSearchSpr.SelectedItem.Value + " AND us.id=spr.requester";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
                if (reader.HasRows)
                {
                    ddlRequesterName.Items.Clear();
                    reader.Read();
                    ddlRequesterName.Items.Clear();
                    //The following segment will fill the fields by spr data
                    txtSprNo.Text = reader["spr_no"].ToString();
                    txtActualSpr.Text = reader["actual_spr_no"].ToString();
                    txtPoNo.Text = reader["po_no"].ToString();
                    strSprNo = reader["spr_no"].ToString();
                    ddlDepartments.Text = reader["department"].ToString();
                    txtDeliveryDate.Text = reader["delivary_date"].ToString();
                    ddlRequesterName.Items.Add(reader["full_name"].ToString());
                    ddlRequesterName.Items[0].Value = reader["requester"].ToString();
                    txtCostCode.Text = reader["cost_code"].ToString();
                    txtJustification.Text = reader["justification"].ToString();
                    txtRequestDate.Text = reader["request_date"].ToString();
                    reader.Close();
                    mFillGridBySprNo(strSprNo);
                    MyAccordion.SelectedIndex = 1;
                    btnDisplaySprStatus.Visible = true;
                    txtSprNo.Enabled = false;
                    btnPrintSpr.Visible = true;
                    btnAddDeliveryForSpr.Visible = false;
                }
                else
                {
                    reader.Close();
                    mClear();
                }

            }


        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSearchByHiddenSpr()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search about a spr information
        /// and display the data on the fields
        /// Author: mutawakelm
        /// Date :10/08/2008 03:13:50 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strSprNo = "";//This variable will hold the number of the spr
            //The following code will be used to search about the information 
            if (!string.IsNullOrEmpty(txtSprNo.Text))
            {
                string strSearchQuery = "SELECT spr.*,us.full_name from t_spr_data as spr,t_users as us WHERE spr_no=" + txtSprNo.Text + " AND us.id=spr.requester";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
                if (reader.HasRows)
                {
                    ddlRequesterName.Items.Clear();
                    reader.Read();
                    ddlRequesterName.Items.Clear();
                    //The following segment will fill the fields by spr data
                    txtSprNo.Text = reader["spr_no"].ToString();
                    txtActualSpr.Text = reader["actual_spr_no"].ToString();
                    txtPoNo.Text = reader["po_no"].ToString();
                    strSprNo = reader["spr_no"].ToString();
                    ddlDepartments.Text = reader["department"].ToString();
                    txtDeliveryDate.Text = reader["delivary_date"].ToString();
                    ddlRequesterName.Items.Add(reader["full_name"].ToString());
                    ddlRequesterName.Items[0].Value = reader["requester"].ToString();
                    txtCostCode.Text = reader["cost_code"].ToString();
                    txtJustification.Text = reader["justification"].ToString();
                    txtRequestDate.Text = reader["request_date"].ToString();
                    reader.Close();
                    mFillGridBySprNo(strSprNo);
                    MyAccordion.SelectedIndex = 1;
                    btnDisplaySprStatus.Visible = true;
                    txtSprNo.Enabled = false;
                    btnPrintSpr.Visible = true;
                    btnAddDeliveryForSpr.Visible = false;
                }
                else
                {
                    reader.Close();
                    mClear();
                }

            }


        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mBtnClearClick(object sneder, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the function "mClear" which is responsible to clear the fields of the page
        /// Author: mutawakelm
        /// Date :12/08/2008 03:51:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mClear();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mClear()
    {

        //=====================================================//
        /// <summary>
        /// Description:The following function will be used to clear all the fields of the 
        /// page to start a new request
        /// Author: mutawakelm
        /// Date :10/08/2008 04:38:11 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
       
        try
        {
            this.txtSprNo.Text = "";
            this.ddlDepartments.SelectedIndex=0;
            this.txtDeliveryDate.Text = "";
            txt_cat_no.Text = "";
            txt_unit_no.Text = "";
            txtPoNo.Text = "";
            txtCostCode.Text = "";
            btnSubmitItems.Visible = false;
            ddlRequesterName.SelectedIndex =0;
            //The following code will be used to clear the grid
            //productsGrid.ClearPreviousDataSource();
            productsGrid.DataSource = null;
            productsGrid.DataBind();
            btnDisplaySprStatus.Visible = false;
            txtSprNo.Enabled = false;
            txtActualSpr.Text = "";
            txtJustification.Text = "";
            txtRequestDate.Text = "";
            btnPrintSpr.Visible = false;
            btnAddDeliveryForSpr.Visible = false;
            mFillRequesterDefaultValues();
            mLastSprReferenceNo();
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mLastSprReferenceNo()
    {
        int intLastNo = 0;
        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT spr_no FROM t_random_spr WHERE id=1");
            if (reader.HasRows)
            {
                reader.Read();
                intLastNo = int.Parse(reader["spr_no"].ToString());
                txtSprNo.Text = intLastNo.ToString();
                reader.Close();
            }
            else reader.Close();
            //The following code will be used to update the last spr_no
            intLastNo++;
            GeneralClass.Program.Add("spr_no", intLastNo.ToString(), "I");
            GeneralClass.Program.UpdateRecordStatement("t_random_spr", "id", "1");
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void productsGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update the contents of the datagrid "productsGrid" after editing
        /// Author: mutawakelm
        /// Date :13/08/2008 10:41:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            System.Web.UI.WebControls.TextBox gridItemNo = new System.Web.UI.WebControls.TextBox();
            gridItemNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0];
            System.Web.UI.WebControls.TextBox gridDescription = new System.Web.UI.WebControls.TextBox();
            gridDescription = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox gridCatNo = new System.Web.UI.WebControls.TextBox();
            gridCatNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
            System.Web.UI.WebControls.TextBox gridUnitNo = new System.Web.UI.WebControls.TextBox();
            gridUnitNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[4].Controls[0];
            System.Web.UI.WebControls.TextBox gridTotal = new System.Web.UI.WebControls.TextBox();
            gridTotal = (System.Web.UI.WebControls.TextBox)e.Item.Cells[5].Controls[0]; 
            System.Web.UI.WebControls.TextBox gridHidden = new System.Web.UI.WebControls.TextBox();
            gridHidden = (System.Web.UI.WebControls.TextBox)e.Item.Cells[6].Controls[0]; 
            //The following code will update the item 
            GeneralClass.Program.Add("item_no", gridItemNo.Text, "I");
            GeneralClass.Program.Add("description", gridDescription.Text, "S");
            GeneralClass.Program.Add("cat_no", gridCatNo.Text, "I");
            GeneralClass.Program.Add("unit_order", gridUnitNo.Text, "I");
            GeneralClass.Program.Add("total", gridTotal.Text, "I");
            GeneralClass.Program.UpdateRecordStatement("t_spr_products", "id",gridHidden.Text);
            productsGrid.EditItemIndex = -1;
            mFillGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void DataGrid1_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to delete a row in the dataGrid "productsGrid"
        /// Author: mutawakelm
        /// Date :13/08/2008 10:41:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
             TableCell itemCell = e.Item.Cells[6];
             string item = itemCell.Text;
             string dateTime = "";
            //The following query will determine the date_time of the seleced item
             string strDateTimeQuery = "SELECT date_time FROM t_spr_products  WHERE id="+ item;
             reader = GeneralClass.Program.gRetrieveRecord(strDateTimeQuery);
             if (reader.HasRows)
             {
                 reader.Read();
                 dateTime = reader["date_time"].ToString();
                 reader.Close();
             }
             else reader.Close();
             GeneralClass.Program.gDeleteRecord("DELETE FROM t_spr_products WHERE id=" + item);
             GeneralClass.Program.gDeleteRecord("DELETE FROM t_delivery_item_details WHERE date_time='" +dateTime+"'");
             mFillGrid();
            
            
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
        }
    }
    protected void productGrid_CancelCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to cancel the changes on the grid
        /// Author: mutawakelm
        /// Date :16/08/2008 09:23:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            productsGrid.EditItemIndex = -1;
            mFillGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mFillCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the category drop down list
        /// Author: mutawakelm
        /// Date :16/08/2008 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            int counter = 1;
            string strCategoryQuery = "SELECT id,category_name FROM t_category WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
            if (reader.HasRows)
            {
                ddlCategory.Items.Clear();
                ddlCategory.Items.Add("--Select category--");
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
    protected void mSubCategoryDDLselected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the filling drop down list function
        /// Author: mutawakelm
        /// Date :16/08/2008 03:44:13 PM
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
    protected void mFillSubCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the category drop down list
        /// Author: mutawakelm
        /// Date :16/08/2008 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            int counter = 0;
            string strSubSub = "";//The following variable will hold 40 characters of the sub sub category string
            string strsubCategoryQuery = "SELECT id,sub_cateogry_name FROM t_sub_catgory where category_id=" + ddlCategory.Items[ddlCategory.SelectedIndex].Value;
            reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
            ddlSubCategory.Items.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["sub_cateogry_name"].ToString().Length > 40)
                        strSubSub = reader["sub_cateogry_name"].ToString().Substring(0, 40).ToString()+"...";
                    else
                        strSubSub = reader["sub_cateogry_name"].ToString();
                    ddlSubCategory.Items.Add(strSubSub);
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
        /// Date :27/08/2008 02:14:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            int counter = 0;
            string strSubSubCategory = "";
            ddlSubSubCategory.Items.Clear();
            if (!string.IsNullOrEmpty(ddlSubCategory.Text))
            {
                string strsubCategoryQuery = "SELECT id,sub_sub_category_name FROM t_sut_sub_category WHERE sub_category_id=" + ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value;
                reader = GeneralClass.Program.gRetrieveRecord(strsubCategoryQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (reader["sub_sub_category_name"].ToString().Length > 40)
                            strSubSubCategory = reader["sub_sub_category_name"].ToString().Substring(0, 40).ToString()+"...";
                        else
                            strSubSubCategory = reader["sub_sub_category_name"].ToString();
                        ddlSubSubCategory.Items.Add(strSubSubCategory);
                        ddlSubSubCategory.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else reader.Close();
            }
            newItemAdingExtender.Show();

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
    protected void mSubSubCategoryDDLselected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub sub category drop down list
        /// Author: mutawakelm
        /// Date :27/08/2008 03:44:16 PM
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
    protected void mBtnSpecificationClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the function "mPutSpecification" which will fill the grid
        /// Author: mutawakelm
        /// Date :01/09/2008 02:42:52 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            //mPutSpecification();
            subSubCategoryDetails.SelectCommand = "SELECT detials_field_name FROM t_sub_sub_category_detail where sub_sub_category_id=" + ddlSubSubCategory.Items[ddlSubSubCategory.SelectedIndex].Value + " order by id";
            btnSubmitItems.Visible = true;
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void btnSubmitItems_Click(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to submit the properties of the delivery item
        /// it will save the items and the properties of those items
        /// Author: mutawakelm
        /// Date :06/09/2008 10:35:18 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DateTime nowDateTime = DateTime.Now;//This variable will hold the date and time of now
            double total = 0.0;//This variable will hold the total of the selected item
            //The following code will be used to store the property attributes
            foreach (DataGridItem dg in deliveryDetialsGrid.Items)
            {
                Label propertyTitle = (Label)dg.FindControl("lblPropertyTitle");
                TextBox propertyDesctiption = (TextBox)dg.FindControl("txtPropertyDescription");
                if (propertyDesctiption.Text != null)
                {
                    GeneralClass.Program.Add("date_time", nowDateTime.ToString(), "S");
                    GeneralClass.Program.Add("property", propertyTitle.Text, "S");
                    GeneralClass.Program.Add("description", propertyDesctiption.Text, "S");
                    GeneralClass.Program.InsertRecordStatement("t_delivery_item_details");
                }
            }
            deliveryDetialsGrid.DataSource = null;
            deliveryDetialsGrid.DataBind();
            btnSubmitItems.Visible = false;
            
            //The following code will be used to add the product information to the table "t_spr_producs"
            GeneralClass.Program.Add("spr_no", txtSprNo.Text, "I");
            GeneralClass.Program.Add("cat_no", txt_cat_no.Text, "I");
            GeneralClass.Program.Add("sub_sub_category_id", ddlSubSubCategory.SelectedItem.Value, "I");
            GeneralClass.Program.Add("unit_order", txt_unit_no.Text, "I");
            total = double.Parse(txt_cat_no.Text) * double.Parse(txt_unit_no.Text);
            GeneralClass.Program.Add("total", total.ToString(), "S");
            GeneralClass.Program.Add("date_time", nowDateTime.ToString(), "D");
            GeneralClass.Program.Add("cat", txtCatalogNo.Text, "S");
            GeneralClass.Program.Add("unit_of_order", txtUnitOfOrder.Text, "S");
            GeneralClass.Program.InsertRecordStatement("t_spr_products");
            mFillGrid();//This call to fill the grid with the reqired spr number

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void productsGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to 
        /// Author: mutawakelm
        /// Date :1/2/2009 10:41:10 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            TableCell itemCell = e.Item.Cells[6];
            string assignedToText = itemCell.Text;
            lblItemNumber.Text = assignedToText;
            mFillSpecificationGrid();
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
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
            string strSpecificationQuery = "SELECT t_delivery_item_details.property,t_delivery_item_details.description FROM t_delivery_item_details join t_spr_products on t_delivery_item_details.date_time=t_spr_products.date_time where t_spr_products.spr_no=" + txtSprNo.Text + " AND t_spr_products.id="+lblItemNumber.Text;
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
    protected void mAddNewItemClicked(object sender, EventArgs e)
    {
        try
        {
            newItemAdingExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mSprStatusBtnClicked(object sender, EventArgs e)
    {
        try
        {
            mFillSprStatusGrid();
            sprStatusPanelExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillSprStatusGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the spr status grid
        /// Author: mutawakelm
        /// Date :2/23/2009 12:12:20 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable tblSprStatus = new DataTable();
            string strSprStatusQuery = "SELECT spr.*,us.full_name FROM t_spr_status as spr,t_users as us where spr_no=" +txtSprNo.Text + " AND spr.reported_by=us.id";
            tblSprStatus.Columns.Add("date");
            tblSprStatus.Columns.Add("progress");
            tblSprStatus.Columns.Add("reported_by");
            //The following code will be used to fill the spr status grid
            reader = GeneralClass.Program.gRetrieveRecord(strSprStatusQuery);
            if (reader.HasRows)
            {
                txtSprStatus.Text = "";
                while (reader.Read())
                {
                    tblSprStatus.Rows.Add(reader["date_time"].ToString(), reader["report"].ToString(), reader["full_name"].ToString());
                }
                reader.Close();
            }
            else reader.Close();
            sprStatusGrid.DataSource = tblSprStatus;
            sprStatusGrid.DataBind();
            
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mAddStatusClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a new status of for the selected spr into the table "t_spr_status"
        /// Author: mutawakelm
        /// Date :2/24/2009 12:20:11 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("spr_no", txtSprNo.Text, "I");
            GeneralClass.Program.Add("date_time", DateTime.Now.ToString(), "D");
            GeneralClass.Program.Add("report", txtSprStatus.Text, "S");
            GeneralClass.Program.Add("reported_by", Session["UserID"].ToString(), "S");
            int returnID=GeneralClass.Program.InsertRecordStatement("t_spr_status");
        }
        catch (Exception exp)
        {

        }
    }
    protected void mSearchBtnClicked1(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to search about spr according to the start and end date that selected
        /// Author: mutawakelm
        /// Date :2/25/2009 3:24:09 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int counter = 1;
            string strJustif = "";
            //The following condition will be exectued if no justification 
            if ((txtStartingDate.Text != "") || (txtEndingDate.Text != ""))
            {
                string strSearchByDatesQuery = "SELECT justification,spr_no FROM t_spr_data WHERE (request_date>='" + txtStartingDate.Text + "' and request_date<='" + txtEndingDate.Text + "') AND justification like '%" + txtJustificationPattern.Text + "%' AND dept_id=" + Session["departmentId"].ToString() + "  order by request_date";
                reader = GeneralClass.Program.gRetrieveRecord(strSearchByDatesQuery);
                if (reader.HasRows)
                {
                    txtSearchSpr.Items.Clear();
                    txtSearchSpr.Items.Add("--Select SPR--");
                    txtSearchSpr.Items[0].Value = "0";
                    while (reader.Read())
                    {
                        if (reader["justification"].ToString().Length > 80)
                            strJustif = reader["justification"].ToString().Substring(0, 80).ToString() + "...";
                        else
                            strJustif = reader["justification"].ToString();

                        txtSearchSpr.Items.Add(strJustif);
                        txtSearchSpr.Items[counter].Value = reader["spr_no"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else reader.Close();
            }
            else
            {
                if ((txtStartingDate.Text == "") || (txtEndingDate.Text == ""))
                {
                    string strSearchByDatesQuery = "SELECT justification,spr_no FROM t_spr_data WHERE  justification like '%" + txtJustificationPattern.Text + "%' AND dept_id=" + Session["departmentId"].ToString() + "  order by request_date";
                    reader = GeneralClass.Program.gRetrieveRecord(strSearchByDatesQuery);
                    if (reader.HasRows)
                    {
                        txtSearchSpr.Items.Clear();
                        txtSearchSpr.Items.Add("--Select SPR--");
                        txtSearchSpr.Items[0].Value = "0";
                        while (reader.Read())
                        {
                            if (reader["justification"].ToString().Length > 80)
                                strJustif = reader["justification"].ToString().Substring(0, 80).ToString() + "...";
                            else
                                strJustif = reader["justification"].ToString();
                            txtSearchSpr.Items.Add(strJustif);
                            txtSearchSpr.Items[counter].Value = reader["spr_no"].ToString();
                            
                            counter++;
                        }
                        reader.Close();
                    }
                    else reader.Close();
                }

            }
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mAddDeliveredItemClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to add a new delivered items for the displayed spr
        /// Author: mutawakelm
        /// Date :2/25/2009 4:41:07 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/delivery_page.aspx?spr_no=" + txtSprNo.Text);
        }
        catch (Exception exp)
        {
        }
    }
    protected void mPrintBtnClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the spr reporting page "spr_report_page.aspx"
        /// Author: mutawakelm
        /// Date :3/2/2009 8:43:35 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            Response.Redirect("~/spr_report_page.aspx?spr_no=" + txtSearchSpr.SelectedItem.Value);
        }
        catch (Exception exp)
        {
        }
    }
 
}
