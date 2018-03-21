using ScopeSuite.ViewModel;
using ScopeSuite.Wrapper;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using ScopeSuite.Command;
using UITestGround.DataProvider;
using System;
using ScopeSuite.Model;

namespace UITestGround.ViewModels
{
    public class MainViewModel : ObservableBase
    {
        readonly TestDataProvider _testDataProvider;

        public MainViewModel(TestDataProvider generalDataProvider)
        {
            _testDataProvider = generalDataProvider;
            PriceBook = new ObservableCollection<AdderWrapper>(_testDataProvider.PriceBook);
            CurrentIPBU = _testDataProvider.RandomIPBUWrapperFromCollection;
            PersonTest = new ObservableCollection<Person>
            {
                new Person{ Name = "Steve", Age = "20"},
                new Person{Name = "Adrian", Age = "44"},
                new Person{Name = "barbie", Age = "13"}

            };
            /*CurrentIPBU.PropertyChanged += UpdatePrices;
            SampleSaveCommand = new RelayCommand(OnSaveExecute, () => PriceBook.Any(aw => aw.IsChanged) && PriceBook.All(aw => aw.IsValid));
            SampleResetCommand = new RelayCommand(OnResetExecute, () => PriceBook.Any(aw => aw.IsChanged));
            DockWindowOpenCommand = new RelayCommand(OpenDockWindow);
            
            foreach (var aw in PriceBook)
            {
                aw.PropertyChanged += (s, e) => { InvalidateCommands(); };
            }
            PriceBook.CollectionChanged += (s, e) => { InvalidateCommands(); }; */
            foreach (var aw in PriceBook)
            {
                aw.PropertyChanged += (s, e) => { PropertyStatus(s, e); };
            }
        }

        private void PropertyStatus(object s, PropertyChangedEventArgs e)
        {
            AdderWrapper wrapper = s as AdderWrapper;
            MessageBox.Show(wrapper.Description, e.PropertyName);
        }


        /*
private void OpenDockWindow()
{
   AvalonDockView dockView = new AvalonDockView();
   dockView.Show();
}

private void OnResetExecute()
{
   MessageBox.Show("Reset");
}

private void OnSaveExecute()
{
   List<string> descriptions = PriceBook.Select(aw => aw.Description).ToList();
   string description = string.Concat(descriptions);
   MessageBox.Show(string.Format("Saved: {0}", description));
}

private void InvalidateCommands()
{
   ((RelayCommand)SampleSaveCommand).RaiseCanExecuteChanged();
   ((RelayCommand)SampleResetCommand).RaiseCanExecuteChanged();
}*/

        public ObservableCollection<AdderWrapper> PriceBook { get; set; }
        public IPBUWrapper CurrentIPBU { get; set; }
        public ObservableCollection<Person> PersonTest { get; set; }
        /*public ICommand SampleSaveCommand { get; private set; }
        public ICommand SampleResetCommand { get; private set; }
        public ICommand DockWindowOpenCommand { get; private set; }

        //debugging here was causing catastrophic failure
        private void UpdatePrices(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CurrentIPBU.SelectedFrame))
            {
                foreach (var aw in CurrentIPBU.JobAdders)
                {
                    aw.CurrentFrame = CurrentIPBU.SelectedFrame;
                }
            }
            if(e.PropertyName == nameof(CurrentIPBU.Discount))
            {
                foreach(var aw in CurrentIPBU.JobAdders)
                {
                    aw.DiscountLevel = CurrentIPBU.Discount;
                }
            }
        }*/
    }
}
