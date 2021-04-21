using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TreeWarehouse.Model;
using TreeWarehouse.ViewModel;

namespace TreeWarehouse.View
{
    /// <summary>
    /// Interaction logic for RandomWindow.xaml
    /// </summary>
    public partial class RandomWindow : Window
    {
        public RandomWindow(MainViewModel mainViewModel) {
            InitializeComponent();
            
            DataContext = new RandomViewModel(mainViewModel);
        }
    }
}
