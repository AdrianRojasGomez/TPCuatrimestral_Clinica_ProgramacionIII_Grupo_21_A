USE master;
GO

-- Cierra todas las conexiones activas inmediatamente
ALTER DATABASE CLINICA_DB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
GO

-- Ahora sí, la elimina
DROP DATABASE CLINICA_DB;
GO


CREATE DATABASE CLINICA_DB
COLLATE SQL_Latin1_General_CP1_CS_AS;   
GO

USE CLINICA_DB;
GO

CREATE TABLE UsuariosApp (
IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
NombreUsuario VARCHAR(50) NOT NULL,
Clave VARCHAR(50) NOT NULL,
TipoUsuario INT NOT NULL, 
Estado BIT NOT NULL
);
GO



CREATE TABLE Pacientes (
 IdPaciente INT PRIMARY KEY IDENTITY(1,1), 
Dni VARCHAR(10) NOT NULL,
Apellido NVARCHAR(100) NOT NULL,
 Nombre NVARCHAR(100) NOT NULL,
FechaNacimiento DATE NULL,
Email NVARCHAR(100) NULL,
Telefono VARCHAR(20) NULL,
Direccion NVARCHAR(255) NULL,
Estado bit NOT NULL,
CONSTRAINT UQ_Pacientes_Dni UNIQUE (Dni)
);
GO


CREATE TABLE Especialidades(
IdEspecialidad INT IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(100) NOT NULL
);
GO


CREATE TABLE Medicos(
IdMedico INT IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(100)  NOT NULL,
Apellido NVARCHAR(100)  NOT NULL,
Matricula VARCHAR(50) NOT NULL,
Activo BIT NOT NULL
);
GO


CREATE TABLE Guardias(
IdGuardia INT IDENTITY(1,1) PRIMARY KEY,
Nombre NVARCHAR(50) NOT NULL,
HoraInicio TIME NOT NULL,
HoraFin TIME NOT NULL
);
GO


CREATE TABLE MedicosPorEspecialidad(
IdMedico INT NOT NULL,
IdEspecialidad INT NOT NULL,
CONSTRAINT PK_MedicoPorEspecialidad PRIMARY KEY (IdMedico, IdEspecialidad),
CONSTRAINT FK_MPE_Medico FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico),
CONSTRAINT FK_MPE_Especialidad FOREIGN KEY (IdEspecialidad) REFERENCES Especialidades(IdEspecialidad)
);
GO


CREATE TABLE MedicosPorGuardia(
IdMedico INT NOT NULL,
IdGuardia INT NOT NULL,
DiaSemana TINYINT NOT NULL DEFAULT 1,
CONSTRAINT PK_MedicoPorGuardia PRIMARY KEY (IdMedico, IdGuardia,DiaSemana),
CONSTRAINT FK_MPG_Medico FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico),
CONSTRAINT FK_MPG_Guardia FOREIGN KEY (IdGuardia) REFERENCES Guardias(IdGuardia)
);
GO


-- 6. TABLA DE TURNOS 

CREATE TABLE Turnos(
IdTurno  INT IDENTITY(1,1) PRIMARY KEY,
NumeroTurno VARCHAR(50) NOT NULL, 
FechaInicio DATE NOT NULL,
FechaFin DATE NOT NULL, 
HoraInicio TIME NOT NULL,
HoraFin TIME NOT NULL,
ObservacionesSolicitud NVARCHAR(500) NULL,
ObservacionesDiagnostico NVARCHAR(500) NULL,
IdMedico INT NOT NULL,
IdPaciente INT NOT NULL,
IdEspecialidad INT NOT NULL,
Motivo VARCHAR(50) NOT NULL,
Estado INT NOT NULL, 
CONSTRAINT FK_Turnos_Medico FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico),
CONSTRAINT FK_Turnos_Paciente FOREIGN KEY (IdPaciente) REFERENCES Pacientes(IdPaciente),
CONSTRAINT FK_Turnos_Especialidad FOREIGN KEY (IdEspecialidad) REFERENCES Especialidades(IdEspecialidad) 
);
GO


CREATE TABLE UsuariosAppxMedico (
    IdUsuariosAppxMedico INT IDENTITY(1,1) PRIMARY KEY,
    IdUsuario INT NOT NULL,
    IdMedico INT NOT NULL,
    CONSTRAINT FK_UxM_Usuario FOREIGN KEY (IdUsuario) REFERENCES UsuariosApp(IdUsuario),
    CONSTRAINT FK_UxM_Medico FOREIGN KEY (IdMedico) REFERENCES Medicos(IdMedico)
);

PRINT '* SCRIPT COMPLETADO CON ÉXITO *';
