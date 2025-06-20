using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GolfApp.ViewModels
{
    public partial class ManageViewModel : ObservableObject
    {
        [RelayCommand]
        private async Task AddGolfCourseAsync()
        {
            await Shell.Current.GoToAsync("AddGolfCourse");
        }
    }
}
