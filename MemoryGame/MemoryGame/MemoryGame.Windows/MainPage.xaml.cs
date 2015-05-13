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

namespace MemoryGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Random rd;
        
        int RightAnswer;
        int TotalQuestionsAnswered;
        int[] PossibleAnswers;
        int TotalPoints;

        DispatcherTimer timer;
        
        int SecondsElapsed; //when 0 hide the number and "spawn" the 4 possible answers
        int MaxWaitSeconds = 3;

        public MainPage()
        {
            this.InitializeComponent();
            //Initialization
            ProblemTxT.Visibility = Visibility.Collapsed;
            Collapser();
        }

        //Check if the tapped answer is correct
        private void CheckPressedButton(object sender)
        {
            Button PressedBtn = (Button)sender;
            if (PressedBtn.Content.ToString() == RightAnswer.ToString())
            {
                TotalPoints += 10;
                ScoreTxT.Text = "Score: " + TotalPoints.ToString();
            }
            TotalQuestionsAnswered++;

            Collapser();

            HandleRangeOfRandomAnswer();
        }
        //Responsible for collapsing possible answers
        private void Collapser()
        {
            Btn1.Visibility = Visibility.Collapsed;
            Btn2.Visibility = Visibility.Collapsed;
            Btn3.Visibility = Visibility.Collapsed;
            Btn4.Visibility = Visibility.Collapsed;
        }

        private void StartQuiz()
        {
            rd = new Random();
            PossibleAnswers = new int[4];
            TotalPoints = 0;
            SecondsElapsed = MaxWaitSeconds;

            HandleRangeOfRandomAnswer();

            ProblemTxT.Visibility = Visibility.Visible;

            TimerTxT.Visibility = Visibility.Visible;
            TimerTxT.Text = SecondsElapsed.ToString();

        }
        //Handle the range of possible answers
        private void HandleRangeOfRandomAnswer()
        {
            Btn1.Content = 0;
            Btn2.Content = 0;
            Btn3.Content = 0;
            Btn4.Content = 0;
            switch (TotalQuestionsAnswered)
            {
                case 0:
                    {
                        GenerateUI(10, 100);
                        break;
                    }
                case 1:
                    {
                        GenerateUI(100, 1000);
                        break;
                    }
                case 2:
                    {
                        GenerateUI(1000, 10000);
                        break;
                    }
                case 3:
                    {
                        GenerateUI(10000, 100000);
                        break;
                    }
                case 4:
                    {
                        GenerateUI(1000000, 10000000);
                        break;
                    }
                case 5:
                    {
                        GenerateUI(10000000, 100000000);
                        break;
                    }
                default:
                    {

                        FinishQuiz();
                        break;
                    }
            }
        }

        private void FinishQuiz()
        {
            ProblemTxT.Text = "Finished!";
            ProblemTxT.Visibility = Visibility.Visible;

            TimerTxT.Visibility = Visibility.Collapsed;

            timer.Stop();
            Collapser();

            StartBtn.Content = "Start Over?";
            StartBtn.Visibility = Visibility.Visible;
        }

        private void GenerateUI(int MinNum, int MaxNum)
        {
            RightAnswer = rd.Next(MinNum, MaxNum);
            int correctResultButton = rd.Next(0, 4);
            PossibleAnswers[correctResultButton] = RightAnswer;

            //Generate 3 random answers
            for (int i = 0; i < 4; i++)
            {
                if (i != correctResultButton)
                {
                    int randomAnswer = rd.Next(MinNum, MaxNum);
                    while (randomAnswer == RightAnswer)
                    {
                        randomAnswer = rd.Next(MinNum, MaxNum);
                    }
                    PossibleAnswers[i] = randomAnswer;
                }
            }

            ProblemTxT.Text = RightAnswer.ToString();
            //Populate buttons with content
            Btn1.Content = PossibleAnswers[0].ToString();
            Btn2.Content = PossibleAnswers[1].ToString();
            Btn3.Content = PossibleAnswers[2].ToString();
            Btn4.Content = PossibleAnswers[3].ToString();

            SecondsElapsed = MaxWaitSeconds;

            ProblemTxT.Visibility = Visibility.Visible;
            TimerTxT.Text = SecondsElapsed.ToString();
            TimerTxT.Visibility = Visibility.Visible;

        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            StartBtn.Visibility = Visibility.Collapsed;

            TotalQuestionsAnswered = 0;
            StartQuiz();
        }
        //Ticks every second
        void timer_Tick(object sender, object e)
        {
            if (SecondsElapsed == 0)
            {
                ProblemTxT.Visibility = Visibility.Collapsed;

                TimerTxT.Visibility = Visibility.Collapsed;

                Btn1.Visibility = Visibility.Visible;
                Btn2.Visibility = Visibility.Visible;
                Btn3.Visibility = Visibility.Visible;
                Btn4.Visibility = Visibility.Visible;
            }
            else if (SecondsElapsed < 0) return;
            else
            {
                SecondsElapsed--;
                TimerTxT.Text = SecondsElapsed.ToString();
            }
        }

        private void Btn1_Click(object sender, RoutedEventArgs e)
        {
            CheckPressedButton(sender);
        }
        private void Btn2_Click(object sender, RoutedEventArgs e)
        {
            CheckPressedButton(sender);
        }
        private void Btn3_Click(object sender, RoutedEventArgs e)
        {
            CheckPressedButton(sender);
        }
        private void Btn4_Click(object sender, RoutedEventArgs e)
        {
            CheckPressedButton(sender);
        }

    }
}
