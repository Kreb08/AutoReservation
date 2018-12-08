﻿using AutoReservation.Common.DataTransferObjects;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoViewModel : OverviewTemplate<AutoDto>
    {
        public AutoViewModel()
        {
            _list = new ObservableCollection<AutoDto>(target.Autos);
        }

        public override void Add()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails);
            AutoDetails.Owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(w => w.IsActive);
            AutoDetails.Show();
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void Modify()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails, Selected);
            AutoDetails.Show();
        }

        public override bool CanModify()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }

        public override void Delete()
        {
            target.DeleteAuto(Selected);
        }

        public override bool CanDelete()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }
    }
}
