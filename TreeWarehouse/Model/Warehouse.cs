using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.Model.Report;

namespace TreeWarehouse.Model
{
    [DataContract(IsReference=true)]
    public class Warehouse : INotifyPropertyChanged
    {
        private Folder _rootFolder = new Folder();

        [DataMember]
        public Folder RootFolder
        {
            get => _rootFolder;
            set
            {
                _rootFolder = value;
                OnPropertyChanged(nameof(RootFolder));
            }
        }

        /// <summary>
        /// Deletes folder without checking conditions. 
        /// </summary>
        /// <param name="folderToRemove">Folder which will be removed. </param>
        /// <returns> True if folder was removed. </returns>
        public bool ForceDeleteFolder(Folder folderToRemove)
        {
            if (folderToRemove.Parent != null)
            {
                return folderToRemove.Parent.SubFolders.Remove(folderToRemove);
            }

            // If folder is in root (no parent), then delete from the root.
            return RootFolder.SubFolders.Remove(folderToRemove);
        }

        /// <summary>
        /// Get repost list using BFS algorithm.
        /// </summary>
        /// <returns> Report list. </returns>
        public List<ReportCSV> GetReportList() {
            List<ReportCSV> resultList = new List<ReportCSV>();

            var bfsQueue = new Queue<Folder>();
            bfsQueue.Enqueue(RootFolder);

            while (bfsQueue.Count > 0) {
                Folder newFolder = bfsQueue.Dequeue();
                string folderPath = newFolder.GetPath();

                foreach (var product in newFolder.Products) {
                    if (!product.IsStockEnough) {
                        ReportCSV newReportLine = new ReportCSV(folderPath, product);
                        resultList.Add(newReportLine);
                    }
                }

                foreach (var newSubFolder in newFolder.SubFolders) {
                    bfsQueue.Enqueue(newSubFolder);
                }
            }

            return resultList;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}