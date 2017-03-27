using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace network.Models{
    public class User{
        public int id {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public string Password {get;set;}
        public string Description {get;set;}
        public DateTime CreatedAt {get;set;}
        public DateTime UpdatedAt {get;set;}

        [InverseProperty("Inviter")]
        public List<Invitation> InvitationsSent { get; set; }
        [InverseProperty("Invitee")]
        public List<Invitation> InvitationsReceived{ get; set; }
        public User () {
            InvitationsSent = new List<Invitation> ();
            InvitationsReceived = new List<Invitation> ();
        }
    }
}