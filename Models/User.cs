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
        public List<Invite> InvitationsSent { get; set; }
        [InverseProperty("Invitee")]
        public List<Invite> InvitationsReceived{ get; set; }
        public User () {
            InvitationsSent = new List<Invite> ();
            InvitationsReceived = new List<Invite> ();
        }
    }
}