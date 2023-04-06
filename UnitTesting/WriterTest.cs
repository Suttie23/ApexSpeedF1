using ApexSpeed.Core.Services.JSONWriter;
using ApexSpeedApp.MVVM.Model;
using MvxStarter.Core.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace UnitTesting
{
    [TestFixture]
    public class Tests
    {
        private JSONWriter _jsonWriter;

        [SetUp]
        public void Setup()
        {
            _jsonWriter = new JSONWriter();
        }

        [Test]
        public async Task WriterTest()
        {

            // Arrange
            List<LapSaveDataModel> lapList = new List<LapSaveDataModel>()
            {
                new LapSaveDataModel(0.58f, 0.12f, 6, 140, 2313.12f, 23.1),
                new LapSaveDataModel(0.09f, 0.98f, 2, 50, 1232.33f, 76.4),
                new LapSaveDataModel(0.342f, 0.632f, 2, 50, 8435.33f, 132.4)
            };

            var filename = @"..\..\..\..\Test Files\writertest.json";
            var expectedJson = JsonSerializer.Serialize(lapList, new JsonSerializerOptions { WriteIndented = true });

            //Act
            await _jsonWriter.WriteLapData(lapList, filename);
            var actualJson = await File.ReadAllTextAsync(filename);

            // Assert
            Assert.That(File.Exists(filename), Is.True, "The file was not created.");
            Assert.That(actualJson, Is.EqualTo(expectedJson));

        }
    }
}