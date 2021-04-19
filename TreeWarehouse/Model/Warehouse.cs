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

        public bool ForceDeleteFolder(Folder folderToRemove)
        {
            if (folderToRemove.Parent != null)
            {
                return folderToRemove.Parent.SubFolders.Remove(folderToRemove);
            }

            // If folder is in root (no parent), then delete from root.
            return RootFolder.SubFolders.Remove(folderToRemove);
        }

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

        /*public bool ForceDeleteFolder(Folder folderToRemove)
        {
            if (Folders.Count <= 0)
                return false;
            var bfsQueue = new Queue<Folder>();
            foreach (var folder in Folders)
            {
                bfsQueue.Enqueue(folder);
                if (folder == folderToRemove)
                {
                    return Folders.Remove(folderToRemove);
                }
            }

            while (bfsQueue.Count > 0)
            {
                Folder newFolder = bfsQueue.Dequeue();
                if (newFolder.SubFolders.Contains(folderToRemove))
                {
                    return newFolder.SubFolders.Remove(folderToRemove);
                }

                foreach (var newSubFolder in newFolder.SubFolders)
                {
                    bfsQueue.Enqueue(newSubFolder);
                }
            }

            return false;
        }*/
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}