using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Kursach2
{
    public partial class Menu : Form
    {
        Thread th;
        public Menu()
        {
            InitializeComponent();
        }

        private void regButton_Click(object sender, EventArgs e)
        {
            Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
            Show();
            registration.Close();
        }
        private void burn_book_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openBurn);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openReg(object obj)
        {
            Application.Run(new Registration());
        }
        public void openBurn(object obj)
        {
            Application.Run(new Finder());
        }

        public void openReturn(object obj)
        {
            Application.Run(new ReturnBook());
        }

        private void return_book_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openReturn);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openDelete(object obj)
        {
            Application.Run(new Delete());
        }

        public void openAddBook(object obj)
        {
            Application.Run(new AddBooks());
        }

        private void delete_book_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openDelete);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        private void Menu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Library.SerializeResult();
        }

        private void add_book_Button_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openAddBook);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
    }
}
