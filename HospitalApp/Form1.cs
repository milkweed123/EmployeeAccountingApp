using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HospitalApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog fileDialog = new OpenFileDialog())
            {
                if (fileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadData(fileDialog.FileName);
                }
            }
        }

        private void LoadData(string path)
        {
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void SaveData()
        {
            throw new NotImplementedException();
        }
    }
}
