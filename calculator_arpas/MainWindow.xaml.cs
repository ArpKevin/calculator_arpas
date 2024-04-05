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
            //gomb megnyomására változóba helyezi a beírt karakterek tönkelegét, s megpróbálja kiszámolni az értékét
            string expression = txtDisplay.Text;

            //nullával való osztály ellenőrzése
            if (expression.Contains("/0") && !expression.Contains("0.")) txtInput.Text = "Division by zero was attempted";
            else
            {
                try
                {
                    //automatikusan megpróbálja kiszámolni a beírt kifejezés értékét
                    var result = new DataTable().Compute(expression, null);
                    txtInput.Text = result.ToString();

                }
                catch (Exception)
                {
                    txtInput.Text = "Invalid";
                }
            }

        }

        //kinyeri a gomb tartalmából a szám értékét, majd hozzáadja a számológép kijelzőjéhez
        private void regularButtonClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                string numberClicked = button.Content.ToString();

                if (txtDisplay.Text == "0") txtDisplay.Text = numberClicked; else txtDisplay.Text += numberClicked;
            }
        }

        //megnézi hogy már van-e pont a kifejezés végén, ha nincs, ad hozzá egyet a "." gomb megnyomására
        private void btnDot_Click(object sender, RoutedEventArgs e)
        {
            if (!txtDisplay.Text.EndsWith(".")) txtDisplay.Text += ".";
        }

        private void operationButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;

            // ellenőrzi, hogy az utolsó karakter operátor-e, ha igen, cserélje ki, ellenkező esetben fűzze hozzá

            if (button != null)
            {
                string newOperation = button.Content.ToString();

                //IsOperator = bool típusú metódus, ami megállapítja, hogy operátor-e az utolsó karakter
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


        //megnézi, hogy üres-e a kijelző, ha nem, lecsépeli az utolsó karaktert
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                txtDisplay.Text = txtDisplay.Text.Substring(0, txtDisplay.Text.Length - 1);
            }
        }

        //0-vá teszi egyenlővé a kifejezést, alapértelmezett helyzetbe visszahelyezi
        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            txtDisplay.Text = "0";
        }


        //műveleti egységenként töröl ki a kifejezésből karakterenként helyett
        private void btnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDisplay.Text))
            {
                //linq segítségével a szövegből kiválasztja a karaktereket és az indexüket, anonym típusú objektumot hoz létre, amely minden karakterhez hozzárendeli önmagát és az indexét, majd ebből ha operátor a karakter értéke, kiválasztom az indexét, különben ha ilyen nincs, az alapértelmezett érték -1, s kiválasztom az utolsó ilyen karaktert mert addig fog kelleni kitörölni a műveleti egységet
                int lastOperatorIndex = txtDisplay.Text.Select((c, index) => new { Char = c, Index = index })
                                                        .Where(pair => IsOperator(pair.Char))
                                                        .Select(pair => pair.Index)
                                                        .DefaultIfEmpty(-1)
                                                        .Last();

                //megnézem, hogy volt-e operátor a kifejezésben, ha volt, a változóba kinyert operátor mentén leszeli a kifejezést, különben lenullázza
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

        //gomb nyomására megjeleníti a gyökvonást és a hatványozás gombjait
        private void btnAdvanced_Click(object sender, RoutedEventArgs e)
        {
            btnRoot.Visibility = Visibility.Visible;
            btnFactoring.Visibility = Visibility.Visible;
        }

        private void btnFactoring_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnRoot_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}