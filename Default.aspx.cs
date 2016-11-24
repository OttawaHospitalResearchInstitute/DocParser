using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using Word = Microsoft.Office.Interop.Word;


namespace DocParser
{
    public partial class Default : System.Web.UI.Page
    {
        SQLHelper helper = new SQLHelper();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnParse_Click(object sender, EventArgs e)
        {
            String filepath = Server.MapPath("\\Upload");
            HttpFileCollection uploadFiles = Request.Files;

            for (int i = 0; i < uploadFiles.Count; i++)
            {
                HttpPostedFile docFile = uploadFiles[i];

                object path = docFile.FileName;
                Word.Application app = new Word.Application();
                Word.Document doc;
                object missing = Type.Missing;
                object readOnly = true;
                try
                {
                    doc = app.Documents.Open(ref path, ref missing, ref readOnly, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                    string text = doc.Content.Text + "\n";

                    //Parse Data
                    String clientCode, studyId, OHREB, studyDate = "";
                    clientCode = getSubstring(text, "Client Code: ");
                    studyId = getSubstring(text, "Study Identifier: ");
                    if (text.Contains("OHREB#: "))
                    {
                        OHREB = getSubstring(text, "OHREB#: ");
                    }
                    else
                    {
                        OHREB = getSubstring(text, "OCREB #: ");
                    }                                       
                    studyDate = getSubstring(text, "Study Initiation Date: ");

                    //fix OHREB
                    int count = OHREB.Split('-').Length - 1;
                    if ( count > 1)
                    {                        
                        OHREB = OHREB.Remove(OHREB.IndexOf("-"), 1);
                    }

                    //get data from DB
                    String status, costcentre = "";
                    status = helper.getOHREBStatus(OHREB);
                    costcentre = helper.getCC(OHREB);

                    //insert data into DB
                    helper.insertParse(clientCode, studyId, OHREB, status, studyDate, costcentre);                    
                }
                catch
                {
                    throw;
                }
                finally
                {
                    object saveChanges = Word.WdSaveOptions.wdDoNotSaveChanges;
                    app.Quit(ref saveChanges, ref missing, ref missing);
                }
            }

            lblMessage.Text = "You have successfully transfered " + uploadFiles.Count + " files.";
        }

        public String getSubstring(String input, String item)
        {
            if (input.Contains(item))
            {
                int Start = input.IndexOf(item, 0) + item.Length;
                int End = input.IndexOf("\r", Start);
                return input.Substring(Start, End - Start);
            }
            else
            {
                return "";
            }
        }

    }
}