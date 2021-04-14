using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.IO;

namespace MCCTexturePackPatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!File.Exists("Files\\" + "texturepacks.txt"))
            {
                Console.WriteLine("Unable to find TexturePacks.txt - create one in order to use this program.");
                Console.ReadLine();
                return;
            }

            StreamReader TexturePacks = new StreamReader("Files\\" + "texturepacks.txt");

            string[] TexturePacksList = TexturePacks.ReadToEnd().Split(' ');
            TexturePacks.Close();

            foreach (string s in TexturePacksList)
            {
                switch(s)
                {
                    case "controllertexturepack":
                        ImportTexture("controllertexturepack");
                        break;
                    case "emblemstexturepack":
                        ImportTexture("emblemstexturepack");
                        break;
                    case "globaluitexturepack":
                        ImportTexture("globaluitexturepack");
                        break;
                    case "hoppertexturepack":
                        ImportTexture("hoppertexturepack");
                        break;
                    case "ingamechapterpack":
                        ImportTexture("ingamechapterpack");
                        break;
                    case "largeavatartexturepack":
                        ImportTexture("largeavatartexturepack");
                        break;
                    case "levelstexturepack":
                        ImportTexture("levelstexturepack");
                        break;
                    case "loadingtexturepack":
                        ImportTexture("loadingtexturepack");
                        break;
                    case "mainmenuandcampaigntexturepack":
                        ImportTexture("mainmenuandcampaigntexturepack");
                        break;
                    case "mainmenutexturepack":
                        ImportTexture("mainmenutexturepack");
                        break;
                    case "medalstexturepack":
                        ImportTexture("medalstexturepack");
                        break;
                    case "skullstexturepack":
                        ImportTexture("skullstexturepack");
                        break;
                    case "spmappreviewtexturepack":
                        ImportTexture("spmappreviewtexturepack");
                        break;
                    case "titlescreentexturepack":
                        ImportTexture("titlescreentexturepack");
                        break;
                    default:
                        Console.WriteLine("Invalid texture pack name: " + s);
                        break;
                }
            }

            Console.WriteLine("All texturepacks installed.\nPress any key to exit.");

            Console.ReadLine();
        }


        private static void ImportTexture(string TexturePackName) //currently only works for mainmenutexturepack
        {
            TextureFile textureFile;

            switch (TexturePackName)
            {
                case "controllertexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.controllertexturepack);
                    break;
                case "emblemstexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.emblemstexturepack);
                    break;
                case "globaluitexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.globaluitexturepack);
                    break;
                case "hoppertexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.hoppertexturepack);
                    break;
                case "ingamechapterpack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.ingamechapterpack);
                    break;
                case "largeavatartexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.largeavatartexturepack);
                    break;
                case "levelstexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.levelstexturepack);
                    break;
                case "loadingtexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.loadingtexturepack);
                    break;
                case "mainmenuandcampaigntexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.mainmenuandcampaigntexturepack);
                    break;
                case "mainmenutexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.mainmenutexturepack);
                    break;
                case "medalstexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.medalstexturepack);
                    break;
                case "skullstexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.skullstexturepack);
                    break;
                case "spmappreviewtexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.spmappreviewtexturepack);
                    break;
                /*
                case "titlescreentexturepack":
                    textureFile = new TextureFile(MCCTexturePackPatcher.Properties.Resources.titlescreentexturepack);
                    break;
                */
                default:
                    Console.WriteLine("Couldn't find texture file index");
                    return;
            }

            if (!File.Exists("Files\\" + TexturePackName + "_filelist.txt"))
            {
                Console.WriteLine("Unable to find " + TexturePackName + "_filelist.txt - create one in order to use this program.");
                Console.ReadLine();
                return;
            }
            StreamReader FileList = new StreamReader("Files\\" + TexturePackName + "_filelist.txt");

            string[] FilesInList = FileList.ReadToEnd().Split(' ');
            FileList.Close();

            //List<string> TexturesToImport = new List<string>();
            //List<int> TextureIndexes = new List<int>();
            Dictionary<int, string> TextureDictionary = new Dictionary<int, string>();

            foreach (string s in FilesInList)
            {

                if (textureFile.TextureNames.Exists(e => e == s))
                {
                    TextureDictionary.Add(textureFile.TextureNames.FindIndex(e => e == s), s);
                }
                else
                {
                    Console.WriteLine("Unable to find texture: " + s);
                }

                
            }

            if (TextureDictionary.Count() <= 0)
            {
                Console.WriteLine("No textures found in " + TexturePackName + "_filelist.txt");
                Console.ReadLine();
                return;
            }

            string TexturePackFileName = GetDirectory() + "\\data\\ui\\texturepacks\\" + TexturePackName +".temp.bin"; //to be replaced
            switch (TexturePackName)
            {
                case "loadingtexturepack":
                    TexturePackFileName = TexturePackFileName.Substring(0, TexturePackFileName.Length - 9) + ".perm.bin";
                    break;
                case "hoppertexturepack":
                    TexturePackFileName = TexturePackFileName.Substring(0, TexturePackFileName.Length - 9) + ".perm.bin";
                    break;
                case "ingamechapterpack":
                    TexturePackFileName = TexturePackFileName.Substring(0, TexturePackFileName.Length - 9) + ".perm.bin";
                    break;
                case "largeavatartexturepack":
                    TexturePackFileName = TexturePackFileName.Substring(0, TexturePackFileName.Length - 9) + ".perm.bin";
                    break;
            }
            //Console.WriteLine("Attempting to access: " + TexturePackFileName);
            if (!File.Exists(TexturePackFileName.Substring(0, TexturePackFileName.Length - 4) + ".bak"))
            {
                File.Copy(TexturePackFileName, TexturePackFileName.Substring(0, TexturePackFileName.Length - 4) + ".bak");
                Console.WriteLine("Backup created for " + TexturePackName + " with the .bak file extension.");
            }

            FileStream TexturePackStream = new FileStream(TexturePackFileName, FileMode.Open);
            byte[] TexturePackBytes = new byte[TexturePackStream.Length];
            TexturePackStream.Read(TexturePackBytes, 0, TexturePackBytes.Length);
            TexturePackStream.Close();

            foreach (KeyValuePair<int, string> kvp in TextureDictionary)
            {
                Console.WriteLine("Attempting to write " + kvp.Value);
                FileStream ImageStream = new FileStream("Files\\" + kvp.Value + ".dds", FileMode.Open);
                byte[] ImageBytes = new byte[ImageStream.Length - 128];
                ImageStream.Seek(128, 0);
                ImageStream.Read(ImageBytes, 0, ImageBytes.Length - 128);
                ImageStream.Close();
                int x = Convert.ToInt32(textureFile.TextureOffsets[kvp.Key], 16);
                foreach (byte i in ImageBytes)
                {
                    TexturePackBytes[x] = i;
                    x++;
                }


            }
            File.Delete(TexturePackFileName);
            FileStream ModdedStream = new FileStream(TexturePackFileName, FileMode.Create);
            ModdedStream.Write(TexturePackBytes, 0, TexturePackBytes.Length);
            ModdedStream.Close();
            Console.WriteLine("Installed textures to " + TexturePackName);
        }

        private static string GetDirectory() //thanks jerryurenaa
        {
            string displayName;
            string InstallPath;
            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";

            //64 bits computer
            RegistryKey key64 = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            RegistryKey key = key64.OpenSubKey(registryKey);

            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Equals("Halo: The Master Chief Collection"))
                    {

                        InstallPath = subkey.GetValue("InstallLocation").ToString();

                        return InstallPath; //or displayName

                    }
                }
                key.Close();
            }

            return null;
        }
    }

    class TextureFile
    {
        public List<string> TextureNames = new List<string>();
        public List<string> TextureFormats = new List<string>();
        public List<string> TextureSizes = new List<string>();
        public List<string> TextureOffsets = new List<string>();
        //string Name = "Unnamed Texture Pack";

        public TextureFile(string fn)
        {

            //Name = fn;
            string Index = fn;
            Regex NameRegex = new Regex("(\\w{1,32})(?=\\s+\\w{1,8}\\s+0x\\w{1,10}\\s+0x\\w{1,10})");
            Regex FormatRegex = new Regex("(?<=\\w{1,32}\\s+)(\\w{1,8})(?=\\s+0x\\w{1,10}\\s+0x\\w{1,10})");
            Regex SizeRegex = new Regex("(?<=\\w{1,32}\\s+\\w{1,8}\\s+)(0x\\w{1,10})(?=\\s+0x\\w{1,10})");
            Regex OffsetRegex = new Regex("(?<=\\w{1,32}\\s+\\w{1,8}\\s+0x\\w{1,10}\\s+)(0x\\w{1,10})");

            var Matches = NameRegex.Matches(Index);
            foreach (Match match in Matches)
            {
                TextureNames.Add(match.ToString());
                //Console.WriteLine(match);

            }
            Matches = FormatRegex.Matches(Index);
            foreach (Match match in Matches)
            {
                TextureFormats.Add(match.ToString());
                //Console.WriteLine(match);

            }
            Matches = SizeRegex.Matches(Index);
            foreach (Match match in Matches)
            {
                TextureSizes.Add(match.ToString());
                //Console.WriteLine(match);

            }
            Matches = OffsetRegex.Matches(Index);
            foreach (Match match in Matches)
            {
                TextureOffsets.Add(match.ToString());
                //Console.WriteLine(match);

            }
        }
    }

}