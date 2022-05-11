using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.Configurations
{
    internal abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : class
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            this.ConfigureProperties(builder);
            this.ConfigureRelationShips(builder);
            this.ConfigureStartupValues(builder);
        }

        protected virtual void ConfigureProperties(EntityTypeBuilder<T> builder) { }
        protected virtual void ConfigureRelationShips(EntityTypeBuilder<T> builder) { }
        protected virtual void ConfigureStartupValues(EntityTypeBuilder<T> builder) { }
    }
}