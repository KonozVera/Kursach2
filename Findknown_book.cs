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
    public partial class Findknown_book : Form
    {
        Thread th;
        private string filters;
        private int filterType;
        public Findknown_book()
        {
            InitializeComponent();
        }
        public Findknown_book(string filters, int filterType)
        {
            InitializeComponent();
            this.filters = filters;
            this.filterType = filterType;
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

        private void outButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void add_bookButton_Click(object sender, EventArgs e)
        {
            if (login_in_find_Field.Text == "Введите логин")
            {
                MessageBox.Show("Вы не заполнили все поля!");
                return;
            }

            if (all_bookcheckedListBox.CheckedItems.Count != 0)
            {
                string s = "";
                List<Books> books = new List<Books>();
                for (int x = 0; x < all_bookcheckedListBox.CheckedItems.Count; x++)
                {
                    s = s + (x + 1).ToString() + all_bookcheckedListBox.CheckedItems[x].ToString() + "\n";
                    string[] books_strings = all_bookcheckedListBox.CheckedItems[x].ToString().Split('\t');
                    Books book = Library.GetBookByName(books_strings[0]);
                    if (book != null) 
                        books.Add(book);
                }
                Clients client = Library.GetClient(this.login_in_find_Field.Text);
                if (client != null)
                {
                    foreach (var book in books)
                        Library.Get_Book(client, book);
                }
                MessageBox.Show("Книга успешно добавлена!");
            }
            else
            {
                MessageBox.Show("Вы не заполнили все поля!");
                return;
            }
            this.Close();
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

        private void Findknown_book_Load(object sender, EventArgs e)
        {
            Dictionary<int, Clients> dictionary = Library.Clients;
            List<Books> list = Library.Books;
            foreach (Books book in list)
            {
                switch (filterType)
                {
                    case 0:
                        if (book.name.Contains(filters))
                            all_bookcheckedListBox.Items.Add(book.name + '\t' + book.author + '\t' + book.date_of_publishing.ToShortDateString());
                        break;
                    case 1:
                        if (book.author.Contains(filters))
                            all_bookcheckedListBox.Items.Add(book.name + '\t' + book.author + '\t' + book.date_of_publishing.ToShortDateString());
                        break;
                    case 2:
                        if (book.date_of_publishing.Year == Convert.ToInt32(filters))
                            all_bookcheckedListBox.Items.Add(book.name + '\t' + book.author + '\t' + book.date_of_publishing.ToShortDateString());
                        break;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string login = login_in_find_Field.Text;
            Clients client = Library.GetClient(login);

            if (client is null)
            {
                MessageBox.Show("Такого клиента не существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            foreach (Recordings recording in client.Carta.Recordings)
            {
                if (!recording.IsAvailable && !checkedListBox1.Items.Contains(recording.Books))
                    checkedListBox1.Items.Add(recording.Books);
            }
        }

        private void login_in_find_Field_TextChanged(object sender, EventArgs e)
        {
            checkedListBox1.Items.Clear();
        }
    }
}
