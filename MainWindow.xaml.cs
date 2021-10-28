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

namespace Tamaian_Rares_Lab2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private PizzaMachine myPizzaMachine;

        private int mMargheritaPizza;
        private int mPepperoniPizza;
        private int mVeggiePizza;
        private int mQuattroStagioniPizza;
        private int mCanibalePizza;

        KeyValuePair<PizzaType, double>[] PriceList =
        {
            new KeyValuePair<PizzaType, double>(PizzaType.Margherita, 21),
            new KeyValuePair<PizzaType, double>(PizzaType.Pepperoni, 23),
            new KeyValuePair<PizzaType, double>(PizzaType.Veggie, 20),
            new KeyValuePair<PizzaType, double>(PizzaType.Quattro_Stagioni, 27),
            new KeyValuePair<PizzaType, double>(PizzaType.Canibale, 30)
        };
        PizzaType selectedPizza;
        public MainWindow()
        {
            InitializeComponent();
            //creare obiect binding pentru comanda
            CommandBinding cmd1 = new CommandBinding();
            //asociere comanda
            cmd1.Command = ApplicationCommands.Print;
            //input gesture: Alt + I
            ApplicationCommands.Print.InputGestures.Add(new KeyGesture(Key.I, ModifierKeys.Alt));
            //asociem un handler
            cmd1.Executed += new ExecutedRoutedEventHandler(CtrlP_CommandHandler);
            //adaugam la colectia CommandBindings
            this.CommandBindings.Add(cmd1);

            //Pizza > Stop menu item
            CommandBinding cmd2 = new CommandBinding();
            cmd2.Command = CustomCommands.StopCommand.Launch;
            cmd2.Executed += new ExecutedRoutedEventHandler(CtrlS_CommandHandler);
            this.CommandBindings.Add(cmd2);

        }

        private void CtrlP_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBox.Show("You have in stock: " + mMargheritaPizza + " Margherita pizza, " +
            mPepperoniPizza + " Pepperoni pizza, " + mVeggiePizza + " Veggie Pizza," +
            mQuattroStagioniPizza + " Quattro Stagioni pizza, " + mCanibalePizza + " Canibale pizza"
            );
        }

        private void CtrlS_CommandHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //handler pentru comanda Ctrl+S -> se va executa stopMenuItem_Click
            MessageBox.Show("Ctrl+S was pressed! The pizza machine will stop!");
            this.stopMenuItem_Click(sender, e);
        }
        private void frmMain_Loaded(object sender, RoutedEventArgs e)
        {
            myPizzaMachine = new PizzaMachine();
            myPizzaMachine.PizzaComplete += new PizzaMachine.PizzaCompleteDelegate(PizzaCompleteHandler);
            cmbType.ItemsSource = PriceList;
            cmbType.DisplayMemberPath = "Key"; 
            cmbType.SelectedValuePath = "Value";
        }
        private void stopMenuItem_Click(object sender, RoutedEventArgs e)
        {
            myPizzaMachine.Enabled = false;
        }
        private void exitMenu_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void txtQuantity_KeyUp(object sender, KeyEventArgs e)
        {
            if (!(e.Key >= Key.D0 && e.Key <= Key.D9))
            {
                MessageBox.Show("Numai cifre se pot introduce!", "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void margPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = true;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Margherita);
        }
        private void pepPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = true;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Pepperoni);
        }
        private void vegPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = true;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Veggie);
        }
        private void quatPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = true;
            canPizzaMenuItem.IsChecked = false;
            myPizzaMachine.MakePizzas(PizzaType.Quattro_Stagioni);
        }
        private void canPizzaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            margPizzaMenuItem.IsChecked = false;
            pepPizzaMenuItem.IsChecked = false;
            vegPizzaMenuItem.IsChecked = false;
            quatPizzaMenuItem.IsChecked = false;
            canPizzaMenuItem.IsChecked = true;
            myPizzaMachine.MakePizzas(PizzaType.Canibale);
        }
        private void PizzaCompleteHandler()
        {
            switch (myPizzaMachine.Ingredients)
            {
                case PizzaType.Margherita:
                    mMargheritaPizza++;
                    txtMargheritaPizza.Text = mMargheritaPizza.ToString();
                    break;
                case PizzaType.Pepperoni:
                    mPepperoniPizza++;
                    txtPepperoniPizza.Text = mPepperoniPizza.ToString();
                    break;
                case PizzaType.Veggie:
                    mVeggiePizza++;
                    txtVeggiePizza.Text = mVeggiePizza.ToString();
                    break;
                case PizzaType.Quattro_Stagioni:
                    mQuattroStagioniPizza++;
                    txtQuatroPizza.Text = mQuattroStagioniPizza.ToString();
                    break;
                case PizzaType.Canibale:
                    mCanibalePizza++;
                    txtCanibaliPizza.Text = mCanibalePizza.ToString();
                    break;
            }
        }

        private int ValidateQuantity(PizzaType selectedPizza)
        {
            int q = int.Parse(txtQuantity.Text);
            int r = 1;
            switch (selectedPizza)
            {
                case PizzaType.Margherita:
                    if (q > mMargheritaPizza)
                        r = 0;
                    break;
                case PizzaType.Pepperoni:
                    if (q > mPepperoniPizza)
                        r = 0;
                    break;
                case PizzaType.Veggie:
                    if (q > mVeggiePizza)
                        r = 0;
                    break;
                case PizzaType.Quattro_Stagioni:
                    if (q > mQuattroStagioniPizza)
                        r = 0;
                    break;
                case PizzaType.Canibale:
                    if (q > mCanibalePizza)
                        r = 0;
                    break;
            }
            return r;
        }
        private void cmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtPrice.Text = cmbType.SelectedValue.ToString();
            KeyValuePair<PizzaType, double> selectedEntry = (KeyValuePair<PizzaType, double>) cmbType.SelectedItem;
            selectedPizza = selectedEntry.Key;
        }

        private void btnAddToSale_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateQuantity(selectedPizza) > 0)
            {
                lstSale.Items.Add(txtQuantity.Text + " " + selectedPizza.ToString() + ":" + txtPrice.Text + " " + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text));
            }
            else
            {
                MessageBox.Show("Cantitatea introdusa nu este disponibila in stoc!");
            }
        }

        private void btnRemoveItem_Click(object sender, RoutedEventArgs e)
        {
            lstSale.Items.Remove(lstSale.SelectedItem);
        }

        private void btnCheckOut_Click(object sender, RoutedEventArgs e)
        {
            txtTotal.Text = (double.Parse(txtTotal.Text) + double.Parse(txtQuantity.Text) * double.Parse(txtPrice.Text)).ToString();
            foreach (string s in lstSale.Items)
            {
                switch (s.Substring(s.IndexOf(" ") + 1, s.IndexOf(":") - s.IndexOf(" ") - 1))
                {
                    case "Margherita":
                        mMargheritaPizza = mMargheritaPizza - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtMargheritaPizza.Text = mMargheritaPizza.ToString();
                        break;
                    case "Pepperoni":
                        mPepperoniPizza = mPepperoniPizza - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtPepperoniPizza.Text = mPepperoniPizza.ToString();
                        break;
                    case "Veggie":
                        mVeggiePizza = mVeggiePizza - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtVeggiePizza.Text = mVeggiePizza.ToString();
                        break;
                    case "Quattro_Stagioni":
                        mVeggiePizza = mVeggiePizza - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtVeggiePizza.Text = mVeggiePizza.ToString();
                        break;
                    case "Canibale":
                        mVeggiePizza = mVeggiePizza - Int32.Parse(s.Substring(0, s.IndexOf(" ")));
                        txtVeggiePizza.Text = mVeggiePizza.ToString();
                        break;
                }
            }
        }

        private void PizzaItemShow_Click(object sender, RoutedEventArgs e)
        {
            string mesaj;
            MenuItem SelectedItem = (MenuItem)e.OriginalSource;
            string stringHeader = SelectedItem.Header as string;

            switch (stringHeader)
            {
                case "Stop":
                    this.Title = "Stopped Machine";
                    break;
                case "Inventory":
                    this.Title = "Checking the Inventory...";
                    break;
                default:
                    mesaj = SelectedItem.Header.ToString() + " is being cooked!";
                    this.Title = mesaj;
                    break;
            }
        }
    }
}
