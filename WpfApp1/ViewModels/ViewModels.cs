using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintingHouseApplication.ViewModels
{
    class ViewModels : BaseViewModel
    {
        public EmployeeViewModel EmployeeVM { get; } = new EmployeeViewModel();
        public DepartmentViewModel DepartmentVM { get; } = new DepartmentViewModel();       
        public ProfileViewModel ProfileVM { get; } = new ProfileViewModel();
        public TypeProdViewModel TypeProdVM { get; } = new TypeProdViewModel();
        public ProductViewModel ProductVM { get; } = new ProductViewModel();
        public ContractViewModel ContractVM { get; } = new ContractViewModel();
        public UserViewModel UserVM { get; } = new UserViewModel();
    }
}
