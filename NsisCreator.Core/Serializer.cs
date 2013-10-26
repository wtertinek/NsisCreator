using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NsisCreator
{
  public class Serializer
  {
    public static void Save(object item, string fileName)
    {
      using (var file = System.IO.File.OpenWrite(fileName))
      {
        var serializer = new XmlSerializer(item.GetType());
        serializer.Serialize(file, item);
      }
    }

    public static T Load<T>(string fileName)
    {
      using (var file = System.IO.File.OpenRead(fileName))
      {
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(file);
      }
    }
  }
}
