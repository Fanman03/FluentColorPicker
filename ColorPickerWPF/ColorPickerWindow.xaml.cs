﻿using System;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Media;
using ColorPickerWPF.Code;
using ColorPickerWPF.Properties;
using SourceChord.FluentWPF;

namespace ColorPickerWPF
{
    /// <summary>
    /// Interaction logic for ColorPickerWindow.xaml
    /// </summary>
    public partial class ColorPickerWindow : AcrylicWindow
    {
        protected readonly int WidthMax = 574;
        protected readonly int WidthMin = 330;
        protected bool SimpleMode { get; set; }
        public ColorPickerWindow()
        {
            InitializeComponent();
            DataContext = this;
        }
        
        public static bool ShowDialog(out Color color, Color currentColor, ColorPickerDialogOptions flags = ColorPickerDialogOptions.None, ColorPickerControl.ColorPickerChangeHandler customPreviewEventHandler = null)
        {
            if ((flags & ColorPickerDialogOptions.LoadCustomPalette) == ColorPickerDialogOptions.LoadCustomPalette)
            {
                ColorPickerSettings.UsingCustomPalette = true;
            }

            var instance = new ColorPickerWindow();
            
            instance.ColorPicker.Color = currentColor; //This should set the value, but it doesn't. Why?

            color = currentColor;

            if ((flags & ColorPickerDialogOptions.SimpleView) == ColorPickerDialogOptions.SimpleView)
            {
                instance.ToggleSimpleAdvancedView();
            }

            if (ColorPickerSettings.UsingCustomPalette)
            {
            }

            if (customPreviewEventHandler != null)
            {
                instance.ColorPicker.OnPickColor += customPreviewEventHandler;
            }
            
            var result = instance.ShowDialog();
            if (result.HasValue && result.Value)
            {
                color = instance.ColorPicker.Color;
                return true;
            }

            return false;
        }
        
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Hide();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Hide();
        }

        private void MinMaxViewButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (SimpleMode)
            {
                SimpleMode = false;
                ExpandTxt.Text = "Simple";
                Width = WidthMax;
            }
            else
            {
                SimpleMode = true;
                ExpandTxt.Text = "Advanced";
                Width = WidthMin;
            }
        }

        public void ToggleSimpleAdvancedView()
        {
            if (SimpleMode)
            {
                SimpleMode = false;
                ExpandTxt.Text = "Simple";
                Width = WidthMax;
            }
            else
            {
                SimpleMode = true;
                ExpandTxt.Text = "Advanced";
                Width = WidthMin;
            }
        }
    }
}
