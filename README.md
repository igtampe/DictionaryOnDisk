# DictionaryOnDisk
Class library that can handle saving and loading dictionaries (maps) to disk. It can handle values with Line Breaks.

An example DOD is as follows:
```
KEY:VALUE
KEY:VALUE
KEY:{MULTILINE
VALUE}
KEY:VALUE
```

There are four methods available in the class library. These are as follows:
|Method|Result|
|-|-|
|Load()|Loads and returns a Dictionary from a DOD file|
|Parse()|Parses a DOD File's contents. It's made public in case a DOD Has to be loaded from somewhere other than disk|
|Save()|Saves a dictionary as a DOD File|
|Prep()|Creates a DOD File's contents with a given dictionary. It's made public in case a DOD has to be saved somewhere other than disk.|

**Take note!**<br>
Prep returns an array where each index is a line of the file. However, if there is a multi line value, the array will include the whole entire value inside one index, not splitting it into different entries. Use the following line of code to fix it if you must: <br>```PreppedDOD = string.Join("\n",PreppedDOD).Split('\n');```
