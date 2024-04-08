using Backend;
using Backend.Engines;
using Backend.Models;
using Backend.Models.Figures;
using Chess_Project.UiEngine;
using Chess_Project.UiModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Chess_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DisplayCoordinateMapper _displayCoordinateMapper;
        private UserMoveTracker _userMoveTracker;
        private GameManager _gameManager;
        private UiFiguresManager _uiFiguresManager;
        public MainWindow()
        {
            InitializeComponent();
            // game objects initialization
            _displayCoordinateMapper = new DisplayCoordinateMapper((int)ChessBoardGrid.Width, (int)ChessBoardGrid.Height);
         //   CreateTestFigures();
            StartNewGame();

        }

        /*
        private void CreateTestFigures()
        {
            Image newimg = new Image();

            newimg.MouseDown += Figure_MouseDown;
            newimg.MouseUp += Figure_MouseUp;

            BitmapImage img = new BitmapImage();
            img.BeginInit();
            img.UriSource = new Uri("/Chess_assets/White_King.png", UriKind.Relative);
            img.EndInit();
            newimg.Source = img;
            newimg.Height = 125;
            newimg.Width = 125;


            Grid.SetRow(newimg, 2);
            Grid.SetColumn(newimg, 2);
            Pieces.Children.Add(newimg);
        }
        */
        private void SetImageEventHandlers(Image imageControl)
        {
            imageControl.MouseDown += Figure_MouseDown;
            imageControl.MouseUp += Figure_MouseUp;
        }
        private void StartNewGame()
        {
            _gameManager = new GameManager();
            _userMoveTracker = new UserMoveTracker(_displayCoordinateMapper, _gameManager);

            _uiFiguresManager = new UiFiguresManager(_displayCoordinateMapper,
                                ChessBoardGrid, MainCanvas, SetImageEventHandlers);

            _uiFiguresManager.InitializeFigures(_gameManager.GetFigures());
        }
        void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (_userMoveTracker.TrackingFigure == null) return;

            Point point = e.GetPosition(MainCanvas);
            
       
            _uiFiguresManager.ReDrawMovingFigure(_userMoveTracker.TrackingFigure, _userMoveTracker.GetClickPointAdjusted(point));
         
        }


        void Figure_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Point p = e.GetPosition(ChessBoardGrid);

            if (e.ButtonState != e.LeftButton) return;

            Image img = (Image)sender;
            UiFigure uiFig = (UiFigure)img.Tag;

            if (_userMoveTracker.TrackingFigure != uiFig) return;

            FigureMoveDescriptor move = _userMoveTracker.EndTracking(e.GetPosition(ChessBoardGrid));
            _uiFiguresManager.EndFigureMove(uiFig);

            if (move != null)
            {// if valid move
                ProcessUserMove(uiFig, move);
                
            }

            _userMoveTracker.ResetTracking();

        }

        private void ProcessUserMove(UiFigure uiFigure, FigureMoveDescriptor move)
        {
           
            _gameManager.RegisterUserMove(move);
           

            _uiFiguresManager.ReDrawMovingFigure(uiFigure,
                        _displayCoordinateMapper.GetUpperLeftCornerPointOfBoardSquare(move.MoveToLocation));
            
            if (move.OpponentFigure != null)
            {// user move eats figure at destination, i.e. it should be removed from board
                _uiFiguresManager.DeleteFigure(move.OpponentFigure);
            }

            _uiFiguresManager.InvalidateFigure(uiFigure);


            ProcessComputerMove();
        }

             

        // we register this event only for user figures,
        // i.e. no additional check required to ensure this is user figure
        void Figure_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ButtonState != e.LeftButton) return;

            _displayCoordinateMapper.ReSize((int)ChessBoardGrid.ActualWidth, (int)ChessBoardGrid.ActualHeight);

            Image img = (Image)sender;
            UiFigure uiFig = (UiFigure)img.Tag;
            _userMoveTracker.StartTracking(uiFig, e.GetPosition(MainGrid));

            _uiFiguresManager.BeginFigureMove(uiFig);
            //StackPanel.SetZIndex(MyWpFigure, 1);
           
        }
        
        private void ProcessComputerMove()
        {

            FigureMoveDescriptor move = _gameManager.DoComputerMove();
            
            if (move.OpponentFigure != null)
            {// user move eats figure at destination, i.e. it should be removed from board
                _uiFiguresManager.DeleteFigure(move.OpponentFigure);
            }

            _uiFiguresManager.InvalidateFigure(move.SourceFigure);
        }
        
    }
}
