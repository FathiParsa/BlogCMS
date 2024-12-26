using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.EF.Entities;

public class BloggingDbContext : DbContext
{
    public BloggingDbContext()
    {
    }

    public BloggingDbContext(DbContextOptions<BloggingDbContext> options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<MediaFile> MediaFiles { get; set; }

    public DbSet<Page> Pages { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<PostTag> PostTags { get; set; }

    public DbSet<Tag> Tags { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Log> Logs { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=.;Database=Wordpress;Trusted_Connection=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Post).WithMany(p => p.Comments)
                .HasForeignKey(d => d.PostId)
                .HasConstraintName("FK_Comments_Posts");

            entity.HasOne(d => d.User).WithMany(p => p.Comments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Comments_Users");
        });

        modelBuilder.Entity<MediaFile>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.FileName).HasMaxLength(255);
            entity.Property(e => e.FilePath).HasMaxLength(255);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.UploadedByNavigation).WithMany(p => p.MediaFiles)
                .HasForeignKey(d => d.UploadedBy)
                .HasConstraintName("FK_MediaFiles_Users");
        });

        modelBuilder.Entity<Page>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.Pages)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pages_Users");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.Posts)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK_Posts_Users");

            entity.HasOne(d => d.Category).WithMany(p => p.Posts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Posts_Categories");
        });

        modelBuilder.Entity<PostTag>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Post).WithMany()
                .HasForeignKey(d => d.PostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostTags_Posts");

            entity.HasOne(d => d.Tag).WithMany()
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PostTags_Tags");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagName).HasMaxLength(100);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.LastLogin)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        base.OnModelCreating(modelBuilder);
    }

}