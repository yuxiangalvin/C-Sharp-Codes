using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assignment_3;

namespace Assignment_3_Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Add_Contain_Test()
        {
            Trie testtrie = new Trie("C:\\users\\yuxiangc\\source\\repos\\dictionary.txt");
            testtrie.Add("Ab");
            Assert.IsTrue(testtrie.Contains("Ab"));
            Assert.IsTrue(testtrie.Contains("abandon"));
            Assert.IsTrue(testtrie.Contains("Abba"));
            Assert.IsFalse(testtrie.Contains("Adfgas"));
            testtrie.Add("Alvin");
            Assert.IsTrue(testtrie.Contains("Alvin"));
            testtrie.Add("Adfgas");
            Assert.IsTrue(testtrie.Contains("Adfgas"));
            testtrie.Add("abandon");
            Assert.IsTrue(testtrie.Contains("abandon"));
            Assert.IsTrue(testtrie.Contains("assess"));
            Assert.IsTrue(testtrie.Contains("assessment"));
            Assert.IsTrue(testtrie.Contains("Albert"));
            Assert.IsTrue(testtrie.Contains("Alberta"));
            Assert.IsTrue(testtrie.Contains("Alberto"));
            string[] allstringarray = System.IO.File.ReadAllLines("C:\\users\\yuxiangc\\source\\repos\\dictionary.txt");
            foreach (string word in allstringarray)
            {
                Assert.IsTrue(testtrie.Contains(word));
            }
        }
    }
}
