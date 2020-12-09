using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Collections;
using System.Diagnostics;
using System.Xml;

namespace StardewValleyExtraSlots
{
    class AddSlots
    {
        public List<GameSave> gameSaves;
        
        public AddSlots()
        {
            gameSaves = new List<GameSave>();
        }

        /// <summary>Retrieves information for the game files to be selected and adds it to List</summary>
        /// <para></para>
        /// <returns></returns>
        public void ReadTitleName()
        {
            string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string saveLocation = appDataPath + @"\StardewValley\Saves";
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            List<string> entries = System.IO.Directory.GetDirectories(saveLocation).ToList();
            List<DirectoryInfo> di =  new List<DirectoryInfo>();

            foreach (string e in entries)
            {
                di.Add(new DirectoryInfo(e));
            }              

            // Get list of files.
            foreach (DirectoryInfo d in di)
            {
                GameSave gameSave = new GameSave();
                Debug.WriteLine("Directory name {0}", d.Name);
                List<string> files = new List<string>();
                files = Directory.GetFiles(saveLocation + "\\" + d.Name).ToList();
                string fileLocation = saveLocation + "\\" + d.Name + "\\SaveGameInfo";
                var saveGameInfo = files.FirstOrDefault(x => x == fileLocation);
                XElement xmlGameInfo = XElement.Load(saveGameInfo);
                string farmName = xmlGameInfo.Element("farmName").Value;
                string name = xmlGameInfo.Element("name").Value;

                gameSave.farmName = farmName;
                gameSave.playerName = name;
                gameSave.saveFile = d.Name;


                //need to get existing number of cabins
                string saveFilePath = saveLocation + "\\" + d.Name + "\\" + d.Name;
                gameSave.filePath = saveFilePath;
                string saveFile = files.FirstOrDefault(x => x == saveFilePath);
                XElement xmlGameSave = XElement.Load(saveFile);
                int cabinNum = 0;

                //try
                //{
                //    int numCabin = 0;
                //    numCabin = xmlGameSave.Descendants("Building").Count();
                //    gameSave.cabinNum = numCabin;
                //}
                //catch(Exception e)
                //{

                //}

                //Check Building type for existing cabin count
                /// XML Paths
                /// SaveGame / locations / GameLocation /
                /// SaveGame / locations / GameLocation / buildings / Building

                try
                {
                    IEnumerable<XElement> farmE =
                        from el in xmlGameSave.Descendants("locations").Elements("GameLocation")
                        where (string)el.Attribute(ns + "type") == "Farm"
                        select el;

                    IEnumerable<XElement> cabinE =
                        from el in farmE.Descendants("Building").Elements("indoors")
                        where (string)el.Attribute(ns + "type") == "Cabin"
                        select el;

                    foreach (XElement building in cabinE)
                    {
                        cabinNum += 1;
                    }

                    gameSave.cabinNum = cabinNum;
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }

                gameSaves.Add(gameSave);
            }
        }

        /// <summary>Adds the desired cabin amount</summary>
        /// <para></para>
        /// <returns></returns>
        public void AddSlotsToFile(GameSave saveFile, int numSlots)
        {
            //backup anyfile that is going to be edited.
            int timestamp = (int)DateTime.UtcNow.Ticks * -1;
            string copyFile = saveFile.filePath + ".backup_" + timestamp.ToString();
            System.IO.File.Copy(saveFile.filePath, copyFile, true);

            //add slots text to streams
            string fh4, fh5, fh6;

            using (StreamReader sr = File.OpenText(@"Farmhand4.txt"))
            {
                while ((fh4 = sr.ReadLine()) != null);
            }
            using (StreamReader sr = File.OpenText(@"Farmhand5.txt"))
            {
                while ((fh5 = sr.ReadLine()) != null) ;
            }
            using (StreamReader sr = File.OpenText(@"Farmhand6.txt"))
            {
                while ((fh6 = sr.ReadLine()) != null) ;
            }


            //add to xml
            //XmlDocument doc = new XmlDocument();
            //doc.LoadXml();

            //XmlNode root = doc.DocumentElement;

            ////Create a new node.
            //XmlElement elem = doc.CreateElement("price");
            //elem.InnerText = "19.95";

            ////Add the node to the document.
            //root.AppendChild(elem);
        }


        /// <summary>Generates a Int64 number for a Cabin ID</summary>
        /// <para></para>
        /// <returns>String for the UniqueMultiplayer element</returns>
        public string GenerateUniqueMultiplayerID()
        {
            long min = 1000000000000000000;
            long max = 9223372036854775807;
            Random rand = new Random((int)DateTime.UtcNow.Ticks);
            
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            long longRand = BitConverter.ToInt64(buf, 0);

            return (Math.Abs(longRand % (max - min)) + min).ToString();
        }

        /// <summary>Generates GUID for a Cabin</summary>
        /// <para></para>
        /// <returns>String for the UniqueName element</returns>
        public string GenerateCabinNameID()
        {
            Guid ID = Guid.NewGuid();
            return "Cabin" + ID;
        }
    }
}
