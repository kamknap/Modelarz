﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Modelarz
{
    public partial class Wizyty : Form
    {
        public Wizyty()
        {
            InitializeComponent();

            SetData();
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";

            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.ShowUpDown = true;
            dateTimePicker2.CustomFormat = "HH:mm";

        }

        public Wizyty(DateTime selectedDate)
        {
            InitializeComponent();
        }

        void SaveDataToFile()
        {

            String imie = textBox1.Text;
            String nazwisko = textBox2.Text;
            String data = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            String godzina = dateTimePicker2.Value.ToString("HH:mm");

            for (int i = 0; i < Home.dataArray.GetLength(0); i++)
            {
                TimeSpan time1 = TimeSpan.Parse(Home.dataArray[i, 3]);
                TimeSpan time2 = TimeSpan.Parse(godzina);
                TimeSpan diff = time2 - time1;
                if (Math.Abs(diff.TotalHours) < 2)
                {
                    MessageBox.Show("Zachowaj odstęp 2 godzin między wizytami");
                    return;
                }

            }

            if (imie == "" || nazwisko == "" || data == "" || godzina == "")
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione");
                return;
            }
           
            String path = "visits.txt";
            String text = data + ";" + imie + ";" + nazwisko + ";" + godzina + Environment.NewLine;

            System.IO.File.AppendAllText(path, text);
            MessageBox.Show("Dodano wizytę");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SaveDataToFile();
            ClearData();
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClearData();
        }

        void ClearData()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
        }

        void SetData()
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 12, 0, 0);
        }

    }
}
