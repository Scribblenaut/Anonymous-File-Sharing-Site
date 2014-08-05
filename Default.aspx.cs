using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

public partial class Default : FindLocation
{
    Guid? CanisterID = null;
    Guid Temp = new Guid();
    Guid? FileID = null;
    string path = "";
    string name;
    string type;
    string filetype;
    int size;
    List<Guid> FileIDs = new List<Guid>();
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        //////////try
        //////////{
            if (Request.QueryString.Count == 0)
            {
                db.InsertCanister(ref CanisterID);
                id = Shorten(CanisterID.Value.ToString());
            }
            else
            {
                //CanisterID = Guid.Parse(Request.QueryString[1]);
                id = Request.QueryString[0];
                var Canister = from s in db.Shorten1s
                               where s.Value == id
                               select s.Url;
                CanisterID = Guid.Parse(Canister.First());
            }
            //////}
        //////////catch
        //////////{
        //////////    db.InsertCanister(ref CanisterID);
        //////////    id = Shorten(CanisterID.Value.ToString());
        //////////}
        //try
        //{
        //    CanisterID = Guid.Parse(Request.QueryString["CanisterID"]);
        ////var iteration = from s in Shorten
        ////                where 
        //////db.ShortenUrl(CanisterID,)
        path = Find();
        Directory.CreateDirectory(MapPath(path + CanisterID.ToString() + "/"));
        //}
        //catch
        //{
        //    throw new Exception("CanisterID is null");
        //}
        //Temp = CanisterID.Value;
        title.InnerText = "Upload to CanisterID: " + id;
        Url.Text = "Canister.aspx?CanisterID=" + id;
        Url.NavigateUrl = "Canister.aspx?CanisterID=" + id;

