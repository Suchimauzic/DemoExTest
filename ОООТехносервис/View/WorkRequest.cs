using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ОООТехносервис.Classes;

namespace ОООТехносервис.View
{
    public partial class WorkRequest : Form
    {
        int mode, requestId;
        Model.Request request;

        public WorkRequest(int mode, int requestId)
        {
            InitializeComponent();

            this.mode = mode;
            this.requestId = requestId;
            request = new Model.Request();
        }

        private void WorkRequest_Load(object sender, EventArgs e)
        {
            
        }

        private void WorkRequest_Shown(object sender, EventArgs e)
        {
            SetComboBox<Model.Equipment>(cbEquipment, "EquipmentName", "EquipmentId", Helper.DB.Equipment.ToList());
            SetComboBox<Model.User>(cbClient, "UserFullName", "UserId", Helper.DB.User.Where(u => u.UserRole == 1).ToList());
            SetComboBox<Model.User>(cbMaster, "UserFullName", "UserId", Helper.DB.User.Where(u => u.UserRole == 2).ToList());
            SetComboBox<Model.Priority>(cbPriority, "PriorityName", "PriorityId", Helper.DB.Priority.ToList());
            SetComboBox<Model.Stage>(cbStage, "StageName", "StageId", Helper.DB.Stage.ToList());
            SetComboBox<Model.Status>(cbStatus, "StatusName", "StatusId", Helper.DB.Status.ToList());
            SetComboBox<Model.Problem>(cbProblem, "ProblemName", "ProblemId", Helper.DB.Problem.ToList());

            tbId.Enabled = tbDate.Enabled = false;

            if (mode == 0)
            {
                tbComment.Enabled = tbRequestTime.Enabled = false;
                tbDate.Text = DateTime.Now.ToShortDateString();
                cbEquipment.Enabled = cbClient.Enabled = cbMaster.Enabled = cbPriority.Enabled = tbDescription.Enabled = cbProblem.Enabled = true;
                tbRequestTime.Text = "0";
                cbStatus.SelectedValue = 2;
                cbStage.SelectedValue = 2;
            }
            else
            {
                request = Helper.DB.Request.Where(r => r.RequestId == requestId).FirstOrDefault();

                tbId.Text = requestId.ToString();
                tbDate.Text = request.RequestDate.ToShortDateString();
                cbEquipment.SelectedValue = request.RequestEquipmentId;
                cbClient.SelectedValue = request.RequestUserId;
                tbDescription.Text = request.RequestDescription;
                cbMaster.SelectedValue = request.RequestMasterId;
                tbRequestTime.Text = request.RequestTime.ToString();
                cbPriority.SelectedValue = request.RequestPriorityId;
                cbStatus.SelectedValue = request.RequestStatusId;
                cbStage.SelectedValue = request.RequestStageId;
                tbComment.Text = request.RequestComment;
                cbProblem.SelectedValue = request.RequestProblemId;

                if (mode == 3)
                {
                    cbMaster.Enabled = true;
                }
                else
                {
                    tbDescription.Enabled = tbRequestTime.Enabled = cbStage.Enabled = tbComment.Enabled = true;
                }
            }
        }

        private void SetComboBox<T>(System.Windows.Forms.ComboBox comboBox, string displayMember, string valueMember, List<T> list)
        {
            comboBox.DataSource = list;
            comboBox.DisplayMember = displayMember;
            comboBox.ValueMember = valueMember;
            comboBox.Enabled = false;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFixed_Click(object sender, EventArgs e)
        {
            request.RequestComment = tbComment.Text;
            request.RequestDate = Convert.ToDateTime(tbDate.Text);
            request.RequestDescription = tbDescription.Text;
            request.RequestEquipmentId = (int)cbEquipment.SelectedValue;
            request.RequestUserId = (int)cbClient.SelectedValue;
            request.RequestMasterId = (int)cbMaster.SelectedValue;
            request.RequestProblemId = (int)cbProblem.SelectedValue;
            request.RequestComment = tbComment.Text;
            request.RequestPriorityId = (int)cbStage.SelectedValue;
            request.RequestStageId = (int)cbStage.SelectedValue;
            request.RequestStatusId = (int)cbStatus.SelectedValue;
            request.RequestTime = Convert.ToInt32(tbRequestTime.Text);

            if (mode == 0)
            {
                Helper.DB.Request.Add(request);
            }

            try
            {
                Helper.DB.SaveChanges();
                MessageBox.Show("Данные успешно обновлены", "Найс");
            }
            catch
            {
                MessageBox.Show("Не получилось сохранить данные", "Ошибка сейва бд");
                return;
            }
        }
    }
}
