using System;
using System.Windows.Forms;

namespace SFCSWin0100_Hi
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
      label1.Text = "";
    }

    private void Button1_Click(object sender, EventArgs e)
    {
      label1.Text = "Hi! WinForm Application!";
    }
  }
}
