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
    public partial class Menu : ContentPage
    {
        Entry entryName;
        public Menu()
        {
            RelativeLayout relative = new RelativeLayout();

            Image image = new Image {Aspect = Aspect.AspectFit , Source = Device.RuntimePlatform == Device.Android ? "LogoType.png" : "Images/LogoType.png" };
            
            entryName = new Entry { Placeholder = "Введите имя"};

            Button button = new Button { CornerRadius = 15, BackgroundColor = Color.Violet, Text = "Вход" };
            button.Clicked += Button_Clicked;

            relative.Children.Add(image,
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => 0),
                Constraint.RelativeToParent((parent) => parent.Width),
                Constraint.RelativeToParent((parent) => parent.Height * 0.5));

            relative.Children.Add(entryName,
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToView(image,(parent,view) => view.Height + view.Y + 5),
                Constraint.RelativeToParent((parent) => parent.Width * 0.5),
                Constraint.RelativeToParent((parent) => 40));

            relative.Children.Add(button,
                Constraint.RelativeToParent((parent) => parent.Width * 0.25),
                Constraint.RelativeToParent((parent) => parent.Height - 80),
                Constraint.RelativeToParent((parent) => parent.Width * 0.5),
                Constraint.RelativeToParent((parent) => 40));

            Content = relative;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PackChoice(entryName.Text));
        }
    }
}