using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bài_tập_lớn.All_user
{
    public partial class UC_CostomerRes : UserControl
    {
        funcion fn = new funcion();
        string query;
        public UC_CostomerRes()
        {
            InitializeComponent();
        }

        // Lấy dữ liệu từ database và thêm dữ liệu đó vào combobox
        public void setCombobox(string query, ComboBox combo)
        {
            SqlDataReader sdr = fn.getforcombo(query);
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++) //FieldCount: trả về số cột trong kết quả của mỗi dòng
                {
                    combo.Items.Add(sdr.GetString(i));
                }
            }
            sdr.Close();

        }


        private void guna2TextBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void guna2ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void guna2ComboBox2_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2ComboBox4_SelectedIndexChanged(object sender, EventArgs e) { }
        private void UC_CostomerRes_Load(object sender, EventArgs e)
        {

        }
        private void guna2TextBox2_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox3_TextChanged(object sender, EventArgs e) { }
        private void guna2TextBox1_TextChanged(object sender, EventArgs e) { }
        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e) { }
        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e) { }
        private void guna2DateTimePicker2_ValueChanged(object sender, EventArgs e) { }
        private void label6_Click(object sender, EventArgs e) { }

        private void ComboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboRoom.SelectedIndex = -1;
            cboRoomno.Items.Clear();
            txtPrice.Clear();
        }

        private void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboRoomno.Items.Clear();
            query = "select roomNO from rooms where bed = '" + cboBed.Text + "' and roomType ='" + cboRoom.Text + "' and booked = 'NO'";
            setCombobox(query, cboRoomno);
        }
        // Biến lưu trữ id phòng
        int rid;
        private void ComboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            query = "select price, roomid from rooms where roomNo  = '" + cboRoomno.Text + "'";
            DataSet ds = fn.getData(query);
            txtPrice.Text = ds.Tables[0].Rows[0][0].ToString();
            rid = int.Parse(ds.Tables[0].Rows[0][1].ToString());
        }

        private void btnAllotroom_Click(object sender, EventArgs e)
        {
            //Kiểm tra dữ liệu không bị bỏ trống
            if (txtName.Text != "" && txtContact.Text != "" && txtnational.Text != "" && txtAddress.Text != "" && cboGenz.Text != "" && dtDob.Text != "" && cboBed.Text != "" && cboRoom.Text != "" && cboRoomno.Text != "" && txtAddress.Text != "" && dtCheckin.Text != ""  && dtcheckout.Text != "")
            {
                string name = txtName.Text;
                Int64 mobile = Int64.Parse(txtContact.Text);
                String national = txtnational.Text;
                String gender = cboGenz.Text;
                String dob = dtDob.Text;
                String address = txtAddress.Text;
                String checkin = dtCheckin.Text;

                // Tạo câu truy vấn để thêm khách hàng vào bảng customer và cập nhật trạng thái phòng
                query = "insert into Customer (cname, mobile,nationality, gender, dob, address, checkin, roomid) values ('" + name + "'," + mobile + ",'" + national + "','" + gender + "', '" + address + "','" + dob + "' , '" + checkin + "' , " + rid + ") update rooms set booked = 'Yes' where roomNo = '" + cboRoomno.Text + "'";
                fn.setData(query, " Số phòng " + cboRoomno.Text + " Đăng kí khách hàng thành công!!! ");
                clearAll();
            }
            else
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        // xóa dữ liệu 
        public void clearAll()
        {
            txtName.Clear();
            txtContact.Clear();
            txtnational.Clear();
            txtAddress.Clear();
            cboGenz.SelectedIndex = -1;
            cboBed.SelectedIndex = -1;
            cboRoom.SelectedIndex = -1;
            cboRoomno.SelectedIndex = -1;
            txtPrice.Clear();
            dtCheckin.ResetText();
            dtcheckout.ResetText();
        }

        private void UC_CostomerRes_Leave(object sender, EventArgs e)
        {
            clearAll();
        }
    }
}
