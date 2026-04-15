using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BibliotecaUNAPEC_Web.Models;

public partial class BibliotecaUnapecContext : IdentityDbContext<IdentityUser>
{
    public BibliotecaUnapecContext()
    {
    }

    public BibliotecaUnapecContext(DbContextOptions<BibliotecaUnapecContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Autore> Autores { get; set; }

    public virtual DbSet<Ciencia> Ciencias { get; set; }

    public virtual DbSet<Editoriale> Editoriales { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Idioma> Idiomas { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<PrestamosDevolucione> PrestamosDevoluciones { get; set; }

    public virtual DbSet<TiposBibliografium> TiposBibliografia { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BibliotecaUNAPEC_web;Trusted_Connection=True;TrustServerCertificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autores__9AE8206A6D278F2F");

            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.Apellido)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nacionalidad)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nacionalidad");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Ciencia>(entity =>
        {
            entity.HasKey(e => e.IdCiencia).HasName("PK__Ciencias__2F70679BCA1A095F");

            entity.Property(e => e.IdCiencia).HasColumnName("idCiencia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PK__Editoria__9DF182DBE1AD9552");

            entity.Property(e => e.IdEditorial).HasColumnName("idEditorial");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Pais)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("pais");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297C12C8C02B");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TandaLaboral)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tandaLaboral");
        });

        modelBuilder.Entity<Idioma>(entity =>
        {
            entity.HasKey(e => e.IdIdioma).HasName("PK__Idiomas__A96571FCD03C47E5");

            entity.Property(e => e.IdIdioma).HasColumnName("idIdioma");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libros__5BC0F0262BDCF95E");

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.AnioPublicacion)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("anioPublicacion");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
            entity.Property(e => e.IdCiencia).HasColumnName("idCiencia");
            entity.Property(e => e.IdEditorial).HasColumnName("idEditorial");
            entity.Property(e => e.IdIdioma).HasColumnName("idIdioma");
            entity.Property(e => e.IdTipoBibliografia).HasColumnName("idTipoBibliografia");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("isbn");
            entity.Property(e => e.Titulo)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("titulo");

            entity.HasOne(d => d.IdAutorNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdAutor)
                .HasConstraintName("FK__Libros__idAutor__31EC6D26");

            entity.HasOne(d => d.IdCienciaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCiencia)
                .HasConstraintName("FK__Libros__idCienci__33D4B598");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK__Libros__idEditor__32E0915F");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK__Libros__idIdioma__34C8D9D1");

            entity.HasOne(d => d.IdTipoBibliografiaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdTipoBibliografia)
                .HasConstraintName("FK__Libros__idTipoBi__35BCFE0A");
        });

        modelBuilder.Entity<PrestamosDevolucione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Prestamo__5B8761F07EF3057A");

            entity.ToTable("Prestamos_Devoluciones");

            entity.Property(e => e.IdTransaccion).HasColumnName("idTransaccion");
            entity.Property(e => e.CantidadDias).HasColumnName("cantidadDias");
            entity.Property(e => e.Comentario)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("comentario");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.FechaDevolucion).HasColumnName("fechaDevolucion");
            entity.Property(e => e.FechaPrestamo).HasColumnName("fechaPrestamo");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.MontoPorDia)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("montoPorDia");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.PrestamosDevoluciones)
                .HasForeignKey(d => d.IdEmpleado)
                .HasConstraintName("FK__Prestamos__idEmp__38996AB5");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.PrestamosDevoluciones)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Prestamos__idLib__398D8EEE");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PrestamosDevoluciones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Prestamos__idUsu__3A81B327");
        });

        modelBuilder.Entity<TiposBibliografium>(entity =>
        {
            entity.HasKey(e => e.IdTipoBibliografia).HasName("PK__TiposBib__8B2D2ADFD6B3C460");

            entity.Property(e => e.IdTipoBibliografia).HasColumnName("idTipoBibliografia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A6EB9E8935");

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Cedula)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("cedula");
            entity.Property(e => e.Estado).HasColumnName("estado");
            entity.Property(e => e.NoCarnet)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("noCarnet");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.TipoPersona)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("tipoPersona");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
