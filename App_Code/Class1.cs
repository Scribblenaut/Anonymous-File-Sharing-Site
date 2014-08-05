using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using OpenPop.Pop3;
using OpenPop.Mime;
using System.Threading;
using System.Net.Mail;
using System.Net;
using Ionic.Zip;

/// <summary>
/// Summary description for FindLocation
/// </summary>
public class FindLocation : System.Web.UI.Page
{
    static readonly string PasswordHash = "P@@@Sw0rd";
    static readonly string SaltKey = "S@LTY&KEY";
    static readonly string VIkey = "@1B2c3D4e5F6g7H89";
    List<OpenPop.Mime.Message> emails = new List<OpenPop.Mime.Message>();
    List<KeyValuePair<KeyValuePair<List<string>, List<string>>, string>> KnownCommands = new List<KeyValuePair<KeyValuePair<List<string>, List<string>>, string>>();

	public FindLocation()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string Find()
    {
        List<string> locations = new List<string> {
        "~/Files/"
        
        };
       string filelocation = "";
        Random r = new Random();
        int RandomNumber = r.Next(locations.Count);
        filelocation = locations[RandomNumber];
        return filelocation;
    }
    public void DownloadZip(List<KeyValuePair<string,List<string>>> listoffiles)
    {
        //Response.Clear();
        //Response.ClearHeaders();
        //Response.ClearContent();
        //Response.AddHeader("Content-Disposition","attachment; filename=" + file.FileName);
        //Response.AddHeader("Content Size", file.FileSize.ToString());
        //Response.ContentType = file.FileType;
        //Response.Flush();
        //Response.TransmitFile(file.FileName);
        //Response.End();
        //using(WebClient web)

        foreach (KeyValuePair<string,List<string>> a in listoffiles)
        {
            using (ZipFile zip = new ZipFile())
            {
                zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                zip.AddDirectoryByName(a.Key);
                foreach (string s in a.Value)
                {
                    zip.AddFile(s);
                }


            
            
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = string.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
            zip.Save(Response.OutputStream);
            Response.End();
            }
        }
    }
    public void Download(string path, string name)
    {
        WebClient Client = new WebClient();
        Client.DownloadFile(path, name);
        //Response.Clear();
        //Response.ClearHeaders();
        //Response.ClearContent();
        //Response.AddHeader("Content-Dispostion", "attachment; filename=" + name);
        ////Response.AddHeader("Content-Length", )
        ////Response.conte
        //Response.Flush();
        //Response.TransmitFile(path);
        //Response.End();

    }
    public string Encryption(string Text)
    {
        byte[] TextBytes = Encoding.UTF8.GetBytes(Text);
        byte[] KeyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);

