using GarageManagement.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GarageManagement
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            using var f = new CustomerForm();
            f.ShowDialog();
        }

        private void btnCars_Click(object sender, EventArgs e)
        {
            using var f = new CarForm();
            f.ShowDialog();
        }

        private void btnRepairOrders_Click(object sender, EventArgs e)
        {
            using var f = new RepairOrderForm();
            f.ShowDialog();
        }
        private void btnParts_Click(object sender, EventArgs e)
        {
            using var f = new PartForm();
            f.ShowDialog();
        }

        private void btnReports_Click(object sender, EventArgs e)
        {
            using var f = new ReportForm();
            f.ShowDialog();
        }
    }
}
