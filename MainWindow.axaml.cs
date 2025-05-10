using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Shapes;
using System;

namespace MyGameApp
{
    public partial class MainWindow : Window
    {
        private Player player;
        private Ball ball;
        private double playerSpeed = 10;
        private double ballSpeed = 2;

        public MainWindow()
        {
            InitializeComponent();
            // Steuerelemente im XAML finden
            var playerRectangle = this.FindControl<Rectangle>("PlayerRectangle");
            var ballEllipse = this.FindControl<Ellipse>("Ball");

            if (playerRectangle == null || ballEllipse == null)
            {
                Console.WriteLine("Die Steuerelemente konnten nicht gefunden werden.");
                return;
            }
            double canvasHeight = 800;
            double canvasWidth = 800;
            // Player und Ball Objekte erstellen
            player = new Player(playerRectangle, playerSpeed);
            ball = new Ball(ballEllipse, canvasHeight, canvasWidth);
            StartBallMovement();

            // Tasteneingabe registrieren
            this.KeyDown += OnKeyDown;
        }



        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        // Bewegungssteuerung für das Rechteck
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)  // Bewege nach links
                player.MoveLeft();
            if (e.Key == Key.D)  // Bewege nach rechts
                player.MoveRight();
            if (e.Key == Key.Space)  // Bewege nach oben oder führe eine andere Aktion aus
                ball.startBall();
        }   
        private async void StartBallMovement()
        {
            await ball.Move();  // Ball bewegt sich in einer Schleife
        }
    }
}