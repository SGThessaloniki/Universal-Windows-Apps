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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SimpleSpeedyMaths
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        int totalscore;
        int correctAnswer;
        Random rnd = new Random();

        enum Operation
        {
            Add,
            Multiplication
        }

        public MainPage()
        {
            this.InitializeComponent();

            foreach (Button i in ButtonsGrid.Children)
                i.Content = " ";
        }

        //enable or disable buttons
        private void ConfigureButtons(bool activation)
        {
            foreach (Button B in ButtonsGrid.Children)
                B.IsEnabled = activation;
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureButtons(true);
            Start();
        }

        //Find 2 numbers and the result and enable buttons
        private void Start()
        {
            int firstNumber = rnd.Next(1, 21);
            int secondNumber = rnd.Next(1, 11);

            int op = rnd.Next(0, 2);
            Operation decidedOp;

            decidedOp = (op == 0) ? Operation.Add : Operation.Multiplication;

            int result;

            if (decidedOp == Operation.Add)
            {
                result = firstNumber + secondNumber;
                ProblemText.Text = firstNumber.ToString() + " + " + secondNumber.ToString();
            }
            else
            {
                result = firstNumber * secondNumber;
                ProblemText.Text = firstNumber.ToString() + " * " + secondNumber.ToString();
            }
            correctAnswer = result;

            FillButtonsWithPossibleAnswers(decidedOp);
        }

        private void FillButtonsWithPossibleAnswers(Operation selectedOp)
        {
            Button[] buttonsArray = { Button1, Button2, Button3, Button4 };

            int correctResultButton = rnd.Next(0, 4);

            buttonsArray[correctResultButton].Content = correctAnswer.ToString();

            //Content to the rest 3 buttons
            for (int i = 0; i < 4; i++)
            {
                if (i != correctResultButton)
                {
                    int randomAnswer;
                    //For smaller results for the addition
                    if (selectedOp == Operation.Add)
                    {
                        randomAnswer = rnd.Next(1, 31);
                    }
                    else
                    {
                        randomAnswer = rnd.Next(1, 200);
                    }
                    while (randomAnswer == correctAnswer)
                    {
                        randomAnswer = rnd.Next(1, 100);
                    }
                    buttonsArray[i].Content = randomAnswer.ToString();
                }
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ConfigureButtons(false);
            ScoreText.Text = "Score : 0";
            totalscore = 0;
            ProblemText.Text = "Hit Start...";
            StartButton.IsEnabled = true;
            ZeroButtonsContent();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            CheckIfResultIsCorrect(sender);
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            CheckIfResultIsCorrect(sender);
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            CheckIfResultIsCorrect(sender);
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            CheckIfResultIsCorrect(sender);
        }

        private void CheckIfResultIsCorrect(object sender)
        {
            Button pressedButton = (Button)sender;

            int buttonsContent = Int32.Parse(pressedButton.Content.ToString());

            if (buttonsContent == correctAnswer)
                totalscore++;

            else
                totalscore--;

            ScoreText.Text = "Score : " + totalscore.ToString();
            ZeroButtonsContent();
            Start();
        }

        private void ZeroButtonsContent()
        {
            foreach (Button B in ButtonsGrid.Children)
                B.Content = "0";
        }
    }
}
