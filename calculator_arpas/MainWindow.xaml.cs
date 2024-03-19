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
            // Get the expression from the text display
            string expression = txtDisplay.Text;

            // Try to evaluate the expression
            try
            {
                // Evaluate the expression using DataTable Compute method
                var result = new DataTable().Compute(expression, null);

                // Display the result
                txtInput.Text = result.ToString();
            }
            catch (Exception)
            {
                // If an exception occurs during evaluation, display "Invalid" in the text display
                txtInput.Text = "Invalid";
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
            // Check if the current text already contains a dot
            if (!txtDisplay.Text.Contains(".")) txtDisplay.Text += ".";
        }

        // Event handler for operation buttons
        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                string newOperation = button.Content.ToString();

                // Check if the current display text ends with an operator
                if (txtDisplay.Text.Length > 0 && IsOperator(txtDisplay.Text[^1]))
                {
                    // Replace the existing operator with the new one
                    txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1) + newOperation;
                }
                else txtDisplay.Text += newOperation;
            }
        }

        // Method to check if a character is an operator
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
                // Find the index of the last operator using LINQ
                int lastOperatorIndex = txtDisplay.Text.Select((c, index) => new { Char = c, Index = index })
                                                        .Where(pair => IsOperator(pair.Char))
                                                        .Select(pair => pair.Index)
                                                        .DefaultIfEmpty(-1)
                                                        .Last();

                // If an operator is found, remove everything after it
                if (lastOperatorIndex >= 0)
                {
                    txtDisplay.Text = txtDisplay.Text.Substring(0, lastOperatorIndex);
                }
                else
                {
                    // If no operator is found, simply clear the display
                    txtDisplay.Text = "0";
                }
            }
        }

    }
}