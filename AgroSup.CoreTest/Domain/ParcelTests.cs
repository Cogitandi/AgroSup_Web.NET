using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain.Tests
{
    [TestClass()]
    public class ParcelTests
    {
        [TestMethod()]
        public void GetFuelApplicationTest()
        {
            // Arrange
            var parcel1 = new Parcel()
            {
                FuelApplication = false
            };
            var expected1 = "Nie";
            var parcel2 = new Parcel()
            {
                FuelApplication = true
            };
            var expected2 = "Tak";
            // Act
            var result1 = parcel1.GetFuelApplication();
            var result2 = parcel2.GetFuelApplication();
            // Assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod()]
        public void GetOperatorNameTest()
        {
            // Arrange
            var parcel1 = new Parcel()
            {
            };
            var expected1 = "Brak dopłat";
            var parcel2 = new Parcel()
            {
                Operator = new Operator() { FirstName="Jan", LastName="Kowalski"}
            };
            var expected2 = "Jan Kowalski";
            // Act
            var result1 = parcel1.GetOperatorName();
            var result2 = parcel2.GetOperatorName();
            // Assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }

        [TestMethod()]
        public void GetFieldNameTest()
        {
            // Arrange
            var parcel = new Parcel()
            {
                Field = new Field() { Name="Za domem"}
            };
            var expected = "Za domem";
            // Act
            var result = parcel.GetFieldName();
            // Assert
            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void GetPlantNameTest()
        {
            // Arrange
            var parcel1 = new Parcel()
            {
                Field = new Field()
                {
                    Plant = new Plant() { Name="Pszenica ozima"}
                }
            };
            var expected1 = "Pszenica ozima";

            var parcel2 = new Parcel()
            {
                Field = new Field() { }
            };
            var expected2 = "Nie ustalono";
            // Act
            var result1 = parcel1.GetPlantName();
            var result2 = parcel2.GetPlantName();
            // Assert
            Assert.AreEqual(expected1, result1);
            Assert.AreEqual(expected2, result2);
        }
    }
}