using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace network.Models{
    public class Invite{
        public int id {get;set;}

        [ForeignKey("UserId")]
        public int InvitorId {get; set;}
        public User Inviter {get;set;}

        [ForeignKey("UserId")]
        public int InviteeId {get; set;}
        public User Invitee {get; set;}
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Accepted {get; set;}
    }
}