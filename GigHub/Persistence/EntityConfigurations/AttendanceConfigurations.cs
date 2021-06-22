using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.EntityConfigurations
{
    public class AttendanceConfigurations : EntityTypeConfiguration<Attendance>
    {
        public AttendanceConfigurations()
        {
           HasRequired(a => a.Gig)
              .WithMany(g => g.Attendances)
              .WillCascadeOnDelete(false);
        }              
    }
}