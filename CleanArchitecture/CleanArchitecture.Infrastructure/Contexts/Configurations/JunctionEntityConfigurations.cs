using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Infrastructure.Contexts.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.ToTable("Followers");
            builder.HasKey(x => new { x.FollowerId, x.ArtistId });
            builder.Property(x => x.FollowerId).HasColumnName("follower_id");
            builder.Property(x => x.ArtistId).HasColumnName("artist_id");
            builder.Property(x => x.FollowedAt).HasColumnName("followed_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(x => x.FollowerUser).WithMany().HasForeignKey(x => x.FollowerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Artist).WithMany().HasForeignKey(x => x.ArtistId).OnDelete(DeleteBehavior.Restrict);
            
            builder.ToTable(t => t.HasCheckConstraint("CHK_Followers_SelfFollow", "follower_id <> artist_id"));
        }
    }

    public class TrackCollaboratorConfiguration : IEntityTypeConfiguration<TrackCollaborator>
    {
        public void Configure(EntityTypeBuilder<TrackCollaborator> builder)
        {
            builder.ToTable("TrackCollaborators");
            builder.HasKey(x => new { x.TrackId, x.UserId });
            builder.Property(x => x.TrackId).HasColumnName("track_id");
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.Role).HasColumnName("role").IsRequired().HasMaxLength(255);

            builder.HasOne(x => x.Track).WithMany().HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class PlaylistTrackConfiguration : IEntityTypeConfiguration<PlaylistTrack>
    {
        public void Configure(EntityTypeBuilder<PlaylistTrack> builder)
        {
            builder.ToTable("PlaylistTracks");
            builder.HasKey(x => new { x.PlaylistId, x.TrackId });
            builder.Property(x => x.PlaylistId).HasColumnName("playlist_id");
            builder.Property(x => x.TrackId).HasColumnName("track_id");

            builder.HasOne(x => x.Playlist).WithMany(x => x.PlaylistTracks).HasForeignKey(x => x.PlaylistId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Track).WithMany(x => x.PlaylistTracks).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("Likes");
            builder.HasKey(x => new { x.UserId, x.TrackId });
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.TrackId).HasColumnName("track_id");
            builder.Property(x => x.LikedAt).HasColumnName("liked_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Track).WithMany().HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class ShareConfiguration : IEntityTypeConfiguration<Share>
    {
        public void Configure(EntityTypeBuilder<Share> builder)
        {
            builder.ToTable("Shares");
            builder.HasKey(x => new { x.UserId, x.TrackId });
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.TrackId).HasColumnName("track_id");
            builder.Property(x => x.PlatformId).HasColumnName("platform_id").HasConversion<byte>();
            builder.Property(x => x.SharedAt).HasColumnName("shared_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Track).WithMany().HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class DownloadConfiguration : IEntityTypeConfiguration<Download>
    {
        public void Configure(EntityTypeBuilder<Download> builder)
        {
            builder.ToTable("Downloads");
            builder.HasKey(x => new { x.UserId, x.TrackId });
            builder.Property(x => x.UserId).HasColumnName("user_id");
            builder.Property(x => x.TrackId).HasColumnName("track_id");
            builder.Property(x => x.DownloadedAt).HasColumnName("downloaded_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.LocalPath).HasColumnName("local_path").IsRequired();

            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Track).WithMany().HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
