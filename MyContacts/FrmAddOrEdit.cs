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
    public partial class FrmAddOrEdit : Form
    {
        IContactsRepository repository;
        public int contactId = 0;
        public FrmAddOrEdit()
        {
            repository = new EFContactRepository();
            InitializeComponent();
        }

        bool ValidateInput()
        {

            if (TxtName.Text == "")
            {
                MessageBox.Show(text: "لطفا نام را وارد کنید");
                return false;
            }
            if (TxtEmail.Text == "")
            {
                MessageBox.Show(text: "لطفا ایمیل را وارد کنید");
                return false;
            }
            if (TxtFamily.Text == "")
            {
                MessageBox.Show(text: "لطفا فامیلی را وارد کنید");
                return false;
            }
            if (TxtAge.Value == 0)
            {
                MessageBox.Show(text: "لطفا سن را وارد کنید");
                return false;
            }
            if (TxtMobile.Text == "")
            {
                MessageBox.Show(text: "لطفا موبایل را وارد کنید");
                return false;
            }
            if (TxtAddress.Text == "")
            {
                MessageBox.Show(text: "لطفا ادرس را وارد کنید");
                return false;
            }
            return true;
        }
        private void btnAddChange_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                bool isSuccess = repository.Insert(TxtName.Text, TxtFamily.Text, TxtMobile.Text, TxtEmail.Text, (int)TxtAge.Value,TxtAddress.Text );
                if (isSuccess == true)
                {
                    MessageBox.Show("عملیات موفقیت امیز بود","موفقیت",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("لطفا مقادیر وارده را درست وارد کنید", "خطا", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FrmAddOrEdit_Load(object sender, EventArgs e)
        {
            if (contactId == 0)
            {
                this.Text = "افزودن شخص جدید";
            }
            else
            {
                this.Text = "ویرایش شخص";
                DataTable dt = repository.SelectRow(contactId);
                TxtName.Text = dt.Rows[0][1].ToString();
                TxtFamily.Text = dt.Rows[0][2].ToString();
                TxtMobile.Text = dt.Rows[0][3].ToString();
                TxtEmail.Text = dt.Rows[0][4].ToString();
                TxtAge.Text = dt.Rows[0][5].ToString();
                TxtAddress.Text = dt.Rows[0][6].ToString();
                btnAddChange.Text = "ویرایش ";
            }
        }
    }
}
