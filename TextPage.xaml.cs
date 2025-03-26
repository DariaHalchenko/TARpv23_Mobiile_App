namespace TARpv23_Mobiile_App;

public partial class TextPage : ContentPage
{
    Label lbl;
    Editor editor;
    HorizontalStackLayout hsl;
    List<string> buttons = new List<string> { "Tagasi", "Avaleht", "Edasi" };
    Random rnd = new Random();
    public TextPage(int k)
    {
        lbl = new Label
        {
            Text = "Pealkiri",
            TextColor = Color.FromRgb(100, 10, 10),
            FontFamily = "Luckymoon 400",
            FontAttributes = FontAttributes.Bold,
            TextDecorations = TextDecorations.Underline,
            HorizontalTextAlignment = TextAlignment.Center,
            VerticalTextAlignment = TextAlignment.Center,
            FontSize = 28,
        };
        TapGestureRecognizer tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += TapGestureRecognizer_Tapped;
        lbl.GestureRecognizers.Add(tapGestureRecognizer);

        editor = new Editor
        {
            Placeholder = "Vihje: Sisesta siia tekst",
            PlaceholderColor = Color.FromRgb(250, 200, 100),
            BackgroundColor = Color.FromRgb(200, 200, 100),
            TextColor = Color.FromRgb(100, 50, 200),
            FontSize = 28,
            FontAttributes = FontAttributes.Italic,
        };

        editor.TextChanged += Teksti_sisestamine; 

        hsl = new HorizontalStackLayout { };
        for (int i = 0; i < 3; i++)
        {
            Button b = new Button
            {
                Text = buttons[i],
                ZIndex = i,
                WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 8.3,
            };
            hsl.Add(b);
            b.Clicked += Liikumine; ;
        }

        VerticalStackLayout vst = new VerticalStackLayout
        {
            Children = { lbl, editor, hsl },
            VerticalOptions = LayoutOptions.End
        };

        Content = vst;
    }

    private async void TapGestureRecognizer_Tapped(object? sender, TappedEventArgs e)
    {
        await lbl.RelRotateTo(360, 500);
        lbl.TextColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)); // ???????? ???? ????????? ??? ?????? ???????
    }

    private async void Liikumine(object? sender, EventArgs e)
    {
        Button btn = (Button)sender;
        if (btn.ZIndex == 0)
        {
            await Navigation.PushAsync(new TextPage(btn.ZIndex));
        }
        else if (btn.ZIndex == 1)
        {
            await Navigation.PushAsync(new StartPage());
        }
        else if (btn.ZIndex == 2)
        {
            await Navigation.PushAsync(new Timer_Page());
        }
        else if (btn.ZIndex == 3)
        {
            await Navigation.PushAsync(new ValgusfoorPage(btn.ZIndex));
        }
        else
        {
            await Navigation.PushAsync(new FigurePage(btn.ZIndex));
        }
    }

    private void Teksti_sisestamine(object? sender, TextChangedEventArgs e)
    {
        lbl.Text = editor.Text;
        lbl.TextColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
        editor.BackgroundColor = Color.FromRgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
    }
    private async void Tagasi_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}
