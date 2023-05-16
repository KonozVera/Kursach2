using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach2
{
    public partial class ReturnBook : Form
    {
        Thread th;
        public ReturnBook()
        {
            InitializeComponent();
        }

        private void returnbookButton_Click(object sender, EventArgs e)
        {
            if (login_in_find_Field.Text == "")
            {
                MessageBox.Show("Вы не заполнили все поля!");
                return;
            }

            Clients client = Library.GetClient(login_in_find_Field.Text);
            if (checkedListBox1.CheckedItems.Count != 0 && client != null)
            {
                string s = "";
                for (int x = checkedListBox1.CheckedItems.Count - 1; x >= 0; x--)
                {
                    Library.Pass_Book(checkedListBox1.CheckedItems[x] as Books, client);
                    checkedListBox1.Items.Remove(checkedListBox1.CheckedItems[x]);
                }

                MessageBox.Show("Книга успешно возвращена!");
            }
            else
            {
                string number = login_in_find_Field.Text;
                MessageBox.Show("Ошибка!");
                this.DialogResult = DialogResult.OK;
            }
        }

        private void login_in_find_Field_Enter(object sender, EventArgs e)
        {
            if (login_in_find_Field.Text == "Введите логин")
            {
                login_in_find_Field.Text = String.Empty;
            }
        }

        private void login_in_find_Field_Leave(object sender, EventArgs e)
        {
            if (login_in_find_Field.Text == String.Empty)
            {
                login_in_find_Field.Text = "Введите логин";
            }
        }

        private void login_in_find_Field_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg;
            if (!Error_Debugger.Is_Number_Valide(login_in_find_Field.Text, out errorMsg))
            {
                e.Cancel = true;
                login_in_find_Field.Select(0, login_in_find_Field.Text.Length);
                this.errorProvider1.SetError(login_in_find_Field, errorMsg);
            }
        }

        private void login_in_find_Field_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(login_in_find_Field, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
            string login = login_in_find_Field.Text;
            Clients client = Library.GetClient(login);

            if (client is null)
            {
                return;
            }

            foreach (Recordings recording in client.Carta.Recordings)
            {
                if (!recording.IsAvailable && !checkedListBox1.Items.Contains(recording.Books))
                    checkedListBox1.Items.Add(recording.Books);
            }
        }

        public void openMenu(object obj)
        {
            Application.Run(new Menu());
        }

        private void return_menuButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openMenu);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
