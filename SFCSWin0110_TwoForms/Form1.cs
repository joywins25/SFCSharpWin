﻿using System.Drawing;
using System.Windows.Forms;

namespace SFCSWin0110_TwoForms
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();

      this.ClientSize = new Size(500, 500);
      //this.FormBorderStyle = FormBorderStyle.Fixed3D;
      //this.StartPosition = FormStartPosition.CenterParent;

      Form2 f2 = new Form2();
      this.AddOwnedForm(f2);
      //f2.StartPosition = FormStartPosition.CenterParent;

      f2.Show();
      //f2.ShowDialog();
    }

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }
    }
}
