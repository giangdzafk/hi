using System;
using System.Collections;
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
   
    public partial class UC_CsDetail : UserControl
    {
        funcion fn = new funcion();
        string query;
        public UC_CsDetail()
        {
            InitializeComponent();
        }

        private void cboTimkiem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTimkiem.SelectedIndex == 0)
            {
                query = "select  customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid ";
                getrecords(query);
            }
            else if (cboTimkiem.SelectedIndex == 1)
            {
                query = "select  customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is null";
                getrecords(query);
            }
            else if (cboTimkiem.SelectedIndex == 2)
            {
                query = "select  customer.cid, customer.cname, customer.mobile, customer.nationality, customer.gender, customer.dob, customer.address, customer.checkin, rooms.roomNo, rooms.roomType, rooms.bed, rooms.price from customer inner join rooms on customer.roomid = rooms.roomid where checkout is not null";
                getrecords(query);
            }
        }
        private void getrecords(String query)
        {
            DataSet ds = fn.getData(query);
            DataGridView1.DataSource = ds.Tables[0];
        }
    }
}
