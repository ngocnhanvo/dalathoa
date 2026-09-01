using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

public partial class GoogleAuth : System.Web.UI.Page
{


    String AuthenticationCode
    {
        get
        {
            if (ViewState["AuthenticationCode"] != null)
                return ViewState["AuthenticationCode"].ToString().Trim();
            return String.Empty;
        }
        set
        {
            ViewState["AuthenticationCode"] = value.Trim();
        }
    }

    String AuthenticationTitle
    {
        get
        {
            return "ANCO1-GGAUTH";
        }
    }


    String AuthenticationBarCodeImage
    {
        get;
        set;
    }

    String AuthenticationManualCode
    {
        get;
        set;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lblResult.Text = String.Empty;
            lblResult.Visible = false;
            GenerateTwoFactorAuthentication();
            imgQrCode.ImageUrl = AuthenticationBarCodeImage;
            lblManualSetupCode.Text = AuthenticationManualCode;
            lblAccountName.Text = AuthenticationTitle;
        }
    }

    protected void btnValidate_Click(object sender, EventArgs e)
    {
        String pin = txtSecurityCode.Text.Trim();
        Boolean status = ValidateTwoFactorPIN(pin);
        if (status)
        {
            lblResult.Visible = true;
            lblResult.Text = "Code Successfully Verified.";
            lblResult.Style.Add("COLOR", "green");
        }
        else
        {
            lblResult.Visible = true;
            lblResult.Text = "Invalid Code.";
            lblResult.Style.Add("COLOR", "red");
        }
    }

    public Boolean ValidateTwoFactorPIN(String authenticationCode)
    {
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var authenticationCodeGenerated = tfa.GetCurrentPIN(AuthenticationCode);
        return authenticationCodeGenerated == authenticationCode;
    }

    public Boolean GenerateTwoFactorAuthentication()
    {
        //Guid guid = Guid.NewGuid();
        //String uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
        //AuthenticationCode = uniqueUserKey;
        AuthenticationCode = "123456";
        Dictionary<String, String> result = new Dictionary<String, String>();
        TwoFactorAuthenticator tfa = new TwoFactorAuthenticator();
        var setupInfo = tfa.GenerateSetupCode("", AuthenticationTitle, AuthenticationCode, false);
        if (setupInfo != null)
        {
            
            AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
            AuthenticationManualCode = setupInfo.ManualEntryKey;
            return true;
        }
        return false;
    }
}