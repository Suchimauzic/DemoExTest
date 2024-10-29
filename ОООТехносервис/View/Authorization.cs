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
using ОООТехносервис.View;

namespace ОООТехносервис
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();

            try
            {
                Helper.DB = new Model.DBRequests();
                MessageBox.Show("Всё чики-пуки!", "Ошибка!.. А не, всё норм!",MessageBoxButtons.OK);
            }
            catch
            {
                MessageBox.Show("Афигенна! Программа не работает!", "Ошибка... Чурхчела....", MessageBoxButtons.OK);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            string login, password;
            List<Model.User> users = Helper.DB.User.ToList();

            login = tbLogin.Text;
            password = tbPass.Text;

            users = users.Where(u => u.UserLogin == login && u.UserPassword == password).ToList();
            Helper.user = users.FirstOrDefault();

            if (Helper.user != null)
            {
                MessageBox.Show("Yes! Yes!... YES!!! Всё круто!! У вас есть такой аккаунт! А ещё Вы правильно указали ваш супер сложный пароль! Круто! " +
                    "Молодец! В том же духе! Не каждый догадается до пароля \"" + password + "\"! Вы просто супер!!", "Ес", MessageBoxButtons.OK);
                MessageBox.Show("Ваша роль: " + Helper.user.Role.RoleName, "Роль", MessageBoxButtons.OK);

                ListRequests listRequests = new ListRequests();
                this.Hide();
                listRequests.ShowDialog();
                this.Show();

            }
            else
            {
                MessageBox.Show("Не удалось найти пользователя в базе данных! Просим вас удостовериться в том, что база данных хранит в себе такого " +
                    "пользователя, а иначе подключение не получиться", "Жалко пчёлку!", MessageBoxButtons.OK);
            }
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            tbLogin.Text = "loginDEppn2018";
            tbPass.Text = "6}i+FD";
        }

        private void btnMaster_Click(object sender, EventArgs e)
        {
            tbLogin.Text = "loginDEdpy2018";
            tbPass.Text = "MJ0W|f";
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            tbLogin.Text = "loginDExvq2018";
            tbPass.Text = "hX0wJz";
        }

        private void btnManager_Click(object sender, EventArgs e)
        {
            tbLogin.Text = "loginDEvke2018";
            tbPass.Text = "WQLXSl";
        }
    }
}
