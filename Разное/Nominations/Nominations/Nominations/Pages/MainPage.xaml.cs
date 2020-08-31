using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Nominations
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        ListView list;
        public MainPage()
        {
            Label label = new Label { Text = "Номинации", HorizontalOptions = LayoutOptions.Center, FontSize = 40, };

            Button button = new Button { HeightRequest = 75, Text = "Добавить", FontSize = 30 };
            button.Clicked += AddPerson;
            #region ListView
            list = new ListView { RowHeight = Device.RuntimePlatform == Device.Android ? 150 : 220, };
            list.ItemTapped += List_ItemTapped;

            // ячейка листа
            list.ItemTemplate = new DataTemplate(() =>
            {
                Label lName = new Label { FontSize = 14, };
                lName.SetBinding(Label.TextProperty, new Binding { Path = "Name", StringFormat = "Название: {0}", });

                Label lModelNumber = new Label { FontSize = 14, };
                lModelNumber.SetBinding(Label.TextProperty, new Binding { Path = "Nominations", StringFormat = "Количество номинаций: {0}", });

                Label lNumberOfGears = new Label { FontSize = 14, };
                lNumberOfGears.SetBinding(Label.TextProperty, new Binding { Path = "NominationsWin", StringFormat = "Номинаций выйграно: {0}", });

                Image image = new Image { Aspect = Aspect.AspectFit };
                image.SetBinding(Image.SourceProperty, new Binding { Path = "Image" });

                // размещение элементов relative
                #region RelativeLayout
                RelativeLayout relative = new RelativeLayout { };

                relative.Children.Add(image,
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => -5),
                     Constraint.RelativeToParent((parent) => parent.Width * 0.25 ),
                     Constraint.RelativeToParent((parent) => Device.RuntimePlatform == Device.Android ? 100 : 150));

                relative.Children.Add(lName,
                     Constraint.RelativeToView(image, (parent, view) => view.X + view.Width + 10),
                     Constraint.RelativeToView(image, (parent, view) => 5));

                relative.Children.Add(lModelNumber,
                     Constraint.RelativeToView(lName, (parent, view) => view.X),
                     Constraint.RelativeToView(lName, (parent, view) => view.Y + view.Height + 2));

                relative.Children.Add(lNumberOfGears,
                     Constraint.RelativeToView(lModelNumber, (parent, view) => view.X),
                     Constraint.RelativeToView(lModelNumber, (parent, view) => view.Y + view.Height + 2));
                #endregion

                return new ViewCell { View = new Frame { Margin = 15, Content = relative, CornerRadius = 20, BackgroundColor = Color.LightYellow, HeightRequest = 70 } };
            });
            #endregion

            Content = new StackLayout { Children = { label, list, button }, };
        }

        private void List_ItemTapped(object sender, ItemTappedEventArgs e) // метод при нажатии на ячейку из ListView
        {
            Navigation.PushAsync(new AddOrEditNominations { BindingContext = (Person)e.Item });
        }

        private void AddPerson(object sender, EventArgs e) // создание нового Person
        {
            Navigation.PushAsync(new AddOrEditNominations { BindingContext = new Person() });
        }

        protected override void OnAppearing() // запускается когда страница становится видима
        {
            list.ItemsSource = App.Database.GetItems(); // добавление данных в ListView
        }
    }
}
