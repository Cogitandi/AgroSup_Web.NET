﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AgroSup.Core.Domain
{
    public class Parcel
    {
        public Guid Id { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public int CultivatedArea { get; set; }
        [Required]
        public bool FuelApplication { get; set; }
        public Operator Operator { get; set; }
        [Required]
        public Field Field { get; set; }

        public Parcel(Parcel parcel)
        {
            this.Number = parcel.Number;
            this.CultivatedArea = parcel.CultivatedArea;
            this.FuelApplication = parcel.FuelApplication;
        }
        public Parcel()
        {

        }
        public string GetFuelApplication()
        {
            return FuelApplication ? "Tak" : "Nie";
        }
        public string GetOperatorName()
        {
            return Operator == null ? "Brak dopłat" : Operator.GetName;
        }
        public string GetFieldName()
        {
            return Field.Name;
        }
        public string GetPlantName()
        {
            return Field.Plant == null ? "Nie ustalono" : Field.Plant.Name;
        }
    }
}
