using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Cal calcul = new Cal();
        
        public MainWindow(){
           
            InitializeComponent();
        }
                
        private void Update() {
            
            TextBlockExpression.Text = calcul.Equation;            
        }

        #region actions
        private void ButtonClick(object sender, RoutedEventArgs e) {

            char character;
            string button_str = Convert.ToString((sender as Button).Tag);
            if(char.TryParse(button_str, out character)) {
                calcul.AddCharacter(character);
                Update();
            }       
        }

        private void ButtonClearAllClick(object sender, RoutedEventArgs e) {

            calcul.ResetEquation();            
            Update();
            TextBlockResult.Text = "";
        }

        private void ButtonDelClick(object sender, RoutedEventArgs e) {

            calcul.RemoveLast();
            Update();
        }

        private void ButtonEqual_Click(object sender, RoutedEventArgs e) {
            TextBlockResult.Text = calcul.Calculate();
            Update();
        }
        #endregion
    }
}
