using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroSup.WebApp.ViewModels
{
    public static class ManageMenu
    {
        // Admin
        public static string InActiveAccounts => "InActiveAccounts";
        public static string Summary => "Summary";
        public static string PlantList => "PlantLst";
        public static string PlantAdd=> "PlantAdd";
        public static string FertilizerList => "FertilizerList";
        public static string FertilizerAdd => "FertilizerAdd";

        public static string InActiveAccountsNavClass(ViewContext viewContext) => PageNavClass(viewContext, InActiveAccounts);
        public static string SummaryNavClass(ViewContext viewContext) => PageNavClass(viewContext, Summary);
        public static string PlantListNavClass(ViewContext viewContext) => PageNavClass(viewContext, PlantList);
        public static string PlantAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, PlantAdd);
        public static string FertilizerListNavClass(ViewContext viewContext) => PageNavClass(viewContext, FertilizerList);
        public static string FertilizerAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, FertilizerAdd);
        // Manage
        public static string CropPlan => "CropPlan";
        public static string Parcels => "Parcels";
        public static string Fields => "Fields";
        public static string TreatmentList => "TreatmentList";
        public static string TreatmentAdd => "TreatmentAdd";
        public static string SummaryManage => "SummaryManage";

        public static string CropPlanNavClass(ViewContext viewContext) => PageNavClass(viewContext, CropPlan);
        public static string ParcelsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Parcels);
        public static string FieldsNavClass(ViewContext viewContext) => PageNavClass(viewContext, Fields);
        public static string TreatmentListNavClass(ViewContext viewContext) => PageNavClass(viewContext, TreatmentList);
        public static string TreatmentAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, TreatmentAdd);
        public static string SummaryManageNavClass(ViewContext viewContext) => PageNavClass(viewContext, SummaryManage);
        // Data
        public static string YearPlanList => "YearPlanList";
        public static string YearPlanAdd => "YearPlanAdd";
        public static string OperatorList => "OperatorList";
        public static string OperatorAdd => "OperatorAdd";
        public static string FieldList => "FieldList";
        public static string FieldAdd => "FieldAdd";
        public static string ChoosePlants => "ChoosePlants";

        public static string YearPlanListNavClass(ViewContext viewContext) => PageNavClass(viewContext, YearPlanList);
        public static string YearPlanAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, YearPlanAdd);
        public static string OperatorListNavClass(ViewContext viewContext) => PageNavClass(viewContext, OperatorList);
        public static string OperatorAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, OperatorAdd);
        public static string FieldListNavClass(ViewContext viewContext) => PageNavClass(viewContext, FieldList);
        public static string FieldAddNavClass(ViewContext viewContext) => PageNavClass(viewContext, FieldAdd);
        public static string ChoosePlantsNavClass(ViewContext viewContext) => PageNavClass(viewContext, ChoosePlants);

        private static string PageNavClass(ViewContext viewContext, string page)
        {
            var activePage = viewContext.ViewData["ActivePage"] as string
                ?? System.IO.Path.GetFileNameWithoutExtension(viewContext.ActionDescriptor.DisplayName);
            return string.Equals(activePage, page, StringComparison.OrdinalIgnoreCase) ? "active" : null;
        }
    }
}
