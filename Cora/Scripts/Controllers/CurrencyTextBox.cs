using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace MeliManager.UI.Controllers
{
    public class CurrencyTextBox : TextBox
    {
        #region Dependency Properties

        public static readonly DependencyProperty NumberProperty = DependencyProperty.Register(
            "Number",
            typeof(decimal),
            typeof(CurrencyTextBox),
            new FrameworkPropertyMetadata(0M, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnNumberChanged));

        public decimal Number
        {
            get => (decimal)GetValue(NumberProperty);
            set => SetValue(NumberProperty, value);
        }

        public static readonly DependencyProperty StringFormatProperty = DependencyProperty.Register(
            "StringFormat",
            typeof(string),
            typeof(CurrencyTextBox),
            new FrameworkPropertyMetadata(GetCurrencyFormat(), StringFormatPropertyChanged));

        public string StringFormat
        {
            get => (string)GetValue(StringFormatProperty);
            set => SetValue(StringFormatProperty, value);
        }

        private static void OnNumberChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is CurrencyTextBox textBox)
            {
                textBox.Text = ((decimal)e.NewValue).ToString(textBox.StringFormat, CultureInfo.CurrentCulture);
            }
        }

        private static string GetCurrencyFormat()
        {
            return "C"; // Usa o formato de moeda conforme a cultura atual
        }

        private static void StringFormatPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            var textBinding = new Binding("Number")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                StringFormat = (string)e.NewValue,
                ConverterCulture = CultureInfo.CurrentCulture
            };

            BindingOperations.SetBinding(obj, TextProperty, textBinding);
        }

        #endregion

        #region Constructor

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var textBinding = new Binding("Number")
            {
                RelativeSource = new RelativeSource(RelativeSourceMode.Self),
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged,
                StringFormat = this.StringFormat,
                ConverterCulture = CultureInfo.CurrentCulture
            };

            BindingOperations.SetBinding(this, TextProperty, textBinding);

            DataObject.AddCopyingHandler(this, PastingEventHandler);
            DataObject.AddPastingHandler(this, PastingEventHandler);

            this.PreviewKeyDown += TextBox_PreviewKeyDown;
            this.TextChanged += TextBox_TextChanged;
            this.ContextMenu = null;
        }

        #endregion

        #region Events

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (decimal.TryParse(this.Text, NumberStyles.Currency, CultureInfo.CurrentCulture, out decimal result))
            {
                Number = result;
            }

            this.CaretIndex = this.Text.Length;
        }

        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (IsNumericKey(e.Key))
            {
                e.Handled = true;

                try
                {
                    Number = (Number * 10M) + (GetDigitFromKey(e.Key) / 100M);
                }
                catch
                {
                    Number = decimal.MaxValue;
                }
            }
            else if (e.Key == Key.Back)
            {
                e.Handled = true;
                Number = (Number - (Number % 0.1M)) / 10M;
            }
            else if (e.Key == Key.Delete)
            {
                e.Handled = true;
                Number = 0M;
            }
            else if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                e.Handled = true;
                Number *= -1;
            }
            else if (IsIgnoredKey(e.Key))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void PastingEventHandler(object sender, DataObjectEventArgs e)
        {
            e.CancelCommand();
        }

        #endregion

        #region Private Methods

        private decimal GetDigitFromKey(Key key)
        {
            return key switch
            {
                Key.D0 or Key.NumPad0 => 0M,
                Key.D1 or Key.NumPad1 => 1M,
                Key.D2 or Key.NumPad2 => 2M,
                Key.D3 or Key.NumPad3 => 3M,
                Key.D4 or Key.NumPad4 => 4M,
                Key.D5 or Key.NumPad5 => 5M,
                Key.D6 or Key.NumPad6 => 6M,
                Key.D7 or Key.NumPad7 => 7M,
                Key.D8 or Key.NumPad8 => 8M,
                Key.D9 or Key.NumPad9 => 9M,
                _ => throw new ArgumentOutOfRangeException("Invalid key: " + key)
            };
        }

        private bool IsNumericKey(Key key)
        {
            return key is >= Key.D0 and <= Key.D9 or >= Key.NumPad0 and <= Key.NumPad9;
        }

        private bool IsIgnoredKey(Key key)
        {
            return key is Key.Up or Key.Down or Key.Tab or Key.Enter;
        }

        #endregion
    }
}
