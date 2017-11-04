using Academy.Domain.Core.Events;
using Academy.Infra.Data.Mapping.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Academy.Infra.Data.Mapping
{
    public class StoredEventMap : IEntityTypeConfiguration<StoredEvent>, IMapping
    {
        public void Configure(EntityTypeBuilder<StoredEvent> builder)
        {
            builder.Property(c => c.Timestamp)
                .HasColumnName("CreationDate");

            builder.Property(c => c.MessageType)
                .HasColumnName("Action")
                .HasColumnType("varchar(100)");
        }
    }
}
