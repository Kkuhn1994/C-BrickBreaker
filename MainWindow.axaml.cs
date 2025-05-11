using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Controls.Shapes;
using System;
using System.Threading.Tasks;
using ReactiveUI;

class Blocks;

namespace MyGameApp
{
    public partial class MainWindow : Window
    {
        private int score = 0;
        private Player player;
        private Ball ball;
        private Blocks blocks;
        private double playerSpeed = 20;
        private double ballSpeed = 2;

        public MainWindow()
        {
            InitializeComponent();
            keyEvents();
            // Steuerelemente im XAML finden
            var playerRectangle = this.FindControl<Rectangle>("PlayerRectangle");
            var ballEllipse = this.FindControl<Ellipse>("Ball");

            if (playerRectangle == null || ballEllipse == null)
            {
                Console.WriteLine("Control Elements could not be found.");
                return;
            }
            double canvasHeight = 1000;
            double canvasWidth = 800;
            // Player und Ball Objekte erstellen
            
            player = new Player(playerRectangle, playerSpeed);
            blocks = new Blocks(this);
            blocks.loadMap();
            ball = new Ball(ballEllipse, canvasHeight, canvasWidth, player, blocks);
    
            StartBallMovement();
            // Tasteneingabe registrieren
            // this.KeyDown += OnKeyDown;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void keyEvents() 
        {
            this.KeyBindings.Add(new KeyBinding
            {
                Command = ReactiveCommand.Create(() => player.MoveLeft()),
                Gesture = new KeyGesture(Key.A)
            });

            this.KeyBindings.Add(new KeyBinding 
            {
                Command = ReactiveCommand.Create(() => player.MoveRight()),
                Gesture = new KeyGesture(Key.D)
            });

            this.KeyBindings.Add(new KeyBinding
            {
                Command = ReactiveCommand.Create(() => ball.startBall()),
                Gesture = new KeyGesture(Key.Space)
            });
        }

        private async void StartBallMovement()
        {
            while(score < 3) 
            {
                await ball.Move();
                ball.placeNewBall();
            }
        }

        public void hideBlock(int blockNr) 
        {
            // Console.WriteLine(blockNr);
            var block = this.FindControl<Rectangle>($"Block{blockNr}");
            if (block != null)
            {
                block.IsVisible = false;
            }
        }
    }
}