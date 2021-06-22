using GigHub.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;

namespace GigHub.Persistence.EntityConfigurations
{
    public class GigConfigurations : EntityTypeConfiguration<Gig>
    {
        public GigConfigurations()
        {
            
            Property(g => g.ArtistID)
            .IsRequired();

            Property(g => g.Venue)
            .IsRequired()
            .HasMaxLength(225);

            Property(g => g.GenreId)
            .IsRequired();
        }
    }
}