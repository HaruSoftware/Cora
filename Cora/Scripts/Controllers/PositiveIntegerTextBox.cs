using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Cora.UI.Controllers
{
    public class PositiveIntegerTextBox : TextBox
    {

        public PositiveIntegerTextBox()
        {
            PreviewTextInput += OnPreviewKeyDown;
        }

        private void OnPreviewKeyDown(object sender, TextCompositionEventArgs e)
        {
            if (!Parsing.IsNumber(e.Text))
            {
                e.Handled = true;
            }
        }
        public int GetValue()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return 0;
            }

            return Parsing.Int(this.Text);
        }
        public decimal GetDecimalValue()
        {
            if (string.IsNullOrEmpty(this.Text))
            {
                return 0m;
            }

            return Parsing.Decimal(this.Text);
        }
        public void SetValue(object value)
        {
            string str = value?.ToString() ?? "0";

            switch (value)
            {
                case int or short:
                    this.Text = Parsing.Int(str).ToString();
                    break;
                case double:
                    this.Text = Parsing.Double(str).ToString();
                    break;
                case float:
                    this.Text = Parsing.Float(str).ToString();
                    break;
                case decimal:
                    this.Text = Parsing.Decimal(str).ToString();
                    break;
                case long:
                    this.Text = Parsing.Long(str).ToString();
                    break;
                default:
                    try
                    {
                        this.Text = Parsing.IsNumber(str) ? Parsing.Double(str).ToString() : "0";
                    }
                    catch
                    {
                        this.Text = "0";
                    }
                    break;
            }
        }
    }
}
