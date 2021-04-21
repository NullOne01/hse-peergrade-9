using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight.CommandWpf;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel {
    public partial class RandomViewModel {
        private RelayCommand _randomizeWarehouseCommand;

        /// <summary>
        /// Command to randomize new Warehouse.
        /// </summary>
        public RelayCommand RandomizeWarehouseCommand {
            get {
                _randomizeWarehouseCommand = new RelayCommand(RandomizeWarehouse);
                return _randomizeWarehouseCommand;
            }
        }

        /// <summary>
        /// Generating new Warehouse using ProductsNum and FoldersNum properties.
        /// </summary>
        private void RandomizeWarehouse() {
            MainViewModel.CurrentWarehouse = new Warehouse();

            Random random = new Random();
            List<Folder> foldersToAdd = new List<Folder>();
            foldersToAdd.Add(MainViewModel.CurrentWarehouse.RootFolder);

            // Generating random folders.
            for (int i = 0; i < FoldersNum; i++) {
                int folderForChild = random.Next(0, foldersToAdd.Count);
                Folder newFolder = Folder.CreateRandomFolder(random, foldersToAdd[folderForChild]);
                foldersToAdd[folderForChild].SubFolders.Add(newFolder);
                foldersToAdd.Add(newFolder);
            }

            // Filling random folders with random products.
            uint productsNumToAdd = ProductsNum;
            while (productsNumToAdd > 0) {
                // randomFolder without rootFolder.
                int randomFolderNum = random.Next(1, foldersToAdd.Count);
                int productsNumForFolder = random.Next(0, (int) productsNumToAdd + 1);
                productsNumToAdd -= (uint) productsNumForFolder;

                for (int i = 0; i < productsNumForFolder; i++) {
                    Product newProduct = Product.CreateRandomProduct(random);
                    foldersToAdd[randomFolderNum].Products.Add(newProduct);
                }
            }
        }
    }
}