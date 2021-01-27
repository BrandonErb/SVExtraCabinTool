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
    public class AddSlots
    {
        public List<GameSave> gameSaves;
        readonly XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";

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
                //Read basic save info
                XElement xmlGameInfo = XElement.Load(saveGameInfo);
                string farmName = xmlGameInfo.Element("farmName").Value;
                string name = xmlGameInfo.Element("name").Value;

                //add info to struct
                gameSave.farmName = farmName;
                gameSave.playerName = name;
                gameSave.saveFile = d.Name;


                //need to get existing number of cabins
                string saveFilePath = saveLocation + "\\" + d.Name + "\\" + d.Name;
                gameSave.filePath = saveFilePath;
                string saveFile = files.FirstOrDefault(x => x == saveFilePath);
                XElement xmlGameSave = XElement.Load(saveFile);
                int cabinNum = 0;

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

        /// <summary>Adds the desired number of cabins</summary>
        /// <para></para>
        /// <returns></returns>
        public string AddSlotsToFile(GameSave saveFile, int numSlots)
        {
            string[] farmhandTemplates = { "Farmhand1Template.xml", "Farmhand2Template.xml", "Farmhand3Template.xml", "Farmhand4Template.xml", "Farmhand5Template.xml", "Farmhand6Template.xml" };
            int templateNum = 0;
            //Check to see if maximum cabins have been added
            if (numSlots + saveFile.cabinNum > 6)
            {
                return "Cannot Add That Many Cabins, Total Would Go Over Maximum";
            }

            switch (saveFile.cabinNum)
            {
                case 0:
                    templateNum = 0;
                    break;
                case 1:
                    templateNum = 1;
                    break;
                case 2:
                    templateNum = 2;
                    break;
                case 3:
                    templateNum = 3;
                    break;
                case 4:
                    templateNum = 4;
                    break;
                case 5:
                    templateNum = 5;
                    break;
                default:
                    return "Max Cabin Reached";
            }

            //backup anyfile that is going to be edited.
            Int64 timestamp = CreateTimestamp();
            string copyFile = saveFile.filePath + "_" + timestamp.ToString() + ".backup";
            System.IO.File.Copy(saveFile.filePath, copyFile, true);

            /// Paths:
            /// Building/indoors/uniqueName
            /// Building/indoors/farmhand/UniqueMultiplayerID
            /// SaveGame / locations / GameLocation / buildings
            try
            {
                //add unique identifyers to xml streams
                string templatePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\SlotFiles\\";
                XDocument xmlDoc = XDocument.Load(templatePath + farmhandTemplates[templateNum]);
                var uniqueName = xmlDoc.Element("Building").Element("indoors").Element("uniqueName");
                uniqueName.Value = GenerateCabinNameID();

                var uniqueMpID = xmlDoc.Element("Building").Element("indoors").Element("farmhand").Element("UniqueMultiplayerID");
                uniqueMpID.Value = GenerateUniqueMultiplayerID();

                //insert new cabin
                XDocument xmlSaveFile = XDocument.Load(saveFile.filePath);

                XElement farmE =
                     (from el in xmlSaveFile.Descendants("locations").Elements("GameLocation")
                      where (string)el.Attribute(ns + "type") == "Farm"
                      select el).FirstOrDefault();

                XElement xmlBuilding = XElement.Parse(xmlDoc.ToString());

                farmE.Element("buildings").Add(xmlBuilding);

                xmlSaveFile.Save(saveFile.filePath);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                return "Failed to Add Slot(s)";
            }

            return "Succesfully Added Slot(s)";

        }

        /// <summary>Gets a number that can be used to sequetially determain which file was modified first</summary>
        /// <para></para>
        /// <returns>The number of ms since 01/01/1970</returns>
        public Int64 CreateTimestamp()
        {
            return Math.Abs((Int64)DateTime.Now.Ticks);
        }


        /// <summary>Generates a Int64 number for a Cabin ID</summary>
        /// <para></para>
        /// <returns>String for the UniqueMultiplayer element</returns>
        public string GenerateUniqueMultiplayerID()
        {
            long min = 1000000000000000000;
            long max = 9223372036854775807; //top limit of unsigned 64 bit int
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
