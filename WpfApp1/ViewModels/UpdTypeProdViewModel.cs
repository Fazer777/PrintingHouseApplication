using System.Windows;
using System.Linq;
using PrintingHouseApplication.MyCommand;
using PrintingHouseApplication.Models;
using PrintingHouseApplication.Views;


namespace PrintingHouseApplication.ViewModels
{
    internal class UpdTypeProdViewModel : BaseViewModel
    {
		private int _typId;
		private string _typeName;

		public string TypeName
		{
			get => _typeName;
			set 
			{ 
				_typeName = value;
				OnPropertyChanged(nameof(TypeName));
			}
		}


		public int TypeId
		{
			get => _typId;
			set 
			{ 
				_typId = value;
				OnPropertyChanged(nameof(TypeId));
			}
		}

		public Command Update { get; private set; }

		public Command Cancel { get; private set; }

		public UpdTypeProdViewModel()
		{
			TypeId = TypesProdModel.SelectedTypeProd.Id;
			TypeName = TypesProdModel.SelectedTypeProd.Name;

			Update = new Command((_) =>
			{
				if (UpdTypeProdModel.UpdateTypeProd(TypeId, TypeName))
				{
					MessageBox.Show("Изменение успешно выполнено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Information);
					Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
				}
				else
				{
                    MessageBox.Show("Ошибка изменения.\nИзменение отменено", "Изменение", MessageBoxButton.OK, MessageBoxImage.Error);
                }
			}, null);

			Cancel = new Command((_) =>
			{
				Application.Current.Windows.OfType<ActionView>().SingleOrDefault(x => x.IsActive).Close();
			}, null);
		}
		

	}
}
