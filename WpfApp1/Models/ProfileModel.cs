using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace PrintingHouseApplication.Models
{
    class ProfileModel
    {
        public static ImageSource GetImage()
        {
            byte[] imageData = UserHelper.GetImageData();
            ImageSource image = (ImageSource)new ImageSourceConverter().ConvertFrom(imageData);
            return image;
        }
    }
}
