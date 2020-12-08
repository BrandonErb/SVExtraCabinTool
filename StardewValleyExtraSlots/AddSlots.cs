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

namespace StardewValleyExtraSlots
{
    class AddSlots
    {
        private List<GameSave> gameSaves;

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

            //List<GameSave> gameSaves = new List<GameSave>();

            //string[] array1 = Directory.GetFiles(saveLocation);

            // Get list of files.
            foreach (DirectoryInfo d in di)
            {
                GameSave gameSave;
                Debug.WriteLine("Directory name {0}", d.Name);
                List<string> files = new List<string>();
                files = Directory.GetFiles(saveLocation + "\\" + d.Name).ToList();
                string fileLocation = saveLocation + "\\" + d.Name + "\\SaveGameInfo";
                var saveGameInfo = files.FirstOrDefault(x => x == fileLocation);
                XElement xmlGameInfo = XElement.Load(saveGameInfo);
                string farmName = xmlGameInfo.Element("farmName").Value;
                string name = xmlGameInfo.Element("name").Value;

                gameSave.farmName = farmName.ToString();
                gameSave.playerName = name.ToString();
                gameSave.saveFile = d.Name;
                gameSaves.Add(gameSave);
                //need to get existing cabin number
            }
        }

        /// <summary>Adds the desired cabin amount</summary>
        /// <para></para>
        /// <returns></returns>
        public void AddSlotsToFile(string file, int numSlots)
        {
            //backup anyfile that is going to be edited.
        }


        /// <summary>Generates a Int64 number for a Cabin ID</summary>
        /// <para></para>
        /// <returns>String for the UniqueMultiplayer element</returns>
        public string GenerateUniqueMultiplayerID()
        {
            Int64 min = 1000000000000000000;
            Int64 max = 9223372036854775807;
            Random rand = new Random((int)DateTime.UtcNow.Ticks);
            
            byte[] buf = new byte[8];
            rand.NextBytes(buf);
            Int64 longRand = BitConverter.ToInt64(buf, 0);

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
