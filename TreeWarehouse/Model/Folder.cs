using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using TreeWarehouse.ViewModel.Utilities;

namespace TreeWarehouse.Model {
    [DataContract(IsReference = true)]
    public class Folder : INotifyPropertyChanged {
        private ObservableCollection<Product> _products = new ObservableCollection<Product>();
        private ObservableCollection<Folder> _subFolders = new ObservableCollection<Folder>();
        private string _name;
        private Folder _parent;
        private int _priority;

        public Folder(Folder parent) {
            Parent = parent;
            OnPropertyChanged(nameof(SubProducts));
        }

        // Empty constructor for serialization.
        public Folder() {
            Parent = null;
            OnPropertyChanged(nameof(SubProducts));
        }

        [DataMember]
        public Folder Parent {
            get => _parent;
            set {
                _parent = value;
                OnPropertyChanged(nameof(Parent));
            }
        }

        [DataMember]
        public ObservableCollection<Product> Products {
            get => _products;
            set {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        [DataMember]
        public ObservableCollection<Folder> SubFolders {
            get => _subFolders;
            set {
                _subFolders = value;
                OnPropertyChanged(nameof(SubFolders));
                // Update SubProducts property if SubFolders were changed.
                OnPropertyChanged(nameof(SubProducts));
            }
        }

        [DataMember]
        public string Name {
            get => _name;
            set {
                // Recursive adding character "1" until Name is unique in the parent folder.
                if (Parent != null && Parent.SubFolders.Any((folder) => folder.Name == value)) {
                    Name = value + "1";
                    OnPropertyChanged(nameof(Name));
                    return;
                }

                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        [DataMember]
        public int Priority {
            get => _priority;
            set {
                _priority = value;
                OnPropertyChanged(nameof(Priority));
            }
        }

        /// <summary>
        /// Get collection of products which are contained in SubFolders.
        /// </summary>
        public ObservableCollection<Product> SubProducts {
            get {
                ObservableCollection<Product> resProducts = new ObservableCollection<Product>();
                // Using BFS.
                var bfsQueue = new Queue<Folder>();
                bfsQueue.Enqueue(this);

                while (bfsQueue.Count > 0) {
                    Folder newFolder = bfsQueue.Dequeue();
                    if (newFolder != this) {
                        foreach (var newProduct in newFolder.Products) {
                            resProducts.Add(newProduct);
                        }
                    }

                    foreach (var newSubFolder in newFolder.SubFolders) {
                        bfsQueue.Enqueue(newSubFolder);
                    }
                }

                return resProducts;
            }
        }

        /// <summary>
        /// Generates Folder with random property values.
        /// </summary>
        /// <param name="random"> Random object. </param>
        /// <param name="parent"> Parent to assign to. </param>
        /// <returns> Randomized folder. </returns>
        public static Folder CreateRandomFolder(Random random, Folder parent) {
            return new Folder(parent)
            {
                Name = random.RandomString(5),
                Priority = random.Next(-5, 6)
            };
        }

        /// <summary>
        /// Get full path to the folder with slash separators.
        /// </summary>
        /// <returns> Full path. </returns>
        public string GetPath() {
            string path = Name;
            Folder nextParent = Parent;
            // Until root folder is found.
            while (nextParent != null && nextParent.Parent != null) {
                path = nextParent.Name + "/" + path;
                nextParent = nextParent.Parent;
            }

            return path;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}