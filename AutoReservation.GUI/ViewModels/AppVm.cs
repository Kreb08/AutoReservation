using System.Windows;

namespace AutoReservation.GUI.ViewModels
{
    public class AppVm
    {

        public AutoViewModel AutoVm { get; }
        public KundeViewModel KundeVm { get; }
        public ReservationViewModel ReservationVm { get; }

        public AppVm(Window mainWindow)
        {
            AutoVm = new AutoViewModel(mainWindow);
            KundeVm = new KundeViewModel(mainWindow);
            ReservationVm = new ReservationViewModel(mainWindow);
        }
    }
}
