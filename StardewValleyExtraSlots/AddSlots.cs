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

            List<GameSave> gameSaves = new List<GameSave>();

            //string[] array1 = Directory.GetFiles(saveLocation);

            // Get list of files.
            foreach (DirectoryInfo d in di)
            {
                GameSave gameSave;
                Debug.WriteLine("Directory names {0}", d.Name);
                List<string> files = new List<string>();
                files = Directory.GetFiles(saveLocation + "\\" + d.Name).ToList();
                files.Remove(saveLocation +"\\SaveGameInfo");
                //IEnumerable<string> player = files[0].Descendants("Item").Select(x => (string)x.Attribute("PartNumber"));

                gameSave.saveFile = files[0];
            }
            //backup anyfile that is going to be edited.

        }
    }
}
