CREATE DATABASE BibliotecaUNAPEC;
GO
USE BibliotecaUNAPEC;
GO


CREATE TABLE Ciencias (
    idCiencia INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(100),
    estado BIT
);

CREATE TABLE Idiomas (
    idIdioma INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(50),
    estado BIT
);

CREATE TABLE Editoriales (
    idEditorial INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100),
    pais VARCHAR(50),
    estado BIT
);

CREATE TABLE Autores (
    idAutor INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100),
    nacionalidad VARCHAR(50),
    estado BIT
);


CREATE TABLE Empleados (
    idEmpleado INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100),
    cedula VARCHAR(11),
    tandaLaboral VARCHAR(20),
    fechaIngreso DATE,
    estado BIT
);

CREATE TABLE Usuarios (
    idUsuario INT PRIMARY KEY IDENTITY(1,1),
    nombre VARCHAR(100),
    cedula VARCHAR(11),
    noCarnet VARCHAR(20),
    tipoPersona VARCHAR(20),
    estado BIT
);


CREATE TABLE Libros (
    idLibro INT PRIMARY KEY IDENTITY(1,1),
    titulo VARCHAR(200),
    isbn VARCHAR(20),
    anioPublicacion VARCHAR(4),
    estado BIT,
    idAutor INT FOREIGN KEY REFERENCES Autores(idAutor),
    idEditorial INT FOREIGN KEY REFERENCES Editoriales(idEditorial),
    idCiencia INT FOREIGN KEY REFERENCES Ciencias(idCiencia),
    idIdioma INT FOREIGN KEY REFERENCES Idiomas(idIdioma)
);

CREATE TABLE Prestamos_Devoluciones (
    idTransaccion INT PRIMARY KEY IDENTITY(1,1),
    fechaPrestamo DATE,
    fechaDevolucion DATE,
    montoPorDia DECIMAL(10,2),
    cantidadDias INT,
    comentario VARCHAR(255),
    estado BIT,
    idEmpleado INT FOREIGN KEY REFERENCES Empleados(idEmpleado),
    idLibro INT FOREIGN KEY REFERENCES Libros(idLibro),
    idUsuario INT FOREIGN KEY REFERENCES Usuarios(idUsuario)
);
GO


CREATE TABLE TiposBibliografia (
    idTipoBibliografia INT PRIMARY KEY IDENTITY(1,1),
    descripcion VARCHAR(100),
    estado BIT
);
GO

ALTER TABLE Libros
ADD idTipoBibliografia INT;
GO

ALTER TABLE Libros
ADD CONSTRAINT FK_Libros_TiposBibliografia
FOREIGN KEY (idTipoBibliografia) REFERENCES TiposBibliografia(idTipoBibliografia);
GO

ALTER TABLE Autores
ADD apellido VARCHAR(100);
GO