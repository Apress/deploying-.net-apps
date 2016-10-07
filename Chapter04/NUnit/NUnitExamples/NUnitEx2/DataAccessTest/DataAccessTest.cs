using System;
using System.Collections.Generic;
using System.Text;

using System.IO;
using NUnit.Framework;
using System.Data;
using DataAccess;
namespace DataAccessTest
{
    /// <summary>
    /// Class that containe NUnit test cases for the DataAccess assembly.
    /// Author: Sayed Ibrahim Hashimi (sayed.hashimi@gmail.com)
    /// </summary>
    [TestFixture]
    public class DataAccessTest
    {
        private IList<IContact> _contacts;

        [SetUp]
        public void SetUpContacts()
        {
            FileInfo _tempFile = this.WriteFile();
            if (_tempFile == null)
            {
                throw new Exception("Unable to write the Contacts test file");
            }
            DataSet ds = new DataSet();
            ds.ReadXml(_tempFile.FullName);
            this._contacts = Contact.BuildContacts(ds);
            //now we can delete the file
            _tempFile.Delete();
        }
        [TearDown]
        public void TearDown()
        {
            //Nothing to do here just, but if we needed to exeucte some code after each test 
            //we could do that here.
        }

        [Test]
        public void TestFirstName()
        {
            IList<string> expectedNames = new List<string>();
            expectedNames.Add("Sayed");
            expectedNames.Add("Sayed");
            expectedNames.Add("Mike");

            Assert.AreEqual(this._contacts.Count, expectedNames.Count);

            for (int i = 0; i < expectedNames.Count; i++)
            {
                Assert.AreEqual(expectedNames[i], _contacts[i].FirstName);
            }
        }
        [Test]
        public void TestMiddleName()
        {
            IList<string> expectedNames = new List<string>();
            expectedNames.Add("Ibrahim");
            expectedNames.Add("Yahya");
            expectedNames.Add("Ray");

            Assert.AreEqual(this._contacts.Count, expectedNames.Count);


            for (int i = 0; i < expectedNames.Count; i++)
            {
                Assert.AreEqual(expectedNames[i], _contacts[i].MiddleName);
            }
        }
        [Test]
        public void TestLastName()
        {
            IList<string> expectedNames = new List<string>();
            expectedNames.Add("Hashimi");
            expectedNames.Add("Hashimi");
            expectedNames.Add("Murphy");

            Assert.AreEqual(this._contacts.Count, expectedNames.Count);


            for (int i = 0; i < expectedNames.Count; i++)
            {
                Assert.AreEqual(expectedNames[i], _contacts[i].LastName);
            }
        }
        [Test]
        public void TestEmail()
        {
            IList<string> expectedEmail = new List<string>();
            expectedEmail.Add("sayed.hashimi@gmail.com");
            expectedEmail.Add("sayed@sayedhashimi.com");
            expectedEmail.Add("magickmike@gmail.com");

            Assert.AreEqual(this._contacts.Count, expectedEmail.Count);


            for (int i = 0; i < expectedEmail.Count; i++)
            {
                Assert.AreEqual(expectedEmail[i], _contacts[i].Email);
            }
        }
        [Test]
        public void TestWebsite()
        {
            Assert.AreEqual("www.sedodream.com", _contacts[0].Website);
            Assert.AreEqual("www.sayedhashimi.com", _contacts[1].Website);
        }
        [Test]
        public void TestSsn()
        {
            IList<string> expectedSsn = new List<string>();
            expectedSsn.Add("111-11-1111");
            expectedSsn.Add("222-22-2222");
            expectedSsn.Add("333-33-3333");

            //Assert.AreEqual(this._contacts.Count, expectedSsn.Count);


            for (int i = 0; i < expectedSsn.Count; i++)
            {
                Assert.AreEqual(expectedSsn[i], _contacts[i].Ssn);
            }
        }
        /// <summary>
        /// Writes the XML content to a file to be read in and retuns its FileInfo
        /// TODO: Make this to a temp file
        /// </summary>
        /// <returns></returns>
        private FileInfo WriteFile()
        {
            string fileName = "tempContacts.xml";
            string fileText = @"
<Contacts>
    <Contact Ssn=""111-11-1111"">
        <FirstName>Sayed</FirstName>
        <MiddleName>Ibrahim</MiddleName>
        <LastName>Hashimi</LastName>
        <Email>sayed.hashimi@gmail.com</Email>
        <Website>www.sedodream.com</Website>
    </Contact>
    <Contact Ssn=""222-22-2222"">
        <FirstName>Sayed</FirstName>
        <MiddleName>Yahya</MiddleName>
        <LastName>Hashimi</LastName>
        <Email>sayed@sayedhashimi.com</Email>
        <Website>www.sayedhashimi.com</Website>
    </Contact>
    <Contact Ssn=""333-33-3333"">
        <FirstName>Mike</FirstName>
        <MiddleName>Ray</MiddleName>
        <LastName>Murphy</LastName>
        <Email>magickmike@gmail.com</Email>
    </Contact>
</Contacts>
";
            FileInfo theFile = new FileInfo(fileName);
            File.WriteAllText(theFile.FullName, fileText);
            return theFile;
        }
    }

}
