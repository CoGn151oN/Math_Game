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
using System.Windows.Markup;
using System.IO;
using System.Xml;

namespace WPF_HF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random rand = new Random();
        string[] questions = { "1 + 1", "2 + 1", "3 + 2", "2 + 2", "5 - 2", "3 - 2", "2 - 1", "4 + 1", "5 - 3", "4 - 2" };
        string[] answers = { "2", "3", "5", "4", "3", "1", "1", "5", "2", "2" };
        int points = 0;
        int index = 0;
        string savedUserName;
        string savedPoints;

        public MainWindow()
        {
            InitializeComponent();
            lblPoints.Content = "Score: " + points.ToString();
            btnA1.IsEnabled = false;
            btnA2.IsEnabled = false;
            btnA3.IsEnabled = false;
            btnA4.IsEnabled = false;
            btnA5.IsEnabled = false;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            base.Close();
        }

        // start game
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            lblPoints.Content = "Score: " + points.ToString();
            lblCorrect.Content = String.Empty;

            index = rand.Next(10);
            lblQuestion.Content = questions[index];

            btnA1.IsEnabled = true;
            btnA2.IsEnabled = true;
            btnA3.IsEnabled = true;
            btnA4.IsEnabled = true;
            btnA5.IsEnabled = true;
            ansPositions();
        }

        // answer button positioning
        public void ansPositions()
        {
            int[] xVal = { 22, 118, 214, 310, 406 };
            xVal = xVal.OrderBy(x => rand.Next()).ToArray();
            btnA1.Margin = new Thickness(xVal[0], 229, 0, 0);
            btnA2.Margin = new Thickness(xVal[1], 229, 0, 0);
            btnA3.Margin = new Thickness(xVal[2], 229, 0, 0);
            btnA4.Margin = new Thickness(xVal[3], 229, 0, 0);
            btnA5.Margin = new Thickness(xVal[4], 229, 0, 0);
        }

        // answer buttons
        public void clickity(string n)
        {
            if (answers[index] == n)
            {
                lblCorrect.Content = "Correct";
                points++;
                lblPoints.Content = "Score: " + points.ToString();
                index = rand.Next(10);
                lblQuestion.Content = questions[index];
                ansPositions();
            }
            else
            {
                lblCorrect.Content = "Incorrect\nPlease try again.";
            }
        }

        private void btnA1_Click(object sender, RoutedEventArgs e)
        {
            clickity("1");
        }

        private void btnA2_Click(object sender, RoutedEventArgs e)
        {
            clickity("2");
        }
        private void btnA3_Click(object sender, RoutedEventArgs e)
        {
            clickity("3");
        }
        private void btnA4_Click(object sender, RoutedEventArgs e)
        {
            clickity("4");
        }
        private void btnA5_Click(object sender, RoutedEventArgs e)
        {
            clickity("5");
        }

        // Popup inputBox
        private void btnUserName_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Visibility = Visibility.Visible;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            lblUserName.Content = "User: " + txtInput.Text;
            inputBox.Visibility = Visibility.Collapsed;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            inputBox.Visibility = Visibility.Collapsed;
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            points = 0;
            lblPoints.Content = "Score: " + points.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            savedUserName = XamlWriter.Save(lblUserName.Content);
            savedPoints = XamlWriter.Save(points.ToString());
            lblSerialize.Content = "Content serialized" ;
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            StringReader sr1 = new StringReader(savedUserName);
            StringReader sr2 = new StringReader(savedPoints);
            XmlReader xmlReader1 = XmlReader.Create(sr1);
            XmlReader xmlReader2 = XmlReader.Create(sr2);
            string uncont = (string)XamlReader.Load(xmlReader1);
            string pcont = (string)XamlReader.Load(xmlReader2);
            lblSerialize.Content = uncont + "\nScore: " + pcont;
        }
    }
}
