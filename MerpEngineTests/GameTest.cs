using MerpEngine;
using NUnit.Framework;

namespace MerpEngineTests
{
    public class GameTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CorrectWidth()
        {
            Assert.AreEqual(Screen.Width, 800);
        }

        [Test]
        public void CorrectHeigth()
        {
            Assert.AreEqual(Screen.Heigth, 600);
        }

        [Test]
        public void DefaultWindowBorder()
        {
            Assert.AreEqual(Screen.Border, OpenTK.WindowBorder.Resizable);
        }

        [Test]
        public void SetEnviroments()
        {
            Arguments.SetEnviroments(new string[] { "--width=200", "--heigth=200", "--borderless" });
            Assert.AreEqual(Screen.Width, 200);
            Assert.AreEqual(Screen.Heigth, 200);
            Assert.AreEqual(Screen.Border, OpenTK.WindowBorder.Hidden);
        }
    }
}