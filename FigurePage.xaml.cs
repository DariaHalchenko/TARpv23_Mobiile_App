namespace TARpv23_Mobiile_App;

public partial class FigurePage : ContentPage
{
    BoxView bw;
    Label lbl;
    Random rnd = new Random();
    HorizontalStackLayout hsl;
    List<string> buttons = new List<string> { "Tagasi", "Avaleht", "Edasi" };
    int click = 0; 

    public FigurePage(int k)
    {
        int r = rnd.Next(0, 255);
        int g = rnd.Next(0, 255);
        int b = rnd.Next(0, 255);

        lbl = new Label
        {
            Text = "Klikid: 0",
            FontSize = 24,
            TextColor = Color.FromRgb(50, 50, 50),
            HorizontalOptions = LayoutOptions.Center
        };

        bw = new BoxView
        {
            Color = Color.FromRgb(r, g, b),
            CornerRadius = 20,
            WidthRequest = 200,
            HeightRequest = 200,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            BackgroundColor = Color.FromRgba(0, 0, 0, 0)
        };

        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.Tapped += Klik_boksi_peal;
        bw.GestureRecognizers.Add(tap);

        hsl = new HorizontalStackLayout { };
        for (int i = 0; i < 3; i++)
        {
            Button nupp = new Button
            {
                Text = buttons[i],
                ZIndex = i,
                WidthRequest = DeviceDisplay.Current.MainDisplayInfo.Width / 8.3,
            };

            hsl.Add(nupp);
            nupp.Clicked += Liikumine;
        }

        VerticalStackLayout vsl = new VerticalStackLayout
        {
            Children = { lbl, bw, hsl },
            VerticalOptions = LayoutOptions.End
        };

        Content = vsl;
    }

    private void Klik_boksi_peal(object? sender, TappedEventArgs e)
    {
        click++; 
        lbl.Text = $"Klikid: {click}"; 

        bw.Color = Color.FromRgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));

        bw.WidthRequest += 20;
        bw.HeightRequest += 20;

        if (bw.WidthRequest > (int)DeviceDisplay.MainDisplayInfo.Width / 3)
        {
            bw.WidthRequest = 200;
            bw.HeightRequest = 200;
            click = 0; 
            lbl.Text = "Klikid: 0";
        }
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

    private async void Tagasi_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }
}
