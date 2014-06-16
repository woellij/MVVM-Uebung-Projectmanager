using System;
namespace MVVM_Uebung.Common
{
    interface INavigation
    {
        bool CanGoBack { get; }
        MVVM_Uebung.RelayCommand GoBackCommand { get; set; }
        MVVM_Uebung.RelayCommand GoForwardCommand { get; }

        void Show(Type viewModelType);
        void Show(Type viewModelType, object parameter);
    }
}
