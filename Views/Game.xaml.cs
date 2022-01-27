using GameOfLife.ViewModels;
using System;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace GameOfLife.Views
{
    /// <summary>
    /// Interaction logic for Game
    /// </summary>
    public partial class Game : UserControl
    {
        GameViewModel _viewModel;

        public Game()
        {
            InitializeComponent();
            _viewModel = (GameViewModel)this.DataContext;
        }

    }
}
