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
using System.Collections;
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
            if (!IsPostBack)
            {
                mFillCategoryGrid();
                mFillParentCategoryDDL();
                mFillSubCategoryGrid();
                mFillSubCategoryDDL();
                mFillSubSubCategoryGrid();
                mFillSubSubCategoryDDL();
                mFillSubSubCategoryDetailsGrid();
                MyAccordion.SelectedIndex = 0;
            }
            MyAccordion.Width = 800;
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/admin_default_page.aspx";

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());

        }
    }
    protected void mFillCategoryGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of the 
        /// Author: mutawakelm
        /// Date :18/08/2008 11:21:05 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            DataTable categoryDataTable = new DataTable();
            categoryDataTable.Columns.Add("cat_id");
            categoryDataTable.Columns.Add("cat_name");
            categoryDataTable.Columns.Add("description");
            string strCategoryQuery = "SELECT * FROM t_category WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    categoryDataTable.Rows.Add(reader["id"].ToString(), reader["category_name"].ToString(), reader["description"].ToString());
                }
                reader.Close();
                CategoryGrid.DataSource = categoryDataTable;
                CategoryGrid.DataBind();
            }
            else reader.Close();
       
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


            //string strSprExQuery = "SELECT spr_no FROM t_spr_data WHERE spr_no="+this.txtSprNo.Text;
            //reader = GeneralClass.Program.gRetrieveRecord(strSprExQuery);
            //if (reader.HasRows)
            //{
            //    reader.Close();
            //    GeneralClass.Program.Add("spr_no", txtSprNo.Text, "I");
            //    GeneralClass.Program.Add("department", ddlDepartments.Text, "S");
            //    GeneralClass.Program.Add("delivary_date", txtDeliveryDate.Text, "S");
            //    GeneralClass.Program.Add("requester", ddlRequesterName.Text, "S");
            //    GeneralClass.Program.UpdateRecordStatement("t_spr_data", "spr_no", txtSprNo.Text.ToString());               
            //    Response.Redirect("product_request_page.aspx");

            //}
            //else
            //{
            //    reader.Close();
            //    GeneralClass.Program.Add("spr_no", txtSprNo.Text, "I");
            //    GeneralClass.Program.Add("department", ddlDepartments.Text, "S");
            //    GeneralClass.Program.Add("delivary_date", txtDeliveryDate.Text, "S");
            //    GeneralClass.Program.Add("requester", ddlRequesterName.Text, "S");
            //    GeneralClass.Program.InsertRecordStatement("t_spr_data");
            //    Response.Redirect("product_request_page.aspx");
            //}


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSearch(object sender, EventArgs e)
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
            //    string strSprNo = "";//This variable will hold the number of the spr
            //    //The following code will be used to search about the information 
            //    if (!string.IsNullOrEmpty(txtSearchSpr.Text))
            //    {
            //        string strSearchQuery = "SELECT * from t_spr_data WHERE spr_no="+txtSearchSpr.Text;
            //        reader = GeneralClass.Program.gRetrieveRecord(strSearchQuery);
            //        if (reader.HasRows)
            //        {
            //            reader.Read();
            //            ddlRequesterName.Items.Clear();
            //            //The following segment will fill the fields by spr data
            //            txtSprNo.Text = reader["spr_no"].ToString();
            //            strSprNo = reader["spr_no"].ToString();
            //            ddlDepartments.Text = reader["department"].ToString();
            //            txtDeliveryDate.Text = reader["delivary_date"].ToString();
            //            ddlRequesterName.Items.Add(reader["requester"].ToString());
            //            reader.Close();
            //            mFillGridBySprNo(strSprNo);
            //            MyAccordion.SelectedIndex = 1;
            //        }
            //        else
            //        {
            //            reader.Close();
            //            mClear();
            //        }
            //    }

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
            //this.txtSprNo.Text = "";
            //this.ddlDepartments.SelectedIndex=0;
            //this.txtDeliveryDate.Text = "";
            //txtItemNo.Text = "";
            //txtDescription.Text = "";
            //txt_cat_no.Text = "";
            //txt_unit_no.Text = "";
            //txtTotal.Text = "";
            //ddlRequesterName.SelectedIndex =0;
            ////The following code will be used to clear the grid
            ////productsGrid.ClearPreviousDataSource();
            //productsGrid.DataSource = null;
            //productsGrid.DataBind();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void categoryGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to edit the contents of the datagrid "categoryGrid"
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
            TableCell itemCell = e.Item.Cells[1];
            lblSelectedId.Text = itemCell.Text;
            CategoryGrid.EditItemIndex = e.Item.ItemIndex;
            mFillCategoryGrid();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void categoryGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
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
            System.Web.UI.WebControls.TextBox categoryName1 = new System.Web.UI.WebControls.TextBox();
            categoryName1 = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox categoryDescription1 = new System.Web.UI.WebControls.TextBox();
            categoryDescription1 = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];

            //The following code will update the item 
            GeneralClass.Program.Add("category_name", categoryName1.Text, "S");
            GeneralClass.Program.Add("description", categoryDescription1.Text, "S");
            GeneralClass.Program.UpdateRecordStatement("t_category", "id", lblSelectedId.Text);
            CategoryGrid.EditItemIndex = -1;
            mFillCategoryGrid();
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
        /// Date :18/08/2008 09:23:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            CategoryGrid.EditItemIndex = -1;
            mFillCategoryGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mAddNewCategory(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This fucntion will be used to add a new category details in the table "t_category"
        /// Author: mutawakelm
        /// Date :18/08/2008 01:02:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("category_name", txtCategoryName.Text, "S");
            GeneralClass.Program.Add("description", txtDescription.Text, "S");
            GeneralClass.Program.Add("dept_id", Session["departmentId"].ToString(), "I");
            GeneralClass.Program.InsertRecordStatement("t_category");
            txtCategoryName.Text = "";
            txtDescription.Text = "";
            mFillCategoryGrid();
            mFillParentCategoryDDL();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillParentCategoryDDL()
    {
        try
        {

            int counter = 1;
            string strCategoryQuery = "SELECT id,category_name FROM t_category WHERE dept_id=" + Session["departmentId"].ToString();
            reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
            if (reader.HasRows)
            {
                ddlParentCategory.Items.Clear();
                ddlParentCategory.Items.Add("--Select--");
                ddlParentCategory.Items[0].Value = "0";
                ddlParentParentCategory.Items.Clear();
                ddlParentParentCategory.Items.Add("--Select--");
                ddlParentParentCategory.Items[0].Value = "0";
                ddlParentOfSub.Items.Clear();
                ddlParentOfSub.Items.Add("--Select--");
                ddlParentOfSub.Items[0].Value = "0";

                while (reader.Read())
                {
                    ddlParentCategory.Items.Add(reader["category_name"].ToString());
                    ddlParentCategory.Items[counter].Value = reader["id"].ToString();
                    ddlParentParentCategory.Items.Add(reader["category_name"].ToString());
                    ddlParentParentCategory.Items[counter].Value = reader["id"].ToString();
                    ddlParentOfSub.Items.Add(reader["category_name"].ToString());
                    ddlParentOfSub.Items[counter].Value = reader["id"].ToString();
                    counter++;
                }
                reader.Close();
            }
            else reader.Close();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mParentCategoryDDLselected(object sender, EventArgs e)
    {
        try
        {
            mFillSubCategoryGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillSubCategoryGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the grid of the sub categories "sub_category_grid"
        /// Author: mutawakelm
        /// Date :18/08/2008 01:25:48 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            
            DataTable subCategoryDataTable = new DataTable();
            subCategoryDataTable.Columns.Add("id");
            subCategoryDataTable.Columns.Add("sub_cateogry_name");
            subCategoryDataTable.Columns.Add("description");
            if (!string.IsNullOrEmpty(ddlParentCategory.Text))
            {
                string strCategoryQuery = "SELECT * FROM t_sub_catgory WHERE category_id=" + ddlParentCategory.Items[ddlParentCategory.SelectedIndex].Value;
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subCategoryDataTable.Rows.Add(reader["id"].ToString(), reader["sub_cateogry_name"].ToString(), reader["description"].ToString());
                    }
                    reader.Close();
                    sub_category_grid.DataSource = subCategoryDataTable;
                    sub_category_grid.DataBind();
                    reader.Close();
                }
                else
                {
                    sub_category_grid.DataSource = null;
                    sub_category_grid.DataBind();
                    reader.Close();
                }
            }
           
            

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void subCategoryGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to edit the contents of the datagrid "categoryGrid"
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
            
            TableCell itemCell = e.Item.Cells[1];
            lblSubSelectedId.Text = itemCell.Text;
            sub_category_grid.EditItemIndex = e.Item.ItemIndex;
            mFillSubCategoryGrid();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void subCategoryGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
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
            System.Web.UI.WebControls.TextBox subCategoryName = new System.Web.UI.WebControls.TextBox();
            subCategoryName = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox subCategoryDescription = new System.Web.UI.WebControls.TextBox();
            subCategoryDescription = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];

            //The following code will update the item 
            GeneralClass.Program.Add("category_id", ddlParentCategory.Items[ddlParentCategory.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("sub_cateogry_name", subCategoryName.Text, "S");
            GeneralClass.Program.Add("description", subCategoryDescription.Text, "S");
            GeneralClass.Program.UpdateRecordStatement("t_sub_catgory", "id", lblSubSelectedId.Text);
            sub_category_grid.EditItemIndex = -1;
            mFillSubCategoryGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void subCategory_CancelCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to cancel the changes on the grid
        /// Author: mutawakelm
        /// Date :18/08/2008 09:23:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            sub_category_grid.EditItemIndex = -1;
            mFillSubCategoryGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mAddNewSubCategory(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This fucntion will be used to add a new sub category details in the table "t_sub_category"
        /// Author: mutawakelm
        /// Date :18/08/2008 01:02:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (ddlParentCategory.SelectedItem.Text!="--Select--")
            {
            GeneralClass.Program.Add("category_id",ddlParentCategory.Items[ddlParentCategory.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("sub_cateogry_name", txtSubCategoryName.Text, "S");
            GeneralClass.Program.Add("description", txtSubCatDescription.Text, "S");
            GeneralClass.Program.InsertRecordStatement("t_sub_catgory");
            txtSubCategoryName.Text = "";
            txtSubCatDescription.Text = "";
            mFillSubCategoryGrid();
            mFillSubCategoryDDL();
            }
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubCategoryDDLselect(object sender, EventArgs e)
    {
        try
        {
            mFillSubCategoryDDL();
            mFillSubSubCategoryGrid();

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
        /// Description:This function will be used to fill the drop down list of the sub-category
        /// Author: mutawakelm
        /// Date :18/08/2008 03:07:51 PM
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
            if (!string.IsNullOrEmpty(ddlParentParentCategory.Text))
            {
                string strCategoryQuery = "SELECT id,sub_cateogry_name FROM t_sub_catgory WHERE category_id=" + ddlParentParentCategory.Items[ddlParentParentCategory.SelectedIndex].Value;
                ddlSubCategory.Items.Clear();
                //ddlSubOfSub.Items.Clear();
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        if (reader["sub_cateogry_name"].ToString().Length > 40)
                            strSubCat = reader["sub_cateogry_name"].ToString().Substring(0, 40).ToString() + "...";
                        else
                            strSubCat = reader["sub_cateogry_name"].ToString();
                        ddlSubCategory.Items.Add(strSubCat);
                        ddlSubCategory.Items[counter].Value = reader["id"].ToString();
                        //ddlSubOfSub.Items.Add(reader["sub_cateogry_name"].ToString());
                        //ddlSubOfSub.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else
                {
                    ddlSubCategory.Items.Clear();
                    reader.Close();
                }

            }


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillSubSubCategoryGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub-sub-category grid
        /// Author: mutawakelm
        /// Date :18/08/2008 03:25:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            DataTable subSubCategoryDataTable = new DataTable();
            subSubCategoryDataTable.Columns.Add("id");
            subSubCategoryDataTable.Columns.Add("sub_sub_category_name");
            subSubCategoryDataTable.Columns.Add("description");
            if (!string.IsNullOrEmpty(ddlSubCategory.Text))
            {
                string strCategoryQuery = "SELECT t_sut_sub_category.id,sub_sub_category_name,t_sut_sub_category.minimum_number FROM t_sut_sub_category join t_sub_catgory on t_sut_sub_category.sub_category_id=t_sub_catgory.id WHERE sub_category_id=" + ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value;
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subSubCategoryDataTable.Rows.Add(reader["id"].ToString(), reader["sub_sub_category_name"].ToString(), reader["minimum_number"].ToString());
                    }
                    reader.Close();
                    subSubCategoryGrid.DataSource = subSubCategoryDataTable;
                    subSubCategoryGrid.DataBind();

                }
                else
                {
                    subSubCategoryGrid.DataSource = null;
                    subSubCategoryGrid.DataBind();
                    reader.Close();
                }
            }
            else
            {
                subSubCategoryGrid.DataSource = null;
                subSubCategoryGrid.DataBind();
            }


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubSubCategoryDDLSelect(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to call the function who is responsible to fill the sub sub category grid "subSubCategoryGrid"
        /// Author: mutawakelm
        /// Date :19/08/2008 08:42:17 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            mFillSubSubCategoryGrid();

        }
        catch (Exception mSubSubCategoryDDLSelect_Exp)
        {

        }
        try
        {
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void subSubCategoryGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to edit the contents of the datagrid "subSubCategoryGrid"
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
            
            TableCell itemCell = e.Item.Cells[1];
            lblSubSubSelected.Text = itemCell.Text;
            subSubCategoryGrid.EditItemIndex = e.Item.ItemIndex;
            mFillSubSubCategoryGrid();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void SubSubCategoryGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update the contents of the datagrid "subSubCategoryGrid" after editing
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
            System.Web.UI.WebControls.TextBox subCategoryName = new System.Web.UI.WebControls.TextBox();
            subCategoryName = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox subCategoryDescription = new System.Web.UI.WebControls.TextBox();
            subCategoryDescription = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];

            //The following code will update the item 
            GeneralClass.Program.Add("sub_category_id", ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("sub_sub_category_name", subCategoryName.Text, "S");
            GeneralClass.Program.Add("minimum_number", subCategoryDescription.Text, "I");
            GeneralClass.Program.UpdateRecordStatement("t_sut_sub_category", "id", lblSubSubSelected.Text);
            subSubCategoryGrid.EditItemIndex = -1;
            mFillSubSubCategoryGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void SubSubCategory_CancelCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to cancel the changes on the grid "subSubCategoryGrid"
        /// Author: mutawakelm
        /// Date :18/08/2008 09:23:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            subSubCategoryGrid.EditItemIndex = -1;
            mFillSubSubCategoryGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mAddNewSubSubCategory(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This fucntion will be used to add a new sub sub category details in the table "t_sut_sub_category"
        /// Author: mutawakelm
        /// Date :18/08/2008 01:02:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            if (ddlSubCategory.Text!="")
            {
            GeneralClass.Program.Add("sub_category_id", ddlSubCategory.Items[ddlSubCategory.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("sub_sub_category_name", txtSubSubCategoryName.Text, "S");
            GeneralClass.Program.Add("minimum_number", txtSubSubDescription.Text, "I");
            GeneralClass.Program.InsertRecordStatement("t_sut_sub_category");
            txtSubSubCategoryName.Text = "";
            txtSubSubDescription.Text = "";
            mFillSubSubCategoryGrid();
            }
            
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillSubSubCategoryDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the drop down list "ddlSubSubDropDown"
        /// Author: mutawakelm
        /// Date :23/08/2008 03:07:51 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            string strSubSubCat = "";
            int counter =1;
            if (!string.IsNullOrEmpty(ddlSubOfSub.Text))
            {
                string strCategoryQuery = "SELECT id,sub_sub_category_name FROM t_sut_sub_category WHERE sub_category_id=" + ddlSubOfSub.Items[ddlSubOfSub.SelectedIndex].Value;
                ddlSubSubDropDown.Items.Clear();
                ddlSubSubDropDown.Items.Add("--Select--");
                ddlSubSubDropDown.Items[0].Value = "0";
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        if (reader["sub_sub_category_name"].ToString().Length > 40)
                            strSubSubCat = reader["sub_sub_category_name"].ToString().Substring(0, 40).ToString() + "...";
                        else
                            strSubSubCat = reader["sub_sub_category_name"].ToString();
                        ddlSubSubDropDown.Items.Add(strSubSubCat);
                        ddlSubSubDropDown.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else
                {
                    ddlSubSubDropDown.Items.Clear();
                    reader.Close();
                }

            }
            else
            {
                ddlSubSubDropDown.Items.Clear();
            }


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubSubDetailsDdlSelected(object sender, EventArgs e)
    {
        try
        {
            mFillSubSubCategoryDDL();
            mFillSubSubCategoryDetailsGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mAddSubSubCategoryDetials(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This fucntion will be used to add a sub sub category details 
        /// in the table "t_sut_sub_category"
        /// Author: mutawakelm
        /// Date :23/08/2008 01:02:40 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            GeneralClass.Program.Add("sub_sub_category_id", ddlSubSubDropDown.Items[ddlSubSubDropDown.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("detials_field_name", txtSubSubDetails.Text, "S");
            GeneralClass.Program.Add("details_field_description", txtSubSubDescription1.Text, "S");
            GeneralClass.Program.InsertRecordStatement("t_sub_sub_category_detail");
            txtSubSubDetails.Text = "";
            txtSubSubDescription1.Text = "";
            mFillSubSubCategoryDetailsGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mFillSubSubCategoryDetailsGrid()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub-sub-category grid
        /// Author: mutawakelm
        /// Date :18/08/2008 03:25:30 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//

        try
        {
            DataTable subSubCategoryDataTable = new DataTable();
            subSubCategoryDataTable.Columns.Add("id");
            subSubCategoryDataTable.Columns.Add("detials_field_name");
            subSubCategoryDataTable.Columns.Add("details_field_description");
            if (!string.IsNullOrEmpty(ddlSubSubDropDown.Text))
            {
                string strCategoryQuery = "SELECT id,detials_field_name,details_field_description FROM t_sub_sub_category_detail WHERE sub_sub_category_id=" + ddlSubSubDropDown.Items[ddlSubSubDropDown.SelectedIndex].Value;
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        subSubCategoryDataTable.Rows.Add(reader["id"].ToString(), reader["detials_field_name"].ToString(), reader["details_field_description"].ToString());
                    }
                    reader.Close();
                    subSubGridDetails.DataSource = subSubCategoryDataTable;
                    subSubGridDetails.DataBind();

                }
                else
                {
                    subSubGridDetails.DataSource = null;
                    subSubGridDetails.DataBind();
                    reader.Close();
                }
            }
            else
            {
                subSubGridDetails.DataSource = null;
                subSubGridDetails.DataBind();
            }


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void subSubCategoryDetailsGrid_EditCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to edit the contents of the datagrid "subSubGridDetails"
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

            TableCell itemCell = e.Item.Cells[1];
            lblSubSubDetailsSelected.Text = itemCell.Text;
            subSubGridDetails.EditItemIndex = e.Item.ItemIndex;
            mFillSubSubCategoryDetailsGrid();

        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void SubSubCategoryDetailsGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
    {
        //=====================================================//
        /// <summary>
        /// Description:This function will be used to update the contents of the datagrid "subSubGridDetails" after editing
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
            System.Web.UI.WebControls.TextBox subCategoryName = new System.Web.UI.WebControls.TextBox();
            subCategoryName = (System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];
            System.Web.UI.WebControls.TextBox subCategoryDescription = new System.Web.UI.WebControls.TextBox();
            subCategoryDescription = (System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
            //The following code will update the item 
            GeneralClass.Program.Add("sub_sub_category_id", ddlSubSubDropDown.Items[ddlSubSubDropDown.SelectedIndex].Value, "I");
            GeneralClass.Program.Add("detials_field_name", subCategoryName.Text, "S");
            GeneralClass.Program.Add("details_field_description", subCategoryDescription.Text, "S");
            GeneralClass.Program.UpdateRecordStatement("t_sub_sub_category_detail", "id", lblSubSubDetailsSelected.Text);
            subSubGridDetails.EditItemIndex = -1;
            mFillSubSubCategoryDetailsGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void SubSubCategoryDetails_CancelCommand(object source, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to cancel the changes on the grid "subSubCategoryGrid"
        /// Author: mutawakelm
        /// Date :18/08/2008 09:23:59 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {

            subSubGridDetails.EditItemIndex = -1;
            mFillSubSubCategoryDetailsGrid();


        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
    protected void mSubSubCtegoryDetailsSelect(object sender, EventArgs e)
    {
        try
        {
            mFillSubSubCategoryDetailsGrid();
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }
    }
    protected void mSubSubParentDDLselected(object sender, EventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the sub-category and sub-sub category of the tab (sub-sub-category-details)
        /// Author: mutawakelm
        /// Date :26/08/2008 08:46:37 AM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
          //The following code will be used to fill the sub-category ddl
            string strSubCat = "";
            int counter = 1;
            if (!string.IsNullOrEmpty(ddlParentOfSub.Text))
            {
                string strCategoryQuery = "SELECT id,sub_cateogry_name FROM t_sub_catgory WHERE category_id=" + ddlParentOfSub.Items[ddlParentOfSub.SelectedIndex].Value;
                ddlSubOfSub.Items.Clear();
                ddlSubOfSub.Items.Add("--Select--");
                ddlSubOfSub.Items[0].Value = "0";
                reader = GeneralClass.Program.gRetrieveRecord(strCategoryQuery);
                if (reader.HasRows)
                {

                    while (reader.Read())
                    {
                        if (reader["sub_cateogry_name"].ToString().Length > 40)
                            strSubCat = reader["sub_cateogry_name"].ToString().Substring(0, 40).ToString()+"...";
                        else
                            strSubCat = reader["sub_cateogry_name"].ToString();
                        ddlSubOfSub.Items.Add(strSubCat);
                        ddlSubOfSub.Items[counter].Value = reader["id"].ToString();
                        counter++;
                    }
                    reader.Close();
                }
                else
                {
                    ddlSubOfSub.Items.Clear();
                    reader.Close();
                }

            }
            //The following code will be used to fill the sub-sub-category ddl
             mFillSubSubCategoryDDL();
            //The following call will be used to call the function which will fill the sub-sub-category-details-grid
            mFillSubSubCategoryDetailsGrid();
             
        }
        catch (Exception exp)
        {
            Response.Write(exp.Message.ToString());
        }

    }
}