        BindDataList();
        
    }
    /*protected void AjaxFileUpload1_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
    {
        //throw new Exception();
    }*/
    protected void BindDataList()
    {
        DirectoryInfo directory = new DirectoryInfo(MapPath(path + CanisterID.ToString()) + "/");
        FileInfo[] files = directory.GetFiles();
        ArrayList items = new ArrayList();
        foreach (FileInfo file in files)
        {
            items.Add(file);
        }
        FileData.DataSource = items;
        FileData.DataBind();

    }
    protected void Unnamed1_Click(object sender, EventArgs e)
    {
        DataClassesDataContext db = new DataClassesDataContext();
        if (FileUpload.HasFile)
        {
            //try
            //{
                FileUpload.SaveAs(MapPath(path + CanisterID.ToString() + "/" + FileUpload.FileName));
                db.InsertFile(FileUpload.FileName, path + FileUpload.FileName, Path.GetExtension(MapPath(path + FileUpload.FileName)), ref FileID, CanisterID, FileUpload.FileContent.Length.ToString());
                Response.Redirect("Canister.aspx?CanisterID=" + CanisterID);
            //}
            //catch
            //{
                //throw new Exception("Cannot save file.");
            //}
        }
        #region
        //Directory.CreateDirectory(MapPath(path + Temp + "/"));
        //string filePath = path + Temp + "/" + FileUpload.FileName;
        //FileUpload.SaveAs(MapPath(path + FileUpload.FileName));
        //name = FileUpload.FileName;
        //type = Path.GetExtension(path + FileUpload.FileName);
        //size = (int)FileUpload.FileContent.Length;
        //filetype = Path.GetExtension(path + FileUpload.FileName);

        //path = "~/Files/";
        //string[] files = Directory.GetFiles(MapPath(path + Temp + "/"));
        //List<FileInfo> fileinfo = new List<FileInfo>();
        //foreach (string file in files)
        //{
        //    fileinfo.Add(new FileInfo(file));
        //}
        //string fids = "";
        //foreach(Guid g in FileIDs)
        //{
        //    fids += Environment.NewLine + g;
        //}
        //string urls = "";
        //foreach (Guid g in FileIDs)
        //{
        //    urls += Environment.NewLine + g;
        //}

        //if (CanisterID == null)
        //{
        //    throw new Exception("Failed to instantiate a new canister.");
        //}
        //if(!string.IsNullOrEmpty(Text.Text))
        //{
        //    string text = Encryption(Text.Text);
        //    db.InsertFile(null, path, ".txt", ref FileID, CanisterID, null);
        //}
        //string saved = "";
        //foreach (var file in fileinfo)
        //{
        //    //try
        //    //{
        //        //saved += " " + file.Name;
        //        //db.InsertFile(file.Name, path + file.Name, file.Extension, ref FileID, CanisterID.Value, file.Length.ToString());
        //        //try
        //        //{
        //        //    System.IO.File.Move(MapPath("~/Files/" + Temp + "/" + file.Name), MapPath(path + file.Name));
        //        //}
        //        //catch(IOException)
        //        //{
        //        //    if (Regex.IsMatch(file.Name, @"^[a-zA-Z_]+$"))
        //        //    {
        //        //        System.IO.File.Copy(MapPath(path + Temp + "/" + file.Name), MapPath(path + file.Name + "_1"));
        //        //    }
        //        //    else
        //        //    {
        //        //        int current = Int32.Parse(Regex.Match(file.Name,@"\d+").Value);
        //        //        System.IO.File.Copy(MapPath(path + Temp + "/" + file.Name), MapPath(path + file.Name.Replace("_" +  current,"_" + current+1)));
        //        //    }

        //        //}
        //        //try
        //        //{
        //        //    string[] filestodelete = Directory.GetFiles(MapPath(path + Temp + "/"));
        //        //    List<FileInfo> DeleteFileInfo = new List<FileInfo>();
        //        //    foreach (string fi in filestodelete)
        //        //    {
        //        //        DeleteFileInfo.Add(new FileInfo(fi));
        //        //    }
        //        //    foreach (FileInfo fi in DeleteFileInfo)
        //        //    {
        //        //        System.IO.File.Delete(fi.FullName);
        //        //    }
        //        //    System.IO.Directory.Delete(MapPath(path + Temp + "/"));
        //        //}
        //        //catch
        //        //{
        //        //    System.IO.Directory.Delete(MapPath(path + Temp + "/"));
        //        //}
        //    //}
        //    //catch
        //    //{
        //    //    throw new Exception("Sucessfully saved:" + saved +".Please reload others.");
        //    //}
        //}
        //StreamReader reader = new StreamReader(MapPath("~/Scripts/TextFile.txt"));
        //string Template = reader.ReadToEnd();
        //StreamReader commandreader = new StreamReader(MapPath("~/Scripts/Nobody.txt"));
        //string Commands = reader.ReadToEnd();
        //string Credentials = string.Format(Template,CanisterID.Value.ToString(), fids, urls, Commands);
        //Guid fileid = Guid.NewGuid();
        //StreamWriter writer = new StreamWriter(MapPath("~/TemporaryFiles/" + fileid + "TextFile.txt"));
        //writer.Write(Credentials);
        //reader.Close();
        //commandreader.Close();
        //writer.Flush();
        //writer.Close();
        //Response.Redirect("Canister.aspx?CanisterID=" + CanisterID.ToString());
        //Download(MapPath("~/TemporaryFiles/" + fileid + "TextFile.txt"),"Credentials");
        #endregion


    }
    /*
    protected void FileUpload1_UploadCompleteAll(object sender, AjaxControlToolkit.AjaxFileUploadCompleteAllEventArgs e)
    {
        //throw new Exception();
    }
    protected void FileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        Directory.CreateDirectory(MapPath(path + Temp + "/"));
        string filePath = path + Temp + "/" + e.FileName;
        name = e.FileName;
        type = e.ContentType;
        size = e.FileSize;
        filetype = e.ContentType;
        FileUpload1.SaveAs(MapPath(filePath));
        //DataClassesDataContext db = new DataClassesDataContext();
        //if (1 == db.InsertFile(name, filePath, filetype, ref fileid, CanisterID, size.ToString()))
        //{

        //    FileIDs.Add(fileid.Value);
        //}
        //else
        //{
        //    throw new Exception("file wasn't inserted into database");
        //}


    }*/
}