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
    public partial class Finder : Form
    {
        Thread th;
        public Finder()
        {
            InitializeComponent();
        }

        private void outButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openOut);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }
        public void openOut(object obj)
        {
            Application.Run(new Menu());
        }

        private void findButton_Click(object sender, EventArgs e)
        {
            if (FinderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали каким образом осуществлять поиск!");
                return;
            }

            string errorMsg = "";
            int type = -1;
            if (!string.IsNullOrWhiteSpace(inputBox.Text))
                switch (FinderComboBox.Items[FinderComboBox.SelectedIndex])
                {
                    case "названию":
                        type = 0;
                        break;
                    case "автору":
                        type = 1;
                        if (!Error_Debugger.CheckStringAthor_finder(inputBox.Text, out errorMsg))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        break;
                    case "году издания":
                        type = 2;
                        if (!Error_Debugger.CheckYear(inputBox.Text, out errorMsg))
                        {
                            this.DialogResult = DialogResult.OK;
                        }
                        break;
                    default:
                        type = -1;
                        break;
                }
            else
            {
                errorProvider1.SetError(findButton, "Произошла ошибка!");
            }
            Hide();
            Findknown_book findknownbook = new Findknown_book(inputBox.Text, type);
            findknownbook.ShowDialog();
            Show();
            findknownbook.Close();
        }

        private void inputBox_Validating(object sender, CancelEventArgs e)
        {
            if (FinderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали каким образом осуществлять поиск!");
                return;
            }
            string errorMsg = "";
            switch (FinderComboBox.Items[FinderComboBox.SelectedIndex])
            {
                case "названию":
                    break;
                case "автору":
                    if (!Error_Debugger.CheckStringAthor_finder(inputBox.Text, out errorMsg))
                        e.Cancel = true;
                    inputBox.Select(0, inputBox.Text.Length);
                    this.errorProvider1.SetError(inputBox, errorMsg);
                    break;
                case "году издания":
                    if (!Error_Debugger.CheckYear(inputBox.Text, out errorMsg))
                        e.Cancel = true;
                    inputBox.Select(0, inputBox.Text.Length);
                    this.errorProvider1.SetError(inputBox, errorMsg);
                    break;
                default:
                    break;
            }
        }

        private void inputBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(inputBox, "");
        }

        private void additionalinputBox_Validating(object sender, CancelEventArgs e)
        {
            if (additional_finderComboBox.SelectedIndex == -1)
            {
                MessageBox.Show("Вы не выбрали каким образом осуществлять поиск!");
                return;
            }

            string errorMsg = "";
            switch (additional_finderComboBox.Items[additional_finderComboBox.SelectedIndex])
            {
                case "названию":
                    break;
                case "автору":
                    if (!Error_Debugger.CheckStringAthor_finder(additionalinputBox.Text, out errorMsg))
                        e.Cancel = true;
                    additionalinputBox.Select(0, additionalinputBox.Text.Length);
                    this.errorProvider1.SetError(additionalinputBox, errorMsg);
                    break;
                case "году издания":
                    if (!Error_Debugger.CheckYear(additionalinputBox.Text, out errorMsg))
                        e.Cancel = true;
                    additionalinputBox.Select(0, additionalinputBox.Text.Length);
                    this.errorProvider1.SetError(additionalinputBox, errorMsg);
                    break;
                default:
                    break;
            }
        }

        private void additionalinputBox_Validated(object sender, EventArgs e)
        {
            errorProvider1.SetError(additionalinputBox, "");
        }

        private void anotherfinderButton_Click(object sender, EventArgs e)
        {
            this.Close();
            th = new Thread(openFindallBooks);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();
        }

        public void openFindallBooks(object obj)
        {
            Application.Run(new FindallBooks());
        }
    }
}
