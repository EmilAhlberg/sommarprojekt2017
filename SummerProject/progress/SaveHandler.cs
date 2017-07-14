using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace SummerProject.achievements
{
    public class SaveHandler
    {
        public static void Save(SaveData data, string fileName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                XmlSerializer serializer = new XmlSerializer(data.GetType());
                using (MemoryStream stream = new MemoryStream())
                {
                    serializer.Serialize(stream, data);
                    stream.Position = 0;
                    doc.Load(stream);
                    doc.Save(fileName);
                    stream.Close();
                }
            }
            catch (Exception e)
            {
                throw new NotImplementedException();
            }
        }

        public static T Load<T>(string fileName)
        {
            T objectOut = default(T);
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(fileName);
                string xmlString = doc.OuterXml;
                using (StringReader read = new StringReader(xmlString))
                {
                    Type outType = typeof(T);

                    XmlSerializer serializer = new XmlSerializer(outType);
                    using (XmlReader reader = new XmlTextReader(read))
                    {
                        objectOut = (T)serializer.Deserialize(reader);
                        reader.Close();
                    }
                    read.Close();
                }
            }
            catch (Exception e)
            {
                //throw new NotImplementedException();
            }
            return objectOut;
        }

        public static void InitializeGame(AchievementController ac)
        {
            SaveData data = SaveHandler.Load<SaveData>("save_file");
            // what if save files dont exists :: this if is false?                                                           
            if (data != null) //! no previous save file --> no progress in game
                for (int i = 0; i < Traits.UNLOCKABLES_CONSTANT; i++)
                {
                    if (data.unlocks[i]) // sub 2 is unlockables, other achievements are xBox bloat
                        ac.Achievements[i].AlreadyUnlocked();
                }
        }
    }
}
