﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
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
using TalentBookingManagement.Models;
using TalentBookingManagement.ViewModels;

namespace TalentBookingManagement
{
    /// <summary>
    /// Interaction logic for CreateCampaignWindow.xaml
    /// </summary>
    public partial class CreateCampaignWindow : Window
    {
        public Campaign CreatedCampaign { get; private set; }

        public CreateCampaignWindow()
        {
            InitializeComponent();
            DataContext = new CreateCampaignViewModel();
        }

    }

}
