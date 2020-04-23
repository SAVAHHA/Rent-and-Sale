﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAVAHHArent;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SAVAHHArent.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public string UserName;
        public int UserAge;


        public MainPage(int Age, string Name)
        {
            InitializeComponent();
            UserAge = Age;
            UserName = Name;

            WelcomeLabel.Text = "Welcome, " + UserName + "!";
        }
    }
}