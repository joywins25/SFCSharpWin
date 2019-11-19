using System;
using System.Collections;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace SFCSWin0300_ListViewDB
{
    public partial class Form1 : Form
    {
        private bool Isort = true;
        string connectionString;

        public Form1()
        {
            InitializeComponent();
            connectionString = getSqlConnectionString();
        }

        private string getSqlConnectionString()
        {
            //...0010.Build connection string
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "localhost";   // update me
            builder.UserID = "sa";              // update me
            builder.Password = "mssql";      // update me
            builder.InitialCatalog = "master";

            return builder.ConnectionString;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {

                this.lvPrice.Items.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    insertData(connection);
                    selectData(connection);
                }
            }
        }

        private void insertData(SqlConnection connection)
        {
            //INSERT INTO COFFEE_SALES (coffee_kind, coffee_name, price, orderTime)
            //VALUES('Coffee', 'Americano', 200, getdate())
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("USE SampleDB; ");
            sb.Append("INSERT INTO COFFEE_SALES (coffee_kind, coffee_name, price, orderTime) ");
            sb.Append("VALUES (@coffee_kind, @coffee_name, @price, getdate());");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@coffee_kind", txtKind.Text);
                command.Parameters.AddWithValue("@coffee_name", txtName.Text);
                command.Parameters.AddWithValue("@price", txtPrice.Text);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " row(s) inserted");
                if (rowsAffected > 0)
                {
                    MessageBox.Show("입력 성공");
                }
                else
                {
                    MessageBox.Show("입력 실패");
                }
            }

            clearTextBox();
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
            this.txtId.Text = this.lvPrice.SelectedItems[0].SubItems[0].Text;
            this.txtKind.Text = this.lvPrice.SelectedItems[0].SubItems[1].Text;
            this.txtName.Text = this.lvPrice.SelectedItems[0].SubItems[2].Text;
            this.txtPrice.Text = this.lvPrice.SelectedItems[0].SubItems[3].Text;
            this.txtOrderTime.Text = this.lvPrice.SelectedItems[0].SubItems[4].Text;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {

                this.lvPrice.Items.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    updateData(connection);
                    selectData(connection);
                }
            }
        }

        private void updateData(SqlConnection connection)
        {
            //UPDATE COFFEE_SALES SET price = 200 WHERE coffee_name='Americano';
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("USE SampleDB; ");
            sb.Append("UPDATE COFFEE_SALES SET price = @price " +
                "WHERE coffee_kind = @coffee_kind and coffee_name = @coffee_name;");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@coffee_kind", txtKind.Text);
                command.Parameters.AddWithValue("@coffee_name", txtName.Text);
                command.Parameters.AddWithValue("@price", txtPrice.Text);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " row(s) updated");
                if (rowsAffected > 0)
                {
                    MessageBox.Show("수정 성공");
                }
                else
                {
                    MessageBox.Show("수정 실패");
                }
            }

            clearTextBox();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (checkInput())
            {

                this.lvPrice.Items.Clear();
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    deleteData(connection);
                    selectData(connection);
                }
            }
        }

        private void deleteData(SqlConnection connection)
        {
            //DELETE FROM COFFEE_SALES WHERE coffee_name LIKE 'A%'
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            sb.Append("USE SampleDB; ");
            sb.Append("DELETE FROM COFFEE_SALES WHERE coffee_kind = @coffee_kind and coffee_name = @coffee_name;");
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@coffee_kind", txtKind.Text);
                command.Parameters.AddWithValue("@coffee_name", txtName.Text);
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine(rowsAffected + " row(s) deleted");
                if (rowsAffected > 0)
                {
                    MessageBox.Show("삭제 성공");
                }
                else
                {
                    MessageBox.Show("삭제 실패");
                }
            }

            clearTextBox();
        }

        private void clearTextBox()
        {
            this.txtId.Clear();
            this.txtKind.Clear();
            this.txtName.Clear();
            this.txtPrice.Clear();
            this.txtOrderTime.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {         
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                selectData(connection);
            }

        }

        private void selectData(SqlConnection connection)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("USE SampleDB; ");
            sb.Append("SELECT CAST(id as nvarchar(5)), coffee_kind, coffee_name, CAST(price as nvarchar(8)), orderTime FROM COFFEE_SALES;"); //...0010.INT 2 STRING.
            String sql = sb.ToString();
            using (SqlCommand command = new SqlCommand(sql, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string strDate = Convert.ToDateTime(reader["orderTime"]).ToString("yyyy년MM월dd일,ddd,HH:mm:ss"); //...0020.DateFormat.
                        var strArray = new String[] {
                                reader.GetString(0),
                                reader.GetString(1),
                                reader.GetString(2),
                                reader.GetString(3),
                                strDate
                            };
                        var lvt = new ListViewItem(strArray);
                        this.lvPrice.Items.Add(lvt);
                    }
                }
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            clearTextBox();
            this.lvPrice.Items.Clear();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                selectData(connection);
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

//...0010.INT 2 STRING.https://www.c-sharpcorner.com/blogs/convert-integer-to-string-in-sql-server

//...0020.DateFormat._CS200_271p.https://stackoverflow.com/questions/5619216/reading-a-date-using-datareader