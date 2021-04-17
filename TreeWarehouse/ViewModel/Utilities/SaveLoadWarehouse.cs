using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml.Serialization;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel.Utilities
{
    public static class SaveLoadWarehouse
    {
        public static void Save(this Warehouse warehouse, string path)
        {
            var serializer = new DataContractSerializer(warehouse.GetType()); 
            
            using (FileStream stream = new FileStream(path, FileMode.Create)) 
            { 
                serializer.WriteObject(stream, warehouse); 
            } 
        }

        public static Warehouse Load(string path) {
            var serializer = new DataContractSerializer(typeof(Warehouse));
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                return (Warehouse)serializer.ReadObject(stream);
            }
        }
    }
}