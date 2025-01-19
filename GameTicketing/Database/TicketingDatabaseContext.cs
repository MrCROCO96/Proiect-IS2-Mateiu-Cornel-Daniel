using Microsoft.EntityFrameworkCore;
using GameTicketing.Database.Models;

public partial class TicketingDatabaseContext : DbContext
{
    public TicketingDatabaseContext()
    {
    }

    public TicketingDatabaseContext(DbContextOptions<TicketingDatabaseContext> options)
        : base(options)
    {
    }

    // Adaugă DbSet pentru tabelul Users
    public DbSet<Users> Users { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data Source=../GameTickets.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configurări explicite pentru entitatea Users (opțional)
        modelBuilder.Entity<Users>(entity =>
        {
            entity.ToTable("Users"); // Confirmă numele tabelului
            entity.HasKey(e => e.Id); // Setează cheia primară
            entity.Property(e => e.Nume).IsRequired();
            entity.Property(e => e.Prenume).IsRequired();
            entity.Property(e => e.Functie).IsRequired();
            entity.Property(e => e.Telefon).IsRequired();
            entity.Property(e => e.Email).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}