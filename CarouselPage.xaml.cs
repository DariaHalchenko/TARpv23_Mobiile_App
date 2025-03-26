namespace TARpv23_Mobiile_App;
using Microsoft.Maui.Controls;

public partial class CarouselPage : ContentPage
{
    public class CarouselItem
    {
        public string Title { get; set; }
        public Color Color { get; set; }
    }

    public CarouselPage(int k)
    {
        InitializeComponent();

        var items = new List<CarouselItem>
        {
            new CarouselItem { Title = "Red", Color = Colors.Red },
            new CarouselItem { Title = "Green", Color = Colors.Green },
            new CarouselItem { Title = "Blue", Color = Colors.Blue }
        };

        carouselView.ItemsSource = items;

        carouselView.ItemTemplate = new DataTemplate(() =>
        {
            var stack = new StackLayout { Padding = 20 };

            var label = new Label
            {
                HorizontalOptions = LayoutOptions.Center,
                FontSize = 24
            };
            label.SetBinding(Label.TextProperty, "Title");

            var box = new BoxView
            {
                WidthRequest = 200,
                HeightRequest = 200,
                HorizontalOptions = LayoutOptions.Center
            };
            box.SetBinding(BoxView.ColorProperty, "Color");

            stack.Children.Add(label);
            stack.Children.Add(box);

            return stack;
        });
    }
}
