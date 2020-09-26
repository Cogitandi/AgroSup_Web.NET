using Microsoft.VisualStudio.TestTools.UnitTesting;
using AgroSup.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AgroSup.Core.Domain.Tests
{
    [TestClass()]
    public class YearPlanTests
    {
        [TestMethod()]
        public void GetDataToImportTest()
        {
            // Arrange
            var yearPlanToImportData = new YearPlan()
            {
                Fields = new List<Field>()
                {
                    new Field()
                    {
                        Parcels = new List<Parcel>()
                        {
                            new Parcel() { CultivatedArea = 12, FuelApplication = true, Number = "844/1" },
                            new Parcel() { CultivatedArea = 2, FuelApplication = false, Number = "844/5" }
                        }
                    }
                },
                Operators = new List<Operator>()
                {
                    new Operator() {FirstName="Jan", LastName="Kowalski"}
                }
            };

            var newYearPlan = new YearPlan();
            // Act
            newYearPlan.GetDataToImport(yearPlanToImportData);
            // Assert
            Assert.AreEqual(yearPlanToImportData.Fields.Count(), newYearPlan.Fields.Count());
            Assert.AreEqual(yearPlanToImportData.Operators.Count(), newYearPlan.Operators.Count());
        }
    }
}