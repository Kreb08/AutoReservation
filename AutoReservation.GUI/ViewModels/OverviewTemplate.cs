using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace AutoReservation.GUI.ViewModels
{
    public abstract class OverviewTemplate<T> : BasisViewModel where T: BasisDto 
    {
        public RelayCommand AddCommand { get; set; }
        public RelayCommand ModifyCommand { get; set; }
        public RelayCommand DeleteCommand { get; set; }

        public Window mainWindow { get; }
        private bool updating;

        public OverviewTemplate(Window mainWindow)
        {
            AddCommand = new RelayCommand(() => this.Add(), () => this.CanAdd());
            ModifyCommand = new RelayCommand(() => this.Modify(), () => this.CanModify());
            DeleteCommand = new RelayCommand(() => this.Delete(), () => this.CanDelete());
            mainWindow = this.mainWindow;

            UpdateList();
        }

        public void UpdateList()
        {
            if (updating)
            {
                return;
            }
            Task.Run(() =>
            {
                updating = true;
                List<T> updatedList = GetList();
                Application.Current.Dispatcher.Invoke(() =>
                {
                    for (int i = 0; i < List.Count; i++)
                    {
                        T oldItem = List[i];
                        T updated = updatedList.Find(o => o.getId() == oldItem.getId());
                        if (updated != null)
                        {
                            List[i] = updated;
                            updatedList.Remove(updated);
                        }
                        else
                        {
                            List.RemoveAt(i);
                        }
                    }
                    updatedList.ForEach(List.Add);
                });
                updating = false;
            });
        }
        public ObservableCollection<T> List { get; } = new ObservableCollection<T>();

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

        protected abstract List<T> GetList();
    }
}
