using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Abp.Application.Services.Dto;
using Abp.Domain.Entities.Auditing;
using Abp.Runtime.Validation;
using Lte.Domain.Geo.Abstract;
using Lte.Domain.Regular;
using Lte.Parameters.Service.Public;

namespace Lte.Parameters.Region.Entities
{
    public class CollegeInfo : AuditedEntity
    {
        public int TownId { get; set; }

        public string Name { get; set; }

        public int TotalStudents { get; set; }

        public int CurrentSubscribers { get; set; }

        public int GraduateStudents { get; set; }

        public int NewSubscribers { get; set; }

        public DateTime OldOpenDate { get; set; }

        public DateTime NewOpenDate { get; set; }

        public CollegeRegion CollegeRegion { get; set; }
    }

    public class CollegeRegion
    {
        [Key]
        public int CollegeId { get; set; }

        public double Area { get; set; }

        public RegionType RegionType { get; set; }

        public string Info { get; set; }
    }
}
