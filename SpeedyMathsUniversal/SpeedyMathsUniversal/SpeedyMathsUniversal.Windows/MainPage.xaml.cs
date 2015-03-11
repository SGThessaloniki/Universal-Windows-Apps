using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// This is a comment!

namespace SpeedyMathsUniversal
{    
    public sealed partial class MainPage : Page
    {
        private Random randomObject = new Random();
        private int result;
        private string[] symbols = new string[] { "+", "x" };
        private int scorePlayer1, scorePlayer2;


        public async void start()
        {
            ButtonStart.IsEnabled = false;

            foreach (Button B in ButtonGrid.Children)
                B.Content = "";

            float seconds = randomObject.Next(0, 30);
            seconds = seconds / 10;

            TextPlayer1.Text = "Wait...";
            TextPlayer2.Text = "Wait...";

            await System.Threading.Tasks.Task.Delay(TimeSpan.FromSeconds(seconds));

            foreach (Button B in ButtonGrid.Children)
                B.IsEnabled = true;

            int number1 = randomObject.Next(0,20);
            int number2 = randomObject.Next(0, 9);
            int i = randomObject.Next(0, 2);

            TextPlayer1.Text = TextPlayer2.Text = number1.ToString() + " " + symbols[i] + " " +
                number2.ToString() + " =";

            if (i == 0)
                result = number1 + number2;
            if (i == 1)
                result = number1 * number2;

            fillBoxes();

        }

        public void stop()
        {
            foreach (Button B in ButtonGrid.Children)
                B.IsEnabled = false;
            
            ButtonStart.IsEnabled = true;
        }

        public void reset()
        {
            foreach (Button B in ButtonGrid.Children)
            {
                B.Content = "";
                B.IsEnabled = false;
            }

            TextPlayer1.Text = "Hit Start";
            TextPlayer2.Text = "Hit Start";

            scorePlayer1 = 0;
            scorePlayer2 = 0;

            TextScore1.Text = scorePlayer1.ToString();
            TextScore2.Text = scorePlayer2.ToString();
        }

        public void fillBoxes()
        {            
            Button1.Content = Button5.Content = generate();
            Button2.Content = Button6.Content = generate();
            Button3.Content = Button7.Content = generate();
            Button4.Content = Button8.Content = generate();

            if (result % 4 == 0)
            {
                Button1.Content = result.ToString();
                Button5.Content = result.ToString();
            }
            if (result % 4 == 1)
            {
                Button2.Content = result.ToString();
                Button6.Content = result.ToString();
            }
            if (result % 4 == 2)
            {
                Button3.Content = result.ToString();
                Button7.Content = result.ToString();
            }
            if (result % 4 == 3)
            {
                Button4.Content = result.ToString();
                Button8.Content = result.ToString();
            }
        }

        public string generate()
        {
            int randomNumber = randomObject.Next(0, result + 10);

            while(randomNumber == result)
                randomNumber = randomObject.Next(0, result + 10);

            return randomNumber.ToString();
        }

        public void checkForWinner(int player, int choice)
        { 
            if (player == 1)
            {
                if (choice == result % 4)
                {
                    scorePlayer1++;
                    TextScore1.Text = scorePlayer1.ToString();
                    TextPlayer1.Text = "You win";
                    TextPlayer2.Text = "You lose";
                }
                else
                {
                    scorePlayer1--;
                    TextScore1.Text = scorePlayer1.ToString();
                    TextPlayer2.Text = "You win";
                    TextPlayer1.Text = "You lose";
                }
            }

            if (player == 2)
            {
                if (choice == result % 4)
                {
                    scorePlayer2++;
                    TextScore2.Text = scorePlayer2.ToString();
                    TextPlayer2.Text = "You win";
                    TextPlayer1.Text = "You lose";
                }
                else
                {
                    scorePlayer2--;
                    TextScore2.Text = scorePlayer2.ToString();
                    TextPlayer1.Text = "You win";
                    TextPlayer2.Text = "You lose";
                }
            }

            if (scorePlayer1 > scorePlayer2)
            {
                TextScore1.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                TextScore2.Foreground = new SolidColorBrush(Windows.UI.Colors.OrangeRed);
            }
            if (scorePlayer1 < scorePlayer2)
            {
                TextScore1.Foreground = new SolidColorBrush(Windows.UI.Colors.OrangeRed);
                TextScore2.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            }
            if (scorePlayer1 == scorePlayer2)
            {
                TextScore1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
                TextScore2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            }

            stop();
        }


        public MainPage()
        {
            this.InitializeComponent();
            reset();
        }
        

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            start();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            reset();
            stop();

            TextScore1.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
            TextScore2.Foreground = new SolidColorBrush(Windows.UI.Colors.White);
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(1, 0);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(1, 1);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(1, 2);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(1, 3);
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(2, 0);
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(2, 1);
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(2, 2);
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            checkForWinner(2, 3);
        }
    }
}
