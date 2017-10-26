using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DDPanBox;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Resources;
using PictureViewPlus.Properties;
using MetadataExtractor;
using System.Drawing.Imaging;

namespace PictureViewPlus
{
    public partial class Form1 : Form
    {

        Image img;
        string accfile;
        string lastfile;
        string nextfile;


        float aspectratio;


        private void load_pictureBox(string openFile)
        {
            if (openFile.EndsWith("CR2"))
            {
                img =convertCR2toJPG(openFile);

            }
            else
            {

                img = (Bitmap)Image.FromFile(openFile);

            }
            // pictureBox1.Load(openFile);
            picbox.Image = img;
            pictureBox1.Image = img;




            if (img.Height < img.Width)
            {
                aspectratio = (float)img.Height / (float)img.Width;
                this.Width = 1024;
                this.Height = (int)Math.Round(this.Width * aspectratio);
            }
            else
            {
                aspectratio = (float)img.Width / (float)img.Height;
                this.Height = 1024;
                this.Width = (int)Math.Round(this.Height * aspectratio);
            }
        }


        private Bitmap convertCR2toJPG(string openFile)
        {
            int nbRotated = 0;
            const int BUF_SIZE = 512 * 1024;

            byte[] buffer = new byte[BUF_SIZE];


            FileStream fi = new FileStream(openFile, FileMode.Open, FileAccess.Read,
                                               FileShare.Read, BUF_SIZE, FileOptions.None);
            // Start address is at offset 0x62, file size at 0x7A, orientation at 0x6E
            fi.Seek(0x62, SeekOrigin.Begin);
            BinaryReader br = new BinaryReader(fi);
            UInt32 jpgStartPosition = br.ReadUInt32();  // 62
            br.ReadUInt32();  // 66
            br.ReadUInt32();  // 6A
            UInt32 orientation = br.ReadUInt32() & 0x000000FF; // 6E
            br.ReadUInt32();  // 72
            br.ReadUInt32();  // 76
            Int32 fileSize = br.ReadInt32();  // 7A

            fi.Seek(jpgStartPosition, SeekOrigin.Begin);


            if (fi.ReadByte() != 0xFF || fi.ReadByte() != 0xD8)
            {
                MessageBox.Show(String.Format("{0}\nEmbedded JPG not recognized. File skipped.", openFile),
                    "Quick JPG from CR2", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                string baseName = openFile.Substring(0, openFile.Length - 4);



                string jpgName = baseName + ".jpg";




               Bitmap bitmap = new Bitmap(new PartialStream(fi, jpgStartPosition, fileSize));

                if (orientation == 6)
                    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);
                //else
                //    bitmap.RotateFlip(RotateFlipType.Rotate90FlipNone);

                EncoderParameters ep = new EncoderParameters(1);
                ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 90L);
                //bitmap.Save(jpgName, m_codecJpeg, ep);

                ++nbRotated;


                fi.Close();


                return bitmap;
            }
            return null;
        }

       static ImageCodecInfo m_codecJpeg = GetJpegCodec();

        private static ImageCodecInfo GetJpegCodec()
        {
            foreach (ImageCodecInfo c in ImageCodecInfo.GetImageEncoders())
            {
                if (c.CodecName.ToLower().Contains("jpeg")
                    || c.FilenameExtension.ToLower().Contains("*.jpg")
                    || c.FormatDescription.ToLower().Contains("jpeg")
                    || c.MimeType.ToLower().Contains("image/jpeg"))
                    return c;
            }

            return null;
        }



