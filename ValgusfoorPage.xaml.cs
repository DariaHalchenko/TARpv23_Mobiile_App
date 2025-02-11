namespace TARpv23_Mobiile_App
{
    public partial class ValgusfoorPage : ContentPage
    {
        private bool isOn = false;
        private bool isAuto = false;
        private bool vilgub = false;
        private Label header;
        private List<Frame> ring;
        private readonly List<Color> aktiivsed = new List<Color> { Colors.Red, Colors.Yellow, Colors.Green };
        private readonly List<string> vastused = new List<string> { "Peatu", "Oota", "Mine" };
        private readonly Random rnd = new Random();
        private int? RandomIndex = null;

        public ValgusfoorPage()
        {
            Title = "Valgusfoor";
            header = new Label
            {
                Text = "Valgusfoor",
                FontSize = 24,
                HorizontalOptions = LayoutOptions.Center
            };

            ring = new List<Frame>();
            StackLayout lightsStack = new StackLayout
            {
                Spacing = 10,
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Start
            };

            for (int i = 0; i < 3; i++)
            {
                var boxview = new BoxView
                {
                    Color = Colors.Gray,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    CornerRadius = 50
                };

                var frame = new Frame
                {
                    Padding = 0,
                    Content = boxview,
                    HasShadow = false,
                    BorderColor = Colors.Black,
                    CornerRadius = 50,
                    HeightRequest = 100,
                    WidthRequest = 100,
                    HorizontalOptions = LayoutOptions.Center,
                    VerticalOptions = LayoutOptions.Center,
                };

                // Circle click handler: muudab päise vastava sõnumi päise
                int index = i;
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) =>
                {
                    if (!isOn || isAuto || vilgub) // Kui valgusfoor on välja lülitatud või automaatne/ vilkuv režiim, ignoreerige vajutamist.
                        return;

                    header.Text = vastused[index];
                    AnimateFrame(frame);
                };
                frame.GestureRecognizers.Add(tapGestureRecognizer);

                lightsStack.Children.Add(frame);
                ring.Add(frame);
            }

            Button onButton = new Button { Text = "Sisse" };
            onButton.Clicked += (s, e) => TurnOn();

            Button offButton = new Button { Text = "Välja" };
            offButton.Clicked += (s, e) => TurnOff();

            Button randomButton = new Button { Text = "Juhuslik valik" };
            randomButton.Clicked += (s, e) => ActivateRandomLight();

            Button autoButton = new Button { Text = "Automaatne režiim " };
            autoButton.Clicked += (s, e) => StartAuto();

            StackLayout kontroll = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 20,
                Children = { onButton, offButton }
            };

            // Vastuse valimise nupud
            Button btnPeatu = new Button { Text = "Peatu" };
            btnPeatu.Clicked += (s, e) => Kontrollivastus("Peatu");

            Button btnOota = new Button { Text = "Oota" };
            btnOota.Clicked += (s, e) => Kontrollivastus("Oota");

            Button btnMine = new Button { Text = "Mine" };
            btnMine.Clicked += (s, e) => Kontrollivastus("Mine");

            StackLayout vastusButtons = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Center,
                Spacing = 20,
                Children = { btnPeatu, btnOota, btnMine }
            };

            // Lehe peamine konteiner
            Content = new StackLayout
            {
                Spacing = 20,
                Padding = new Thickness(20),
                VerticalOptions = LayoutOptions.Center,
                Children = { header, lightsStack, kontroll, autoButton, randomButton, vastusButtons }
            };
        }

        // Liiklustulede sisselülitamine
        private void TurnOn()
        {
            if (isAuto || vilgub)
                return;

            isOn = true;
            header.Text = "Valgusfoor on sisse lülitatud. Vali režiim.";
            RandomIndex = null;
            for (int i = 0; i < ring.Count; i++)
            {
                var box = (BoxView)ring[i].Content;
                box.Color = aktiivsed[i];
            }
        }

        // Liiklustulede väljalülitamine
        private void TurnOff()
        {
            isOn = false;
            isAuto = false;
            vilgub = false;
            header.Text = "Lülita esmalt valgusfoor sisse";
            RandomIndex = null;
            foreach (var frame in ring)
            {
                var box = (BoxView)frame.Content;
                box.Color = Colors.Gray;
            }
        }

        // Juhuslik värvirežiim
        private void ActivateRandomLight()
        {
            if (!isOn || isAuto || vilgub) // Kui valgusfoor on välja lülitatud või automaatne/ vilkuv režiim, ignoreerige vajutamist.
                return;

            int index = rnd.Next(0, 3);
            RandomIndex = index;
            header.Text = "Mis on õige vastus?";

            for (int i = 0; i < ring.Count; i++)
            {
                var box = (BoxView)ring[i].Content;
                box.Color = (i == index) ? aktiivsed[i] : Colors.Gray;
            }
        }

        // Automaatne režiim
        private async void StartAuto()
        {
            if (!isOn)
            {
                header.Text = "Lülitage esmalt valgusfoor põlema!";
                return;
            }

            isAuto = true;
            vilgub = true;
            header.Text = "Automaatne režiim";

            while (isAuto)
            {
                for (int i = 0; i < 3; i++)
                {
                    ((BoxView)ring[i].Content).Color = Colors.Gray;
                }
                
                if (vilgub)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        ((BoxView)ring[j].Content).Color = aktiivsed[j];
                        await Task.Delay(500);
                        ((BoxView)ring[j].Content).Color = Colors.Gray;
                    }
                }
                else
                {
                    ((BoxView)ring[1].Content).Color = aktiivsed[1];
                    await Task.Delay(500);
                    ((BoxView)ring[1].Content).Color = Colors.Gray;
                }

                await Task.Delay(1000);
            }

            vilgub = false;
        }

        // Valitud vastuse kontrollimine
        private void Kontrollivastus(string answer)
        {
            if (!isOn || RandomIndex == null)
            {
                header.Text = "Lülita esmalt valgusfoor sisse ja vali juhuslik režiim";
                return;
            }

            header.Text = answer == vastused[RandomIndex.Value] ? "Õige!" : "Vale!";
        }
        // Kliki animatsioon
        private async void AnimateFrame(Frame frame)
        {
            await frame.ScaleTo(1.2, 100);
            frame.BorderColor = Colors.White;
            await Task.Delay(200);
            await frame.ScaleTo(1.0, 100);
            frame.BorderColor = Colors.Black;
        }
    }
}
