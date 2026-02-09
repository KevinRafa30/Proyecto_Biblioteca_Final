using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaUNAPEC_Web.Models;

public partial class BibliotecaUnapecContext : DbContext
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

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=BibliotecaUNAPEC;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Autore>(entity =>
        {
            entity.HasKey(e => e.IdAutor).HasName("PK__Autores__9AE8206ACAFEBAB2");

            entity.Property(e => e.IdAutor).HasColumnName("idAutor");
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
            entity.HasKey(e => e.IdCiencia).HasName("PK__Ciencias__2F70679B32F73DE3");

            entity.Property(e => e.IdCiencia).HasColumnName("idCiencia");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Editoriale>(entity =>
        {
            entity.HasKey(e => e.IdEditorial).HasName("PK__Editoria__9DF182DB278D32A8");

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
            entity.HasKey(e => e.IdEmpleado).HasName("PK__Empleado__5295297C3F55F169");

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
            entity.HasKey(e => e.IdIdioma).HasName("PK__Idiomas__A96571FC3BAE7E62");

            entity.Property(e => e.IdIdioma).HasColumnName("idIdioma");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Estado).HasColumnName("estado");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__Libros__5BC0F0263497D897");

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
                .HasConstraintName("FK__Libros__idAutor__300424B4");

            entity.HasOne(d => d.IdCienciaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdCiencia)
                .HasConstraintName("FK__Libros__idCienci__31EC6D26");

            entity.HasOne(d => d.IdEditorialNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdEditorial)
                .HasConstraintName("FK__Libros__idEditor__30F848ED");

            entity.HasOne(d => d.IdIdiomaNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdIdioma)
                .HasConstraintName("FK__Libros__idIdioma__32E0915F");
        });

        modelBuilder.Entity<PrestamosDevolucione>(entity =>
        {
            entity.HasKey(e => e.IdTransaccion).HasName("PK__Prestamo__5B8761F06C241236");

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
                .HasConstraintName("FK__Prestamos__idEmp__35BCFE0A");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.PrestamosDevoluciones)
                .HasForeignKey(d => d.IdLibro)
                .HasConstraintName("FK__Prestamos__idLib__36B12243");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.PrestamosDevoluciones)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__Prestamos__idUsu__37A5467C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__645723A608199529");

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
