using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Infrastructure.Contexts.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Username).HasColumnName("username").IsRequired().HasMaxLength(255);
            builder.HasIndex(x => x.Username).IsUnique();
            builder.Property(x => x.Email).HasColumnName("email").IsRequired().HasMaxLength(255);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.PasswordHash).HasColumnName("password_hash").IsRequired().HasMaxLength(255);
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.Bio).HasColumnName("bio").HasMaxLength(1000);
            builder.Property(x => x.Location).HasColumnName("location").HasMaxLength(255);
            builder.Property(x => x.Website).HasColumnName("website").HasMaxLength(500);
            builder.Property(x => x.RoleId).HasColumnName("role_id").HasConversion<byte>();
        }
    }

    public class PlaylistConfiguration : IEntityTypeConfiguration<Playlist>
    {
        public void Configure(EntityTypeBuilder<Playlist> builder)
        {
            builder.ToTable("Playlists");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(255);
            builder.Property(x => x.IsPublic).HasColumnName("is_public").HasDefaultValue(true);

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ArtistApplicationConfiguration : IEntityTypeConfiguration<ArtistApplication>
    {
        public void Configure(EntityTypeBuilder<ArtistApplication> builder)
        {
            builder.ToTable("ArtistApplications");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.IdProofUrl).HasColumnName("id_proof_url").IsRequired();
            builder.Property(x => x.PortfolioLinks).HasColumnName("portfolio_links").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName("status_id").HasConversion<byte>();
            builder.Property(x => x.ReviewedBy).HasColumnName("reviewed_by");

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Reviewer).WithMany().HasForeignKey(x => x.ReviewedBy).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class ArtistApplicationCycle2Configuration : IEntityTypeConfiguration<ArtistApplicationCycle2>
    {
        public void Configure(EntityTypeBuilder<ArtistApplicationCycle2> builder)
        {
            builder.ToTable("ArtistApplications_Cycle2");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.IdProofUrl).HasColumnName("id_proof_url").IsRequired();
            builder.Property(x => x.PortfolioLinks).HasColumnName("portfolio_links").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName("status_id").HasConversion<byte>();
            builder.Property(x => x.ReviewedBy).HasColumnName("reviewed_by");

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Reviewer).WithMany().HasForeignKey(x => x.ReviewedBy).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
