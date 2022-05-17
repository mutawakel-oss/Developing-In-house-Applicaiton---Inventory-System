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
            if (!IsPostBack)
            {
                
               
            }
            HyperLink Hlk = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkHome");
            HyperLink LB = (HyperLink)this.Master.Page.Controls[0].Controls[3].FindControl("hlkLogin");
            LB.Text = "Log Out";
            Hlk.NavigateUrl = "~/admin_default_page.aspx";

            mCheckAuthority();
        }
        catch (Exception exp)
        {
            
        }
    }
    
    protected void mCheckAuthority()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to check the privileges
        /// Author: mutawakelm
        /// Date :3/10/2009 3:36:24 PM
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
                else
                {
                    Response.Redirect("~/error_page.aspx?error=you do not have privileges to access this page.");
                }
            }
            else

            Response.Redirect("~/error_page.aspx?error=you do not have privileges to access this page.");
        }
        catch (Exception exp)
        {
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
           
                mFillGridBySprNo(txtSprNoSearch.Text);
                
         

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
            bool deptRelated = false;
            string strStatus = ddlSprStatus.SelectedItem.Value;
            if ((spr_n != "") && (spr_n != " "))
            {
                DataTable productTable = new DataTable();
                int assignedItemsCounter = 0;
                int unAssignedItemsCounter = 0;
                string strInspectedBy = "";//This variable will hold the name of the inspecter
                string strApprovedBy = "";//This variable will hold the name of the approval responsible
                string strAllocatedTo = "";//This variable will hold the name of the allocating employee
                productTable.Columns.Add("item");//This column will hold the item name
                productTable.Columns.Add("tag");
                productTable.Columns.Add("serial");
                productTable.Columns.Add("room");//This column will hold the room where the item reside
                productTable.Columns.Add("spr_no");//This column will hold the spr number
                productTable.Columns.Add("hidden");
                
               

                string strProductQuery = "SELECT t_products.room,t_products.act_serial_no,t_vendor.supplier_name,t_products.serial_no,t_products.spr_no,t_products.delivery_date,t_products.recieved_by," +
                    "t_products.inspected_by,t_products.approved_by,t_products.allocated_to,t_products.id" +
                     ",t_sut_sub_category.sub_sub_category_name" +
                    " FROM t_products JOIN t_sut_sub_category on t_products.sub_sub_cat=t_sut_sub_category.id,t_vendor  " +
                    "where t_products.spr_no=" + spr_n + " and t_products.inspected_by is not null and t_products.approved_by is not null and t_products.vendor=t_vendor.id AND t_products.dept_id=" + Session["departmentId"].ToString() + " AND t_products.status_id=" + strStatus;
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
                        productTable.Rows.Add(reader["sub_sub_category_name"].ToString(), reader["serial_no"].ToString(), reader["act_serial_no"].ToString(), reader["room"].ToString(), reader["spr_no"].ToString(), reader["id"].ToString());
                        //The following segment will be used to count the unassigned 
                    }
                    reader.Close();
                   
                }
                else reader.Close();
                productsGrid.DataSource = productTable;
                productsGrid.DataBind();
                //The following call will fill the productsGrid use full names
           
                
            }
            else
            {
                productsGrid.DataSource = null;
                productsGrid.DataBind();
             

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
           
            specificationExtender.Show();
        }
        catch (Exception exp)
        {
        }
    }
    protected void mPorductEditCommandClicked(object sender, DataGridCommandEventArgs e)
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to open the selected item in the editing mode
        /// Author: mutawakelm
        /// Date :3/10/2009 1:37:32 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            productsGrid.EditItemIndex = e.Item.ItemIndex;
            mFillGridBySprNo(txtSprNoSearch.Text);
        }
        catch (Exception exp)
        {
        }
    }
    protected void mProductCancelCommandClicked(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            productsGrid.EditItemIndex = -1;
            mFillGridBySprNo(txtSprNoSearch.Text);
            
        }
        catch (Exception exp)
        {
        }
    }
    protected void mProductUpdateCommandClicked(object sender, DataGridCommandEventArgs e)
    {
        try
        {
            System.Web.UI.WebControls.TextBox tag_string =(System.Web.UI.WebControls.TextBox)e.Item.Cells[2].Controls[0];         
            System.Web.UI.WebControls.TextBox serial_no =(System.Web.UI.WebControls.TextBox)e.Item.Cells[3].Controls[0];
            System.Web.UI.WebControls.TextBox itemNo = (System.Web.UI.WebControls.TextBox)e.Item.Cells[6].Controls[0];         
            GeneralClass.Program.Add("serial_no",tag_string.Text,"S");
            GeneralClass.Program.Add("act_serial_no",serial_no.Text,"S");
            GeneralClass.Program.Add("status_id", "8", "I");
            int ret=GeneralClass.Program.UpdateRecordStatement("t_products","id",itemNo.Text);
            if (ret != 0)
            {
                productsGrid.EditItemIndex = -1;
                mFillGridBySprNo(txtSprNoSearch.Text);
            }
           

        }
        catch (Exception exp)
        {
        }
    }
    
}
