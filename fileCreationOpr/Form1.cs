using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq.Expressions;

namespace fileCreationOpr
{
    public partial class FileHandler : Form
    {
        public FileHandler()
        {
            InitializeComponent();
        }

        private void FileHandler_Load(object sender, EventArgs e)
        {

            // fetch all the drive from the system 
            foreach (DriveInfo dir in DriveInfo.GetDrives())
            {
                drivers.Items.Add(dir);
            }
            // Show previous button or not
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter write = new StreamWriter(path.Text,append:true);
            write.WriteLine(textBox1.Text);
            textBox1.Text ="";
            write.Flush();
            write.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CREATE_Click(object sender, EventArgs e)
        {
            try
            {
                if(FileTextName.Text == "")
                {
                    throw new ArgumentNullException("File name is empty!!");
                }
                
            }
            catch (ArgumentNullException ex)
            {
                MessageBox.Show(ex.Message);
            }
            string pathFolder = path.Text; 
            string pathFile = FileTextName.Text;
            string pathName = Path.Combine(pathFolder,pathFile);
            File.Create(pathName);
            path.Text = pathName;
            textBox1.Text = pathName + "-------->Created!!";
       
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            File.Delete(path.Text);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //check text box has char or not
            if (textBox1.Text.Length > 0)
            {
                button4.Visible = true;
            }
            else
            {
                button4.Visible = false;
            }
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            FileInfo fileData = new FileInfo(path.Text);
            textBox1.Text = fileData.Name;
        }

        private void FileTextName_TextChanged(object sender, EventArgs e)
        {

        }
            
        private void READ_Click(object sender, EventArgs e)

        {
            try
            {
                if (path.Text.Length < 1)
                {
                    throw new ArgumentException("File path cannot be empty.");
                }

                if (!File.Exists(path.Text))
                {
                    throw new FileNotFoundException("The specified file does not exist.");
                }

                StreamReader reader = new StreamReader(path.Text);
                reader.BaseStream.Seek(0, SeekOrigin.Begin);


                textBox1.Text = reader.ReadToEnd();

                reader.Close();

            }
            catch (ArgumentException ex) { 
                MessageBox.Show(ex.Message);
            }
            catch (FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
       
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filePath = path.Text;
            string[] lines = File.ReadAllLines(filePath);

            textBox1.Lines = lines;
    
        }

        private void done_Click(object sender, EventArgs e)
        {
            List<string> updatedLines = new List<string>();

            foreach (string line in textBox1.Lines)
            {
                updatedLines.Add(line);
            }
            // Write the updated content back to the file
            File.WriteAllLines(path.Text, updatedLines);
            textBox1.Text = "";
        }

        private void clear_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }

        Stack<string> PrePath = new Stack<string>();

        private void drivers_SelectedIndexChanged(object sender, EventArgs e)
        {
            Folders.Items.Clear(); //clear all the itmes avil in folders list

            try
            {
                DriveInfo drive = (DriveInfo)drivers.SelectedItem;
                PrePath.Push(drive.Name);
                currentPath.Text = drive.Name;
                foreach (DirectoryInfo dirInfo in drive.RootDirectory.GetDirectories())
                {
                    Folders.Items.Add(dirInfo);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }
        }

        private void Folders_SelectedIndexChanged(object sender, EventArgs e)
        {
            Files.Items.Clear();

            try
            {

                DirectoryInfo dir = (DirectoryInfo)Folders.SelectedItem;
                PrePath.Push(dir.FullName);
                currentPath.Text = dir.FullName;
                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    Files.Items.Add(fileInfo);
                }

                foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                {
                    Files.Items.Add(dirInfo);
                }


            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Files_SelectedIndexChanged(object sender, EventArgs e)
        {

            try
            {
                DirectoryInfo dir = (DirectoryInfo)Files.SelectedItem;
                
                Clipboard.SetText(dir.FullName);
                PrePath.Push(dir.FullName);
                currentPath.Text = dir.FullName;
                Files.Items.Clear();    //clear all unused file / folders
                foreach (FileInfo fileInfo in dir.GetFiles())
                {
                    Files.Items.Add(fileInfo);
                }

                foreach (DirectoryInfo dirInfo in dir.GetDirectories())
                {
                    Files.Items.Add(dirInfo);
                }
        
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void currentPath_Click(object sender, EventArgs e)
        {

        }

        private void path_TextChanged(object sender, EventArgs e)
        {

        }

        private void color(object sender, MouseEventArgs e)
        {

        }

        private void ToolTip(object sender, EventArgs e)
        {
       

        
        }

        private void button3_Click(object sender, EventArgs e)
        {         
                currentPath.Text = PrePath.Peek();
                string dir = PrePath.Pop();
                Files.Items.Clear();

                foreach (string file in Directory.GetFiles(dir))
                {
                    FileInfo fileinfo = new FileInfo(file);
                    Files.Items.Add(fileinfo);
                }

                foreach (string dirInfo in Directory.GetDirectories(dir))
                {
                    DirectoryInfo dirinfo = new DirectoryInfo(dirInfo);
                    Files.Items.Add(dirinfo);
                }
        }

       

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.Text);
        }

    }
}
