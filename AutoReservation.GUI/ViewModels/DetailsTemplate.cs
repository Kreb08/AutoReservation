using AutoReservation.Common.DataTransferObjects;
using AutoReservation.GUI.Commands;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public abstract class DetailsTemplate<T> : BasisViewModel where T: BasisDto
    {
        public RelayCommand CommitCommand { get; set; }
        public RelayCommand CancleCommand { get; set; }
        public T Current { get; set; }
        protected Window Window { get; set; }
        protected OverviewTemplate<T> ParentVm;

        public DetailsTemplate(Window window, OverviewTemplate<T> parentVm, T current)
        {
            Window = window;
            ParentVm = parentVm;
            Current = current;

            CommitCommand = new RelayCommand(() => this.Commit(), () => this.CanCommit());
            CancleCommand = new RelayCommand(() => this.Cancle(), () => this.CanCancle());
        }

        public abstract void Commit();
        public abstract bool CanCommit();

        public abstract void Cancle();
        public abstract bool CanCancle();
    }
}
