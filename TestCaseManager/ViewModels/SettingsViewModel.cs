﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using FirstFloor.ModernUI.Presentation;

namespace TestCaseManagerApp.ViewModels
{
    public class SettingsViewModel
              : NotifyPropertyChanged
    {
        // 9 accent colors from metro design principles
        //private Color[] accentColors = new Color[]{
        //    Color.FromRgb(0x33, 0x99, 0xff),   // blue
        //    Color.FromRgb(0x00, 0xab, 0xa9),   // teal
        //    Color.FromRgb(0x33, 0x99, 0x33),   // green
        //    Color.FromRgb(0x8c, 0xbf, 0x26),   // lime
        //    Color.FromRgb(0xf0, 0x96, 0x09),   // orange
        //    Color.FromRgb(0xff, 0x45, 0x00),   // orange red
        //    Color.FromRgb(0xe5, 0x14, 0x00),   // red
        //    Color.FromRgb(0xff, 0x00, 0x97),   // magenta
        //    Color.FromRgb(0xa2, 0x00, 0xff),   // purple            
        //};

        // 20 accent colors from Windows Phone 8
        private Color[] accentColors = new Color[]{
            Color.FromRgb(0xa4, 0xc4, 0x00),   // lime
            Color.FromRgb(0x60, 0xa9, 0x17),   // green
            Color.FromRgb(0x00, 0x8a, 0x00),   // emerald
            Color.FromRgb(0x00, 0xab, 0xa9),   // teal
            Color.FromRgb(0x1b, 0xa1, 0xe2),   // cyan
            Color.FromRgb(0x00, 0x50, 0xef),   // cobal
            Color.FromRgb(0x6a, 0x00, 0xff),   // indigo
            Color.FromRgb(0xaa, 0x00, 0xff),   // violet
            Color.FromRgb(0xf4, 0x72, 0xd0),   // pink
            Color.FromRgb(0xd8, 0x00, 0x73),   // magenta
            Color.FromRgb(0xa2, 0x00, 0x25),   // crimson
            Color.FromRgb(0xe5, 0x14, 0x00),   // red
            Color.FromRgb(0xfa, 0x68, 0x00),   // orange
            Color.FromRgb(0xf0, 0xa3, 0x0a),   // amber
            Color.FromRgb(0xe3, 0xc8, 0x00),   // yellow
            Color.FromRgb(0x82, 0x5a, 0x2c),   // brown
            Color.FromRgb(0x6d, 0x87, 0x64),   // olive
            Color.FromRgb(0x64, 0x76, 0x87),   // steel
            Color.FromRgb(0x76, 0x60, 0x8a),   // mauve
            Color.FromRgb(0x87, 0x79, 0x4e),   // taupe
        };

        private Color selectedAccentColor;
        private LinkCollection themes = new LinkCollection();
        private Link selectedTheme;

        public SettingsViewModel()
        {
            // add the default themes
            this.themes.Add(new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource });
            this.themes.Add(new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource });

            // add additional themes
            this.themes.Add(new Link { DisplayName = "hello kitty", Source = new Uri("Assets/ModernUI.HelloKitty.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "love", Source = new Uri("Assets/ModernUI.Love.xaml", UriKind.Relative) });
            this.themes.Add(new Link { DisplayName = "snowflakes", Source = new Uri("Assets/ModernUI.Snowflakes.xaml", UriKind.Relative) });
            //SyncThemeAndColor();

            AppearanceManager.Current.PropertyChanged += OnAppearanceManagerPropertyChanged;
            SetPrevious();
        }

        private void SetPrevious()
        {
            string previouslySelectedTheme = RegistryManager.GetTheme();
            string[] colors = RegistryManager.GetColors();
            if (colors != null && colors.Length == 3 && previouslySelectedTheme != string.Empty)
            {
                Color currentColor = default(Color);
                currentColor = Color.FromRgb(byte.Parse(colors[0]), byte.Parse(colors[1]), byte.Parse(colors[2]));
                SyncThemeAndColor(previouslySelectedTheme, currentColor);
            }
            else
                SyncThemeAndColor();      
        }

        private void SyncThemeAndColor()
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.Source.Equals(AppearanceManager.Current.ThemeSource));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = AppearanceManager.Current.AccentColor;
        }

        private void SyncThemeAndColor(string themeName, Color currentColor)
        {
            // synchronizes the selected viewmodel theme with the actual theme used by the appearance manager.
            this.SelectedTheme = this.themes.FirstOrDefault(l => l.DisplayName.Equals(themeName));

            // and make sure accent color is up-to-date
            this.SelectedAccentColor = currentColor;
        }

        private void OnAppearanceManagerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "ThemeSource" || e.PropertyName == "AccentColor")
            {
                SyncThemeAndColor();
            }
        }

        public LinkCollection Themes
        {
            get { return this.themes; }
        }


        public Color[] AccentColors
        {
            get { return this.accentColors; }
        }

        public Link SelectedTheme
        {
            get { return this.selectedTheme; }
            set
            {
                if (this.selectedTheme != value)
                {
                    this.selectedTheme = value;
                    OnPropertyChanged("SelectedTheme");
                    RegistryManager.WriteCurrentTheme(value.DisplayName);
                    // and update the actual theme
                    AppearanceManager.Current.ThemeSource = value.Source;
                }
            }
        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set
            {
                if (this.selectedAccentColor != value)
                {
                    this.selectedAccentColor = value;
                    OnPropertyChanged("SelectedAccentColor");
                    RegistryManager.WriteCurrentColors(value.R, value.G, value.B);
                    AppearanceManager.Current.AccentColor = value;
                }
            }
        }
    }
}

