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
    public partial class UC_Add : UserControl
    {
        // Khai báo lớp funcion
        funcion fn = new funcion();
        string query;
        public UC_Add()
        {
            InitializeComponent();
        }

        private void guna2TextBox2_TextChanged(object sender, EventArgs e)
        {

        }
        // Sự kiện load
        private void UC_Add_Load(object sender, EventArgs e)
        {

            query = "select * from rooms";
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];

        }
        // Phương thức kiểm tra sự tồn tại của phòng trong cơ sở dữ liệu


        private void btnAddroom_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu không bị bỏ trống
            if (txtRoomno.Text != "" && txtRoomtype.Text != "" && txtBed.Text != "" && txtPrice.Text != "")
            {
                string roomno = txtRoomno.Text;
                string type = txtRoomtype.Text;
                string bed = txtBed.Text;
                Int64 price = Int64.Parse(txtPrice.Text);

                // Kiểm tra xem phòng đã tồn tại hay chưa
                string queryCheck = "SELECT COUNT(*) FROM rooms WHERE roomNo = '" + roomno + "'";
                DataSet ds = fn.getData(queryCheck); // Gọi phương thức getData từ lớp funcion

                // Kiểm tra số lượng phòng trùng
                int count = Convert.ToInt32(ds.Tables[0].Rows[0][0]);

                if (count > 0)
                {
                    // Nếu phòng đã tồn tại
                    MessageBox.Show("Số phòng này đã tồn tại, vui lòng chọn số phòng khác.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    // Nếu phòng chưa tồn tại, thực hiện thêm phòng
                    string queryInsert = "INSERT INTO rooms (roomNo, roomType, bed, price) VALUES ('" + roomno + "', '" + type + "', '" + bed + "', '" + price + "')";
                    fn.setData(queryInsert, "Đã thêm phòng");

                    // Tải lại dữ liệu phòng
                    UC_Add_Load(this, null);
                    clearAll();
                }
            }
            else
            {
                // Cảnh báo khi chưa nhập đủ thông tin
                MessageBox.Show("Xin vui lòng điền đủ thông tin!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // xóa dữ liệu sau khi thêm phòng
        public void clearAll()
        {
            txtRoomno.Clear();
            txtRoomtype.SelectedIndex = -1;
            txtBed.SelectedIndex = -1;
            txtPrice.Clear();
        }

        private void DataGridView1_Leave(object sender, EventArgs e)
        {

        }

        private void DataGridView1_Enter(object sender, EventArgs e)
        {

        }

        private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UC_Add_Leave(object sender, EventArgs e)
        {
            clearAll();
        }

        private void UC_Add_Enter(object sender, EventArgs e)
        {
            UC_Add_Load(this, null);
        }

        private void btnSuaphong_Click(object sender, EventArgs e)
        {

        }

        private void btnSuaphong_Click_1(object sender, EventArgs e)
        {
            if (txtRoomno.Text != "")
            {
                string roomno = txtRoomno.Text;

                // Truy vấn xóa phòng trực tiếp bằng chuỗi nối
                query = "DELETE FROM rooms WHERE roomNo = '" + roomno + "'";

                // Thực hiện lệnh xóa phòng
                fn.setData(query, "Đã xóa phòng");

                // Tải lại dữ liệu phòng
                UC_Add_Load(this, null);
                clearAll();
            }
            // Cảnh báo khi chưa nhập số phòng
            else
            {
                MessageBox.Show("Xin vui lòng nhập số phòng để xóa!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }
    }
}
