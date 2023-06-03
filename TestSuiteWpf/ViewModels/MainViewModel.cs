using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestSuiteWpf.ViewModels
{
    internal class MainViewModel : ViewModel
    {
        public ViewModel CurrentViewModel { get; }

        public MainViewModel()
        {
            CurrentViewModel = new BlockViewModel();
        }
    }
}
