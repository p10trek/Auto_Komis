﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Auto_Komis
{
    /// <summary>
    /// Logika interakcji dla klasy CarList.xaml
    /// </summary>
    public partial class CarList : Window,ISqlComunicator
    {
        public List<Car> ListOfCars { get; set; }
        public string ProcedureName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public CarList()
        {
            InitializeComponent();
        }

        public bool AddData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool GetData(string ProcedureName)
        {
            throw new NotImplementedException();
        }

        public bool ModifyData(string ProcedureName)
        {
            throw new NotImplementedException();
        }
    }
}