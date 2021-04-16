using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel.Utilities
{
    public static class SaveLoadWarehouse
    {
        public static void Save(this Warehouse warehouse, string path)
        {
            var serializer = new DataContractSerializer(warehouse.GetType()); 
            using (FileStream stream = File.Create(path)) 
            { 
                serializer.WriteObject(stream, warehouse); 
            } 
        }
    }
}