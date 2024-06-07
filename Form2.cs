using Lab2_Yampol.Models;
using MaterialSkin;
using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Lab2_Yampol
{
  public interface IListUpdater
  {
    void UpdateList();
  }


  public partial class Form2 : MaterialForm, IListUpdater
  {
    private int totalMoney = 0;
    private Dictionary<string, Order> orderIndexes = new Dictionary<string, Order>();

    public Form2()
    {
        InitializeComponent();
        UpdateList();
    }

    public void UpdateList()
    {
      using (var context = new AppDbContext())
      {
        listBox1.Items.Clear();
        orderIndexes.Clear();

        //All Orders
        var orders = context.Orders.Include(el => el.Car);

        //OrdersSum
        var totalSum = orders.Where(o => !o.IsDecline)
                .Sum(o => (o.Car.Price * (double)o.Days) + (o.IsBroken ? 100 : 0));

        //BrokenCount
        var brokenCount = orders.Where(el => el.IsBroken).Count();

        //Group
        var nameCount = orders.GroupBy(el => el.Car.Name).Count();

        //Filter
        if (checkBox2.Checked)
        {
          orders = orders.Where(o => o.IsDecline);
        }

        if (checkBox3.Checked)
        {
          orders = orders.Where(o => o.IsBroken);
        }

        foreach (var order in orders)
        {
          string displayText = $"{order.Id} | {order.UserName} | {order.PassportCode} | {order.Days} day(s) | {order.Car.Price}$ | {order.Car.Name} | {(order.IsBroken ? "Broken" : "Not Broken")} | {(order.IsDecline ? "Declined" : "Approved")}";
          listBox1.Items.Add(displayText);
          orderIndexes[displayText] = order;
        }

        textBox1.Text = totalSum + "$";

        materialMultiLineTextBox1.Text = brokenCount + "";
        materialMultiLineTextBox2.Text = nameCount + "";
      }
    }


    private void button1_Click(object sender, EventArgs e)
    {
      modifyDecline(false);
    }

    private void button1_Click_1(object sender, EventArgs e)
    {
      modifyDecline(true);
    }

    private void materialButton1_Click(object sender, EventArgs e)
    {
      modifyBroken(true);
    }

    private void materialButton3_Click(object sender, EventArgs e)
    {
      modifyBroken(false);
    }

    private void modifyDecline(bool decline)
    {
      if (listBox1.SelectedIndex != -1)
      {
        using (var context = new AppDbContext())
        {
          Order order = OrdersSelectedIndex();
          var orderFromBD = context.Orders.SingleOrDefault(o => o.Id == order.Id);

          if (orderFromBD == null) return;

          orderFromBD.IsDecline = decline;
          context.SaveChanges();
        }

        UpdateList();
      }
    }

    private void modifyBroken(bool broken)
    {
      if (listBox1.SelectedIndex != -1)
      {
        using (var context = new AppDbContext())
        {
          Order order = OrdersSelectedIndex();
          var orderFromBD= context.Orders.SingleOrDefault(o => o.Id == order.Id);
          if (orderFromBD == null) return;

          orderFromBD.IsBroken = broken;
          context.SaveChanges();
        }

        UpdateList();
      }
    }

    private void checkBox2_CheckedChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void checkBox3_CheckedChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private Order OrdersSelectedIndex()
    {
      string selectedItem = listBox1.SelectedItem.ToString();

      if (orderIndexes.TryGetValue(selectedItem, out Order order))
      {
        return order;
      }

      return null;
    }

    private void materialButton2_Click(object sender, EventArgs e)
    {
      if (listBox1.SelectedIndex != -1)
      {
        using (var context = new AppDbContext())
        {
          Order order = OrdersSelectedIndex();
          var orderForRemove = context.Orders.SingleOrDefault(o => o.Id == order.Id);
          if (orderForRemove == null) return;

          context.Orders.Remove(orderForRemove);
          context.SaveChanges();
        }

        UpdateList();
      }
    }
  }
}
