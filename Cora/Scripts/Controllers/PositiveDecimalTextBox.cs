using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace MeliManager.UI.Controllers
{
    public class PositiveDecimalTextBox : TextBox
    {
        public PositiveDecimalTextBox()
        {
            PreviewTextInput += OnPreviewTextInput;
            DataObject.AddPastingHandler(this, OnPaste); // Trata colar com Ctrl+V
        }

        private void OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string text = this.Text.Insert(this.SelectionStart, e.Text);

            e.Handled = !IsValidInput(text);
        }

        private void OnPaste(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(DataFormats.Text))
            {
                string pasteText = (string)e.DataObject.GetData(DataFormats.Text);
                string newText = this.Text.Insert(this.SelectionStart, pasteText);

                if (!IsValidInput(newText))
                {
                    e.CancelCommand();
                }
            }
            else
            {
                e.CancelCommand();
            }
        }

        private bool IsValidInput(string text)
        {
            // Remove espaços e verifica se é número decimal positivo com vírgula
            text = text.Trim();
            return decimal.TryParse(text, NumberStyles.Number, new CultureInfo("pt-BR"), out var result) && result >= 0;
        }

        public decimal GetValue()
        {
            if (string.IsNullOrWhiteSpace(this.Text))
                return 0m;

            if (decimal.TryParse(this.Text, NumberStyles.Number, new CultureInfo("pt-BR"), out var value))
                return value;

            return 0m;
        }

        public void SetValue(object value)
        {
            try
            {
                if (value == null)
                {
                    this.Text = "0";
                    return;
                }

                if (decimal.TryParse(value.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out var dec))
                {
                    this.Text = dec.ToString("N2", new CultureInfo("pt-BR")); // Ex: 1.234,56
                }
                else
                {
                    this.Text = "0";
                }
            }
            catch
            {
                this.Text = "0";
            }
        }
    }
}
