using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ОООТехносервис.View
{
    public partial class Report : Form
    {
        public Report()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Report_Load(object sender, EventArgs e)
        {
            labelCountComleteRequests.Text = "Количество выполненных заданий = " + Classes.Helper.DB.Request.Count(r => r.RequestStatusId == 3).ToString();
            labelAverageTime.Text = "Среднее время выполненных заявок (часов) = " + (Classes.Helper.DB.Request.Average(r => r.RequestTime) / 60.0).ToString("F2");
            dgvStatistics.DataSource = Classes.Helper.DB.ViewGroupProblem.ToList();
            dgvStatistics.Columns[0].HeaderText = "Тип неисправности";
            dgvStatistics.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvStatistics.Columns[1].HeaderText = "Количество неисправности";
            dgvStatistics.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }
    }
}