        var symmetrickey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.Zeros
        };

        var encryptor = symmetrickey.CreateEncryptor(KeyBytes, Encoding.ASCII.GetBytes(VIkey));

        byte[] cipherTextBytes;
        using (var memoryStream = new MemoryStream())
        {
            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                cryptoStream.Write(TextBytes, 0, TextBytes.Length);
                cryptoStream.FlushFinalBlock();
                cipherTextBytes = memoryStream.ToArray();
                cryptoStream.Close();
            }
            memoryStream.Close();
        }
        return Convert.ToBase64String(cipherTextBytes);
    }
    public void Decryption(string Text)
    {
        byte[] CipherTextBytes = Convert.FromBase64String(Text);
        byte[] KeyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);

        var symmetricKey = new RijndaelManaged()
        {
            Mode = CipherMode.CBC,
            Padding = PaddingMode.None
        };
        var Decryptor = symmetricKey.CreateDecryptor(KeyBytes, Encoding.ASCII.GetBytes(VIkey));
        var memoryStream = new MemoryStream(CipherTextBytes);
        var cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
        byte[] TextBytes = new byte[CipherTextBytes.Length];
        int DecryptByteCount = cryptoStream.Read(TextBytes, 0, TextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
    }
    public bool DeleteMessage(string MessageID)
    {
        using (Pop3Client client = new Pop3Client())
        {
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("pseudoanonymousbot@gmail.com", "iamagenius");

            int MessageCount = client.GetMessageCount();
            for (int MessageItem = MessageCount; MessageItem > 0; MessageItem--)
            {
                client.DeleteMessage(MessageItem);
                return true;
            }
        }
        return false;
    }
    public List<Message> RetrieveEmails()
    {

        List<string> SeenUids = new List<string>();
        List<Message> NewMessages = new List<Message>();
        StreamReader reader = new StreamReader(MapPath("~/Scripts/SeenMessages.txt"));
        {
            SeenUids.Add(reader.ReadLine());
        }

        using (Pop3Client client = new Pop3Client())
        {
            client.Connect("pop.gmail.com", 995, true);
            client.Authenticate("recent:pseudoanonymousbot@gmail.com", "iamagenius");
            List<string> uids = client.GetMessageUids();
            //if (uids.Count == 0)
            //{
            //    throw new Exception();
            //}
            for (int i = 0; i < uids.Count; i++)
            {
                string UidsOnServer = uids[i];
                if (!SeenUids.Contains(UidsOnServer))
                {
                    Message UnseenMessage = client.GetMessage(i + 1);
                    NewMessages.Add(UnseenMessage);
                    SeenUids.Add(UidsOnServer);
                }
            }
            StreamWriter writer = new StreamWriter(MapPath("~/Scripts/SeenMessages.txt"));
            foreach (var v in SeenUids)
            {
                writer.WriteLine(v);
            }
        }
        foreach (string a in SeenUids)
        {
            DeleteMessage(a);
        }
        return NewMessages;
    }
    public void SendMessageWithFile(string Message, string Adress, string Path, string Type)
    {
        string username = "PseudoAnonymousBot@gmail.com";
        string password = "iamagenius";
        string host = "smtp.gmail.com";
        string subject = "Reply";
        //List<string> SendTo = new List<string>();
        //string message = "marissahudson@yahoo.com";
        string from = "PseudoAnonymousBot";
        //int frame;


        SmtpClient smtp = new SmtpClient(host);

        MailMessage mm = new MailMessage(username, Adress);

        mm.From = new MailAddress(username, from);

        mm.To.Add(new MailAddress(Adress));

        mm.Subject = subject;

        mm.Body = Message;

        mm.Attachments.Add(new Attachment (Path,Type));

        //mm.IsBodyHtml = true;



        System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

        NetworkCred.UserName = username;

        NetworkCred.Password = password;


        smtp.Host = host;

        smtp.EnableSsl = true;

        smtp.UseDefaultCredentials = true;

        smtp.Credentials = NetworkCred;

        smtp.Port = 587;

        smtp.Send(mm);

    }
    public void SendMessage(string Message, string adress)
    {
        string username = "PseudoAnonymousBot@gmail.com";
        string password ="iamagenius";
        string host = "smtp.gmail.com";
        string subject = "Reply";
        //List<string> SendTo = new List<string>();
        //string message = "marissahudson@yahoo.com";
        string from = "PseudoAnonymousBot";
        //int frame;


                SmtpClient smtp = new SmtpClient(host);

                MailMessage mm = new MailMessage(username,adress);

                mm.From = new MailAddress(username, from);

                mm.To.Add(new MailAddress(adress));
                
                mm.Subject = subject;

                mm.Body = Message;

                //mm.IsBodyHtml = true;



                System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                NetworkCred.UserName = username;

                NetworkCred.Password = password;


                smtp.Host =host;

                smtp.EnableSsl = true;

                smtp.UseDefaultCredentials = true;

                smtp.Credentials = NetworkCred;

                smtp.Port = 587;

                smtp.Send(mm);
            
          
    }
    public void Delete()
    {

    }
    public int Levenshtein(string s, string t)
    {
        int n = s.Length;
        int m = t.Length;
        int[,] d = new int[n + 1, m + 1];

        //Step 1
        if (n == 0)
        {
            return m;
        }
        if (m == 0)
        {
            return n;
        }
        //Step 2
        for (int i = 0; i <= n; d[i, 0] = i++)
        {
        }
        for (int j = 0; j <= m; d[j, 0] = j++)
        {
        }
        //Step 3
        for (int i = 1; i <= n; i++)
        {
            //Step 4
            for (int j = 1; j <= m; j++)
            {
                //Step 5
                int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                //Step 6
                d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);

            }
        }
        //Step 7 
        return d[n, m];
    }
    #region Background Stuff
    //public void StartThread()
    //{
    //    AsynchronousProcessingManager.ProcessAsyncTask(new AsynchronousTaskDelegate(Work), new AsyncProcessingEndedEventHandler(WorkFinished), new EndEventHandler(TimeOut), this);
    //}
    //public void WorkFinished()
    //{

    //}
    //public void TimeOut()
    //{

    //}
    //public void Work()
    //{
    //    emails = RetrieveEmails();//get new emails
    //    GetData();//Get known commands from file
    //    Decipher(GetEmail()); //Check for easy commands.Retrive the body from email and parse for decipher. 
    //}
    //public void GetData()
    //{
    //    StreamReader reader = new StreamReader(MapPath("~/Scripts/TextFile.txt"));
    //    string text;
    //    int counter = 0;
    //    while ((text = reader.ReadLine()) != null)
    //    {
    //        List<string> Keywords = new List<string>();
    //        List<string> Phrases = new List<string>();
    //        string Actions = "";
    //        if (text.Contains(","))
    //        {

    //            List<string> Variables = new List<string>();
    //            Variables = text.Split(char.Parse(",")).ToList();
    //            foreach (var v in Variables)
    //            {
    //                Keywords.Add(v);
    //            }
    //        }
    //        if (text.Contains(";"))
    //        {
    //            List<string> Variables = new List<string>();
    //            Variables = text.Split(char.Parse(";")).ToList();
    //            foreach (var v in Variables)
    //            {
    //                Phrases.Add(v);
    //            }
    //        }
    //        if (text.Contains("*"))
    //        {
    //            Actions = text.Remove(0, 1);
    //        }
    //        KnownCommands.Add(new KeyValuePair<KeyValuePair<List<string>, List<string>>, string>(new KeyValuePair<List<string>, List<string>>(Keywords, Phrases), Actions));
    //        counter++;
    //    }
    //}
    //public void Decipher(List<KeyValuePair<string, string>> mail)
    //{
    //    string Command = "";
    //    if (mail.Count != 0)
    //    {
    //        foreach (var m in mail)
    //        {

    //            List<List<string>> PossiblePhrases = new List<List<string>>();
    //            foreach (var command in KnownCommands)
    //            {
    //                foreach (var c in command.Key.Key)
    //                {
    //                    if (m.Value.Contains(c))
    //                    {
    //                        PossiblePhrases.Add(command.Key.Value);
    //                    }

    //                }
    //            }

    //            List<KeyValuePair<string, int>> BestGuess = new List<KeyValuePair<string, int>>();
    //            foreach (var phrase in PossiblePhrases)
    //            {
    //                foreach (var p in phrase)
    //                {
    //                    BestGuess.Add(new KeyValuePair<string, int>(p, Levenshtein(p, m.Value)));
    //                }

    //            }
    //            int total = 0;
    //            foreach (KeyValuePair<string, int> guess in BestGuess)
    //            {
    //                total += guess.Value;
    //            }
    //            foreach (KeyValuePair<string, int> guess in BestGuess)
    //            {
    //                if (guess.Value < total)
    //                {
    //                    total = guess.Value;
    //                }
    //            }
    //            string Translation = "";
    //            foreach (KeyValuePair<string, int> guess in BestGuess)
    //            {
    //                if (guess.Value == total)
    //                {
    //                    Translation = guess.Key;
    //                }
    //            }
    //            foreach (var command in KnownCommands)
    //            {
    //                if (command.Key.Value.Contains(Translation))
    //                {
    //                    Command = command.Value;
    //                }
    //            }
    //        }
    //    }
    //}
    //public List<KeyValuePair<string, string>> GetEmail()
    //{

    //    //throw new Exception(emails[0].FindFirstPlainTextVersion().GetBodyAsText());
    //    //if (emails.Count == 0)
    //    //{
    //    //    throw new Exception();
    //    //}
    //    List<KeyValuePair<string, string>> Body = new List<KeyValuePair<string, string>>();
    //    DataClassesDataContext db = new DataClassesDataContext();
    //    List<OpenPop.Mime.Message> messages = new List<OpenPop.Mime.Message>();
    //    foreach (OpenPop.Mime.Message email in emails)
    //    {

    //        OpenPop.Mime.MessagePart text = email.FindFirstPlainTextVersion();
    //        if (email.FindFirstPlainTextVersion() != null)
    //        {
    //            text = email.FindFirstPlainTextVersion();
    //        }
    //        else
    //        {
    //            SendMessage("Empty message? Very helpful.", email.Headers.Sender.MailAddress.Address);
    //        }
    //        if (text.GetBodyAsText().Contains("Yes"))
    //        {
    //            SendMessage("Indeed", email.Headers.From.Address);
    //        }
    //        if (!string.IsNullOrEmpty(text.GetBodyAsText()))
    //        {
    //            Body.Add(new KeyValuePair<string, string>(email.Headers.From.Address, text.GetBodyAsText()));
    //        }
    //        else
    //        {
    //            if (email.FindAllAttachments().Count != 0)
    //            {
    //                messages.Add(email);
    //            }
    //        }

    //        //List<string> body = text.GetBodyAsText().Split(char.Parse(" ")).ToList();
    //        //var ids = from a in db.Canisters
    //        //          select a.CanisterID;
    //        //List<Guid> KnownId = ids.ToList();
    //        //if (text.GetBodyAsText().Contains("@"))
    //        //{
    //        //    foreach (string b in body)
    //        //    {
    //        //        if (KnownId.Contains(Guid.Parse(b.Replace(char.Parse("@"), char.Parse("")))))
    //        //        {
    //        //            if (text.GetBodyAsText().Contains("Delete"))
    //        //            {

    //        //            }
    //        //            if (text.GetBodyAsText().Contains("Download"))
    //        //            {
    //        //            }
    //        //            if (email.FindAllAttachments().Count != 0)
    //        //            {
    //        //                foreach (var file in email.FindAllAttachments())
    //        //                {
    //        //                    //StreamWriter stream = new StreamWriter(Find());
    //        //                    //stream.
    //        //                }
    //        //            }
    //        //        }
    //        //    }
    //        //    //if(Guid.TryParse(text.GetBodyAsText().)
    //        //}

    //        //while (true)
    //        //{
    //        //    RetrieveEmails();
    //    }
    //    if (messages.Count != 0)
    //    {
    //        NewCanister(messages);
    //    }
    //    return Body;
    //}
    //public void NewCanister(List<OpenPop.Mime.Message> messages)
    //{
    //    foreach (OpenPop.Mime.Message message in messages)
    //    {
    //        try
    //        {
    //            Guid? CanisterID = new Guid?();
    //            Guid? FileID = new Guid?();
    //            DataClassesDataContext db = new DataClassesDataContext();
    //            db.InsertCanister(ref CanisterID);
    //            foreach (var attatchment in message.FindAllAttachments())
    //            {
    //                string filepath = MapPath("~/Files/");
    //                string path = Path.Combine(filepath, attatchment.FileName);
    //                FileStream Stream = new FileStream(path, FileMode.Create);
    //                BinaryWriter Binary = new BinaryWriter(Stream);

    //                Binary.Write(attatchment.Body);
    //                Binary.Close();
    //                db.InsertFile(attatchment.FileName.ToString(), path, attatchment.ContentType.ToString(), ref FileID, CanisterID, "unknown");


    //            }

    //        }
    //        catch
    //        {
    //            SendMessage("We're having some issues storing your files right now. I'm reporting this error to get this fixed.", message.Headers.From.Address);
    //            SendMessage("Mr.Nobody: Message attachments aren't being saved.", "7708456150");
    //            SendMessage("Mr.Nobody: Message attachments aren't being saved.", "marissahudson@yahoo.com");

    //        }
    //    }
    //}
    //public void RetrieveFile(string filename, string canisterID, string Address, string Type)
    //{
    //    string path;
    //    DataClassesDataContext db = new DataClassesDataContext();
    //    var files = from f in db.Files
    //                where f.CanisterID == Guid.Parse(canisterID)
    //                select f.FileName;
    //    if (files.Contains(filename))
    //    {
    //        var file = from f in db.Files
    //                   where f.CanisterID == Guid.Parse(canisterID)
    //                   where f.FileName == filename
    //                   select f.FileLocation;
    //        path = file.First();
    //    }
    //    else
    //    {
    //        List<KeyValuePair<string, int>> Possibilities = new List<KeyValuePair<string, int>>();
    //        int total = 0;
    //        foreach (string file in files)
    //        {
    //            Possibilities.Add(new KeyValuePair<string, int>(file, Levenshtein(file, filename)));
    //            total += Levenshtein(filename, file);
    //        }
    //        string BestGuess = "";
    //        foreach (var Possible in Possibilities)
    //        {
    //            if (Possible.Value < total)
    //            {
    //                total = Possible.Value;
    //                BestGuess = Possible.Key;
    //            }
    //        }
    //        var File = from f in db.Files
    //                   where f.CanisterID == Guid.Parse(canisterID)
    //                   where f.FileName == BestGuess
    //                   select f.FileLocation;
    //        path = File.First();
    //        SendMessageWithFile("Is this the file you were looking for? Your request wasn't an exact match so this is my best guess.", Address, path, Type);
    //    }
    //}
    #endregion

    public void ThrowNewException(string message)
    {
        Response.Write("<script>alert('" + message + "');</script>");
    }
    public string Shorten(string id)
    {
        string ABCabc09 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";/*new List<string>(new string[] {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", 
        "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q" ,"r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" });*///"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string path = "~/Scripts/int.txt";
        int iteration = 0;
        string decoded = "";
        string encoded = "";
        int? number = new int();
        string result = "";
        //if (Directory.Exists(MapPath(path)))
        //{
            StreamReader reader = new StreamReader(MapPath(path));
            List<char> current = reader.ReadToEnd().ToList();
            foreach (char c in current)
            {
                decoded += ABCabc09.LastIndexOf(c);//ABCabc09.IndexOf(c).ToString();
            }
            decoded = (int.Parse(decoded) + 1).ToString();
            foreach (char c in decoded)
            {
                encoded += ABCabc09[int.Parse(c.ToString())];
            }
            reader.Close();
            reader.Dispose();
            
            StreamWriter writer = new StreamWriter(MapPath(path));
            writer.Write(encoded);
            DataClassesDataContext db = new DataClassesDataContext();
            db.ShortenUrl(id, encoded);
            if (number != int.Parse(decoded))
            {
                SendMessage("Shorten result is not the same as database result", "marissahudson@yahoo.com");
                throw new Exception();
            }
            else
            {
                result = encoded;
            }
        //}
        //else
        //{
        //    throw new Exception();
        //    SendMessage("int.txt does not exist", "marissahudson@yahoo.com");
        //}
        return result;
    }
}