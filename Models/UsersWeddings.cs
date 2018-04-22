using System;
using System.ComponentModel.DataAnnotations;

namespace weddingplanner.Models{
    public class UsersWeddings : BaseEntity{
        public int UsersWeddingsId {get; set;}
        public int UsersId {get; set;}
        public User Users {get; set;}
        public int WeddingsId {get; set;}
        public Wedding Weddings {get; set;}
    }
}