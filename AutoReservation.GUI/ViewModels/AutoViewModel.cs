using AutoReservation.Common.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class AutoViewModel : OverviewTemplate<AutoDto>
    {
        public AutoViewModel(Window mainWindow) : base(mainWindow)
        {
        }

        public override void Add()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails, this);
            AutoDetails.Owner = mainWindow;
            AutoDetails.Show();
        }

        public override bool CanAdd()
        {
            return true;
        }

        public override void Modify()
        {
            Window AutoDetails = new AutoDetailsWindow();
            AutoDetails.DataContext = new AutoDetailsVM(AutoDetails, this, Selected);
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
            if (MessageBox.Show("Reservation wirklich löschen?", "Bestätigen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return;
            }
            try
            {
                target.DeleteAuto(Selected);
            } catch (FaultException e)
            {
                MessageBox.Show(
                    "Löschen Fehlgeschlagen.\n\n" +
                    e.Message,
                    "Fehler beim Löschen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Löschen",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            UpdateList();
        }

        public override bool CanDelete()
        {
            if (Selected == null)
            {
                return false;
            }
            return true;
        }

        protected override List<AutoDto> GetList()
        {
            List<AutoDto> list;
            try
            {
                list = target.Autos;
                return list;
            }
            catch (FaultException e)
            {
                MessageBox.Show(
                    "Liste konnte nicht aktualisiert werden.\n\n" +
                    e.Message,
                    "Fehler beim Aktualisieren",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    "Ein Fehler ist beim aktualisieren aufgetreten.\n\n" +
                    e.Message,
                    "Fehler beim Aktualisieren",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
            }
            return new List<AutoDto>();
        }
    }
}
