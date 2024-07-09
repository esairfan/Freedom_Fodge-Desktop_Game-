using System;
using System.Windows.Forms;
using System.Drawing;
using FreedomFodgeFrameWork;
using NAudio.Wave;

namespace FreedomFodge
{
    public partial class Form1 : Form
    {
        private string filePath = "D:\\Computer science\\PF\\THEORY\\PD\\PF Projects\\Game project\\Tom And Jerry Mouse Maze\\Level1Maze.txt";
        private Game game;
        private WaveOutEvent waveOut; 
        private AudioFileReader audioFile;

        public Form1()
        {
            InitializeComponent();
            game = Game.GetInstance(this);

        
            waveOut = new WaveOutEvent();
             
            audioFile = new AudioFileReader("D:\\Computer science\\OOP\\Business Application Project\\Game\\songs\\WhatsApp Audio 2024-05-08 at 04.31.46_b5dc7a55.mp3"); 
            waveOut.Init(audioFile);
            PlaySound();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Point Boundary = new Point(this.Width, this.Height);
            game.SetMaze(new Maze(this, filePath, '@', ImagePaths.MazeTile1, '#', ImagePaths.MazeTile2));
            game.SetPlayer(new Player(this, game, 1400, 924, 20, Boundary, ImagePaths.LeftPlayerImage1, ImagePaths.LeftPlayerImage2, ImagePaths.RightPlayerImage1, ImagePaths.RightPlayerImage2, ImagePaths.UpPlayerImage, ImagePaths.DownPlayerImage, ImagePaths.PlayerFire, 500,30, 2));
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy1Image, ImagePaths.RightEnemy1Image, game, 20, this, 450, 200, 1)); 
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy3Image, ImagePaths.RightEnemy3Image, game, 20, this, 570, 800, 1)); 
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy3Image, ImagePaths.RightEnemy3Image, game, 20, this, 1400, 580, 1)); 
            game.AddEnemies(new RandomEnemy(ImagePaths.LeftEnemy3Image, ImagePaths.RightEnemy3Image, game, 20, this, 1000, 924, 1));
            game.AddEnemies(new VerticalEnemy(ImagePaths.Enemy4Image, game, 7, ImagePaths.EnemyFire2, this, 1400, 800, 1));
            game.AddEnemies(new VerticalEnemy(ImagePaths.Enemy4Image, game, 11, ImagePaths.EnemyFire2, this, 1400, 280, 1));
            game.AddEnemies(new HorizontalEnemy(ImagePaths.LeftEnemy2Image, ImagePaths.RightEnemy2Image, game, ImagePaths.EnemyFire3, 11, this, 700, 349, 1));
            game.UpdateEnemyLabel();
            game.GetPlayer().UpdatePlayerScoreLabel();
           
            
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            game.Update();
          
            
        }

        private void PlaySound()
        {
            if (waveOut.PlaybackState == PlaybackState.Playing)
            {
                waveOut.Stop();
            }
            audioFile.Volume = 0.5f;
                waveOut.Play();
        }



        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            waveOut.Stop();
            waveOut.Dispose();
            audioFile.Dispose();
        }

         
    }
}
