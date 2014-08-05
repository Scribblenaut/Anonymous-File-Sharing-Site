using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Default2 : FindLocation
{
    List<OpenPop.Mime.Message> emails = new List<OpenPop.Mime.Message>();
    List<KeyValuePair<KeyValuePair<List<string>, List<string>>, string>> KnownCommands = new List<KeyValuePair<KeyValuePair<List<string>, List<string>>, string>>();

    protected void Page_Load(object sender, EventArgs e)
    {
        Work();
        //AsynchronousProcessingManager.ProcessAsyncTask(new AsynchronousTaskDelegate(Work), new AsyncProcessingEndedEventHandler(Ended), new EndEventHandler(TimeOut), this);
    }
    private void Work()
    {
        //AsyncResultObject ARO = new AsyncResultObject();
        //try
        //{
            while (true)
            {
                emails = RetrieveEmails();//get new emails
                GetData();//Get known commands from file
                Decipher(GetEmail()); //Check for easy commands.Retrive the body from email and parse for decipher. 
                //ARO.Result = "fdafa";
            }
        //}
        //catch(Exception e)
        //{
        //    ARO.Ex = e;
        //}
        //return ARO;
    }
    //private void Ended(object sender, AsyncEndedEventArgs e)
    //{
    //    AsyncResultObject ARO = e.ResultObject;
    //    if (ARO.Ex != null)
    //    {
    //        //throw new Exception("Background Worker Exception");
    //        SendMessage("Background worker error in Ended", "marissahudson@yahoo.com");
    //    }
    //    else
    //    {

    //    }
    //}
    //private void TimeOut(IAsyncResult ar)
    //{
    //    SendMessage("The background worker timed out!!!", "marissahudson@yahoo.com");
    //}
    public void GetData()
    {
        StreamReader reader = new StreamReader(MapPath("~/Scripts/TextFile.txt"));
        string text;
        int counter = 0;
        while ((text = reader.ReadLine()) != null)
        {
            List<string> Keywords = new List<string>();
            List<string> Phrases = new List<string>();
            string Actions = "";
            if (text.Contains(","))
            {

                List<string> Variables = new List<string>();
                Variables = text.Split(char.Parse(",")).ToList();
                foreach (var v in Variables)
                {
                    Keywords.Add(v);
                }
            }
            if (text.Contains(";"))
            {
                List<string> Variables = new List<string>();
                Variables = text.Split(char.Parse(";")).ToList();
                foreach (var v in Variables)
                {
                    Phrases.Add(v);
                }
            }
            if (text.Contains("*"))
            {
                Actions = text.Remove(0, 1);
            }
            KnownCommands.Add(new KeyValuePair<KeyValuePair<List<string>, List<string>>, string>(new KeyValuePair<List<string>, List<string>>(Keywords, Phrases), Actions));
            counter++;
        }
    }
    public void Decipher(List<KeyValuePair<string, string>> mail)
    {
        string Command = "";
        if (mail.Count != 0)
        {
            foreach (var m in mail)
            {

                List<List<string>> PossiblePhrases = new List<List<string>>();
                foreach (var command in KnownCommands)
                {
                    foreach (var c in command.Key.Key)
                    {
                        if (m.Value.Contains(c))
                        {
                            PossiblePhrases.Add(command.Key.Value);
                        }

                    }
                }

                List<KeyValuePair<string, int>> BestGuess = new List<KeyValuePair<string, int>>();
                foreach (var phrase in PossiblePhrases)
                {
                    foreach (var p in phrase)
                    {
                        BestGuess.Add(new KeyValuePair<string, int>(p, Levenshtein(p, m.Value)));
                    }

                }
                int total = 0;
                foreach (KeyValuePair<string, int> guess in BestGuess)
                {
                    total += guess.Value;
                }
                foreach (KeyValuePair<string, int> guess in BestGuess)
                {
                    if (guess.Value < total)
                    {
                        total = guess.Value;
                    }
                }
                string Translation = "";
                foreach (KeyValuePair<string, int> guess in BestGuess)
                {
                    if (guess.Value == total)
                    {
                        Translation = guess.Key;
                    }
                }
                foreach (var command in KnownCommands)
                {
                    if (command.Key.Value.Contains(Translation))
                    {
                        Command = command.Value;
                    }
                }
            }
        }
    }
    public List<KeyValuePair<string, string>> GetEmail()
    {

        //throw new Exception(emails[0].FindFirstPlainTextVersion().GetBodyAsText());
        //if (emails.Count == 0)
        //{
        //    throw new Exception();
        //}
        List<KeyValuePair<string, string>> Body = new List<KeyValuePair<string, string>>();
        DataClassesDataContext db = new DataClassesDataContext();
        List<OpenPop.Mime.Message> messages = new List<OpenPop.Mime.Message>();
        foreach (OpenPop.Mime.Message email in emails)
        {

            OpenPop.Mime.MessagePart text = email.FindFirstPlainTextVersion();
            if (email.FindFirstPlainTextVersion() != null)
            {
                text = email.FindFirstPlainTextVersion();
            }
            else
            {
                try
                {
                    email.FindAllAttachments();
                }
                catch
                {
                    SendMessage("Empty message? Very helpful.", email.Headers.From.MailAddress.Address);// email.Headers.Sender.MailAddress.Address);
                }
            }
            if (text.GetBodyAsText().Contains("Yes"))
            {
                SendMessage("Indeed", email.Headers.From.Address);
            }
            if (!string.IsNullOrEmpty(text.GetBodyAsText()))
            {
                Body.Add(new KeyValuePair<string, string>(email.Headers.From.Address, text.GetBodyAsText()));
            }
            else
            {
                if (email.FindAllAttachments().Count != 0)
                {
                    messages.Add(email);
                }
            }

            //List<string> body = text.GetBodyAsText().Split(char.Parse(" ")).ToList();
            //var ids = from a in db.Canisters
            //          select a.CanisterID;
            //List<Guid> KnownId = ids.ToList();
            //if (text.GetBodyAsText().Contains("@"))
            //{
            //    foreach (string b in body)
            //    {
            //        if (KnownId.Contains(Guid.Parse(b.Replace(char.Parse("@"), char.Parse("")))))
            //        {
            //            if (text.GetBodyAsText().Contains("Delete"))
            //            {

            //            }
            //            if (text.GetBodyAsText().Contains("Download"))
            //            {
            //            }
            //            if (email.FindAllAttachments().Count != 0)
            //            {
            //                foreach (var file in email.FindAllAttachments())
            //                {
            //                    //StreamWriter stream = new StreamWriter(Find());
            //                    //stream.
            //                }
            //            }
            //        }
            //    }
            //    //if(Guid.TryParse(text.GetBodyAsText().)
            //}

            //while (true)
            //{
            //    RetrieveEmails();
        }
        if (messages.Count != 0)
        {
            NewCanister(messages);
        }
        return Body;
    }
    public void NewCanister(List<OpenPop.Mime.Message> messages)
    {
        foreach (OpenPop.Mime.Message message in messages)
        {
            try
            {
                Guid? CanisterID = new Guid?();
                Guid? FileID = new Guid?();
                DataClassesDataContext db = new DataClassesDataContext();
                db.InsertCanister(ref CanisterID);
                foreach (var attatchment in message.FindAllAttachments())
                {
                    string filepath = MapPath("~/Files/");
                    string path = Path.Combine(filepath, attatchment.FileName);
                    FileStream Stream = new FileStream(path, FileMode.Create);
                    BinaryWriter Binary = new BinaryWriter(Stream);

                    Binary.Write(attatchment.Body);
                    Binary.Close();
                    db.InsertFile(attatchment.FileName.ToString(), path, attatchment.ContentType.ToString(), ref FileID, CanisterID, "unknown");


                }

            }
            catch
            {
                SendMessage("We're having some issues storing your files right now. I'm reporting this error to get this fixed.", message.Headers.From.Address);
                SendMessage("Mr.Nobody: Message attachments aren't being saved.", "7708456150");
                SendMessage("Mr.Nobody: Message attachments aren't being saved.", "marissahudson@yahoo.com");

            }
        }
    }
    public void RetrieveFile(string filename, string canisterID, string Address, string Type)
    {
        string path;
        DataClassesDataContext db = new DataClassesDataContext();
        var files = from f in db.Files
                    where f.CanisterID == Guid.Parse(canisterID)
                    select f.FileName;
        if (files.Contains(filename))
        {
            var file = from f in db.Files
                       where f.CanisterID == Guid.Parse(canisterID)
                       where f.FileName == filename
                       select f.FileLocation;
            path = file.First();
        }
        else
        {
            List<KeyValuePair<string, int>> Possibilities = new List<KeyValuePair<string, int>>();
            int total = 0;
            foreach (string file in files)
            {
                Possibilities.Add(new KeyValuePair<string, int>(file, Levenshtein(file, filename)));
                total += Levenshtein(filename, file);
            }
            string BestGuess = "";
            foreach (var Possible in Possibilities)
            {
                if (Possible.Value < total)
                {
                    total = Possible.Value;
                    BestGuess = Possible.Key;
                }
            }
            var File = from f in db.Files
                       where f.CanisterID == Guid.Parse(canisterID)
                       where f.FileName == BestGuess
                       select f.FileLocation;
            path = File.First();
            SendMessageWithFile("Is this the file you were looking for? Your request wasn't an exact match so this is my best guess.", Address, path, Type);
        }
    }
}