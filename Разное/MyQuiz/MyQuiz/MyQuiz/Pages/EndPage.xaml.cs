using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyQuiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndPage : ContentPage
    {
        public EndPage(string namePack,string name ,List<CheckForAnswers> checkForAnswers)
        {
            NavigationPage.SetHasBackButton(this,false);
            int i = 0;
            foreach (var item in checkForAnswers)
            {
                if (item.Answer == item.CorrectAnswer)
                {
                    i++;
                }
            }
            Label label = new Label { Text = "Список ответов на вопросы", HorizontalOptions = LayoutOptions.Center, FontSize = 25, };

            Label labelStatus = new Label { Text = $"Пользователь: {name}\nНаименование комплекта: {namePack}\nПравильных ответов: {i} из {checkForAnswers.Count}", FontSize = 18, };

            #region ListView
            ListView list = new ListView { RowHeight = 100, };
            list.ItemsSource = checkForAnswers;
            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lAnswer = new Label { FontSize = 14, };
                lAnswer.SetBinding(Label.TextProperty, new Binding { Path = "Answer", StringFormat = "Ответ: {0}", });

                Label lCorrectAnswer = new Label { FontSize = 14, };
                lCorrectAnswer.SetBinding(Label.TextProperty, new Binding { Path = "CorrectAnswer", StringFormat = "Правильный ответ: {0}", });

                StackLayout stack = new StackLayout { Children = { lAnswer, lCorrectAnswer } };
                Frame frame = new Frame
                {
                    Margin = 15,
                    Content = stack,
                    CornerRadius = 20,
                    BackgroundColor = Color.LightSkyBlue,
                };

                return new ViewCell { View = frame };
            });
            #endregion
            Button button = new Button { Text = "В начало", FontSize = 25,HeightRequest = 100};
            button.Clicked += Button_Clicked;

            Content = new StackLayout { Children = { label, labelStatus, list, button }, };
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PopToRootAsync();
        }
    }
}