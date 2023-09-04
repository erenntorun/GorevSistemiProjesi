using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace ProjeGörev
{
    public class ExceleYaz
    {


        public static void OkuveYaz(System.Windows.Forms.ListView lv, string ExcelIsim, IWin32Window f)
        {
            string folder = "";

            if (folder == "")
            {
                using (FolderBrowserDialog dialog = new FolderBrowserDialog())
                {
                    dialog.Description = "Dosyanın kaydedileceği alanı seçiniz";
                    dialog.RootFolder = Environment.SpecialFolder.Desktop;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        folder = dialog.SelectedPath;
                    }
                }
            }

            if (folder != "")
            {
                string yol = ExcelIsim + "_" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
                            + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                //string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), yol + ".xls");
                string filePath = folder + "\\" + yol + ".xls";

                StreamWriter writer = new StreamWriter(File.Create(filePath), Encoding.Default);
                string str = string.Empty;

                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    str += lv.Columns[i].Text + "\t";
                }

                str += "\n";
                writer.Write(str);
                writer.Flush();

                for (int i = 0; i < lv.Items.Count; i++)
                {
                    System.Windows.Forms.ListViewItem lvitem = lv.Items[i];
                    str = "";

                    for (int col = 0; col < lv.Columns.Count; col++)
                    {
                        string c32 = char.ConvertFromUtf32(32);
                        string c13 = char.ConvertFromUtf32(13);
                        string c10 = char.ConvertFromUtf32(10);
                        string c09 = char.ConvertFromUtf32(9);
                        string yeniKelime = lvitem.SubItems[col].Text;
                        yeniKelime = yeniKelime.Replace(c13, "").Replace(c10, "").Replace(c09, "");
                        str += yeniKelime + "\t";
                    }
                    str += "\n";
                    writer.Write(str);
                    writer.Flush();
                }
                writer.Close();
                if (MessageBox.Show(yol + " dosyası oluşturuldu !.. Oluşturulan dosya açılsın mı ?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Process.Start(filePath);
                }
            }
        }



    }
}