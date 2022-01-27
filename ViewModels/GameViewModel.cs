using Prism.Mvvm;
using System.Threading;
using GameOfLife.Models;
using Prism.Events;
using GameOfLife.Events;
using System;

namespace GameOfLife.ViewModels
{
    public class GameViewModel : BindableBase
    {
        private IEventAggregator _eventAggregator;

        private bool[] _gameGrid;

        public bool[] GameGrid
        {
            get => _gameGrid;
            set => SetProperty(ref _gameGrid, value);
        }

        private int _rows;

        public int Rows
        {
            get => _rows;
            set => SetProperty(ref _rows, value);
        }

        private int _columns;

        public int Columns
        {
            get => _columns;
            set => SetProperty(ref _columns, value);
        }

        private bool _startGame = false;

        private TheGame _gameOfLife;

        private Thread t;

        public GameViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Columns = 30;
            Rows = 30;

            GameGrid = new bool[Rows * Columns];
            //GameGrid[50 * 25 + 25] = true;
            //GameGrid[50 * 25 + 26] = true;
            //GameGrid[50 * 25 + 27] = true;
            //GameGrid[50 * 24 + 26] = true;
            Random r = new Random();
            int count = 0;
            for (int i = 0; i < GameGrid.Length; i++)
            {
                if (r.Next(2) == 1 ? true : false && count <= 90)
                {
                    count++;
                    GameGrid[i] = true;
                }
            }


            _gameOfLife = new TheGame(GameGrid, Rows, Columns);

            t = new Thread(UpdateGame);

            _eventAggregator.GetEvent<ToggleGame>().Subscribe(StartEndGame);
        }

        public void StartEndGame()
        {
            _startGame = !_startGame;
            if (_startGame)
                t.Start();
        }

        private void UpdateGame()
        {
            while (_startGame)
            {
                Thread.Sleep(50);
                if (App.Current != null)
                {
                    App.Current.Dispatcher.Invoke(() =>
                    {
                        GameGrid = _gameOfLife.NextMove();

                    });
                }
            }

        }

    }
}
