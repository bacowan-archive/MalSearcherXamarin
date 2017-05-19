using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using NUnit.Framework;
using System.Xml.Linq;
using MalSearcher.Model.MalProxy;

namespace UnitTestApp2.UtilsTests
{
    [TestFixture]
    class TestXmlUtils
    {
        [Test]
        public void TestBlankNode()
        {
            XElement element = XElement.Parse("<root><blank/></root>");
            int asInt = XmlUtils.ParseSingleNode<int>(element, "blank");
            Assert.AreEqual(default(int), asInt);
        }

        [Test]
        public void TestNotBlankNode()
        {
            XElement element = XElement.Parse("<root><notBlank>3</notBlank></root>");
            int asInt = XmlUtils.ParseSingleNode<int>(element, "notBlank");
            Assert.AreEqual(3, asInt);
        }
    }
}