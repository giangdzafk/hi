using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bài_tập_lớn.All_user
{
    public partial class UC_Employee : UserControl
    {
        funcion fn = new funcion();
        string query;
        public UC_Employee()
        {
            InitializeComponent();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void UC_Employee_Load(object sender, EventArgs e)
        {
            getID();
        }
       
        public void getID()
        {
            query = "select max(eid) from employee";
            DataSet ds = fn.getData(query);

            if(ds.Tables[0].Rows[0][0].ToString() != "")
            {
               Int64 num = Int64.Parse(ds.Tables[0].Rows[0][0].ToString());
               labelToset.Text = (num + 1).ToString();              

            }
          

        }

        private void btnRegis_Click(object sender, EventArgs e)
        {
            if (txtName.Text != "" && txtEmail.Text != "" && txtMobie.Text != "" && txtPassword.Text != "" && txtUsername.Text != "" && cboGen.Text != "")
            {
                string name = txtName.Text;
                Int64 mobile = Int64.Parse(txtMobie.Text);
                String gender = cboGen.Text;
                string email = txtEmail.Text;
                string username = txtUsername.Text;
                string password = txtPassword.Text;


                query = "insert into employee (ename, mobile, gender, emailid, username, pass) values ('" + name + "','" + mobile + "', '" + gender + "', '" + email + "', '" + username + "', '" + password + "')";
                fn.setData(query, "Đăng kí nhân viên thành công !!!");


                ClearAll();
                getID();
            }
        }
        public void ClearAll()
        {
            txtName.Clear();
            txtMobie.Clear();
            txtEmail.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
            cboGen.SelectedIndex = -1;
        }

        private void tabEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabEmployee.SelectedIndex == 1)
            {
                setEmployee(DataGridView1);
            }
            else if (tabEmployee.SelectedIndex == 2)
            {
                setEmployee(DataGridView2);
            }
        }

        public void setEmployee(DataGridView dgv)
        {
            query = "select * from employee";
            DataSet ds = fn.getData(query);
            dgv.DataSource = ds.Tables[0];
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                if (MessageBox.Show("Bạn chắc chắn muốn xóa không ?", "Xác Nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    query = "delete from employee where eid = " + txtID.Text + "";
                    fn.setData(query, "Xóa nhân viên thành công !!!");
                    tabEmployee_SelectedIndexChanged(this, null);
                }

            }
        }

        private void UC_Employee_Leave(object sender, EventArgs e)
        {
            ClearAll();
        }
    }
}
