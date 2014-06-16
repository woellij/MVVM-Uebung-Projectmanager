using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml;
using System.Windows.Input;

namespace MVVM_Uebung.Common
{
    class Navigation : BindableBase, INavigation
    {
        public ICommand this[string key]
        {
            get
            {
                return new RelayCommand<object>(parameter =>
                {
                    this.Show(key, parameter);
                });
            }
        }


        private Windows.UI.Xaml.Controls.Frame frame;

        private Dictionary<string, Type> resolvedPages;
        private Dictionary<string, Type> resolvedViewModels;

        public Navigation(Frame frame)
        {
            this.frame = frame;
            frame.Navigated += frame_Navigated;
            frame.Navigating += frame_Navigating;
            ResolvePages();
            //ResolveViewModels();
        }

        void frame_Navigating(object sender, Windows.UI.Xaml.Navigation.NavigatingCancelEventArgs e)
        {
            if (currentPage == null)
                return;

            var iRestore = currentPage.DataContext as State.IStateSaving;
            if (iRestore != null)
                iRestore.SaveStateAsync();
        }

        private void ResolveViewModels()
        {
            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes;
            resolvedViewModels = types.Where(t => t.IsSubclassOf(typeof(Common.ViewModelBase))).ToDictionary(type => type.Name.ToLowerInvariant().Replace("viewmodel", ""), type => type.AsType());
        }

        private void ResolvePages()
        {
            var types = this.GetType().GetTypeInfo().Assembly.DefinedTypes;
            resolvedPages = types.Where(t => t.AsType().Equals(typeof(Page)) || t.IsSubclassOf(typeof(Page))).ToDictionary(type => type.Name.ToLowerInvariant().Replace("page", ""), type => type.AsType());
        }

        Page currentPage;
        NavigationHelper helper;
        void frame_Navigated(object sender, Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            currentPage = e.Content as Page;
            helper = new NavigationHelper(currentPage);


            var iRestore = currentPage.DataContext as State.IStateSaving;
            if (iRestore != null)
                iRestore.RestoreStateAsync();

            this.RaisePropertyChanged("GoBackCommand");
        }

        public bool CanGoBack
        {
            get
            {
                if (helper != null)
                    return helper.CanGoBack();
                else return false;
            }
        }

        public RelayCommand GoBackCommand
        {
            get
            {
                if (helper != null)
                    return helper.GoBackCommand;
                return null;
            }
            set
            {

            }
        }

        public RelayCommand GoForwardCommand
        {
            get
            {
                if (helper != null)
                    return helper.GoForwardCommand;
                return null;
            }
        }

        public void Show(string key, object parameter)
        {
            Type targetType;
            if (this.resolvedPages.TryGetValue(key.ToLowerInvariant(), out targetType))
            {
                frame.Navigate(targetType, parameter);
            }
            else
                throw new NotSupportedException("Couldn't resolve the Page / View for the requested ViewModel " + key);
        }

        public void Show(Type viewModelType)
        {
            this.Show(viewModelType, null);
        }

        public void Show(Type viewModelType, object parameter)
        {
            var name = viewModelType.Name.ToLowerInvariant().Replace("viewmodel", "");
            this.Show(name, parameter);
        }
    }
}
