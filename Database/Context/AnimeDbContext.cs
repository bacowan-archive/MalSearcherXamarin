using Database.Objects;
using Database.Objects.Intersections;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Context
{
    public class AnimeDbContext : DbContext
    {
        private string mDbPath;

        public AnimeDbContext(string mDbPath)
        {
            this.mDbPath = mDbPath;
        }

        public virtual DbSet<Anime> Anime { get; set; }
        public virtual DbSet<MyAnimeEntry> MyAnimeEntries { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Character> Characters { get; set; }
        public virtual DbSet<CharacterActor> CharacterActors { get; set; }
        public virtual DbSet<CharacterAnime> CharacterAnime { get; set; }
        public virtual DbSet<Language> Languages { get; set; }
        public virtual DbSet<Name> Names { get; set; }
        public virtual DbSet<VoiceActor> VoiceActors { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            setUpCharacterActorIntersections(modelBuilder);
            setUpCharacterAnimeIntersections(modelBuilder);
            setUpMyAnimeEntryKeys(modelBuilder);
        }

        private void setUpMyAnimeEntryKeys(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MyAnimeEntry>().HasKey(a => new { a.AnimeId, a.UserId });
        }

        private void setUpCharacterAnimeIntersections(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterActor>().HasKey(ca => new { ca.CharacterId, ca.ActorId, ca.LanguageId });
            modelBuilder.Entity<CharacterActor>()
                .HasOne(ca => ca.Character)
                .WithMany(c => c.Actors)
                .HasForeignKey(ca => ca.CharacterId);
            modelBuilder.Entity<CharacterActor>()
                .HasOne(ca => ca.Actor)
                .WithMany(a => a.Characters)
                .HasForeignKey(ca => ca.ActorId);
        }

        private void setUpCharacterActorIntersections(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CharacterAnime>().HasKey(ca => new { ca.CharacterId, ca.AnimeId });
            modelBuilder.Entity<CharacterAnime>()
                .HasOne(ca => ca.Character)
                .WithMany(c => c.CharacterAnimes)
                .HasForeignKey(ca => ca.CharacterId);
            modelBuilder.Entity<CharacterAnime>()
                .HasOne(ca => ca.Anime)
                .WithMany(a => a.CharacterAnimes)
                .HasForeignKey(ca => ca.AnimeId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=" + mDbPath);
        }
    }

    public static class DbSetExtensions
    {
        public static void AddIfNotExists<T>(this DbSet<T> set, T item) where T : class
        {
            if (!set.Any(s => s.Equals(item)))
                set.Add(item);
        }

        public static void AddAllIfNotExists<T>(this DbSet<T> set, IEnumerable<T> items) where T : class
        {
            foreach (var item in items)
                set.AddIfNotExists(item);
        }
    }
}
