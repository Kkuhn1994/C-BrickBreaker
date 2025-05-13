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
        private int _score = 0;
        private Player player;
        private Ball ball;
        private Blocks blocks;
        // private TextBlock ScoreText;
        private double playerSpeed = 20;
        private double ballSpeed = 2;

        public int Score
        {
            get => _score;
            set
            {
                _score = value;
                UpdateScoreText();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            keyEvents();
            // Steuerelemente im XAML finden
            var playerRectangle = this.FindControl<Rectangle>("PlayerRectangle");
            var ballEllipse = this.FindControl<Ellipse>("Ball");
            ScoreText = this.FindControl<TextBlock>("ScoreText");
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
            ball = new Ball(ballEllipse, canvasHeight, canvasWidth, player, blocks, this);
    
            StartBallMovement();
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

        public async void StartBallMovement()
        {
            while(_score < 3) 
            {
                if(await ball.Move())
                {
                    return;
                }
                ball.placeNewBall();
                Score ++;
            }
        }

        private void UpdateScoreText()
        {
            ScoreText.Text = $"Life Lost: {Score}";
        }

        public async Task<int> ShowWinMenue()
        {
            if (this.FindControl<Canvas>("GameCanvas") is { } gameCanvas)
            {
                var popup = new WinPopup();   
                double canvasWidth = gameCanvas.Bounds.Width > 0 ? gameCanvas.Bounds.Width : 800;
                double canvasHeight = gameCanvas.Bounds.Height > 0 ? gameCanvas.Bounds.Height : 600;
                Canvas.SetLeft(popup, (canvasWidth - 300) / 2);
                Canvas.SetBottom(popup, (canvasHeight) / 2 + 100);   
                gameCanvas.Children.Add(popup);
                int menueOptionPressed = await popup.menueLoop();
                gameCanvas.Children.Remove(popup);
                return menueOptionPressed;         
            }
            return 0;
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

        public void resetBlock(int blockNr) 
        {
            // Console.WriteLine(blockNr);
            var block = this.FindControl<Rectangle>($"Block{blockNr}");
            if (block != null)
            {
                block.IsVisible = true;
            }
        }
    }
}