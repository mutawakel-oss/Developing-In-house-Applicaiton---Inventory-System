using System;
using System.Web.UI.WebControls;
using System.Security.Principal;
using System.DirectoryServices;
using System.Text;
using GeneralClass;
using System.Web.UI;
using System.Data.Odbc;
using System.Net.Mail;
using System.Web;
//using AspNet.StarterKits.Classifieds.BusinessLogicLayer;

public partial class Login_aspx : System.Web.UI.Page
{
    OdbcDataReader reader;
    string strCurrentLanguage = "";
    
   protected void Page_Load(object sender, EventArgs e)
	{
        GeneralClass.Program.DatabaseConnect();
        DateTime dtCreatedOn =Convert.ToDateTime(DateTime.Now.ToString());
		if (!Page.IsPostBack)
		{
			AccessNoticePanel.Visible = (Request.QueryString["ReturnUrl"] != null);
            Session.Abandon();
            mFillDepartmentDDL();    
        }
		else
			AccessNoticePanel.Visible = false;
        
	}
    protected void mFillDepartmentDDL()
    {

        //=====================================================//
        /// <summary>
        /// Description:This function will be used to fill the drop down list "ddlDepartmentName"
        /// Author: mutawakelm
        /// Date :4/22/2009 2:20:19 PM
        /// Parameter:
        /// input:
        /// output:
        /// Example:
        /// <summary>
        //=====================================================//
        try
        {
            string strDeptQuery = "SELECT * FROM t_organization_depts";
            int counter=0;
            reader = GeneralClass.Program.gRetrieveRecord(strDeptQuery);
            if (reader.HasRows)
            {
                ddlDepartmentName.Items.Clear();
                while (reader.Read())
                {
                    ddlDepartmentName.Items.Add(reader["department_name"].ToString());
                    ddlDepartmentName.Items[counter].Value = reader["id"].ToString();
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

    public static string GetProperty(SearchResult searchResult, string PropertyName)
    {
        if (searchResult.Properties.Contains(PropertyName))
            return searchResult.Properties[PropertyName][0].ToString();
        else
            return string.Empty;
    }

	protected void ForgotPasswordButton_Click(object sender, EventArgs e)
	{
		//this.PasswordRecovery.Visible = true;
	}

	protected void PasswordRecovery_Init(object sender, EventArgs e)
	{
		//SiteSettings s = SiteSettings.GetSharedSettings();
		//PasswordRecovery.MailDefinition.From = s.SiteEmailFromField;

	}
	protected void PasswordRecovery_SendMailError(object sender, SendMailErrorEventArgs e)
	{
		System.Text.StringBuilder mailLink = new System.Text.StringBuilder("<a href=\"mailto:");
		try
		{
			//mailLink.Append(SiteSettings.GetSharedSettings().SiteEmailAddress);
		}
		catch
		{
			mailLink.Append("#");
		}
		mailLink.Append("\">system administrator</a>");
		//PasswordRecovery.SuccessText = "A problem occurred sending the email. " +
		//	"Please contact the " + mailLink.ToString() + ".";
		e.Handled = true;
	}
    protected void LoginConrol_Authenticate(object sender, AuthenticateEventArgs e)
    {


        
            bool authorized = false;//This variable will determine if the user has an administrative priviliges or no
            CheckBox chb = (CheckBox)LoginConrol.FindControl("RememberMe");
            if (chb.Checked)
            {
                if (LocalAuthentication(LoginConrol.UserName.ToString(), LoginConrol.Password.ToString()))
                {
                    Response.Redirect("admin_default_page.aspx");
                    Session["UserFullName"] = "Local Admin";
                    Session["UserID"] = "123456";  // it's permanent userid for the local administratrator.

                }
            }


            string bln = IsAuthenticated("", LoginConrol.UserName.ToString(), LoginConrol.Password.ToString());
            if (bln != null && bln != "")
            {
                
                Session["UserFullName"] = bln.ToString();

                if (!UserPrivileges(LoginConrol.UserName.ToString()))
                {
                    Session["User"] = "true";
                    Session["UserID"] = LoginConrol.UserName.ToString();
                    
                    
                }
                if (Session["departmentId"].ToString()==ddlDepartmentName.SelectedItem.Value.ToString())
                {
                if (Session["Technician"] != null)
                {
                    if (Session["Technician"].ToString() == "true")
                    {
                        Session["UserID"] = LoginConrol.UserName.ToString();
                        Session["Technician"] ="True";
                        authorized = true;
                    }
                }
                    if (null != Session["spr_creatror"])
                        if (Session["spr_creatror"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["spr_creator"] = "True";
                            authorized = true;
                        }
                    if (null != Session["reciver"])
                        if (Session["reciver"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["reciver"] = "True";
                            authorized = true;
                        }
                    if (null != Session["inspector"])
                        if (Session["inspector"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["inspector"] = "True";
                            authorized = true;
                        }
                    if (null != Session["approval"])
                        if (Session["approval"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["approval"] = "True";
                            authorized = true;
                        }
                    if (null != Session["assignmenter"])
                        if (Session["assignmenter"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["assignmenter"] = "True";
                            authorized = true;
                        }
                    if (null != Session["maintainance_manager"])
                        if (Session["maintainance_manager"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["maintainance_manager"] = "True";
                            authorized = true;
                        }
                    if (null != Session["reporter"])
                        if (Session["reporter"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["reporter"] = "True";
                            authorized = true;
                        }
                    if (null != Session["user_management"])
                         if (Session["user_management"].ToString() == "true")
                        {
                            Session["UserID"] = LoginConrol.UserName.ToString();
                            Session["user_management"] = "True";
                            authorized = true;
                        }
                 
                    
                    
                    if (authorized == true)
                        Response.Redirect("admin_default_page.aspx");
                
            }
               
            }
            else
            {


                Response.Redirect("error_page.aspx?Error= Invalid user name or password.  ");
            }
    }
    public string IsAuthenticated(string domain, string username, string pwd)
    {
        try
        {
            
            string domainAndUsername = username.Trim();
            DirectoryEntry entry = new DirectoryEntry("LDAP://10.8.128.100", domainAndUsername, pwd);
            Object obj = entry.NativeObject;
            DirectorySearcher search = new DirectorySearcher(entry);
            search.Filter = "(SAMAccountName=" + username + ")";
            search.PropertiesToLoad.Add("cn");
            SearchResult result = search.FindOne();
            if (null == result)
            {
                return string.Empty;
            }
            else
            {
                // Update the new path to the user in the directory
                //_path = result.Path;
                //_filterAttribute = (string)result.Properties["cn"][0];
                GeneralClass.Program.strLOGINUSERPATH = result.GetDirectoryEntry().Parent.Path;  // ldap path 
                GeneralClass.Program.SKODE = pwd;      


                string fullName = result.GetDirectoryEntry().Properties["Name"].Value.ToString();
               // string str1 = result.GetDirectoryEntry().Properties["userAccountControl"].Value.ToString();
                if(null != result.GetDirectoryEntry().Properties["employeeid"].Value)
                Session["Badge"] = result.GetDirectoryEntry().Properties["employeeid"].Value.ToString();
            if (null != result.GetDirectoryEntry().Properties["telephonenumber"].Value)
                Session["Extn"] = result.GetDirectoryEntry().Properties["telephonenumber"].Value.ToString();
            if (null != result.GetDirectoryEntry().Properties["mobile"].Value)
                Session["Mobile"] = result.GetDirectoryEntry().Properties["mobile"].Value.ToString();
            if (null != result.GetDirectoryEntry().Properties["mail"].Value)
                Session["mail"] = result.GetDirectoryEntry().Properties["mail"].Value.ToString();
            if (null != result.GetDirectoryEntry().Properties["Description"].Value)
                Session["Title"] = result.GetDirectoryEntry().Properties["Description"].Value.ToString();
            if (null != result.GetDirectoryEntry().Properties["Department"].Value)
                Session["Department"] = result.GetDirectoryEntry().Properties["Department"].Value.ToString();
               
                
                InitializeUserTable(username.Trim(), fullName);
                return (string)result.Properties["cn"][0].ToString();
            }
        }
        catch (Exception exp)
        {
            return string.Empty;
        }
    }

    protected bool UserPrivileges(string loginID)
    {
        /// <summary>
        /// 	Description: to get id,full and group details of the loggedin user 
        ///	
        ///
        /// 	Date:18/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:loginID
        ///		input:
        ///		output: details will be assigned to the session variables
        /// 	Example:  UserPrivileges("usera")
        /// </summary>

        bool privilege = false;
        try
        {
            //reader = GeneralClass.Program.gRetrieveRecord("select id,full_name,user_group from t_users where login_name='" + loginID + "'");

            //reader = GeneralClass.Program.gRetrieveRecord("select id, user_group from t_userrights where id='" + loginID + "'");
            reader = GeneralClass.Program.gRetrieveRecord("select id, user_group,dept_id from t_users where id='" + loginID + "'");
            if (reader.HasRows)
            {
                reader.Read();
                
                    Session["UserID"] = reader["id"].ToString();
                    Session["UserGroups"] = reader["user_group"].ToString();
                    Session["departmentId"] = reader["dept_id"].ToString();
                
                reader.Close();
            }
            else
            reader.Close();

            if (null != Session["UserGroups"])
                if (Session["UserGroups"].ToString() != "")
                {
                    string groups = Session["UserGroups"].ToString();

                    string[] grps = groups.Split(',');

                    for (int i = 0; i <= grps.Length - 1; i++)
                    {
                        if (grps[i] == "1")
                        {
                            Session["spr_creatror"] = "true";
                            privilege = true;
                        }

                        if (grps[i] == "2")
                        {
                            Session["Technician"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "3")
                        {
                            Session["reciver"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "4")
                        {
                            Session["inspector"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "5")
                        {
                            Session["approval"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "6")
                        {
                            Session["assignmenter"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "7")
                        {
                            Session["maintainance_manager"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "8")
                        {
                            Session["reporter"] = "true";
                            privilege = true;
                        }
                        if (grps[i] == "9")
                        {
                            Session["user_management"] = "true";
                            privilege = true;
                        }
                    
                    }
                }
            return privilege;

        }

        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
            return privilege;
        }
        catch (Exception ex)
        {
            if (null != reader)
                reader.Close();
            
            Response.Redirect("error.aspx?error=" + ex.Message.ToString());
            return privilege;
        }
    }


    private void pullusers(string domain, string username, string pwd)
    {
        try
        {
            DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=staff,OU=collegeusers,DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);
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
            //StringBuilder OctetToHexStr = new StringBuilder();
            byte[] arraybyte = (byte[])entry1.Properties["objectSid"].Value;

            //for (int k = 0; k < arraybyte.Length; k++)
            //{

            //    OctetToHexStr.Append(Convert.ToString(Convert.ToByte(arraybyte[k]), 16));

            //}



            DirectorySearcher dSearch = new DirectorySearcher(entry1);
            //string strString = "";
            foreach (SearchResult sResultSet in dSearch.FindAll())
            {

                //byte[] arraybyte1 = (byte[])entry1.Properties["objectSid"].Value;
                //byte[] arraybyte2 = (byte[])entry1.Properties["objectGUID"].Value;          

                string chekc = GetProperty(sResultSet, "objectGUID");
                string test = GetProperty(sResultSet, "uid");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                strFullName = GetProperty(sResultSet, "Name");
                strEMail = GetProperty(sResultSet, "mail");
                strBadgeNo = GetProperty(sResultSet, "employeeid");
                strDepartment = GetProperty(sResultSet, "department");
                strPager = GetProperty(sResultSet, "pager");
                strTitle = GetProperty(sResultSet, "title");
                strTele = GetProperty(sResultSet, "telephonenumber");
                strMobile = GetProperty(sResultSet, "mobile");

                if (strFullName != string.Empty)
                {

                    //GeneralClass.Program.Add(
                    
                    //dbc.SaveLdapUserData(strFullName, strBadgeNo, strEMail, strTele, strMobile, strDepartment, strTitle, strLoginName);

                }
            }

        }
        catch (Exception ex)
        {

        }
    }

    protected bool LocalAuthentication(string username, string pwd)
    {
        // <summary>
        /// 	Description: fetch data from settings table 
        ///	
        ///
        /// 	Date:21/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool Authenticated = false;
        string strUserName = "";
        string strPwd = "";
        
          

        try
        {
            reader = GeneralClass.Program.gRetrieveRecord("SELECT admin_user_name,admin_password FROM t_settings  WHERE admin_user_name ='"+username+"' AND admin_password='"+pwd+"'");
            while (reader.Read())
            {

                strUserName = reader["admin_user_name"].ToString();
                strPwd = reader["admin_password"].ToString();

                Session["LocalAdmin"] = "true";
            }
            reader.Close();

            if (strUserName.Trim() == LoginConrol.UserName.Trim() && strPwd.Trim() == LoginConrol.Password.Trim())
            {
                Authenticated = true;
            }
            return Authenticated;
        }
        catch (OdbcException exp_1)
        {
            if (null != reader)
                reader.Close();
            
            Response.Redirect("error.aspx?error=" + exp_1.Message.ToString());
            return Authenticated;
        }
    }


    protected void TempUserData()
    {
        //InitializeUserTable();
      // GetUserData("mis", "wstaff", "test123");
      // GetUserData("staff,OU=collegeusers", "wstaff", "test123");
      //// GetUserData("collegeusers", "wstaff", "test123");    

       GetUserData("", "wstaff", "test123");
    }
    
    
    
    
    private void GetUserData(string domain, string username, string pwd)
    {
        /// <summary>
        /// 	Description: Import users from the LDAP
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input: 
        ///		output: 
        /// 	Example:  pullusers("", txtusername.Text, txtpwd.Text)
        /// </summary>

       
        
        try
        {
            //GeneralClass.Program.gRetrieveRecord("delete from t_ldapUsers");

            DirectoryEntry entry1 = new DirectoryEntry("LDAP://med.ksuhs.edu.sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis
            
           // DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU="+domain+",DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis

           // DirectoryEntry entry1 = new DirectoryEntry("LDAP://OU=mis,DC=med,DC=ksuhs,DC=edu,DC=sa", username, pwd);//OU=staff,OU=collegeusers,OU=mis
            DirectorySearcher mySearcher = new DirectorySearcher(entry1);
            SearchResultCollection results;
            results = mySearcher.FindAll();

            string strFullName;
            string strLoginName;


            DirectorySearcher dSearch = new DirectorySearcher(entry1);
            
            foreach (SearchResult sResultSet in dSearch.FindAll())
            {

                strFullName = GetProperty(sResultSet, "Name");
                strLoginName = GetProperty(sResultSet, "sAMAccountName");
                  if ("" != strLoginName.Trim())
                    if (strFullName != string.Empty)
                    {
                        GeneralClass.Program.Add("id", strLoginName, "S");
                        GeneralClass.Program.Add("full_name", strFullName, "S");
                        int intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
                    }
            }

        }
        catch (Exception ex)
        {

        }
    }        
    
    protected void InitializeUserTable(string logID, string fullName)
     {
        // <summary>
        /// 	Description: Create intial entry for local admin  
        ///	
        ///
        /// 	Date:27/Aug/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        //GeneralClass.Program.gRetrieveRecord("delete from t_users");
        //GeneralClass.Program.Add("id", "123456", "S");
        //GeneralClass.Program.Add("full_name", "Local Admin", "S");

         if (!UserExists(logID))
         {
             GeneralClass.Program.Add("id", logID, "S");
             GeneralClass.Program.Add("full_name", fullName, "S");
             int intReturnID = GeneralClass.Program.InsertRecordStatement("t_users");
         }
    }

    protected bool UserExists(string logID)
    {
        // <summary>
        /// 	Description: Check for the exitstance of user in the temp table  
        ///	
        ///
        /// 	Date:1st/Sep/2007
        /// 	Author:Oliver
        /// 	Parameter:
        ///		input:
        ///		output: 
        /// 	Example:  
        /// </summary>

        bool userExists = false;
        //GeneralClass.Program.DatabaseConnect();
        reader = GeneralClass.Program.gRetrieveRecord("SELECT COUNT(id) FROM t_users WHERE id='"+logID+"'");
        while (reader.Read())
        {
            if (Convert.ToInt32(reader[0]) > 0)
                userExists = true;
        }
        reader.Close();
        return userExists;
    }
    protected void CreateNew(object sender, EventArgs e)
    {
        Response.Redirect("./frmNewUserAccount.aspx");
    }

    protected void arabicLogin(object sender, EventArgs e)
    {
        Response.Redirect("./Login.aspx");
    }

    //protected void NewUserAccountRequest(object sender, EventArgs e)
    //{
    //    SmtpClient smtpClient = new SmtpClient();
    //    MailMessage message = new MailMessage();
    //    bool success = false;

    //    try
    //    {
    //        MailAddress fromAddress = new MailAddress(txtMailID.Text, txtFullName.Text + " - " + txtDept.Text);
    //        //smtpClient.Host = "mail.med.ksuhs.edu.sa";
    //        smtpClient.Host = "mail1.ksuhs.edu.sa";
    //        message.From = fromAddress;
    //        // To address collection of MailAddress
    //        message.To.Add("olivery@ksau-hs.edu.sa");
    //       // message.To.Add("helpdesk@ksau-hs.edu.sa");
    //        message.Subject = "New User Account";
    //        message.IsBodyHtml = false;
    //        string strMessage = "Subject    : Request for New user account"+"\n\nFull Name  : " + txtFullName.Text + "\nBadge No   : " + txtBadge.Text + "\nPhone/Pager: " + txtPager.Text + "\nDepartment :  " + txtDept.Text + "\nEmail      : " + txtMailID.Text + "\nJobTitle   : " + txtJobTitle.Text + "\nLocation   : " + txtLocation.Text;
    //        message.Body = strMessage;
    //        smtpClient.Send(message);
    //        success = true;
    //       Response.Redirect("redirect_1.aspx?QUICKREQUEST=true", true);

    //    }
    //    catch (Exception ex)
    //    {
    //        if (success == false)
    //            Response.Redirect("error.aspx?error=Mail is not send successfully - " + ex.Message, true);
    //    }
    //}
}
