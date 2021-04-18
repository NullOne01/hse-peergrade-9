using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using TreeWarehouse.Model;

namespace TreeWarehouse.View.Converters
{
    class TreeSortConverter : IMultiValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            System.Collections.IList collection = value as System.Collections.IList;
            ListCollectionView view = new ListCollectionView(collection);
            Folder paramFolder = (Folder)parameter;
            SortDescription sort = new SortDescription((paramFolder.Name), ListSortDirection.Ascending);
            view.SortDescriptions.Add(sort);

            return view;
        }

        /*public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            System.Collections.IList collection = values[0] as System.Collections.IList;
            ListCollectionView view = new ListCollectionView(collection);
            Folder paramFolder = (Folder)parameter;
            SortDescription sort = new SortDescription((paramFolder.Name), ListSortDirection.Ascending);
            view.SortDescriptions.Add(sort);

            return view;
        }*/

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var Length = values.Length;
            if (Length >= 1 && Length < 3) {
                var IList = values[0] as IList;

                if (IList.Count <= 0)
                    return null;

                var SortName = string.Empty;
                if (Length > 1)
                    SortName = values[1].ToString();

                var SortDirection = ListSortDirection.Ascending;
                if (Length > 2)
                    SortDirection = values[2] is ListSortDirection ? (ListSortDirection)values[2] : (values[2] is string ? (ListSortDirection)Enum.Parse(typeof(ListSortDirection), values[2].ToString()) : SortDirection);

                var Result = new ListCollectionView(IList);
                Result.SortDescriptions.Add(new SortDescription(SortName, SortDirection));
                return Result;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) {
            return null;
        }
    }
}
