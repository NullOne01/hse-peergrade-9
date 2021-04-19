using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using TreeWarehouse.Model;

namespace TreeWarehouse.View.Converters
{
    /// <summary>
    /// Converter for auto-sorting treeView by Priority and Name properties.
    /// </summary>
    class PriorityTreeSortConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            System.Collections.IList collection = value as System.Collections.IList;
            ListCollectionView view = new ListCollectionView(collection);
            view.IsLiveSorting = true;

            view.SortDescriptions.Add(new SortDescription("Priority", ListSortDirection.Descending));
            view.SortDescriptions.Add(new SortDescription("Name", ListSortDirection.Ascending));

            return view;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
