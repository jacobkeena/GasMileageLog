using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace MilesPerGallonSite.Models
{
    public class InformationView : Information
    {

        public void MapDataToViewModel(Information obj)
        {
            if (obj != null)
            {
                this.PersonId = obj.PersonId;
                this.FName = obj.FName;
                this.LName = obj.LName;
                this.CarModel = obj.CarModel;
                this.MilesDriven = obj.MilesDriven;
                this.GallonsFilled = obj.GallonsFilled;
                this.FillUpDate = obj.FillUpDate;

            }
        }
        public decimal MilesPerGallon
        {
            get
            {
                return MilesDriven / GallonsFilled;
            }  
        }


    }
}