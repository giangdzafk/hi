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
    public partial class UC_Checkout : UserControl
    {
        funcion fn = new funcion();
        string query;   
        public UC_Checkout()
        {
            InitializeComponent();
        }

        private void UC_Checkout_Load(object sender, EventArgs e)
        {
            query = "select  customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where chekout = 'NO'";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            query = "select customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where cname like '" + txtName.Text+ "%' and chekout = 'NO'";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }
        int id;
        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DataGridView1.Rows[e.RowIndex].Cells[e.RowIndex].Value != null)
            {
               id = int.Parse(DataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
               txtCName.Text = DataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
               txtRoom.Text = DataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
        }

        private void btnCheckout_Click(object sender, EventArgs e)
        {
            if(txtCName.Text != "")
            {
                if (MessageBox.Show("Bạn có chắc chắn thanh toán không ?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    String cdate = dtDate.Text;
                    query = "update customer set chekout = 'YES', checkout = '" + cdate + "' where cid = " + id + " update rooms set booked = 'NO' where roomNo = '" + txtRoom.Text + "'";
                    fn.setData(query, "Thanh toán thành công!!!");
                    UC_Checkout_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                MessageBox.Show("Không có khách hàng", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void clearAll()
        {
            txtCName.Clear();
            txtName.Clear();
            txtRoom.Clear();
            dtDate.ResetText();
        }

        private void UC_Checkout_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
