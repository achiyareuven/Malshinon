﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Malshinon.Dal;

namespace Malshinon.Models
{
    public class People
    {
        public int Id { get; set; }               
        public string FirstName { get; set; }     
        public string LastName { get; set; }    
        public string SecretCode { get; set; }

        public int NumReports { get; set; }

        public int NumMentions { get; set; }

        public bool IsDangerous {  get; set; }
        public bool IsRecruitCandidate{ get;set; }

    }
}
