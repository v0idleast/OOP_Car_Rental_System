using Lab2_Yampol.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab2_Yampol
{

  public partial class Form1 : MaterialForm
  {
    private Dictionary<string, int> carIndexes = new Dictionary<string, int>();
    private Form2 formReceiver;

    public Form1()
    {
      InitializeComponent();
      LoadComboBoxData();
    }

    private void button1_Click(object sender, EventArgs e)
    {
      if(String.IsNullOrEmpty( textBox1.Text ) || String.IsNullOrEmpty(TextBox2.Text))
      {
        MessageBox.Show("Write data in filds!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      string name = textBox1.Text;
      string passport = TextBox2.Text;
      decimal rentdays = numericUpDown1.Value;

      int carId = CarsSelectedIndex();

      if (carId == -1)
      {
        MessageBox.Show("Choose car!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        return;
      }

      Models.Order order = new Order() {
        UserName = name,
        PassportCode = passport,
        Days = rentdays,
        CarId = carId,
        IsBroken = false,
        IsDecline = true,
      };

      using (var context = new AppDbContext())
      {
        context.Orders.Add(order);
        context.SaveChanges();
      }

      if (formReceiver != null)
      {
        formReceiver.UpdateList();
      }

      textBox1.Text = "";
      TextBox2.Text = "";
    }

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (formReceiver != null && !formReceiver.IsDisposed)
      {
        formReceiver.Close();
      }
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      formReceiver = new Form2();
      formReceiver.Show();
    }

    private void LoadComboBoxData()
    {
      comboBoxCars.DropDownStyle = ComboBoxStyle.DropDownList;
      using (var context = new AppDbContext())
      {
        var cars = context.Cars.ToList();

        foreach (var car in cars)
        {
          string displayText = $"{car.Name} - {car.Price}$";
          comboBoxCars.Items.Add(displayText);
          carIndexes[displayText] = car.Id;
        }

        if (comboBoxCars.Items.Count > 0)
        {
          comboBoxCars.SelectedIndex = 0;
        }
      }
    }

    private int CarsSelectedIndex()
    {
      string selectedItem = comboBoxCars.SelectedItem.ToString();

      if (carIndexes.TryGetValue(selectedItem, out int carId))
      {
        return carId;
      }

      return -1;
    }

  }
}
