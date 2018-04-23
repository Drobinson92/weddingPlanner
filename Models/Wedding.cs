using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace weddingplanner.Models{
    public class Wedding : BaseEntity{
        public int WeddingId {get; set;}
        [Required]
        public string WedderOne {get; set;}
        [Required]
        public string WedderTwo {get; set;}
        [FutureDate(ErrorMessage="Wedding must be in the future!")]
        [DataType(DataType.Date, ErrorMessage="Nah playa")]
        public DateTime WeddingDate {get; set;}
        [Required]
        public string WeddingAddress {get; set;}
        public int CreatedBy {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public List<UsersWeddings> UsersWeddings {get; set;}
        public Wedding(){
            UsersWeddings = new List<UsersWeddings>();
        }
    }
    public class FutureDateAttribute : ValidationAttribute{
        public FutureDateAttribute(){}
        public override bool IsValid(object value){
            var date = (DateTime)value;
            if(date > DateTime.Now){
                return true;
            }
            return false;
        }
    }
}