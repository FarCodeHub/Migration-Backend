using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Persons");
            builder.HasKey(f => f.Id);
            

            builder.HasOne(f => f.User)
                .WithOne(f => f.Person)
                .HasForeignKey<User>(f => f.PersonId);

            builder.HasOne(f => f.Visa)
                .WithOne(f => f.Person)
                .HasForeignKey<Visa>(f => f.PersonId);
        }
    }
}
