using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace calculator_arpas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnEquals_Click(object sender, RoutedEventArgs e)
        {
            string expression = txtDisplay.Text;

            if (expression.Contains("/0")) txtInput.Text = "Division by zero was attempted";
            else
            {
                try
                {
                    var result = new DataTable().Compute(expression, null);

                }
                catch (Exception)
                {
                    txtInput.Text = "Invalid";
                }
            }

        }

        private void regularButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                string numberClicked = button.Content.ToString();

                if (txtDisplay.Text == "0") txtDisplay.Text = numberClicked; else txtDisplay.Text += numberClicked;
            }
        }


        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtDisplay.Text.Contains(".")) txtDisplay.Text += ".";
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                string newOperation = button.Content.ToString();

                if (txtDisplay.Text.Length > 0 && IsOperator(txtDisplay.Text[^1]))
                {
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + newOperation;
                }
                else txtDisplay.Text += newOperation;
            }
        }

        private bool IsOperator(char c)
        {
            return c == '+' || c == '-' || c == '*' || c == '/';
        }



        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
        }



        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                int lastOperatorIndex = txtDisplay.Text.Select((c, index) => new { Char = c, Index = index })
                                                        .Where(pair => IsOperator(pair.Char))
                                                        .Select(pair => pair.Index)
                                                        .DefaultIfEmpty(-1)
                                                        .Last();

                if (lastOperatorIndex >= 0)
                {
                    txtDisplay.Text = txtDisplay.Text.Substring(0, lastOperatorIndex);
                }
                else
                {
                    txtDisplay.Text = "0";
                }
            }
        }

    }
}