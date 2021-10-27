﻿using FormsControls.Base;
using Microcharts.Forms;
using System;
using WindesHeartApp.Resources;
using WindesHeartSDK;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace WindesHeartApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeartratePage : ContentPage, IAnimationPage
    {
        public Button interval15Button;
        public Button interval10Button;
        public Button interval5Button;
        public Button intervaldefaultButton;
        public HeartratePage()
        {
            InitializeComponent();
            BuildPage();
        }

        protected override void OnAppearing()
        {
            Globals.HeartratePageViewModel.OnAppearing();
        }

        private void BuildPage()
        {
            absoluteLayout = new AbsoluteLayout();
            PageBuilder.BuildPageBasics(absoluteLayout, this);
            PageBuilder.AddHeaderImages(absoluteLayout);
            PageBuilder.AddLabel(absoluteLayout, "Heartrate", 0.05, 0.10, Globals.LightTextColor, "", 0);
            PageBuilder.AddReturnButton(absoluteLayout, this);

            ImageButton previousBtn = new ImageButton
            {
                Source = "arrow_left.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(previousBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(previousBtn, new Rectangle(0.3, 0.135, 0.1, 0.1));
            previousBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "PreviousDayBinding" });
            previousBtn.Clicked += Globals.HeartratePageViewModel.PreviousDayBtnClick;
            absoluteLayout.Children.Add(previousBtn);


            ImageButton previousDayBtn = new ImageButton
            {
                Source = "arrow_left.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(previousDayBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(previousDayBtn, new Rectangle(0.3, 0.1, 0.1, 0.1));
            previousDayBtn.SetBinding(Button.CommandProperty, new Binding() { Path = "PreviousDayBinding" });
            previousDayBtn.Clicked += Globals.HeartratePageViewModel.PreviousDay2BtnClick;
            absoluteLayout.Children.Add(previousDayBtn);

            ImageButton nextBtn = new ImageButton
            {
                Source = "arrow_right.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(nextBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(nextBtn, new Rectangle(0.7, 0.135, 0.1, 0.1));
            nextBtn.Clicked += Globals.HeartratePageViewModel.NextDayBtnClick;
            absoluteLayout.Children.Add(nextBtn);

            ImageButton nextDayBtn = new ImageButton
            {
                Source = "arrow_right.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(nextDayBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(nextDayBtn, new Rectangle(0.7, 0.1, 0.1, 0.1));
            nextDayBtn.Clicked += Globals.HeartratePageViewModel.NextDay2BtnClick;
            absoluteLayout.Children.Add(nextDayBtn);


            ImageButton exportBtn = new ImageButton
            {
                Source = "arrow_right.png",
                BackgroundColor = Color.Transparent
            };
            AbsoluteLayout.SetLayoutFlags(exportBtn, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(exportBtn, new Rectangle(0.7, .75, 0.1, 0.1));
            exportBtn.Clicked += Globals.HeartratePageViewModel.ExportBtnClick;
            absoluteLayout.Children.Add(exportBtn);

            PageBuilder.AddLabel(absoluteLayout, "", 0.5, 0.20, Color.Black, "DayLabelText", 12);

            ChartView chart = new ChartView { BackgroundColor = Globals.PrimaryColor };
            //chart.Chart.BackgroundColor = Globals.PrimaryColor.ToSKColor();

            chart.SetBinding(ChartView.ChartProperty, "Chart");
            AbsoluteLayout.SetLayoutBounds(chart, new Rectangle(0.5, 0.4, 0.95, 0.35));
            AbsoluteLayout.SetLayoutFlags(chart, AbsoluteLayoutFlags.All);

            absoluteLayout.Children.Add(chart);

            PageBuilder.AddLabel(absoluteLayout, "", 0.5, 0.65, Color.Black, "AverageLabelText", 16);

            PageBuilder.AddLabel(absoluteLayout, "", 0.5, 0.70, Color.Black, "PeakHeartrateText", 16);

            #region heartrateinterval selector
            Image heartonlyImage2 = new Image { Source = "HeartOnlyTransparent.png" };
            AbsoluteLayout.SetLayoutFlags(heartonlyImage2, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(heartonlyImage2, new Rectangle(0.05, 0.85, 40, 40));
            absoluteLayout.Children.Add(heartonlyImage2);
            var itnervallabel = PageBuilder.AddLabel(absoluteLayout, "Interval:", 0.22, 0.84, Color.Black, "", 13);

            intervaldefaultButton = PageBuilder.AddButton(absoluteLayout, "1", OnIntervalLabelClicked, 0.50, 0.85, 40, 40, 25, 0, AbsoluteLayoutFlags.PositionProportional, Globals.SecondaryColor);
            intervaldefaultButton.BorderWidth = 1;
            intervaldefaultButton.BorderColor = Color.White;


            interval5Button = PageBuilder.AddButton(absoluteLayout, "5", OnIntervalLabelClicked, 0.65, 0.85, 40, 40, 25, 0, AbsoluteLayoutFlags.PositionProportional, Globals.SecondaryColor);
            interval5Button.BorderWidth = 1;
            interval5Button.BorderColor = Color.Black;

            interval10Button = PageBuilder.AddButton(absoluteLayout, "10", OnIntervalLabelClicked, 0.80, 0.85, 40, 40, 25, 0, AbsoluteLayoutFlags.PositionProportional, Globals.SecondaryColor);
            interval10Button.BorderWidth = 1;
            interval10Button.BorderColor = Color.White;

            interval15Button = PageBuilder.AddButton(absoluteLayout, "15", OnIntervalLabelClicked, 0.95, 0.85, 40, 40, 25, 0, AbsoluteLayoutFlags.PositionProportional, Globals.SecondaryColor);
            interval15Button.BorderWidth = 1;
            interval15Button.BorderColor = Color.White;
            #endregion



            #region refreshbutton
            Grid grid = new Grid
            {
            };
            Frame frame = new Frame();
            frame.CornerRadius = 10;
            frame.BorderColor = Color.White;
            frame.BackgroundColor = Globals.SecondaryColor;
            frame.HorizontalOptions = LayoutOptions.FillAndExpand;
            frame.VerticalOptions = LayoutOptions.FillAndExpand;


            frame.HasShadow = true;

            grid.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(execute: () => { RefreshButtonClicked(this, EventArgs.Empty); })
            });
            grid.Opacity = 20;
            AbsoluteLayout.SetLayoutBounds(grid, new Rectangle(0.15, 0.95, Globals.ScreenHeight / 100 * 10, Globals.ScreenHeight / 100 * 6));
            AbsoluteLayout.SetLayoutFlags(grid, AbsoluteLayoutFlags.PositionProportional);
            ImageButton RefreshButton = new ImageButton
            {
                Source = "Refresh.png",
                HorizontalOptions = LayoutOptions.Start,
                HeightRequest = Globals.ScreenHeight / 100 * 4.5,
                BackgroundColor = Color.Transparent,
            };
            
            RefreshButton.Margin = new Thickness(2, 0, 0, 0);
            RefreshButton.Clicked += RefreshButtonClicked;
            grid.Children.Add(frame);
            grid.Children.Add(RefreshButton);

            Label RefreshLabel = new Label() { Text = "data", VerticalOptions = LayoutOptions.CenterAndExpand, HorizontalOptions = LayoutOptions.End, FontSize = 20, FontAttributes = FontAttributes.Italic };
 

            RefreshLabel.Margin = new Thickness(0, 0, 2, 0);
            grid.Children.Add(RefreshLabel);

            RefreshLabel.GestureRecognizers.Add(new TapGestureRecognizer
            {
                NumberOfTapsRequired = 1,
                Command = new Command(execute: () => { RefreshButtonClicked(this, EventArgs.Empty); }),
            });
            absoluteLayout.Children.Add(grid);
            #endregion
        }

        private async void RefreshButtonClicked(object sender, EventArgs e)
        {
            if (Windesheart.PairedDevice == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error while refreshing data",
                    "Can only refresh data when connected to a device!", "Ok");
                return;
            }
            Application.Current.MainPage.Navigation.PopAsync();
            Globals.SamplesService.StartFetching();
        }

        private void OnIntervalLabelClicked(object sender, EventArgs args)
        {
            var intervalButton = sender as Button;

            intervaldefaultButton.BorderColor = Color.White;
            interval5Button.BorderColor = Color.White;
            interval15Button.BorderColor = Color.White;
            interval10Button.BorderColor = Color.White;
            intervalButton.BorderColor = Color.Black;

            var interval = Convert.ToInt32(intervalButton.Text);
            Globals.HeartratePageViewModel.UpdateInterval(interval);
        }

        public IPageAnimation PageAnimation { get; } = new SlidePageAnimation
        { Duration = AnimationDuration.Short, Subtype = AnimationSubtype.FromTop };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }
    }
}

