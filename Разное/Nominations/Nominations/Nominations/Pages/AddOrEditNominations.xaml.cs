using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Nominations
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddOrEditNominations : ContentPage
    {
        List<string> elementsPickerImage = new List<string> // начальные ссылки для картинок
        {
            Device.RuntimePlatform == Device.Android ? "BiznesMan.jpg" :"Images/BiznesMan.jpg",
            Device.RuntimePlatform == Device.Android ? "Lul.jpg" :"Images/Lul.jpg",
            Device.RuntimePlatform == Device.Android ? "Master.jpg" :"Images/Master.jpg",
            Device.RuntimePlatform == Device.Android ? "Oskar.jpg" :"Images/Oskar.jpg",
            Device.RuntimePlatform == Device.Android ? "Sotrydnik.jpg" :"Images/Sotrydnik.jpg",
        };

        Button button;
        Button buttonDel;
        Image image;
        Picker pImage;
        public AddOrEditNominations() // страница нужна для добавления или редактирования данных которые лежат в БД
        {
            #region elements
            Label lID = new Label { };
            lID.SetBinding(Label.TextProperty, new Binding { Path = "ID", StringFormat = "ID: {0}" });

            Label lName = new Label { Text = "Наименование: ", WidthRequest = Device.RuntimePlatform == Device.Android ? 110 : 200, };
            Entry eName = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eName.SetBinding(Entry.TextProperty, new Binding { Path = "Name" });
            StackLayout sName = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lName, eName } };

            Label lModelNumber = new Label { Text = "Кол-во номинаций: ", WidthRequest = Device.RuntimePlatform == Device.Android ? 110 : 200, };
            Entry eModelNumber = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eModelNumber.SetBinding(Entry.TextProperty, new Binding { Path = "Nominations" });
            StackLayout sModelNumber = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lModelNumber, eModelNumber } };

            Label lNumberOfGears = new Label { Text = "Кол-во выйграных номинаций: ", WidthRequest = Device.RuntimePlatform == Device.Android ? 110 : 200, };
            Entry eNumberOfGears = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand, };
            eNumberOfGears.SetBinding(Entry.TextProperty, new Binding { Path = "NominationsWin" });
            StackLayout sNumberOfGears = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lNumberOfGears, eNumberOfGears } };

            Label lImage = new Label { Text = "Основная номинация: ", WidthRequest = Device.RuntimePlatform == Device.Android ? 110 : 200, };
            pImage = new Picker { HorizontalOptions = LayoutOptions.StartAndExpand,WidthRequest = Device.RuntimePlatform == Device.Android ? 110 : 200, };
            pImage.SelectedIndexChanged += PImage_SelectedIndexChanged;
            foreach (var item in elementsPickerImage)
            {
                pImage.Items.Add(item);
            }
            pImage.SetBinding(Picker.SelectedItemProperty, new Binding { Path = "Image" });
            StackLayout sImage = new StackLayout { Orientation = StackOrientation.Horizontal, Children = { lImage, pImage } };

            image = new Image
            {
                HeightRequest = 300,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Aspect = Aspect.AspectFit,
                IsVisible = false,
            };

            #endregion

            Frame frame = new Frame
            {
                Content = new StackLayout { Children = { lID, sName, sModelNumber, sNumberOfGears, sImage, image } },
                CornerRadius = 20,
                BackgroundColor = Color.LightYellow,
                Margin = 20,
            };

            buttonDel = new Button { FontSize = 30, HeightRequest = 75, Text = "Удалить", IsVisible = false };
            buttonDel.Clicked += DeletePerson;
            button = new Button { FontSize = 30, HeightRequest = 75, };
            StackLayout sButtons = new StackLayout { Children = { buttonDel, button }, VerticalOptions = LayoutOptions.EndAndExpand };

            StackLayout mainStack = new StackLayout { Children = { frame, sButtons } };
            ScrollView scroll = new ScrollView { Content = mainStack };
            Content = scroll;
        }

        private void DeletePerson(object sender, EventArgs e)
        {
            App.Database.DeleteItem(BindingContext as Person);
            Navigation.PopAsync();
        }

        private void PImage_SelectedIndexChanged(object sender, EventArgs e) // изменение картинки при нажатии у Picker на ячейку
        {
            if (pImage.SelectedIndex != -1)
            {
                image.IsVisible = true;
                image.Source = elementsPickerImage[pImage.SelectedIndex];
            }
        }

        protected override void OnAppearing()
        {
            if ((BindingContext as Person).ID == 0)
            {
                button.Text = "Добавить";
                button.Clicked += AddPersonDB;
            }
            else
            {
                image.IsVisible = true;
                buttonDel.IsVisible = true;
                button.Text = "Редактировать";
                button.Clicked += EditPersonDB;
            }
        }

        private void EditPersonDB(object sender, EventArgs e)
        {
            App.Database.EditItem(BindingContext as Person);
            Navigation.PopAsync();
        }

        private void AddPersonDB(object sender, EventArgs e)
        {
            App.Database.SaveItem(BindingContext as Person);
            Navigation.PopAsync();
        }
    }
}