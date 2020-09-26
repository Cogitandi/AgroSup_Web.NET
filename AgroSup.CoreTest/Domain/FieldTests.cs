using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace AgroSup.Core.Domain.Tests
{
    [TestClass]
    public class FieldTests
    {
        [TestMethod]
        public void GetFieldAreaTest()
        {
            // Arrange
            int parcelArea1 = 11;
            int parcelArea2 = 61;
            int expected = 72;

            var field = new Field()
            {
                Parcels = new List<Parcel>()
                {
                    new Parcel() {CultivatedArea = parcelArea1},
                    new Parcel() {CultivatedArea = parcelArea2},
                }
            };

            // Act
            var fieldArea = field.GetFieldArea();
            //Assert
            Assert.AreEqual(fieldArea, expected);
        }

        [TestMethod()]
        public void GetTotalCultivatedAreaTest()
        {
            // Arrange
            int parcelArea1 = 11;
            int parcelArea2 = 61;
            int parcelArea3 = 41;
            int parcelArea4 = 91;
            int expected = 204;

            var field = new Field()
            {
                Parcels = new List<Parcel>()
                {
                    new Parcel() {CultivatedArea = parcelArea1},
                    new Parcel() {CultivatedArea = parcelArea2},
                }
            };
            var field2 = new Field()
            {
                Parcels = new List<Parcel>()
                {
                    new Parcel() {CultivatedArea = parcelArea3},
                    new Parcel() {CultivatedArea = parcelArea4},
                }
            };

            // Act
            var totalArea = Field.GetTotalCultivatedArea(new List<Field>(){ field,field2});
            //Assert
            Assert.AreEqual(totalArea, expected);
        }
    }
}