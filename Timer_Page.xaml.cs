namespace TARpv23_Mobiile_App;

public partial class Timer_Page : ContentPage
{
    List<string> buttons = new List<string> { "Tagasi", "Avaleht", "Edasi" };
    HorizontalStackLayout hsl;
	public Timer_Page()
	{
        InitializeComponent();

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
    }
	bool on_off = true;
	private async void ShowTime()
	{
		while (on_off)
		{
            timer_btn.Text = DateTime.Now.ToString("T");
			await Task.Delay(1000);
		}
	}
    private void timer_btn_Clicked(object sender, EventArgs e)
    {
        if (on_off)
        {
            on_off = false;
        }
        else
        {
            on_off = true;
            ShowTime();
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

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}