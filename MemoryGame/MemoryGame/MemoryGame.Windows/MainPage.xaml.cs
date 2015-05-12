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
        int rightAnswer;
        int totalAnswers;
        int[] answers;
        int points;
        DispatcherTimer timer;
        int totalseconds;
        int WaitSeconds = 3;

        public MainPage()
        {
            this.InitializeComponent();
            ProblemTxT.Visibility = Visibility.Collapsed;
            Collapser();
        }

        private void CheckPressedButton(object sender)
        {
            Button PressedBtn = (Button)sender;
            if (PressedBtn.Content.ToString() == rightAnswer.ToString())
            {
                points += 10;
                ScoreTxT.Text = "Score: " + points.ToString();
            }
            totalAnswers++;

            Collapser();

            HandleRangeOfRandomAnswer();
        }

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
            answers = new int[4];
            points = 0;
            totalseconds = WaitSeconds;

            HandleRangeOfRandomAnswer();

            ProblemTxT.Visibility = Visibility.Visible;

            TimerTxT.Visibility = Visibility.Visible;
            TimerTxT.Text = totalseconds.ToString();

        }

        private void HandleRangeOfRandomAnswer()
        {
            Btn1.Content = 0;
            Btn2.Content = 0;
            Btn3.Content = 0;
            Btn4.Content = 0;
            switch (totalAnswers)
            {
                case 0:
                    {
                        GenerateUI(0, 100);
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
            TimerTxT.Visibility = Visibility.Collapsed;

            timer.Stop();
            Collapser();
        }

        private void GenerateUI(int MinNum, int MaxNum)
        {
            rightAnswer = rd.Next(MinNum, MaxNum);
            int correctResultButton = rd.Next(0, 4);
            answers[correctResultButton] = rightAnswer;

            //Content to the rest 3 buttons
            for (int i = 0; i < 4; i++)
            {
                if (i != correctResultButton)
                {
                    int randomAnswer = rd.Next(MinNum, MaxNum);
                    while (randomAnswer == rightAnswer)
                    {
                        randomAnswer = rd.Next(MinNum, MaxNum);
                    }
                    answers[i] = randomAnswer;
                }
            }

            ProblemTxT.Text = rightAnswer.ToString();
            Btn1.Content = answers[0].ToString();
            Btn2.Content = answers[1].ToString();
            Btn3.Content = answers[2].ToString();
            Btn4.Content = answers[3].ToString();

            totalseconds = WaitSeconds;

            ProblemTxT.Visibility = Visibility.Visible;
            TimerTxT.Text = totalseconds.ToString();
            TimerTxT.Visibility = Visibility.Visible;

        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

            StartBtn.Visibility = Visibility.Collapsed;
            StartQuiz();
        }

        void timer_Tick(object sender, object e)
        {
            if (totalseconds == 0)
            {
                ProblemTxT.Visibility = Visibility.Collapsed;

                TimerTxT.Visibility = Visibility.Collapsed;

                Btn1.Visibility = Visibility.Visible;
                Btn2.Visibility = Visibility.Visible;
                Btn3.Visibility = Visibility.Visible;
                Btn4.Visibility = Visibility.Visible;
            }
            else if (totalseconds < 0) return;
            else
            {
                totalseconds--;
                TimerTxT.Text = totalseconds.ToString();
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
