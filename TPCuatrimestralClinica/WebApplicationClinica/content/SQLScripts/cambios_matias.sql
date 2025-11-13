-- GUARDIAS: horarios correctos + sin duplicados + UNIQUE
------------------------------------------------------------
-- Corrige horarios oficiales
UPDATE Guardias SET HoraInicio='06:00:00', HoraFin='14:00:00' WHERE Nombre='Mañana';
UPDATE Guardias SET HoraInicio='14:00:00', HoraFin='22:00:00' WHERE Nombre='Tarde';
UPDATE Guardias SET HoraInicio='22:00:00', HoraFin='06:00:00' WHERE Nombre='Noche';

-- Dejar 1 fila por Nombre (conserva menor Id)
DELETE FROM Guardias
WHERE IdGuardia NOT IN (
  SELECT MIN(IdGuardia) FROM Guardias GROUP BY Nombre
);
-- Dejar 1 fila por Nombre (conserva menor Id)
DELETE FROM Especialidades
WHERE IdEspecialidad NOT IN (
  SELECT MIN(IdEspecialidad) FROM Especialidades GROUP BY Nombre
);

-- Agregar 3 nuevas si faltan
IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre='Neurología')
  INSERT INTO Especialidades (Nombre) VALUES ('Neurología');

IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre='Ginecología')
  INSERT INTO Especialidades (Nombre) VALUES ('Ginecología');

IF NOT EXISTS (SELECT 1 FROM Especialidades WHERE Nombre='Oftalmología')
  INSERT INTO Especialidades (Nombre) VALUES ('Oftalmología');

-- 1️⃣ Agregar columna "Activo" a la tabla Médicos
ALTER TABLE Medicos
ADD Activo BIT NOT NULL DEFAULT 1;
GO

-- 2️⃣ Eliminar la clave primaria actual de MedicosPorGuardia
ALTER TABLE MedicosPorGuardia
DROP CONSTRAINT PK_MedicoPorGuardia;
GO

-- 3️⃣ Crear nueva clave primaria compuesta (IdMedico, IdGuardia, DiaSemana)
ALTER TABLE MedicosPorGuardia
ADD CONSTRAINT PK_MedicoPorGuardia
PRIMARY KEY (IdMedico, IdGuardia, DiaSemana);
GO