﻿using TaskNoter.MVVM.ViewModels;

namespace TaskNoter
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
            //BindingContext = new MainViewModel();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }

        //private void Button_Clicked(object sender, EventArgs e)
        //{
        //    ((MainViewModel)BindingContext).AddTaskCommand.Execute(null);
        //}
    }

}
