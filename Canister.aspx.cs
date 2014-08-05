using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Canister : FindLocation
{
    List<CheckBox> marks = new List<CheckBox>();
    string variable;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        variable = Request.QueryString["CanisterID"];
        if (string.IsNullOrEmpty(Request.QueryString["CanisterID"]))
        {
            ThrowNewException("QueryString cannot be empty. Please insert your CanisterID in the url.");
        }
        var v = from s in db.Shorten1s
                where variable == s.Value
                select s.Url;
        variable = v.First();
        CanisterId.InnerText = "Canister: " + variable;
        this.Session["CanisterID"] = variable;
    }
    protected void DropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void SubmitNewName_Click(object sender, EventArgs e)
    {

    }
    protected void DownloadMarked_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        List<KeyValuePair<string,List<string>>> Files = new List<KeyValuePair<string,List<string>>>();
        List<string> paths = new List<string>();
        foreach (GridViewRow row in FilesGrid.Rows)
        {
            if ((row.FindControl("CheckBox1") as CheckBox).Checked)
            {
                string fileid = (row.FindControl("Label1") as Label).Text;
                var path = from f in db.Files
                           where Guid.Parse(fileid) == f.FileID
                           select f.FileLocation;
                string filepath = path.First();
                paths.Add(filepath);
            }
        }
        Files.Add(new KeyValuePair<string, List<string>>("CanisterID_" + variable, paths));
    }
    protected void DeleteMarked_Click(object sender, EventArgs e)
    {
        //DataClassesDataContext db = new DataClassesDataContext();
        //List<KeyValuePair<string,List<string>>> Files = new List<KeyValuePair<string,List<string>>>();
        //List<string> paths = new List<string>();
        //foreach (GridViewRow row in FilesGrid.Rows)
        //{
        //    if ((row.FindControl("CheckBox1") as CheckBox).Checked)
        //    {
        //        string filepath = (row.FindControl("Label1") as Label).Text;
        //        paths.Add(filepath);
        //    }
        //}
        //Files.Add(new KeyValuePair<string,List<string>>("CanisterID_" + variable, paths))

    }
    protected void FilesGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        bool downloadZip = false;
        string filepath = "";
        string name = "";
        if (downloadZip == true)
        {
            DataClassesDataContext db = new DataClassesDataContext();
            List<KeyValuePair<string, List<string>>> Files = new List<KeyValuePair<string, List<string>>>();
            List<string> paths = new List<string>();
            foreach (GridViewRow row in FilesGrid.Rows)
            {
                if ((row.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    string fileid = (row.FindControl("Label1") as Label).Text;
                    var path = from f in db.Files
                               where Guid.Parse(fileid) == f.FileID
                               select f.FileLocation;
                    filepath = path.First();
                    var filename = from n in db.Files
                               where Guid.Parse(fileid) == n.FileID
                               select n.FileLocation;
                    name = filename.First();
                    paths.Add(filepath);
                }
            }
            Files.Add(new KeyValuePair<string, List<string>>("CanisterID_" + variable, paths));
            DownloadZip(Files);
        }
        else
        {
            DataClassesDataContext db = new DataClassesDataContext();
            foreach (GridViewRow row in FilesGrid.Rows)
            {
                if ((row.FindControl("CheckBox1") as CheckBox).Checked)
                {
                    string fileid = (row.FindControl("Label1") as Label).Text;
                    var path = from f in db.Files
                               where Guid.Parse(fileid) == f.FileID
                               select f.FileLocation;
                    filepath = path.First();
                    var filename = from n in db.Files
                                   where Guid.Parse(fileid) == n.FileID
                                   select n.FileLocation;
                    name = filename.First();
                    Download(filepath, name);

                }
            }


        }
    }
    protected void Unnamed3_Click(object sender, EventArgs e)
    {
        Response.Redirect("Upload.aspx?CanisterID=" + variable);
    }
}