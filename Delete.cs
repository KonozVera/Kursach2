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
    public partial class Delete : Form
    {
        Thread th;
        public Delete()
        {
            InitializeComponent();
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

        private void deletebookButton_Click(object sender, EventArgs e)
        {
            int Key = Convert.ToInt32(index_textBox.Text);
            //Library.Pass_Book(books, client)
            if (Library.Delete_Book(Key))
            {
                MessageBox.Show("Книгу успешно удалили!");
                this.Close();
                th = new Thread(openMenu);
                th.SetApartmentState(ApartmentState.STA);
                th.Start();
            }
            else
                MessageBox.Show("Книга есть у клиентов!");
            
        }

        private void index_textBox_Enter(object sender, EventArgs e)
        {
            if (index_textBox.Text == "Введите индекс книги")
            {
                index_textBox.Text = String.Empty;
            }
        }

        private void index_textBox_Leave(object sender, EventArgs e)
        {
            if (index_textBox.Text == String.Empty)
            {
                index_textBox.Text = "Введите индекс книги";
            }
        }

        private void index_textBox_Validating(object sender, CancelEventArgs e)
        {
            string errorMsg = "";
            if (!Error_Debugger.CheckYear(index_textBox.Text, out errorMsg))
            {
                e.Cancel = true;
                index_textBox.Select(0, index_textBox.Text.Length);
                this.errorProvider1.SetError(index_textBox, errorMsg);
            }
        }

        private void index_textBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(index_textBox, "");
        }
    }
}
