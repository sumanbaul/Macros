﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using Macros;
using Xamarin.Forms;

namespace Macros
{
	public partial class App : Application
	{
        public static string DB_PATH = string.Empty;

        public App ()
		{
			InitializeComponent();

            //MainPage = new MenuPage();
            MainPage = new NavigationPage(new Splash.splash());
		}

        public App(string DB_Path)
        {
            InitializeComponent();
            DB_PATH = DB_Path;
            //MainPage = new MenuPage();
            MainPage = new NavigationPage(new Splash.splash());
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
