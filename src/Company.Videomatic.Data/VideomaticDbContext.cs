﻿using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Company.Videomatic.Infrastructure.Data;

public abstract class VideomaticDbContext : DbContext
{    
    public VideomaticDbContext(DbContextOptions options) 
        : base(options)
    {        
    }
    
    public DbSet<Video> Videos { get; set; } = null!;
    public DbSet<Thumbnail> Thumbnails { get; set; } = null!;
    public DbSet<Folder> Folders { get; set; } = null!;
    public DbSet<Transcript> Transcripts { get; set; } = null!;
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);        

        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}
