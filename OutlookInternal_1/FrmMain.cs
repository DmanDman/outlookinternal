using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;


namespace OutlookInternal_1
{
    public partial class FrmMain : Form
    {       
        public static Outlook.Application app = null;
        public static Outlook._NameSpace ns = null;
        public static Outlook.MailItem item = null;
        public static Outlook.MAPIFolder inboxFolder = null;        
        public static Outlook.MAPIFolder destFolder = null;    
        public static Outlook.Items items; 


        public class GlobalVar
        {
            public static string sAppPath = Directory.GetCurrentDirectory();            
            public static string FolderName = null;
            public static string[] Folders = null;
            public static string PicPath = null;
        }

        public FrmMain()
        {
            InitializeComponent();
            InitializeVariables();
            InitializeFolders();
            SetFolderStructure();
        }       

        static void ReadMail()
        {                                    
            Console.WriteLine("Folder Name: {0}, EntryId: {1}", inboxFolder.Name, inboxFolder.EntryID);
            Console.WriteLine("Num Items: {0}", inboxFolder.Items.Count.ToString());

            //System.IO.StreamWriter strm = new System.IO.StreamWriter("C:/Test/Inbox.txt");
            for (int counter = 1; counter <= inboxFolder.Items.Count; counter++)
            {
                Console.Write(inboxFolder.Items.Count + " " + counter);
                item = ( Outlook.MailItem )inboxFolder.Items[counter];
                Console.WriteLine("Item: {0}", counter.ToString());
                Console.WriteLine("Subject: {0}", item.Subject);
                Console.WriteLine("Sent: {0} {1}", item.SentOn.ToLongDateString(), item.SentOn.ToLongTimeString());
                Console.WriteLine("Sendername: {0}", item.SenderName);
                Console.WriteLine("Body: {0}", item.Body);
                //strm.WriteLine(counter.ToString() + "," + item.Subject + "," + item.SentOn.ToShortDateString() + "," + item.SenderName);
            }
            //strm.Close();

            for ( int counter = 1; counter <= inboxFolder.Items.Count; counter++ ) 
            {
                item = ( Outlook.MailItem )inboxFolder.Items[counter];                
            }
        }

        public void Items_AddItem( object Item  )
        {
            //  Loop through Inbox
            for ( int counter = 1; counter <= inboxFolder.Items.Count; counter++ )
            {
                item = ( Outlook.MailItem )inboxFolder.Items[counter];

                Outlook.MailItem mail = item;

                //  If valid surgeon name
                if ( GlobalVar.Folders.Contains( item.Subject.ToLower() ))
                {
                    destFolder = inboxFolder.Folders[item.Subject.ToLower()];
                    
                    GlobalVar.FolderName = GlobalVar.Folders[GlobalVar.Folders.ToList().IndexOf( item.Subject.ToLower() )];

                    //  Move attachment to Widows folder
                    Move_Attachment( mail, GlobalVar.FolderName.ToLower() );

                    //  Move email to Outlook folder
                    item.Move( destFolder );
                }
            }
        }

        public void Items_AddItemNoItem()
        {
            //  Loop though entire inbox
            for ( int counter = 1; counter <= inboxFolder.Items.Count; counter++ )
            {
                //  Get mail item
                item = ( Outlook.MailItem )inboxFolder.Items[counter];

                Outlook.MailItem mail = item;

                //  Subject must equal valid surgeon name               
                if ( GlobalVar.Folders.Contains( item.Subject.ToLower() ))
                {
                    destFolder = inboxFolder.Folders[item.Subject.ToLower()];
                    
                    GlobalVar.FolderName = GlobalVar.Folders[GlobalVar.Folders.ToList().IndexOf( item.Subject.ToLower() )];

                    //  Move picture to windows folder
                    Move_Attachment( mail, GlobalVar.FolderName.ToLower() );

                    //  Move email to surgeon's folder
                    item.Move( destFolder );
                }
            }
        }

        public void Move_Attachment( Outlook.MailItem Email, string FolderName )
        {
            //  Emails with attachment
            if ( Email.Attachments.Count > 0 )
            {
                for ( int i = 1; i <= Email.Attachments.Count; i++ )
                {
                    string filename = Email.Attachments[i].FileName;
                    string subjectName = Email.Subject.ToLower();
                    string fullFileName = GlobalVar.PicPath + @"\" + FolderName + @"\" + Email.Attachments[i].FileName;

                    if ( File_Exists( fullFileName ))
                    {
                        Write_File_New_Name( Email, fullFileName );
                    }
                    else
                    {
                        //  Move to folder
                        Email.Attachments[i].SaveAsFile( fullFileName );
                    }                    
                }
            }
        }

        public bool File_Exists( string path )
        {            
            if ( File.Exists( path ))
            {
                return true;
            }
            else
            {
                return false;
            }            
        }

