using MyContacts.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyContacts
{
    public partial class Form1 : Form
    {
        IContactsRepository repository;
        
        public Form1()
        {
            
            InitializeComponent();
            repository = new EFContactRepository();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BindGrid();
            
        }

        private void BindGrid()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.DataSource = repository.SelectAll();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frm = new FrmAddOrEdit();
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
            {
                BindGrid();
            }
           
                
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                string name = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                string family = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                string Fullname = name +" "+family ;
                if (MessageBox.Show($" ایا از حذف{Fullname} مطمئن هستید", "توجه",MessageBoxButtons.YesNo,MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    int contactid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                    repository.Delete(contactid);
                    BindGrid();
                }
            }
            else
            {
                MessageBox.Show("لطفا یک شخص را انتخاب نمایید","خطا",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int contactid = int.Parse(dataGridView1.CurrentRow.Cells[0].Value.ToString());
                FrmAddOrEdit frm = new FrmAddOrEdit();
                frm.contactId = contactid;
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    BindGrid();
                }
               
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = repository.Search(textBox1.Text);
        }
    }
}
