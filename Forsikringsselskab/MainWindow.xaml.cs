﻿using FuncLayer;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Forsikringsselskab
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Func Func { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Func;
        }

        private void BtnGem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRediger_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSlet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
