using System;
using System.Collections;
using System.Windows.Forms;

namespace SFCSWin0300_ListViewDB
{
    public partial class Form1 : Form
    {
        private bool Isort = true;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                //MessageBox.Show("Okay and Go!"); 

                var strArray = new String[] { this.txtKind.Text, this.txtName.Text, this.txtPrice.Text };
                var lvt = new ListViewItem(strArray);
                this.lvPrice.Items.Add(lvt);

                this.txtKind.Clear();
                this.txtName.Clear();
                this.txtPrice.Clear();
            }
        }

        private bool checkInput()
        {
            if (this.txtKind.Text == "")
            {
                MessageBox.Show("종류를 입력하세요!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtKind.Focus();
                return false;
            }
            else if (this.txtName.Text == "")
            {
                MessageBox.Show("품명을 입력하세요!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtName.Focus();
                return false;
            }
            else if (this.txtPrice.Text == "")
            {
                MessageBox.Show("가격 입력하세요!!", "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.txtPrice.Focus();
                return false;
            }
            else
            {
                return true;
            }
        }

        private void lvPrice_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (Isort == true)
            {
                this.lvPrice.ListViewItemSorter = new ListViewItemComparer(e.Column, Isort);
                Isort = false;
            }
            else
            {
                this.lvPrice.ListViewItemSorter = new ListViewItemComparer(e.Column, Isort);
                Isort = true;
            }

            this.txtKind.Clear();
            this.txtName.Clear();
            this.txtPrice.Clear();

        }

        private void lvPrice_Click(object sender, EventArgs e)
        {
            this.txtKind.Text = this.lvPrice.SelectedItems[0].SubItems[0].Text;
            this.txtName.Text = this.lvPrice.SelectedItems[0].SubItems[1].Text;
            this.txtPrice.Text = this.lvPrice.SelectedItems[0].SubItems[2].Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                this.lvPrice.SelectedItems[0].SubItems[0].Text = this.txtKind.Text;
                this.lvPrice.SelectedItems[0].SubItems[1].Text = this.txtName.Text;
                this.lvPrice.SelectedItems[0].SubItems[2].Text = this.txtPrice.Text;

                this.txtKind.Clear();
                this.txtName.Clear();
                this.txtPrice.Clear();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {
                this.lvPrice.SelectedItems[0].Remove();

                this.txtKind.Clear();
                this.txtName.Clear();
                this.txtPrice.Clear();
            }

        }

    }

    internal class ListViewItemComparer : IComparer
    {
        private int column;
        private bool isort;

        public ListViewItemComparer(int column, bool isort)
        {
            this.column = column;
            this.isort = isort;
        }

        public int Compare(object x, object y)
        {
            object a, b;
            if (isort == true)
            {
                a = x;
                b = y;
            }
            else
            {
                a = y;
                b = x;
            }
            return String.Compare(((ListViewItem)a).SubItems[column].Text, ((ListViewItem)b).SubItems[column].Text);
        }
    }
}
