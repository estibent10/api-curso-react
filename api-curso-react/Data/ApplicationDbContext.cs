using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using api_curso_react.Models;
using Microsoft.EntityFrameworkCore;

namespace api_curso_react.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Operador> Operadores { get; set; }
        public DbSet<Cobrador> Cobradores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Especificar Predeterminados en BD
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                //https://entityframeworkcore.com/knowledge-base/51763168/common-configurations-for-entities-implementing-an-interface                

                if (typeof(IEntidadAuditable).IsAssignableFrom(entityType.ClrType))
                {
                    builder.Entity(entityType.ClrType).Property<DateTime>(nameof(IEntidadAuditable.FechaCreacion)).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    builder.Entity(entityType.ClrType).Property<String>(nameof(IEntidadAuditable.CreadoPor)).HasMaxLength(75);
                    builder.Entity(entityType.ClrType).Property<String>(nameof(IEntidadAuditable.ModificadoPor)).HasMaxLength(75);
                }

                builder.Entity(entityType.ClrType).Property<bool>("Activo").HasDefaultValueSql("1");
            }
            #endregion

            #region Especificar Comentarios y Documentación de Campos
            #endregion

            #region Especificar Campos Requeridos, Longitudes de Campos de Texto, Valores Predeterminados, Llaves Primarias y Llaves Foraneas
            builder.Entity<Operador>(
                   modelo =>
                   {
                       modelo.HasKey(C => new { C.TipoIdentificacion, C.Identificacion }).HasName("PK_Operadores");
                       modelo.Property(C => C.TipoIdentificacion).IsRequired();
                       modelo.Property(C => C.Identificacion).IsRequired().HasMaxLength(22);
                       modelo.Property(C => C.Nombre).IsRequired().HasMaxLength(150);
                       modelo.Property(C => C.Email).IsRequired().HasMaxLength(150);
                       modelo.Property(C => C.CuentaIBAN).IsRequired().HasMaxLength(50);
                   });

            builder.Entity<Cobrador>(
                   modelo =>
                   {
                       modelo.HasKey(C => new { C.TipoIdentificacion, C.Identificacion }).HasName("PK_Cobradores");
                       modelo.Property(C => C.TipoIdentificacion).IsRequired();
                       modelo.Property(C => C.Identificacion).IsRequired().HasMaxLength(22);
                       modelo.Property(C => C.Nombre).IsRequired().HasMaxLength(150);
                       modelo.Property(C => C.Email).IsRequired().HasMaxLength(150);

                       //Relaciones
                       modelo.Property(C => C.TipoIdentificacionOperador).IsRequired();
                       modelo.Property(C => C.IdentificacionOperador).IsRequired().HasMaxLength(22);
                       modelo.HasOne(C => C.Operador)
                       .WithMany(C => C.Cobradores)
                       .HasForeignKey(C => new { C.TipoIdentificacionOperador,  C.IdentificacionOperador})
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasConstraintName("FK_Operadores_TipoIdentificacionOperador_IdentificacionOperador");


                   });
            #endregion

        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var EntryForUpdate in this.ChangeTracker.Entries()
                  .Where(e => e.Entity is IEntidadAuditable && e.State == EntityState.Modified)
                  .Select(e => e.Entity as IEntidadAuditable))
            {
                EntryForUpdate.FechaModificacion = DateTime.Now;
                
            }

            return await base.SaveChangesAsync();
        }

    }
}
