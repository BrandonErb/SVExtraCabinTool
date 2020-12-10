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
        public string AddSlotsToFile(GameSave saveFile, int numSlots)
        {
            string[] farmhandTemplates = { "Farmhand4Template.xml", "Farmhand5Template.xml", "Farmhand6Template.xml" };
            int templateNum = 0;
            //Check to see if maximum cabins have been added
            if (numSlots >= 6)
            {
                return "Max Cabins Reached";
            }

            switch (saveFile.cabinNum)
            {
                case 0:
                    return "Too Few Cabins on Original Save";
                    //break;
                case 1:
                    return "Too Few Cabins on Original Save";
                    //break;
                case 2:
                    return "Too Few Cabins on Original Save";
                    //break;
                case 3:
                    templateNum = 0;
                    break;
                case 4:
                    templateNum = 1;
                    break;
                case 5:
                    templateNum = 2;
                    break;
                case 6:
                    return "Max Cabins Reached";
                    //break;
            }

            //backup anyfile that is going to be edited.
            int timestamp = Math.Abs((int)DateTime.UtcNow.Ticks);
            string copyFile = saveFile.filePath + ".backup_" + timestamp.ToString();
            System.IO.File.Copy(saveFile.filePath, copyFile, true);


            /// Building/indoors/uniqueName
            /// Building/indoors/farmhand/UniqueMultiplayerID
            /// SaveGame / locations / GameLocation / buildings
            try
            {
                //add unique identifyers to xml streams
                //string templatePath = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + "\\SlotFiles\\FarmHand4Template.xml"; //for release
                string templatePath = @".\SlotFiles\"; //for debug
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

                //var location = farmE.Element("buildings");
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
