﻿using ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.ViewModel;
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
using System.Windows.Shapes;

namespace ObjektorienteradProgrammeringGrundArvidRönnkvistSYSM8.View
{
    /// <summary>
    /// Interaction logic for UserDetailsWindow.xaml
    /// </summary>
    public partial class UserDetailsWindow : Window
    {
        public UserDetailsWindow()
        {
            InitializeComponent();
            var viewModel = new UserDetailsViewModel();
            viewModel.CloseRequested += ViewModel_CloseRequested;
            DataContext = new UserDetailsViewModel();
        }
        private void ViewModel_CloseRequested(object sender, EventArgs e)
        {
            this.Close(); // Close the window
        }
    }
}
