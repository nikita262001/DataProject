using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyQuiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuestionsPage : ContentPage
    {
        List<CheckForAnswers> answersOnQuestions = new List<CheckForAnswers>();
        string name;
        ObservableCollection<Answer> answersObs = new ObservableCollection<Answer>();
        IEnumerable<Question> questions;
        IEnumerable<Answer> answers;
        Label labelQuestionName;
        ListView list;
        int numberQuestions;
        public QuestionsPage(string _name, IEnumerable<Question> _questions, IEnumerable<Answer> _answers)
        {
            name = _name;
            questions = _questions;
            answers = _answers;
            numberQuestions = 0;

            foreach (var item in answers.Where((item) => item.IdQuestion == questions.ElementAt(numberQuestions).IdQuestion))
            {
                answersObs.Add(item);
            }

            labelQuestionName = new Label { Text = questions.ToArray()[numberQuestions].Name, HorizontalOptions = LayoutOptions.CenterAndExpand, HorizontalTextAlignment = TextAlignment.Center, FontSize = 25 };
            list = new ListView { RowHeight = 95, };
            Button button = new Button { Text = "Далее", HeightRequest = 75 };
            button.Clicked += Button_Clicked;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Label label = new Label();
                label.SetBinding(Label.TextProperty, new Binding { Path = "Name", });


                return new ViewCell { View = new Frame { Margin = 10, Content = label, CornerRadius = 5, BackgroundColor = Color.LightSkyBlue, } };
            });
            Content = new StackLayout { Children = { labelQuestionName, list, button } };
        }
        protected override void OnAppearing()
        {
            if (answers.Count() != 0)
            {
                list.ItemsSource = answersObs;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (list.SelectedItem != null) // проверка на выбранный элемент
            {
                string correctAnswer = "Все ответы не правильны";
                foreach (var item in answersObs)
                {
                    if (item.CorrectAnswer)
                    {
                        correctAnswer = item.Name;
                    }
                }

                answersOnQuestions.Add(new CheckForAnswers { Answer = ((Answer)list.SelectedItem).Name, CorrectAnswer = correctAnswer });
                if (answersOnQuestions.Count < questions.Count())
                {
                    numberQuestions++;
                    labelQuestionName.Text = questions.ElementAt(numberQuestions).Name;
                    answersObs.Clear();
                    foreach (var item in answers.Where((item) => item.IdQuestion == questions.ElementAt(numberQuestions).IdQuestion))
                    {
                        answersObs.Add(item);
                    }
                }
                else
                {
                    string namePack = (await App.DatabasePack.GetItemAsync(questions.ElementAt(numberQuestions).IdPack)).Name;
                    Navigation.PushAsync(new EndPage(namePack,name, answersOnQuestions));
                }
            }
            else
            {
                var answer = await DisplayAlert("Уведомление", "Вы не выбрали ответ\nЕсли ответов на экране нет то нажмите на кнопку 'Назад'", "Назад", "Вернуться к вопросу");
                if (answer)
                {
                    Navigation.PopAsync();
                }
            }
        }
    }
}