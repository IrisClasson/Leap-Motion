using Leap;
using Leap_WPF.Model;
using Leap_WPF.Util;
using Leap_WPF.ViewModel;

namespace Leap_WPF
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var vm = new MainViewModel(new Controller(), new CustomLeapListener(), new GameModel());
            DataContext = vm.GameModel;
            Closing += vm.OnClosing;
            mouse.Click += vm.MouseOnClick;
        }
    }
}
