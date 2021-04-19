using System.IO;
using System.Runtime.Serialization;
using System.Windows;
using System.Xml.Serialization;
using TreeWarehouse.Model;

namespace TreeWarehouse.ViewModel.Utilities
{
    public static class SaveLoadWarehouse
    {
        /// <summary>
        /// Save chosen warehouse into path. Saves using XML. Exceptions are possible.
        /// </summary>
        /// <param name="warehouse"> Warehouse to save. </param>
        /// <param name="path"> Destination for Warehouse XML file. </param>
        public static void Save(this Warehouse warehouse, string path)
        {
            var serializer = new DataContractSerializer(warehouse.GetType()); 
            
            using (FileStream stream = new FileStream(path, FileMode.Create)) 
            { 
                serializer.WriteObject(stream, warehouse); 
            } 
        }

        /// <summary>
        /// Load warehouse from path. Load XML format. Exceptions are possible.
        /// </summary>
        /// <param name="path"> Destination of Warehouse XML file. </param>
        /// <returns> Loaded warehouse. </returns>
        public static Warehouse Load(string path) {
            var serializer = new DataContractSerializer(typeof(Warehouse));
            using (FileStream stream = new FileStream(path, FileMode.Open)) {
                return (Warehouse)serializer.ReadObject(stream);
            }
        }
    }
}