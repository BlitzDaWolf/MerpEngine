using MerpEngine;
using NUnit.Framework;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Text;

namespace MerpEngineTests
{
    public class CameraTest
    {
        Camera camera;

        [SetUp]
        public void Setup()
        {
            camera = new Camera(OpenTK.Vector2.Zero);
        }

        [Test]
        public void CameraExsists()
        {
            Assert.AreNotEqual(camera, null);
            Assert.AreNotEqual(Camera.Main, null);
        }

        [Test]
        public void ToWorldPos()
        {
            Assert.AreEqual(new Vector2(-(Screen.Width / 2), -(Screen.Heigth / 2)), camera.ToWorld(Vector2.Zero));
            Assert.AreEqual(Vector2.Zero, camera.ToWorld(new Vector2( (Screen.Width / 2), (Screen.Heigth / 2))));
        }

        [Test]
        public void SetPosition()
        {
            Vector2 newPosition = new Vector2(50, 100);
            Camera.Main.SetPosition(newPosition);
            Assert.AreEqual(newPosition, camera.Position);
        }
    }
}