        public void Write_File_New_Name( Outlook.MailItem Email, string fullPath )
        {
            int count = 1;

            string fileNameOnly = Path.GetFileNameWithoutExtension( fullPath );
            string extension = Path.GetExtension( fullPath );
            string path = Path.GetDirectoryName( fullPath) ;
            string newFullPath = fullPath;

            while ( File.Exists( newFullPath ))
            {
                string tempFileName = string.Format( "{0}({1})", fileNameOnly, count++ );
                newFullPath = Path.Combine( path, tempFileName + extension );
            }

            //  Move to folder
            Email.Attachments[1].SaveAsFile( newFullPath );
        }

        private void InitializeVariables()
        {           
            app = new Outlook.Application();    

            ns = app.GetNamespace( "MAPI" );            

            inboxFolder = ns.GetDefaultFolder( Outlook.OlDefaultFolders.olFolderInbox );

            items = inboxFolder.Items;

            //  Add event handler when new mail arrives in Inbox
            items.ItemAdd +=
                new Outlook.ItemsEvents_ItemAddEventHandler( Items_AddItem );
        }

        private void InitializeFolders()
        {
            // Read doctors file
            GlobalVar.Folders = File.ReadAllLines( GlobalVar.sAppPath + "\\folders.txt" );

            for ( int i = 0; i <= GlobalVar.Folders.Count() - 1; i++ )
            {
                GlobalVar.Folders[i] = GlobalVar.Folders[i].ToLower();
            }

            // Read picture path file
            GlobalVar.PicPath = File.ReadAllText( GlobalVar.sAppPath + "\\picpath.txt" );                   
        }

        private void SetFolderStructure()
        {
            // Add folder structure               
            for (int i = 0; i <= GlobalVar.Folders.Count() - 1; i++)
            {
                //  Outlook folder                
                try { inboxFolder.Folders.Add(GlobalVar.Folders[i]); }
                catch { }   // Folder already exists

                //  Windows folder                                
                try { Directory.CreateDirectory(GlobalVar.PicPath + @"\" + GlobalVar.Folders[i]); }
                catch { }   // Subdirectory alread exists
            }
        }

        private void BtnMoveInbox_Click( object sender, EventArgs e )
        {
            //  Process Inbox
            Items_AddItemNoItem();
        }
               
        public void BtnAddDr_Click( object sender, EventArgs e )
        {
            AddDr();
        }

        public void AddDr()
        {
            //  If doctor is valid, add folder structure to Outlook and Windows
            if ( IsDrValid() )
            {
                File.AppendAllText( GlobalVar.sAppPath + "\\folders.txt", TxtNewDr.Text.Trim().ToLower() + Environment.NewLine );

                InitializeFolders();

                SetFolderStructure();

                TxtNewDr.Text = "";

                MessageBox.Show( "Doctor successfully added.",
                                 "Doctor Added",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information );
            }
        }

        private bool IsDrValid()
        {
            bool result = true;
            string DisplayMsg = null;
            string DisplayTitle = null;

            //  Can not add blank doctor
            if ( TxtNewDr.Text.Trim() == "" || TxtNewDr.Text.Length == 0 )
            {
                DisplayMsg = "Doctor can not be blank.";
                DisplayTitle = "Blank Doctor Name";               
                result = false;
            }

            //  Can not be duplicate
            if ( GlobalVar.Folders.Contains( TxtNewDr.Text.ToLower()) )
            {
                DisplayMsg = "Doctor name already exists.";
                DisplayTitle = "Doctor Exists";                                                 
                result = false;
            }

            //  Can not be numeric
            var isNumeric = int.TryParse( TxtNewDr.Text.Trim(), out int n );

            if ( isNumeric )
            {
                DisplayMsg = "Doctor name can not be numeric.";
                DisplayTitle = "Numeric Doctor Name";
                result = false;
            }

            //  No spaces
            if ( TxtNewDr.Text.Trim().Contains(" ") )
            {
                DisplayMsg = "Doctor name can not contain spaces.";
                DisplayTitle = "Spaes in Doctor Name";                                
                result = false;
            }

            if ( result == false )
            {
                TxtNewDr.Focus();
                TxtNewDr.SelectAll();

                MessageBox.Show( DisplayMsg, DisplayTitle, MessageBoxButtons.OK, MessageBoxIcon.Error );
            }

            return result;
        }

        private void TxtNewDr_KeyPress( object sender, KeyPressEventArgs e )
        {
            //  Press Enter Key to add doctor
            if ( e.KeyChar == Convert.ToChar( Keys.Return ))
            {
                AddDr();
            }
        }

        private void FrmMain_Resize( object sender, EventArgs e )
        {
            if ( WindowState == FormWindowState.Minimized )
            {
                this.Hide();
                this.ShowInTaskbar = false;
            }
        }

        private void TSHide_Click( object sender, EventArgs e )
        {
            this.Hide();
        }

        private void TSShow_Click( object sender, EventArgs e )
        {
            //  Show window
            ShowWindow();
        }

        private void TSProcessInbox_Click( object sender, EventArgs e )
        {
            //  Process Inbox
            Items_AddItemNoItem();
        }

        private void NotifyIcon1_MouseDoubleClick( object sender, MouseEventArgs e )
        {
            //  Show window
            ShowWindow();
        }

        private void ShowWindow()
        {
            //  Show window
            this.Show();
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
        }
    }
}
