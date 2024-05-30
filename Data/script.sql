-- Active: 1717029937252@@bt1ge2zkwhtfry5lrpjd-mysql.services.clever-cloud.com@3306@bt1ge2zkwhtfry5lrpjd

CREATE TABLE Medicos (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(125),
    Correo VARCHAR(125),
    Telefono VARCHAR(125),
    Estado ENUM('Disponible', 'Eliminado'),
    EspecialidadId INT
)

ALTER TABLE `Medicos`
ADD FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id);

CREATE TABLE Especialidades (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(125),
    Descripcion TEXT,
    Estado ENUM('Disponible', 'Eliminado')
)

CREATE TABLE Citas (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Fecha DATE,
    Estado ENUM('Disponible', 'Eliminado'),
    MedicoId INT,
    PacienteId INT
)

ALTER TABLE `Citas`
ADD FOREIGN KEY (MedicoId) REFERENCES Medicos(Id);

ALTER TABLE `Citas`
ADD FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id);

CREATE TABLE Pacientes (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(125),
    Apellido VARCHAR(125),
    FechaNacimiento DATE,
    Correo VARCHAR(125),
    Telefono VARCHAR(75),
    Direccion VARCHAR(125),
    Estado ENUM('Disponible', 'Eliminado')
)

CREATE TABLE Tratamientos(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Descripcion TEXT,
    CitaId INT,
    Estado ENUM('Disponible', 'Eliminado')
)

ALTER TABLE `Tratamientos`
ADD FOREIGN KEY (CitaId) REFERENCES Citas(Id);



