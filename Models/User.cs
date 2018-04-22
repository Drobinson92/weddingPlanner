using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using weddingplanner.Models;

namespace weddingplanner.Models{
    public abstract class BaseEntity{}
    public class User: BaseEntity{
        [Key]
        public int UserId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public string Email {get; set;}
        public string Password {get; set;}

        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public List<UsersWeddings> UsersWeddings {get; set;}
        public User(){
            UsersWeddings = new List<UsersWeddings>();
        }
        
       
    }

}