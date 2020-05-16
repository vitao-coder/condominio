using Condominio.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Condominio.Repository.Mappings
{

    public class BlocoMap : IEntityTypeConfiguration<Bloco>
    {
        public void Configure(EntityTypeBuilder<Bloco> builder)
        {
            builder.ToTable(nameof(Bloco))
                .HasMany(c => c.Apartamentos);
        }
    }
}
