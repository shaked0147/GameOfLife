using Prism.Mvvm;
using Prism.Regions;
using GameOfLife.Views;
using System.Windows.Input;
using Prism.Events;
using Prism.Commands;
using GameOfLife.Events;
using System;

namespace GameOfLife.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region Service Declaration

        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        #endregion

        public ICommand StartEndGameCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;

            _regionManager.RegisterViewWithRegion("GameOfLife", typeof(Game));

            StartEndGameCommand = new DelegateCommand(StartEndGame);
        }

        public void StartEndGame()
        {
            _eventAggregator.GetEvent<ToggleGame>().Publish();
        }
    }
}
