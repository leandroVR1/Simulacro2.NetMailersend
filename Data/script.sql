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

INSERT INTO Especialidades (Nombre, Descripcion, Estado)
VALUES 
('Cardiología', 'Tratamiento de enfermedades del corazón', 'Disponible'),
('Dermatología', 'Diagnóstico y tratamiento de enfermedades de la piel', 'Disponible'),
('Neurología', 'Tratamiento de trastornos del sistema nervioso', 'Disponible');

INSERT INTO Medicos (Nombre, Correo, Telefono, Estado, EspecialidadId)
VALUES 
('Dr. Juan Pérez', 'juan.perez@correo.com', '123456789', 'Disponible', 1),
('Dra. María López', 'maria.lopez@correo.com', '987654321', 'Disponible', 2),
('Dr. Carlos Sánchez', 'carlos.sanchez@correo.com', '456123789', 'Disponible', 3);

INSERT INTO Pacientes (Nombre, Apellido, FechaNacimiento, Correo, Telefono, Direccion, Estado)
VALUES 
('Ana', 'García', '1985-04-12', 'ana.garcia@correo.com', '111222333', 'Calle Falsa 123', 'Disponible'),
('Luis', 'Martínez', '1990-07-23', 'luis.martinez@correo.com', '444555666', 'Avenida Siempre Viva 456', 'Disponible'),
('Marta', 'Rodríguez', '1978-11-30', 'marta.rodriguez@correo.com', '777888999', 'Boulevard Sol 789', 'Disponible');


INSERT INTO Citas (Fecha, Estado, MedicoId, PacienteId)
VALUES 
('2024-06-01', 'Disponible', 1, 1),
('2024-06-02', 'Disponible', 2, 2),
('2024-06-03', 'Disponible', 3, 3);


INSERT INTO Tratamientos (Descripcion, CitaId, Estado)
VALUES 
('Tratamiento para la hipertensión', 1, 'Disponible'),
('Tratamiento para el acné severo', 2, 'Disponible'),
('Terapia para migrañas crónicas', 3, 'Disponible');


SELECT * FROM Medicos;

