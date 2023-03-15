using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;

namespace PrintingHouseApplication.ViewModels
{
    internal class AddTypeProdViewModel : BaseViewModel
    {
		private int _typeId;
        private string _typeName;

        public int TypeId
		{
			get => _typeId;
			set 
			{ 
				_typeId = value;
				OnPropertyChanged(nameof(TypeId));
			}
		}
	
	
		public string TypeName
		{
			get => _typeName;
			set 
			{ 
				_typeName = value;
				OnPropertyChanged(nameof(TypeName));
			}
		}

		public Command Add { get; private set; }
		public Command Cancel { get; private set; }

		public AddTypeProdViewModel()
		{
			Add = new Command((_) =>
			{
				if (AddTypeProdModel.CreateTypeProd(TypeId, TypeName))
				{
					MessageBox.Show("Добавление успешно выполнено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Information);
					Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
				}
				else
				{
                    MessageBox.Show("Ошибка добавления.\nДобавление отменено", "Добавление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
			}, null);

			Cancel = new Command((_) =>
			{
                Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
            }, null);
		}


	}
}
