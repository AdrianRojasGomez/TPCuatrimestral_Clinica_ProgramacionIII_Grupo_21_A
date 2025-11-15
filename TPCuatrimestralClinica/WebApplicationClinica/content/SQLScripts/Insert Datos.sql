
USE CLINICA_DB;
GO

-- Datos para UsuariosApp
INSERT INTO UsuariosApp (NombreUsuario, Clave, TipoUsuario, Estado)
VALUES
('admin', '1234', 1,1),          -- 1 = Administrador
('medico1', 'med123', 2,1),      -- 2 = Médico
('recepcion1', 'rec123', 3,1),   -- 3 = Recepcionista
('prueba', 'pass', 2,1),         -- 2 = Médico
('usuario', 'clave', 3,1);       -- 3 = Recepcionista
GO

PRINT '--- UsuariosApp Creados ---';
SELECT * FROM UsuariosApp;
GO

-- Datos para Pacientes (5 pedidos)
INSERT INTO Pacientes (Dni, Apellido, Nombre, FechaNacimiento, Email, Telefono, Direccion, Estado)
VALUES
('30111222', 'García', 'Juan', '1985-03-15', 'juan.garcia@email.com', '11-5555-1234', 'Av. Siempre Viva 123',1),
('32222333', 'Martinez', 'Maria', '1990-07-20', 'maria.martinez@email.com', '11-5555-5678', 'Calle Falsa 456',1),
('28333444', 'Lopez', 'Carlos', '1980-11-01', 'carlos.lopez@email.com', '11-5555-9012', 'Boulevard de los Sueños 789',1),
('35444555', 'Rodriguez', 'Ana', '1995-01-30', 'ana.rodriguez@email.com', '11-5555-3456', 'Pasaje del Sol 101',1),
('38555666', 'Perez', 'Laura', '2000-05-10', 'laura.perez@email.com', '11-5555-7890', 'Rivadavia 2030',1);
GO

PRINT '--- Pacientes Creados ---';
SELECT * FROM Pacientes;
GO

INSERT INTO Especialidades (Nombre)
VALUES
('Cardiología'),   -- ID 1
('Dermatología'),  -- ID 2
('Pediatría'),     -- ID 3
('Traumatología'); -- ID 4
GO

PRINT '--- Especialidades Creadas ---';
SELECT * FROM Especialidades;
GO

INSERT INTO Medicos (Nombre, Apellido, Matricula, Activo)
VALUES
('Martín', 'Gonzalez', 'MN-1001',1),  -- ID 1
('Lucía', 'Fernandez', 'MN-1002',1), -- ID 2
('Diego', 'Alvarez', 'MP-2001',1),   -- ID 3
('Valeria', 'Ruiz', 'MP-2002',1);    -- ID 4
GO

PRINT '--- Medicos Creados ---';
SELECT * FROM Medicos;
GO


INSERT INTO Guardias (Nombre, HoraInicio, HoraFin)
VALUES
('Mañana', '08:00:00', '14:00:00'), -- ID 1
('Tarde', '14:00:00', '20:00:00'),  -- ID 2
('Noche', '20:00:00', '08:00:00'); -- ID 3
GO

PRINT '--- Guardias (Horarios) Creadas ---';
SELECT * FROM Guardias;
GO

INSERT INTO MedicosPorEspecialidad (IdMedico, IdEspecialidad)
VALUES
(1, 1), -- Gonzalez (1) es Cardiologo (1)
(2, 2), -- Fernandez (2) es Dermatologa (2)
(3, 3), -- Alvarez (3) es Pediatra (3)
(4, 4), -- Ruiz (4) es Traumatologa (4)
(1, 4); -- Gonzalez (1) tambien es Traumatologo (4)
GO

PRINT '--- Relación Medicos-Especialidad Creada ---';
SELECT * FROM MedicosPorEspecialidad;
GO


INSERT INTO MedicosPorGuardia (IdMedico, IdGuardia)
VALUES
(1, 1), -- Gonzalez (1) hace guardia Mañana (1)
(2, 2), -- Fernandez (2) hace guardia Tarde (2)
(3, 1), -- Alvarez (3) hace guardia Mañana (1)
(4, 3); -- Ruiz (4) hace guardia Noche (3)
GO

PRINT '--- Relación Medicos-Guardia Creada ---';
SELECT * FROM MedicosPorGuardia;
GO


-- Datos para Turnos
-- Uso GETDATE() para que las fechas sean actuales (mañana, pasado mañana, etc.)
INSERT INTO Turnos (NumeroTurno, FechaInicio, FechaFin, HoraInicio, HoraFin, ObservacionesSolicitud, ObservacionesDiagnostico, IdMedico, IdPaciente, IdEspecialidad, Motivo, Estado)
VALUES
-- Turno 1: Paciente 1 (Garcia) con Medico 1 (Gonzalez - Cardio)
(1, DATEADD(day, 1, CAST(GETDATE() AS DATE)), DATEADD(day, 1, CAST(GETDATE() AS DATE)), '09:00:00', '09:30:00', 'Chequeo general', NULL, 1, 1, 1, 'Control', 1),
            
-- Turno 2: Paciente 2 (Martinez) con Medico 2 (Fernandez - Derma)
(1, DATEADD(day, 2, CAST(GETDATE() AS DATE)), DATEADD(day, 2, CAST(GETDATE() AS DATE)), '15:00:00', '15:30:00', 'Consulta por erupción', NULL, 2, 2, 2, 'Consulta', 1),

-- Turno 3: Paciente 3 (Lopez) con Medico 4 (Ruiz - Trauma) - Pasado (Completado)
(1, DATEADD(day, -5, CAST(GETDATE() AS DATE)), DATEADD(day, -5, CAST(GETDATE() AS DATE)), '10:30:00', '11:00:00', 'Dolor de rodilla', 'Reposo y antiinflamatorios', 4, 3,3, 'Urgencia', 1),
            
-- Turno 4: Paciente 4 (Rodriguez) con Medico 3 (Alvarez - Pedia)
(2, DATEADD(day, 1, CAST(GETDATE() AS DATE)), DATEADD(day, 1, CAST(GETDATE() AS DATE)), '11:00:00', '11:30:00', 'Control pediátrico', NULL, 3, 4,4,'Control', 0);
GO

PRINT '--- Turnos Creados ---';
SELECT * FROM Turnos;
GO