using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Upload : FindLocation
{
    string path = "";
    Guid Temp;
    string CanisterID;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            path = Find();
            CanisterID = Request.QueryString["CanisterID"];
        }
        catch
        {
            ThrowNewException("QueryString cannot be empty. Please insert your CanisterID in the url.");
        }
        Temp = Guid.Parse(CanisterID);
    }
    /*
    protected void FileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        if (!Directory.Exists(MapPath(path + Temp + "/")))
        {
            Directory.CreateDirectory(MapPath(path + Temp + "/"));
            FileUpload1.SaveAs(MapPath(path + Temp + "/" + e.FileName));
        }
        else
        {
            FileUpload1.SaveAs(MapPath(path + Temp + "/" + e.FileName));
        }
    }
    protected void FileUpload1_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
    {
    }*/
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        if (FileUpload.HasFile)
        {
            if (!Directory.Exists(MapPath(path + Temp + "/")))
            {
                Directory.CreateDirectory(MapPath(path + Temp + "/"));
                FileUpload.SaveAs(MapPath(path + Temp + "/" + FileUpload.FileName));
            }
            else
            {
                FileUpload.SaveAs(MapPath(path + Temp + "/" + FileUpload.FileName));
            }
        }
        Guid? FileID = null;
        string[] files = Directory.GetFiles(MapPath(path + Temp + "/"));
        List<FileInfo> Info = new List<FileInfo>();
        foreach (string f in files)
        {
            Info.Add(new FileInfo(f));
        }
        DataClassesDataContext db = new DataClassesDataContext();
        foreach (FileInfo file in Info)
        {
            //try
            //{
                db.InsertFile(file.Name, path, file.Extension, ref FileID, Guid.Parse(CanisterID), file.Length.ToString());
                try
                {
                    System.IO.File.Move(file.FullName, MapPath(path + file.Name));
                    //}
                }
                catch
                {
                    #region Overwrite?

                    #endregion
                }
            //catch
            //{
                //throw new Exception("File already exists.");
            //}
        }
        Directory.Delete(MapPath(path + Temp + "/"));

    }
}