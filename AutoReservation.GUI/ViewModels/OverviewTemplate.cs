using AutoReservation.GUI.Commands;
using System.Collections.ObjectModel;

namespace AutoReservation.GUI.ViewModels
{
    public abstract class OverviewTemplate<T> : BasisViewModel
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ModifyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public OverviewTemplate()
        {
            AddCommand = new RelayCommand(() => this.Add(), () => this.CanAdd());
            ModifyCommand = new RelayCommand(() => this.Modify(), () => this.CanModify());
            DeleteCommand = new RelayCommand(() => this.Delete(), () => this.CanDelete());
        }

        protected ObservableCollection<T> _list;
        public ObservableCollection<T> List
        {
            get { return _list; }
        }

        private T _selected;
        public T Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                OnPropertyChanged(nameof(Selected));
                ModifyCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }

        public abstract void Add();
        public abstract bool CanAdd();

        public abstract void Modify();
        public abstract bool CanModify();

        public abstract void Delete();
        public abstract bool CanDelete();
    }
}
