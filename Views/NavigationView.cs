using FlowShop.ViewModels;
using System;

namespace FlowShop.Views
{
    internal class NavigationView
    {
        private BaseViewModel _currentView;

        public BaseViewModel CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = null;
                _currentView = value;
                OnViewChanged();
            }
        }

        public event Action ViewChanged;
        private void OnViewChanged()
        {
            ViewChanged.Invoke();
        }
    }
}
