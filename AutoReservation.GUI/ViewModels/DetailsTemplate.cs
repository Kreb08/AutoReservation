using AutoReservation.Common.DataTransferObjects;
using AutoReservation.Common.Interfaces;
using AutoReservation.GUI.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public abstract class DetailsTemplate<T> : BasisViewModel
    {
        public RelayCommand CommitCommand { get; set; }
        public RelayCommand CancleCommand { get; set; }
        public T Current { get; set; }
        protected Window Window { get; set; }

        public DetailsTemplate(Window window, T current)
        {
            Window = window;
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
