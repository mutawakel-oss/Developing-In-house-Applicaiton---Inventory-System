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
    #region 
    //replcae "mutawakelm" by the session user name in the function "mAddNewDeliveryClicked"
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
                mFillVendorDDL();
                if (Request.QueryString["spr_no"] != null)
                {
                    txtSearchSpr.Text=Request.QueryString["spr_no"].ToString();
                    mSearch();
                }
                mCheckAuthority();
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
         /// Description:
         /// Author: mutawakelm
        /// Date :2/28/2009 11:57:14 AM
         /// Parameter:
         /// input:
         /// output:
         /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (null != Session["reciver"])
                if (Session["reciver"].ToString() == "True")
                {
                    sprProductsGrid.Visible = true;
                }
            if (null != Session["inspector"])
                if (Session["inspector"].ToString() == "True")
                {
                    inspectionPanel.Visible = true;
                }
            if (null != Session["approval"])
                if (Session["approval"].ToString() == "True")
                {
                    pnlApproveItems.Visible = true;
                }
            
        }
        catch(Exception exp)
        {
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
    protected void mClearFields()
    {
        
        //=====================================================//
        //     <summary>
        //  Description:
        //  Author: mutawakelm
        // Date :09/09/2008 11:21:50 AM
        //  Parameter:
        //  input:
        //  output:
        //  Example:
        // <summary>
        //=====================================================//
        try
        {
        
            txtDeliveryDate.Text="";


            txtSearchSpr.Text = "";
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSearchButtonClicked(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will call the search function "mSearch"
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


            mSearch();

        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
            Response.Write(exp.Message.ToString());
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
            //The following query will check if the spr related to the user sprs
            string strQuery = "SELECT dept_id FROM t_spr_data WHERE spr_no=" + txtSearchSpr.Text;
            bool deptRelated = false;
            reader = GeneralClass.Program.gRetrieveRecord(strQuery);
            if (reader.HasRows)
            {
                reader.Read();
                if (reader["dept_id"].ToString() == Session["departmentId"].ToString())
                {
                    deptRelated = true;
                    
                }

                reader.Close();
            }
            else reader.Close();
            if (deptRelated == true)
            {
                mFillGridBySprNo(txtSearchSpr.Text);
                mFillInspectionGrid(txtSearchSpr.Text);
                mFillApprovalGrid(txtSearchSpr.Text);
                //MyAccordion.SelectedIndex = 1;
                mFillSprGridBySprNo(txtSearchSpr.Text);
            }
         
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
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
            TableCell itemCell = e.Item.Cells[10];
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
            //System.Web.UI.WebControls.TextBox gridItemNo = new System.Web.UI.WebControls.TextBox();
            //gridItemNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[1].Controls[0];
            //System.Web.UI.WebControls.TextBox gridDescription = new System.Web.UI.WebControls.TextBox();
            //gridDescription = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            //System.Web.UI.WebControls.TextBox gridCatNo = new System.Web.UI.WebControls.TextBox();
            //gridCatNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
            //System.Web.UI.WebControls.TextBox gridUnitNo = new System.Web.UI.WebControls.TextBox();
            //gridUnitNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[4].Controls[0];
            //System.Web.UI.WebControls.TextBox gridTotal = new System.Web.UI.WebControls.TextBox();
            //gridTotal = (System.Web.UI.WebControls.TextBox)e.Item.Cells[5].Controls[0];
            //System.Web.UI.WebControls.TextBox gridHidden = new System.Web.UI.WebControls.TextBox();
            //gridHidden = (System.Web.UI.WebControls.TextBox)e.Item.Cells[6].Controls[0];
            ////The following code will update the item 
            //GeneralClass.Program.Add("item_no", gridItemNo.Text, "I");
            //GeneralClass.Program.Add("description", gridDescription.Text, "S");
            //GeneralClass.Program.Add("cat_no", gridCatNo.Text, "I");
            //GeneralClass.Program.Add("unit_order", gridUnitNo.Text, "I");
            //GeneralClass.Program.Add("total", gridTotal.Text, "I");
            //GeneralClass.Program.UpdateRecordStatement("t_spr_products", "id", gridHidden.Text);
            //productsGrid.EditItemIndex = -1;
            //mFillGrid();
        }
        catch (Exception exp)
        {
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

            //productsGrid.EditItemIndex = -1;
            //mFillGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
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
                    "where t_products.spr_no=" + spr_n+" AND t_products.vendor=t_vendor.id AND t_products.status_id !=4";
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

                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), strInspectedBy, strApprovedBy, strAllocatedTo, reader["id"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                mGetUserFullName(productsGrid, 6);
                mGetUserFullName(productsGrid, 7);
                mGetUserFullName(productsGrid, 8);
                mGetUserFullName(productsGrid, 9);
                //The following code will display number of items in the data grid
                lblSprItemsCount.Text = productsGrid.Items.Count.ToString();
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();
                lblSprItemsCount.Text = "0";

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
    protected void mFillInspectionGrid(string spr_n)
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
                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("vendor");//This column will hold the vendor
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("rec_date");//This column will hold the date of reciving the item
                productTable.Columns.Add("rec_by");//This column will hold the reciver of the item
                productTable.Columns.Add("hidden");
                productTable.Columns.Add("status");
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name,t_product_status.status " +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor,t_product_status  " +
                    "where t_products.spr_no=" + spr_n + "  AND t_products.vendor=t_vendor.id AND t_products.status_id=t_product_status.id AND (t_products.status_id=2 OR t_products.status_id=3) ";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), reader["id"].ToString(), reader["status"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                inspectionDataGrid.DataSource = productTable;
                inspectionDataGrid.DataBind();
                mGetUserFullName(inspectionDataGrid, 6);
                //The following code will display number of items in the data grid
                lblInsepctedIemsCount.Text = inspectionDataGrid.Items.Count.ToString();
            }
            else
            {
                inspectionDataGrid.DataSource = null;
                inspectionDataGrid.DataBind();
                lblInsepctedIemsCount.Text = "0";
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
    protected void mFillApprovalGrid(string spr_n)
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

                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("vendor");//This column will hold the vendor
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("rec_date");//This column will hold the date of reciving the item
                productTable.Columns.Add("rec_by");//This column will hold the reciver of the item
                productTable.Columns.Add("insp_by");//This column will hold the reciver of the item
                productTable.Columns.Add("hidden");
                productTable.Columns.Add("status");
                string strProductQuery = "SELECT t_products.room,t_vendor.supplier_name,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name,t_product_status.status" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor,t_product_status  " +
                    "where t_products.spr_no=" + spr_n + "  AND t_products.vendor=t_vendor.id AND t_products.status_id=t_product_status.id AND (t_products.status_id=6 OR t_products.status_id=7) ";
                reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {


                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["room"].ToString(), reader["supplier_name"].ToString(), reader["spr_no"].ToString(), reader["delivery_date"].ToString(), reader["recieved_by"].ToString(), reader["inspected_by"].ToString(), reader["id"].ToString(),reader["status"].ToString());
                    }
                    reader.Close();
                }
                else reader.Close();
                ApprovingGrid.DataSource = productTable;
                ApprovingGrid.DataBind();
                mGetUserFullName(ApprovingGrid, 6);
                mGetUserFullName(ApprovingGrid, 7);
                //The following code will display number of items in the data grid
                lblUnapprovedItemsCount.Text = ApprovingGrid.Items.Count.ToString();
            }
            else
            {
                ApprovingGrid.DataSource = null;
                ApprovingGrid.DataBind();
                lblUnapprovedItemsCount.Text = "0";
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
    protected void inpectGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when an inspection has been applied for an item
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
            
            DropDownList statusDDL = (DropDownList)e.Item.FindControl("ddlItemStatus");
            if (statusDDL.SelectedItem.Value!="0")
            {
                GeneralClass.Program.Add("inspected_by", Session["UserID"].ToString(), "S");
            GeneralClass.Program.Add("status_id",statusDDL.SelectedItem.Value , "I");
            GeneralClass.Program.UpdateRecordStatement("t_products", "id", e.Item.Cells[7].Text.ToString());
            mFillInspectionGrid(txtSearchSpr.Text);
            mFillApprovalGrid(txtSearchSpr.Text);
            }
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mInspectAll(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to put the status of the items in the grid "inspectionGrid"
        /// as inspected items
        /// Author: mutawakelm
        /// Date :2/1/2009 4:09:42 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            for (int i = 0; i < inspectionDataGrid.Items.Count; i++)
            {
                GeneralClass.Program.Add("status_id", ddlInspectedItemsStatus.SelectedItem.Value, "I");
                GeneralClass.Program.Add("inspected_by", Session["UserID"].ToString(), "S");
                GeneralClass.Program.UpdateRecordStatement("t_products", "id", inspectionDataGrid.Items[i].Cells[7].Text.ToString());
            }
            mFillInspectionGrid(txtSearchSpr.Text);
            mFillApprovalGrid(txtSearchSpr.Text);

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void approvalGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when an inspection has been applied for an item
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
            DropDownList statusDDL = (DropDownList)e.Item.FindControl("ddlApprovedItemStatus");
            GeneralClass.Program.Add("approved_by", Session["UserID"].ToString(), "S");
            GeneralClass.Program.Add("status_id", statusDDL.SelectedItem.Value, "I");
            GeneralClass.Program.UpdateRecordStatement("t_products", "id", e.Item.Cells[8].Text.ToString());
            mFillApprovalGrid(txtSearchSpr.Text);
            mFillGridBySprNo(txtSearchSpr.Text);
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mApprovAll(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to put the status of the items in the grid "approvalGrid"
        /// as inspected items
        /// Author: mutawakelm
        /// Date :2/1/2009 4:09:42 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            for (int i = 0; i < ApprovingGrid.Items.Count; i++)
            {
                GeneralClass.Program.Add("status_id", ddlApproveAllStatus.SelectedItem.Value, "I");
                GeneralClass.Program.Add("approved_by", Session["UserID"].ToString(), "S");
                GeneralClass.Program.UpdateRecordStatement("t_products", "id", ApprovingGrid.Items[i].Cells[8].Text.ToString());
            }
            mFillApprovalGrid(txtSearchSpr.Text);
            mFillGridBySprNo(txtSearchSpr.Text);
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mInspectGridDisplay(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when an inspection has been applied for an item
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
            TableCell itemCell = e.Item.Cells[7];
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
    protected void mApproveGridDisplay(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used when an inspection has been applied for an item
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

            TableCell itemCell = e.Item.Cells[8];
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

            string strSpecificationQuery = "SELECT t_delivery_item_details.property,t_delivery_item_details.description FROM t_delivery_item_details join t_products on t_delivery_item_details.date_time=t_products.date_time where t_products.id=" + lblItemNumber.Text;
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
    protected void mCallPopupWindow(string URL)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the pop up window
        /// Author: mutawakelm
        /// Date :2/2/2009 9:02:14 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string popupScript = "<script language='javascript'>" +
                 "window.open('" + URL + "', 'CustomPopUp', " +
                "'width=800, height=400, menubar=yes, resizable=no,scrollbars=yes')" +
                 "</script>";

            Page.RegisterStartupScript("PopupScript", popupScript);

        }
        catch (Exception exp)
        {
            
        }

    }
    protected void mFillVendorDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the drop down list of the vendors
        /// Author: mutawakelm
        /// Date :2/2/2009 3:55:52 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strVendorsQuery = "SELECT id,supplier_name FROM t_vendor WHERE dept_id=" + Session["departmentId"].ToString();
            int counter = 1;
            reader = GeneralClass.Program.gRetrieveRecord(strVendorsQuery);
            if (reader.HasRows)
            {

                ddlVendorName.Items.Clear();
                ddlVendorName.Items.Add(" ");
                while (reader.Read())
                {
                    ddlVendorName.Items.Add(reader["supplier_name"].ToString());
                    ddlVendorName.Items[counter].Value = reader["id"].ToString();
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
    protected void mFillSprGridBySprNo(string spr_n)
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
            string strProductQuery = "SELECT pro.*,sub.sub_sub_category_name FROM t_spr_products as pro join t_sut_sub_category as sub on pro.sub_sub_category_id=sub.id  where spr_no=" + txtSearchSpr.Text;
            reader = GeneralClass.Program.gRetrieveRecord(strProductQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    productTable.Rows.Add(txtSearchSpr.Text, counter.ToString(), reader["sub_sub_category_name"].ToString(), reader["cat_no"].ToString(), reader["unit_order"].ToString(), reader["total"].ToString(), reader["id"].ToString());
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();
            sprProductsGrid.DataSource = productTable;
            sprProductsGrid.DataBind();

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
    protected void SprproductsGrid_EditCommand(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to show the specification of the item
        /// Author: mutawakelm
        /// Date :2/25/2009 9:06:29 AM
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
            mFillSprSpecificationGrid();
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mFillSprSpecificationGrid()
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
            string strSpecificationQuery = "SELECT t_delivery_item_details.property,t_delivery_item_details.description FROM t_delivery_item_details join t_spr_products on t_delivery_item_details.date_time=t_spr_products.date_time where t_spr_products.spr_no=" +txtSearchSpr.Text + " AND t_spr_products.id=" + lblItemNumber.Text;
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
    protected void sprProductsGrid_deleteCommand(object sender , DataGridCommandEventArgs e)
    {
        
        //=====================================================//
	        /// <summary>
         /// Description:This function will be used to add a new delivery of the seleced item
         /// Author: mutawakelm
        /// Date :2/25/2009 9:15:19 AM
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
            lblQuantityAmount.Text = e.Item.Cells[3].Text;
            mFillSprProductQuantity();
            newDeliveryExtender.Show();
        }
        catch(Exception exp)
        {
        }
    }
    protected void mFillSprProductQuantity()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to find number of products in stick of the same spr products 
        /// then decide if there is an ability to add a delivery.
        /// Author: mutawakelm
        /// Date :2/25/2009 9:58:15 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            int intApprovedItemsNo = 0;
            int intRemainedNo = 0;
            //The following query will count how many items are approved out of the spr product
            string strApprovedProductsQuery = "SELECT COUNT(id) as approved FROM t_products WHERE spr_product_id=" + lblItemNumber.Text + " and (status_id<4 OR status_id=6 OR status_id=7 OR status_id=8)";
            reader = GeneralClass.Program.gRetrieveRecord(strApprovedProductsQuery);
            if (reader.HasRows)
            {
                reader.Read();
                intApprovedItemsNo = int.Parse(reader["approved"].ToString());
                reader.Close();
            }
            else reader.Close();
            intRemainedNo = int.Parse(lblQuantityAmount.Text)-intApprovedItemsNo;
            //The following code will fill the quantity drop down list
            ddlSprQuantity.Items.Clear();
            ddlSprQuantity.Items.Add(" ");
            for (int i = 1; i <= intRemainedNo; i++)
                ddlSprQuantity.Items.Add(i.ToString());
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
        }
    }
    protected void mAddNewDeliveryClicked(object sender, EventArgs e)
    {

        try
        {
            int strProSubSubCatId = 0;
            string proDateTime = "";
            if ((ddlSprQuantity.Text != " ") && (ddlVendorName.Text != " "))
            {
                //The following code will be used to retrieve details of the product
                string strSprProductQuery = "SELECT pro.sub_sub_category_id,pro.date_time FROM t_spr_products as pro WHERE id=" + lblItemNumber.Text;
                reader = GeneralClass.Program.gRetrieveRecord(strSprProductQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    strProSubSubCatId = int.Parse(reader["sub_sub_category_id"].ToString());
                    proDateTime = reader["date_time"].ToString();
                    reader.Close();
                }
                else reader.Close();
                //OLD CODE
                int intLastSerialNo = 0;
                //The following code will be used to retrieve the last serial number 
                string strLastSerialQuery = "SELECT last_serial_no FROM t_last_serial_no WHERE id=1";
                reader = GeneralClass.Program.gRetrieveRecord(strLastSerialQuery);
                if (reader.HasRows)
                {
                    reader.Read();
                    intLastSerialNo = int.Parse(reader["last_serial_no"].ToString());
                    reader.Close();
                }
                else
                    reader.Close();
                //The following code will be used to store the items details
                for (int i = 0; i < int.Parse(ddlSprQuantity.SelectedItem.Text); i++)
                {
                    GeneralClass.Program.Add("date_time", proDateTime, "D");
                    GeneralClass.Program.Add("sub_sub_cat", strProSubSubCatId.ToString(), "I");
                    GeneralClass.Program.Add("room", txtRoomName.Text, "S");
                    GeneralClass.Program.Add("vendor", ddlVendorName.SelectedItem.Value, "S");
                    GeneralClass.Program.Add("spr_no", txtSearchSpr.Text, "I");
                    GeneralClass.Program.Add("delivery_date", txtDeliveryDate.Text, "S");
                    intLastSerialNo++;
                    GeneralClass.Program.Add("serial_no", "KSAU-HS/COM/P" + intLastSerialNo.ToString(), "S");
                    GeneralClass.Program.Add("delivery_date", txtDeliveryDate.Text, "S");
                    GeneralClass.Program.Add("recieved_by", Session["UserID"].ToString(), "S");
                    GeneralClass.Program.Add("spr_product_id", lblItemNumber.Text, "I");
                    GeneralClass.Program.Add("status_id", "3", "I");
                    GeneralClass.Program.Add("dept_id", Session["departmentId"].ToString(), "I");
                    GeneralClass.Program.InsertRecordStatement("t_products");
                }
                //The following code will be used to update the last serial number table
                GeneralClass.Program.Add("last_serial_no", intLastSerialNo.ToString(), "I");
                GeneralClass.Program.UpdateRecordStatement("t_last_serial_no", "id", "1");
                //The following code will fill the spr items grid "productsGrid"
                //txtSearchSpr.Text = lblSprNo1.Text;
                //mFillGridBySprNo(ddlSprNo.Text);
                //mFillInspectionGrid(ddlSprNo.Text);
                //mFillApprovalGrid(ddlSprNo.Text);
                //addNewDeliveredItemsPnl.Visible = false;
                //********OLD CODE
                lblNewItemError.Visible = false;
                //The following calls will fill the data grids
                mFillGridBySprNo(txtSearchSpr.Text);
                mFillInspectionGrid(txtSearchSpr.Text);
                mFillApprovalGrid(txtSearchSpr.Text);
                MyAccordion.SelectedIndex = 1;
            }
            else
            {
                lblNewItemError.Visible = true;
                lblNewItemError.Text = "You should fill all fields.";
                newDeliveryExtender.Show();

            }
            
        }
        catch (Exception exp)
        {
            if (reader != null)
                reader.Close();
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
            TableCell itemCell = e.Item.Cells[10];
            TableCell appvoeCell = e.Item.Cells[8];
            string item = itemCell.Text;
            string approveCellText = appvoeCell.Text;
            string spr_no = "";//This variable will hold the SPR number 
            if (approveCellText == "&nbsp;")
            {
                GeneralClass.Program.gDeleteRecord("DELETE FROM t_products WHERE id=" + item);
                spr_no = txtSearchSpr.Text;
                mFillGridBySprNo(spr_no);
                mFillInspectionGrid(spr_no);
                mFillApprovalGrid(spr_no);
            }


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mProductGridPageIndex(object sender, DataGridPageChangedEventArgs e)
    {
        try
        {

            productsGrid.CurrentPageIndex = e.NewPageIndex;
            mFillGridBySprNo(txtSearchSpr.Text);


        }
        catch (Exception exp)
        {
        }
    }
    protected void mInspectionGridPageIndex(object sender, DataGridPageChangedEventArgs e)
    {
        try
        {

            inspectionDataGrid.CurrentPageIndex = e.NewPageIndex;
            mFillInspectionGrid(txtSearchSpr.Text);


        }
        catch (Exception exp)
        {
        }
    }
    protected void mApproveGridPageIndex(object sender, DataGridPageChangedEventArgs e)
    {
        try
        {

            ApprovingGrid.CurrentPageIndex = e.NewPageIndex;
            mFillApprovalGrid(txtSearchSpr.Text);


        }
        catch (Exception exp)
        {
        }
    }
}
    