using System.IO;
using System.Xml.Serialization;
using HoMM.GameComponents;

namespace HoMM.Serialization
{
    /// <summary>
    /// Trida pro vykonani savu/loadu do/ze souboru
    /// </summary>
    public class Serializer
    {
        #region Constructors

        public Serializer() { }

        #endregion

        #region Methods

        /// <summary>
        /// Ulozeni hry
        /// </summary>
        /// <param name="gamename"></param>
        /// <param name="objectToSerialize"></param>
        public void SerializeGame(string gamename, GameSerialize objectToSerialize)
        {            
            string filename = Path.Combine(Session.GameRef.Content.RootDirectory, "Saves", gamename);
            FileStream stream = new FileStream(filename, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(GameSerialize));
            serializer.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        /// <summary>
        /// Nacteni hry
        /// </summary>
        /// <param name="gamename"></param>
        /// <returns></returns>
        public GameSerialize DeSerializeGame(string gamename)
        {
            GameSerialize objectToSerialize;
            string filename = Path.Combine(Session.GameRef.Content.RootDirectory, "Saves", gamename);
            FileStream stream = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(GameSerialize));
            objectToSerialize = (GameSerialize)serializer.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }

        /// <summary>
        /// Ulozeni mapy
        /// </summary>
        /// <param name="gamename"></param>
        /// <param name="objectToSerialize"></param>
        public void SerializeMap(string gamename, MapSerialize objectToSerialize)
        {
            string filename = Path.Combine(Session.GameRef.Content.RootDirectory, "Maps", gamename);
            FileStream stream = new FileStream(filename, FileMode.Create);
            XmlSerializer serializer = new XmlSerializer(typeof(MapSerialize));
            serializer.Serialize(stream, objectToSerialize);
            stream.Close();
        }

        /// <summary>
        /// Nacteni mapy
        /// </summary>
        /// <param name="gamename"></param>
        /// <returns></returns>
        public MapSerialize DeSerializeMap(string gamename)
        {
            MapSerialize objectToSerialize;
            string filename = Path.Combine(Session.GameRef.Content.RootDirectory, "Maps", gamename);
            FileStream stream = new FileStream(filename, FileMode.Open);
            XmlSerializer serializer = new XmlSerializer(typeof(MapSerialize));
            objectToSerialize = (MapSerialize)serializer.Deserialize(stream);
            stream.Close();
            return objectToSerialize;
        }

        #endregion
    }
}