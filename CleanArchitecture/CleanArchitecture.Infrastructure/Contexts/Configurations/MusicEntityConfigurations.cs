using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Core.Entities;

namespace CleanArchitecture.Infrastructure.Contexts.Configurations
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("Tracks");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.Title).HasColumnName("title").IsRequired().HasMaxLength(255);
            builder.Property(x => x.ArtistId).HasColumnName("artist_id").IsRequired();
            builder.Property(x => x.FileUrl).HasColumnName("file_url").IsRequired();
            builder.Property(x => x.GenreId).HasColumnName("genre_id");
            builder.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.IsAiGenerated).HasColumnName("is_ai_generated").HasDefaultValue(false);
            builder.Property(x => x.RefTrackId).HasColumnName("ref_track_id");
            builder.Property(x => x.ReleaseDate).HasColumnName("release_date").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.CanvasUrl).HasColumnName("canvas_url");
            builder.Property(x => x.CoverImageUrl).HasColumnName("cover_image_url").HasMaxLength(500);
            builder.Property(x => x.LyricSyncUrl).HasColumnName("lyric_sync_url");
            builder.Property(x => x.Lyrics).HasColumnName("lyrics");
            builder.Property(x => x.AudioFormatId).HasColumnName("audio_format_id").HasConversion<byte>();
            builder.Property(x => x.PLine).HasColumnName("p_line").HasMaxLength(255);
            builder.Property(x => x.AiPermission).HasColumnName("ai_permission");
            builder.Property(x => x.AllowSystemAnalysis).HasColumnName("allow_system_analysis").HasDefaultValue(true);

            builder.HasOne(x => x.Artist).WithMany().HasForeignKey(x => x.ArtistId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.RefTrack).WithMany(x => x.DerivedTracks).HasForeignKey(x => x.RefTrackId).OnDelete(DeleteBehavior.Restrict);
            
            builder.ToTable(t => t.HasCheckConstraint("CHK_Tracks_AIPermission", "ai_permission IN (0, 1, 2)"));
        }
    }

    public class AIVisualConfiguration : IEntityTypeConfiguration<AIVisual>
    {
        public void Configure(EntityTypeBuilder<AIVisual> builder)
        {
            builder.ToTable("AIVisuals");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.TrackId).HasColumnName("track_id").IsRequired();
            builder.Property(x => x.VideoUrl).HasColumnName("video_url").IsRequired();
            builder.Property(x => x.ViewCount).HasColumnName("view_count").HasDefaultValue(0);

            builder.HasOne(x => x.Track).WithMany(x => x.AIVisuals).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
        }
    }

    public class GenerativePromptConfiguration : IEntityTypeConfiguration<GenerativePrompt>
    {
        public void Configure(EntityTypeBuilder<GenerativePrompt> builder)
        {
            builder.ToTable("GenerativePrompts");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.TrackId).HasColumnName("track_id").IsRequired();
            builder.Property(x => x.GenreId).HasColumnName("genre_id");
            builder.HasOne(x => x.Genre).WithMany().HasForeignKey(x => x.GenreId).OnDelete(DeleteBehavior.SetNull);
            builder.Property(x => x.MoodTag).HasColumnName("mood_tag").HasMaxLength(255);
            builder.Property(x => x.InstrumentTags).HasColumnName("instrument_tags");
            builder.Property(x => x.RawPromptText).HasColumnName("raw_prompt_text");

            builder.HasOne(x => x.Track).WithMany(x => x.GenerativePrompts).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
            builder.ToTable(t => t.HasCheckConstraint("CHK_Prompts_InstrumentTagsJSON", "ISJSON(instrument_tags) = 1"));
        }
    }

    public class TrackStatisticConfiguration : IEntityTypeConfiguration<TrackStatistic>
    {
        public void Configure(EntityTypeBuilder<TrackStatistic> builder)
        {
            builder.ToTable("TrackStatistics");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.TrackId).HasColumnName("track_id").IsRequired();
            builder.Property(x => x.StreamCount).HasColumnName("stream_count").HasDefaultValue(0);
            builder.Property(x => x.Timestamp).HasColumnName("timestamp").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.ListenerId).HasColumnName("listener_id");

            builder.HasOne(x => x.Track).WithMany(x => x.TrackStatistics).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Listener).WithMany().HasForeignKey(x => x.ListenerId).OnDelete(DeleteBehavior.Restrict);
        }
    }

    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasDefaultValueSql("NEWID()");
            builder.Property(x => x.TrackId).HasColumnName("track_id").IsRequired();
            builder.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            builder.Property(x => x.Content).HasColumnName("content").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("DATETIME2").HasDefaultValueSql("SYSUTCDATETIME()");
            builder.Property(x => x.ParentId).HasColumnName("parent_id");

            builder.HasOne(x => x.Track).WithMany(x => x.Comments).HasForeignKey(x => x.TrackId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.Parent).WithMany(x => x.Replies).HasForeignKey(x => x.ParentId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
