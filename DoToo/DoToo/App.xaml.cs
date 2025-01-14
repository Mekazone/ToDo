﻿using System;
using DoToo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DoToo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //Set the initial view as MainView.xaml
            MainPage = new NavigationPage(Resolver.Resolve<MainView>());

        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
