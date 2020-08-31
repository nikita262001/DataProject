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
    public partial class PackChoice : ContentPage
    {
        List<Pack> bicycles = new List<Pack>();
        ListView list;
        string name;
        public PackChoice(string _name)
        {
            name = _name;
            Label label = new Label { Text = "Список всех комплектов с вопросами", HorizontalOptions = LayoutOptions.Center, FontSize = 25, };

            #region ListView
            list = new ListView { RowHeight = 100, };
            list.ItemTapped += List_ItemTapped; ;

            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Название: {0}", });

                Label lDateCreate = new Label { FontSize = 14, };
                lDateCreate.SetBinding(Label.TextProperty, new Binding { Path = "DateCreate", StringFormat = "Дата создания: {0:dd/MM/yyyy}", });

                StackLayout stack = new StackLayout { Children = { lName, lDateCreate } };

                return new ViewCell { View = new Frame { Margin = 15, Content = stack, CornerRadius = 20, BackgroundColor = Color.BlueViolet, } };
            });
            #endregion

            Content = new StackLayout { Children = { label, list }, };
            ListAddItem();
        }

        private async void ListAddItem()
        {
            list.ItemsSource = await App.DatabasePack.GetItemsAsync();
        }


        private async void List_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            bool result = await DisplayAlert("Подтвердить действие", $"Вы хотите перейти к вопросам {((Pack)e.Item).Name}", "Да", "Нет");

            if (result)
            {
                IEnumerable<Question> questions = (await App.DatabaseQuestion.GetItemsAsync()).Where((item)=> item.IdPack == ((Pack)e.Item).IdPack);
                List<Answer> answers = new List<Answer>();
                foreach (var itemA in await App.DatabaseAnswer.GetItemsAsync()) // получаем все ответы который связаны с вопросами из комплекта
                {
                    foreach (var itemQ in questions)
                    {
                        if (itemA.IdQuestion == itemQ.IdQuestion)
                        {
                            answers.Add(itemA);
                        }
                    }
                }
                if (questions.Count() != 0)
                {
                    await Navigation.PushAsync(new QuestionsPage(name, questions, answers));
                }
                else
                {
                    await DisplayAlert("Уведомление","В комплекте нет вопросов","Назад");
                }
            }
        }
    }
}