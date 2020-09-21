using System.Collections.Generic;
using Igtampe.DictionaryOnDisk;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Igtampe.DictionaryOnDiskTest {
    [TestClass]
    public class DODTests {

        public string[] DOD1 = { "foo:bar" };
        public string[] DOD2 = { 
            "Yee:Haw",
            "Coincidence:Probably not",
            "Mystery:Maybe",
            "Hotel:Trivago" };
        public string[] DOD3 = {
            "Name:Chopo",
            "Experience:{Existence",
            "Life",
            "Coding}",
            "Cool:Beans",
            "Rules:{No Fighting",
            "No Running}",
            "beep:boop"};

        [TestMethod]
        public void TestSingleKey() {
            Dictionary<string,string> Dict1 = DOD.Parse(DOD1);
            Assert.AreEqual("bar",Dict1["foo"]);
        }

        [TestMethod]
        public void TestMultiKey() {
            Dictionary<string,string> Dict2 = DOD.Parse(DOD2);
            Assert.AreEqual("Probably not",Dict2["Coincidence"]);
            Assert.AreEqual("Maybe",Dict2["Mystery"]);
            Assert.AreEqual("Trivago",Dict2["Hotel"]);
        }

        [TestMethod]
        public void TestMultiLineKey() {
            Dictionary<string,string> Dict3 = DOD.Parse(DOD3);
            Assert.AreEqual("Chopo",Dict3["Name"]);
            Assert.AreEqual("Existence\nLife\nCoding",Dict3["Experience"]);
            Assert.AreEqual("Beans",Dict3["Cool"]);
            Assert.AreEqual("No Fighting\nNo Running",Dict3["Rules"]);
            Assert.AreEqual("boop",Dict3["beep"]);
        }


        [TestMethod]
        public void TestSaveSingleLine() {
            string[] PreppedDOD = DOD.Prep(DOD.Parse(DOD2));
            for(int i = 0; i < DOD2.Length; i++) { Assert.AreEqual(DOD2[i],PreppedDOD[i]); }
        }

        [TestMethod]
        public void TestSaveMultiLine() {
            string[] PreppedDOD = DOD.Prep(DOD.Parse(DOD3));

            //DOD.PREP has \n. In order for this simulation to go through, it must be written.
            PreppedDOD = string.Join("\n",PreppedDOD).Split('\n');
            
            for(int i = 0; i < DOD3.Length; i++) { Assert.AreEqual(DOD3[i],PreppedDOD[i]); }
        }

    }
}
