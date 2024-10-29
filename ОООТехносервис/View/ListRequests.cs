using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОООТехносервис.Classes;

namespace ОООТехносервис.View
{
    public partial class ListRequests : Form
    {
        List<Model.Request> requests;

        public ListRequests()
        {
            InitializeComponent();
        }
        
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void ShowRequests()
        {
            requests = Helper.DB.Request.ToList();

            /* Поиск по номеру */
            if (tbSearch.Text.Length > 0)
            {
                requests = requests.Where(r => r.RequestId.ToString().Contains(tbSearch.Text)).ToList();
            }

            /* Поиск по фильтру */
            if ((int)cbFilter.SelectedIndex != 0)
            {
                requests = requests.Where(r => r.RequestStatusId == (int)cbFilter.SelectedIndex).ToList();
            }

            // Фильтрация по роли
            if (Helper.user.UserRole == 1)
            {
                requests = requests.Where(r => r.RequestUserId == Helper.user.UserId).ToList();
            }
            if (Helper.user.UserRole == 2)
            {
                requests = requests.Where(r => r.RequestMasterId == Helper.user.UserId).ToList();
            }

            dgvRequests.Rows.Clear();

            for (int i = 0; i < requests.Count; i++)
            {
                dgvRequests.Rows.Add();
                dgvRequests.Rows[i].Cells[0].Value = requests[i].RequestId.ToString();
                dgvRequests.Rows[i].Cells[1].Value = requests[i].RequestDate.ToShortDateString();
                dgvRequests.Rows[i].Cells[2].Value = requests[i].Equipment.EquipmentName.ToString();
                dgvRequests.Rows[i].Cells[3].Value = requests[i].User.UserFullName.ToString();
                dgvRequests.Rows[i].Cells[4].Value = requests[i].Status.StatusName.ToString();
                dgvRequests.Rows[i].Cells[5].Value = requests[i].User1.UserFullName.ToString();
                dgvRequests.Rows[i].Cells[6].Value = requests[i].Stage.StageName.ToString();
            }
        }

        private void ListRequests_Shown(object sender, EventArgs e)
        {
            btnNewRequest.Visible = btnEditRequest.Visible = btnReports.Visible = false;

            switch (Helper.user.UserRole)
            {
                case 2:
                    btnEditRequest.Visible = true;
                    break;
                case 3:
                    btnNewRequest.Visible = btnEditRequest.Visible = true;
                    break;
                case 4:
                    btnEditRequest.Visible = true;
                    break;
            }

            /* Настройка сипска статусов */

            List<Model.Status> statuses = Helper.DB.Status.ToList();
            Model.Status status = new Model.Status();
            status.StatusID = 0;
            status.StatusName = "Все статусы";
            statuses.Insert(0, status);
            cbFilter.DataSource = statuses;
            cbFilter.DisplayMember = "StatusName";
            cbFilter.ValueMember = "StatusID";

            ShowRequests();
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowRequests();
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            ShowRequests();
        }

        private void btnNewRequest_Click(object sender, EventArgs e)
        {
            View.WorkRequest workRequest = new WorkRequest(0, 0);
            this.Hide();
            workRequest.ShowDialog();
            this.Show();
        }

        private void btnEditRequest_Click(object sender, EventArgs e)
        {
            if (dgvRequests.SelectedCells.Count != 0)
            {
                View.WorkRequest workRequest = new WorkRequest(Helper.user.UserRole, Convert.ToInt32(dgvRequests.CurrentRow.Cells[0].Value));
                this.Hide();
                workRequest.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("Завявку выберите, пж", "Брах");
                return;
            }
        }        
    }
}