        public Form1(string openFile)
        {
            //try
            //{





            accfile = openFile;
            InitializeComponent();

            picbox.BackColor = Color.Black;
            pictureBox1.BackColor = Color.Black;

            picbox.Visible = false;
            pictureBox1.Visible = true;

            this.pictureBox1.MouseWheel += pictureBox1_MouseWheel;



            if (openFile != "")
            {
                load_pictureBox(openFile);
                getmetadata(openFile);
            }
            else
            {

                Bitmap img = new Bitmap(typeof(Form1), "start.jpg");


                if (img.Height < img.Width)
                {
                    aspectratio = (float)img.Height / (float)img.Width;
                    this.Width = 1024;
                    this.Height = (int)Math.Round(this.Width * aspectratio);
                }
                else
                {
                    aspectratio = (float)img.Width / (float)img.Height;
                    this.Height = 1024;
                    this.Width = (int)Math.Round(this.Height * aspectratio);
                }


                picbox.Image = img;
                pictureBox1.Image = img;
            }








            this.Focus();
            get_programm();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }


        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                previous();
            }
            if (e.Delta > 0)
            {
                next();
            }

        }




        private void getmetadata(string path)
        {
            List<string> ImageInfo = new List<string>();

            String[] infos = new string[] { "Date/Time", "Model", "Exposure Time", "F-Number", "Shutter Speed", "Focal Length" };

            label1.Text = "";
            IEnumerable<MetadataExtractor.Directory> directories = ImageMetadataReader.ReadMetadata(path);
            foreach (var directory in directories)
            {
                foreach (var tag in directory.Tags)
                {
                    foreach (string info in infos)
                    {
                        if (tag.Name == info)
                        {
                            ImageInfo.Add(tag.Description);

                        }
                    }



                    Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
                }
            }

            if(ImageInfo.Count>0)
            ImageInfo.RemoveAt(ImageInfo.Count - 1);


            foreach (string i in ImageInfo)
            {
                label1.Text = label1.Text + i + "  |  ";
            }

        }


        public void next()
        {
            try
            {
                UseWaitCursor = true;
                if (file_infos.LastIndexOf(accfile) + 1 < file_infos.Count())
                {
                    accfile = file_infos[file_infos.IndexOf(accfile) + 1];
                    img.Dispose();

                    if (accfile.ToLower().EndsWith(".cr2"))
                    {
                        img = convertCR2toJPG(accfile);
                    }
                    else
                    {
                        img = Image.FromFile(accfile);
                    }

                    picbox.Image = img;
                    pictureBox1.Image = img;
                }
                else
                {
                    accfile = file_infos[0];
                    img.Dispose();


                    if (accfile.ToLower().EndsWith(".cr2"))
                    {
                        img = convertCR2toJPG(accfile);
                    }
                    else
                    {
                        img = Image.FromFile(accfile);
                    }

                    picbox.Image = img;
                    pictureBox1.Image = img;
                }

                getmetadata(accfile);
                UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                UseWaitCursor = false;
            }
        }


        public void previous()
        {
            try
            {
                UseWaitCursor = true;
                if (file_infos.IndexOf(accfile) - 1 < file_infos.Count() && file_infos.IndexOf(accfile) - 1 != -1)
                {
                    accfile = file_infos[file_infos.IndexOf(accfile) - 1];
                    img.Dispose();
                   


                    if (accfile.ToLower().EndsWith(".cr2"))
                    {
                        img = convertCR2toJPG(accfile);
                    }
                    else
                    {
                        img = Image.FromFile(accfile);
                    }

                    picbox.Image = img;
                    pictureBox1.Image = img;
                }
                else
                {
                    accfile = file_infos[file_infos.Count - 1];
                    img.Dispose();
             


                    if (accfile.ToLower().EndsWith(".cr2"))
                    {
                        img = convertCR2toJPG(accfile);
                    }
                    else
                    {
                        img = Image.FromFile(accfile);
                    }

                    picbox.Image = img;
                    pictureBox1.Image = img;

                }
                getmetadata(accfile);
                UseWaitCursor = false;
            }
            catch (Exception ex)
            {
                UseWaitCursor = false;
            }

        }

        List<string> std_open_with_programm = new List<string>();

        public void get_programm()
        {
            for (int i = 97; i < 122; i++)
            {
                try
                {
                    string p = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\FileExts\.jpg\OpenWithList", Convert.ToString((char)i), null).ToString();
                    string pp_ = @"HKEY_CLASSES_ROOT\Applications\" + p + @"\shell\open\command";
                    string pp = Registry.GetValue(pp_, null, null).ToString();
                    std_open_with_programm.Add(pp);
                }
                catch (Exception ex)
                { }
            }

            for (int s = 0; s < std_open_with_programm.Count; s++)
            {

                std_open_with_programm[s] = std_open_with_programm[s].Substring(0, std_open_with_programm[s].LastIndexOf(".") + 4);
                if (std_open_with_programm[s].StartsWith("\""))
                {
                    std_open_with_programm[s] = std_open_with_programm[s].Substring(1, std_open_with_programm[s].Length - 1);
                }

            }
            std_open_with_programm.Sort();

        }



        bool showinghelp = false;
        bool showinginfo = false;


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Right)
            {
                next();
            }
            if (e.KeyCode == Keys.Left)
            {
                previous();
            }

            if (e.KeyCode == Keys.Tab)
            {
                try
                {
                    if (pictureBox1.Visible)
                    {

                        pictureBox1.Visible = false;
                        picbox.Visible = true;
                    }
                    else
                    {

                        pictureBox1.Visible = true;
                        picbox.Visible = false;

                    }
                }
                catch (Exception ex)
                { }
            }


            if (e.KeyCode == Keys.Space)
            {
                try
                {
                    if (showinginfo)
                    {
                        this.Controls.Remove(panel1);
                        this.Refresh();
                        showinginfo = false;
                    }
                    else
                    {
                        this.Controls.Add(panel1);
                        panel1.BringToFront();
                        this.Invalidate();
                        this.Refresh();
                        this.Update();
                        showinginfo = true;
                    }


                }
                catch (Exception ex)
                { }
            }


            if (e.KeyCode == Keys.F1)
            {
                try
                {
                    if (showinghelp)
                    {
                        showinghelp = false;


                        load_pictureBox(accfile);
                        getmetadata(accfile);

                        picbox.Image = img;
                        pictureBox1.Image = img;
                    }
                    else
                    {
                        showinghelp = true;
                        // pictureBox1.Load(openFile);
                        picbox.Image = new Bitmap(typeof(Form1), "default.png");
                        pictureBox1.Image = new Bitmap(typeof(Form1), "default.png");
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }



            if (e.KeyCode == Keys.Enter)
            {
                if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;

                }
                else
                {
                    this.WindowState = FormWindowState.Normal;


                }

            }
        }


        List<string> file_infos = new List<string>();


        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Controls.Remove(panel1);
            if (accfile.Length != 0)
            {
                DirectoryInfo dinfo = new DirectoryInfo(Path.GetDirectoryName(accfile));
                var files = dinfo.GetFiles("*.jpg")
                    .Concat(dinfo.GetFiles("*.tiff"))
                    .Concat(dinfo.GetFiles("*.png"))
                    //.Concat(dinfo.GetFiles("*.CR2"))
                    .Concat(dinfo.GetFiles("*.cr2"))
                    .Concat(dinfo.GetFiles("*.bmp"));


                foreach (FileInfo fi in files)
                {
                    file_infos.Add(fi.FullName);
                }
                file_infos.Sort();
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }

            pictureBox1.MouseDown += PictureBox_MouseDown;

        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    var info = new FileInfo(accfile);
                    string[] paths = { info.FullName };
                    var pictureBox = (PictureBox)sender;
                    pictureBox.DoDragDrop(new DataObject(DataFormats.FileDrop, paths), DragDropEffects.Copy);
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void Form1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (this.WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;

            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
        

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {

                this.ControlBox = false;
                this.MaximizeBox = false;
                this.MinimizeBox = false;
                this.TopMost = false;
            }
            else
            {
                this.ControlBox = true;
                this.MaximizeBox = true;
                this.MinimizeBox = true;
                this.TopMost = false;





                this.Height = (int)Math.Round(this.Width * aspectratio);



            }


            panel1.Size = new Size(this.Width, panel1.Height);

        }


        private void pictureBox1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.WindowState == FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Normal;



                }
                else if (this.WindowState == FormWindowState.Normal)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
            }
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Focus();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Right:
                    {
                        öffnenInToolStripMenuItem.DropDown.Items.Clear();
                        foreach (string s in std_open_with_programm)
                        {

                            //string ass = s.Substring(0, s.LastIndexOf("."));
                            //if (ass.StartsWith("\""))
                            //{
                            //    ass = ass.Substring(1, ass.Length - 1);
                            //}

                            öffnenInToolStripMenuItem.DropDown.Items.Add(Path.GetFileName(@s));
                        }
                        contextMenuStrip1.Show(this, new Point(e.X, e.Y));//places the menu at the pointer position

                    }
                    break;
            }
        }


        private void öffnenInToolStripMenuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            int i = this.öffnenInToolStripMenuItem.DropDownItems.IndexOf(e.ClickedItem);
            string ass = std_open_with_programm[i];
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = ass;
            process.StartInfo.Arguments = "\"" + @accfile + "\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();

        }

        private void showInExplorerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            // Configure the process using the StartInfo properties.
            process.StartInfo.FileName = "explorer.exe";
            process.StartInfo.Arguments = "/select, \"" + @accfile + "\"";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
            process.Start();
        }

        private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Collections.Specialized.StringCollection FileCollection = new System.Collections.Specialized.StringCollection();
            FileCollection.Add(accfile);
            Clipboard.SetFileDropList(FileCollection);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            int x = this.PointToClient(new Point(e.X, e.Y)).X;

            int y = this.PointToClient(new Point(e.X, e.Y)).Y;

            if (x >= pictureBox1.Location.X && x <= pictureBox1.Location.X + pictureBox1.Width && y >= pictureBox1.Location.Y && y <= pictureBox1.Location.Y + pictureBox1.Height)

            {

                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

                pictureBox1.Image = Image.FromFile(files[0]);

            }
        }

        private void rotateLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate270FlipNone);
            pictureBox1.Image = img;
        }

        private void rotateRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.Rotate90FlipNone);
            pictureBox1.Image = img;
        }

        private void flipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.RotateNoneFlipY);
            pictureBox1.Image = img;
        }

        private void mirrorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            img.RotateFlip(RotateFlipType.RotateNoneFlipX);
            pictureBox1.Image = img;
        }
    }
    public static class FileAssoc
    {
        [DllImport("Shlwapi.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern uint AssocQueryString(AssocF flags, AssocStr str, string pszAssoc, string pszExtra, [Out] StringBuilder sOut, [In][Out] ref uint nOut);

        [Flags]
        public enum AssocF
        {
            Init_NoRemapCLSID = 0x1,
            Init_ByExeName = 0x2,
            Open_ByExeName = 0x2,
            Init_DefaultToStar = 0x4,
            Init_DefaultToFolder = 0x8,
            NoUserSettings = 0x10,
            NoTruncate = 0x20,
            Verify = 0x40,
            RemapRunDll = 0x80,
            NoFixUps = 0x100,
            IgnoreBaseClass = 0x200
        }

        public enum AssocStr
        {
            Command = 1,
            Executable,
            FriendlyDocName,
            FriendlyAppName,
            NoOpen,
            ShellNewValue,
            DDECommand,
            DDEIfExec,
            DDEApplication,
            DDETopic
        }

        public static string GetApplicationName(string fileExtensionIncludingDot)
        {
            uint cOut = 0;
            if (AssocQueryString(AssocF.Verify, AssocStr.FriendlyAppName, fileExtensionIncludingDot, null, null, ref cOut) != 1)
                return null;
            StringBuilder pOut = new StringBuilder((int)cOut);
            if (AssocQueryString(AssocF.Verify, AssocStr.FriendlyAppName, fileExtensionIncludingDot, null, pOut, ref cOut) != 0)
                return null;
            return pOut.ToString();
        }
    }


    class PartialStream : Stream  // Fun solution and experiment... probably not the best idea here
    {
        internal PartialStream(FileStream p_f, uint p_start, int p_length)
        {
            m_f = p_f;
            m_start = p_start;
            m_length = p_length;

            m_f.Seek(p_start, SeekOrigin.Begin);
        }

        FileStream m_f;
        uint m_start;
        int m_length;

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return m_f.BeginRead(buffer, offset, count, callback, state);
        }
        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return m_f.BeginWrite(buffer, offset, count, callback, state);
        }
        public override bool CanRead
        {
            get { return m_f.CanRead; }
        }
        public override bool CanSeek
        {
            get { return m_f.CanSeek; }
        }
        public override bool CanTimeout
        {
            get { return m_f.CanTimeout; }
        }
        public override bool CanWrite
        {
            get { return m_f.CanWrite; }
        }
        public override void Close()
        {
            m_f.Close();
        }
        public override System.Runtime.Remoting.ObjRef CreateObjRef(Type requestedType)
        {
            return m_f.CreateObjRef(requestedType);
        }
        protected override void Dispose(bool disposing)
        {
            //m_f.Dispose(disposing); // Can't...
            base.Dispose(disposing);
        }
        public override int EndRead(IAsyncResult asyncResult)
        {
            return m_f.EndRead(asyncResult);
        }
        public override void EndWrite(IAsyncResult asyncResult)
        {
            m_f.EndWrite(asyncResult);
        }
        public override bool Equals(object obj)
        {
            return m_f.Equals(obj);
        }
        public override void Flush()
        {
            m_f.Flush();
        }
        public override int GetHashCode()
        {
            return m_f.GetHashCode();
        }
        public override object InitializeLifetimeService()
        {
            return m_f.InitializeLifetimeService();
        }
        public override long Length
        {
            get { return m_length; }
        }
        public override long Position
        {
            get { return m_f.Position - m_start; }
            set { m_f.Position = value + m_start; }
        }
        public override int Read(byte[] buffer, int offset, int count)
        {
            long maxRead = Length - Position;
            return m_f.Read(buffer, offset, (count <= maxRead) ? count : (int)maxRead);
        }
        public override int ReadByte()
        {
            if (Position < Length)
                return m_f.ReadByte();
            else
                return 0;
        }
        public override int ReadTimeout
        {
            get { return m_f.ReadTimeout; }
            set { m_f.ReadTimeout = value; }
        }
        public override void SetLength(long value)
        {
            m_f.SetLength(value);
        }
        public override string ToString()
        {
            return m_f.ToString();
        }
        public override void Write(byte[] buffer, int offset, int count)
        {
            m_f.Write(buffer, offset, count);
        }
        public override void WriteByte(byte value)
        {
            m_f.WriteByte(value);
        }
        public override int WriteTimeout
        {
            get { return m_f.WriteTimeout; }
            set { m_f.WriteTimeout = value; }
        }
        public override long Seek(long offset, SeekOrigin origin)
        {
            return m_f.Seek(offset + m_start, origin);
        }

    }
}