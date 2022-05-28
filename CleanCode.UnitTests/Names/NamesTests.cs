using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.UnitTests.Names
{
    [TestFixture]
    class NamesTests
    {

        private string _testPath;
        private CleanCode.Names.Names _namesClass;

        [SetUp]
        public void SetUp()
        {
            _testPath = @"D:\Libraries\Pictures";

            _namesClass = new CleanCode.Names.Names();
        }

        //Don't know how to create bitmaps :c
        //[Test]
        //public void GenerateImage_WhenCalled_ReturnNewBitmap()
        //{
        //    var result = _namesClass.GenerateImage(_testPath);

        //    Assert.IsInstanceOf<Bitmap>(result);
        //}

        [Test]
        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void GenerateImage_EmptyStringOrNullOrWhitespacePath_ThrowException(string testPath)
        {
            _testPath = testPath;

            Assert.Throws<ArgumentNullException>(() => _namesClass.GenerateImage(_testPath));
        }

        [Test]
        public void GenerateImage_NotValidParameter_ThrowInvalidParameter()
        {
            _testPath = "a";

            Assert.Throws<ArgumentException>(() => _namesClass.GenerateImage(_testPath));
        }
    }
}
