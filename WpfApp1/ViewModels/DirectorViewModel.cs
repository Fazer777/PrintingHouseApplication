using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.ViewModels
{
    //TODO Закидывай сюда новые VM
    class DirectorViewModel : BaseViewModel
    {
        public EmployeeViewModel EmployeeVM { get; } = new EmployeeViewModel();
        public DepartmentViewModel DepartmentVM { get; } = new DepartmentViewModel();       
        public ProfileViewModel ProfileVM { get; } = new ProfileViewModel();
        public TypeProdViewModel TypeProdVM { get; } = new TypeProdViewModel();
    }
}
