﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCabinet.Domain.Entities;
using System.Reflection.Emit;

namespace UniCabinet.Infrastructure.Data.EntityConfigurations
{
    public class SpecialtyConfiguration : IEntityTypeConfiguration<SpecialtyEntity>
    {
        public void Configure(EntityTypeBuilder<SpecialtyEntity> builder)
        {
            builder.HasKey(s => s.Id);

        }
    }
}
