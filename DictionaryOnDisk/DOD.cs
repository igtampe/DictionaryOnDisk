using System;
using System.IO;
using System.Collections.Generic;

namespace Igtampe.DictionaryOnDisk{
    
    /// <summary>Dictionary On Disk utilities</summary>
    public static class DOD{

        /// <summary>Loads a Dictionary from disk</summary>
        /// <param name="Filename">Filename of the DOD to load</param>
        /// <returns>The loaded DOD (A string,string dictionary)</returns>
        public static Dictionary<string,string> Load(string Filename) {return Parse(File.ReadAllLines(Filename));}

        /// <summary>Parses an array of lines that formatted like a DOD. Used by the load function, and made public in case a DOD has to be loaded from somewhere other than disk.</summary>
        /// <param name="Lines">Lines of a DOD</param>
        /// <returns>The loaded DOD (A string,string Dictionary)</returns>
        public static Dictionary<string,string> Parse(string[] Lines) {
            Dictionary<string,string> ReturnDictionary = new Dictionary<string,string>();

            //Go through each line.
            for(int i = 0; i < Lines.Length; i++) {
                if(!string.IsNullOrWhiteSpace(Lines[i])) {
                    //Find the first colon
                    int Colon = Lines[i].IndexOf(':');
                    if(Colon == -1) { throw new InvalidOperationException("Malformed DOD entry at line " + (i + 1)); } //Catch if there is no colon.

                    //Find the key:
                    string Key = Lines[i].Substring(0,Colon);
                    string Value;

                    //Find the value.
                    if(Lines[i].Length == Colon+1) {
                        Value = "";
                    } else if(Lines[i][Colon + 1] == '{') {
                        //Multiline value.
                        Value = Lines[i].Substring(Colon + 2);
                        int startI = i;

                        //Keep going down the array until the value ends in '}'
                        while(!Value.EndsWith("}")) {
                            i++;
                            if(i == Lines.Length) { throw new IndexOutOfRangeException("Unfinished Multi-line DOD Entry starting at line " + startI); } //make sure we don't go on looking for a } that doesn't exist.
                            Value += "\n" + Lines[i];
                        }

                        //Trim that }
                        Value = Value.TrimEnd('}');

                    } else {
                        Value = Lines[i].Substring(Colon + 1);
                    }

                    //Add the Key Value pair.
                    ReturnDictionary.Add(Key,Value);
                }
            }

            return ReturnDictionary;
        }

        /// <summary>Handles saving the dictionary to disk.</summary>
        /// <param name="Dict">Dictionary to save</param>
        /// <param name="Filename">Filename to save it to</param>
        public static void Save(Dictionary<string,string> Dict, string Filename) {
            string[] Contents = Prep(Dict); //Prepare this here
            if(File.Exists(Filename)) { File.Delete(Filename); } //delete *AFTER* prep in case something goes wrong during the prep.
            File.WriteAllLines(Filename,Contents); //actually save it.
        }

        /// <summary>Prepares the dictionary for saving, in case the DOD has to be saved somewhere other than disk.</summary>
        /// <param name="Dict">Dictionary to convert to a DOD</param>
        /// <returns>A DOD Array ready to be saved somewhere.</returns>
        public static string[] Prep(Dictionary<string,string> Dict) {
            List<String> ReturnList = new List<String>();
            foreach(KeyValuePair<string,string> Entry in Dict) {
                if(Entry.Key.Contains("\n")) { throw new InvalidOperationException("Key " + Entry.Key + " with value " + Entry.Value + " has a line break. It cannot be saved"); }
                string DODEntry = Entry.Key + ":";
                if(Entry.Value?.Contains("\n")==true) { DODEntry += "{" + Entry.Value + "}"; } else { DODEntry += Entry.Value; }
                ReturnList.Add(DODEntry);
            }

            return ReturnList.ToArray();
        }



    }
}
