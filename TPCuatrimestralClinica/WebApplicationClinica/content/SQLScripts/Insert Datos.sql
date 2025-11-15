
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


INSERT INTO Pacientes (Dni, Apellido, Nombre, FechaNacimiento, Email, Telefono, Direccion, Estado)
VALUES
('40111222', 'Sosa', 'Miguel Angel', '1998-11-20', 'miguel.sosa@email.com', '11-5555-1111', 'Calle 1, Nro 11', 1),
('41222333', 'Gomez', 'Sofia', '1999-01-05', 'sofia.gomez@email.com', '11-5555-2222', 'Av. 2, Nro 22', 1),
('42333444', 'Diaz', 'Martin', '2000-02-10', 'martin.diaz@email.com', '11-5555-3333', 'Pasaje 3, Nro 33', 1),
('43444555', 'Fernandez', 'Valentina', '2001-03-15', 'valen.fer@email.com', '11-5555-4444', 'Ruta 4, Km 44', 1),
('44555666', 'Torres', 'Lucas', '2002-04-20', 'lucas.torres@email.com', '11-5555-5555', 'Calle 5, Nro 55', 1),
('45666777', 'Romero', 'Julieta', '1995-05-25', 'juli.romero@email.com', '11-5555-6666', 'Av. 6, Nro 66', 1),
('46777888', 'Acosta', 'Daniel', '1990-06-30', 'daniel.acosta@email.com', '11-5555-7777', 'Pasaje 7, Nro 77', 1),
('47888999', 'Medina', 'Camila', '1988-07-01', 'cami.medina@email.com', '11-5555-8888', 'Ruta 8, Km 88', 1),
('48999000', 'Silva', 'Nicolas', '1992-08-02', 'nico.silva@email.com', '11-5555-9999', 'Calle 9, Nro 99', 1),
('49000111', 'Benitez', 'Rocio', '1993-09-03', 'rocio.benitez@email.com', '11-5555-0000', 'Av. 10, Nro 100', 1);
GO

SELECT * FROM Pacientes;
GO

PRINT '--- Limpiando Turnos anteriores ---';
DELETE FROM Turnos;
GO

PRINT '--- Creando 30 Turnos Nuevos ---';
GO

INSERT INTO Turnos (NumeroTurno, FechaInicio, FechaFin, HoraInicio, HoraFin, ObservacionesSolicitud, ObservacionesDiagnostico, IdMedico, IdPaciente, IdEspecialidad, Motivo, Estado)
VALUES

-- TURNOS FUTUROS 

('1', DATEADD(day, 1, CAST(GETDATE() AS DATE)), DATEADD(day, 1, CAST(GETDATE() AS DATE)), '09:00:00', '09:30:00', 'Chequeo general', NULL, 1, 1, 1, 'Control', 0), -- M1-Cardio
('2', DATEADD(day, 1, CAST(GETDATE() AS DATE)), DATEADD(day, 1, CAST(GETDATE() AS DATE)), '11:00:00', '11:30:00', 'Control pediátrico', NULL, 3, 4, 3, 'Control', 0), -- M3-Pedia
('3', DATEADD(day, 2, CAST(GETDATE() AS DATE)), DATEADD(day, 2, CAST(GETDATE() AS DATE)), '15:00:00', '15:30:00', 'Consulta por erupción', NULL, 2, 2, 2, 'Consulta', 0), -- M2-Derma
('4', DATEADD(day, 3, CAST(GETDATE() AS DATE)), DATEADD(day, 3, CAST(GETDATE() AS DATE)), '10:00:00', '10:30:00', 'Control de lunar', NULL, 2, 5, 2, 'Consulta', 0), -- M2-Derma
('5', DATEADD(day, 3, CAST(GETDATE() AS DATE)), DATEADD(day, 3, CAST(GETDATE() AS DATE)), '11:30:00', '12:00:00', 'Dolor de espalda', NULL, 4, 6, 4, 'Urgencia', 0), -- M4-Trauma
('6', DATEADD(day, 4, CAST(GETDATE() AS DATE)), DATEADD(day, 4, CAST(GETDATE() AS DATE)), '08:30:00', '09:00:00', 'Apto físico', NULL, 1, 7, 1, 'Control', 0), -- M1-Cardio
('7', DATEADD(day, 5, CAST(GETDATE() AS DATE)), DATEADD(day, 5, CAST(GETDATE() AS DATE)), '14:00:00', '14:30:00', 'Vacunación', NULL, 3, 8, 3, 'Control', 0), -- M3-Pedia
('8', DATEADD(day, 5, CAST(GETDATE() AS DATE)), DATEADD(day, 5, CAST(GETDATE() AS DATE)), '16:00:00', '16:30:00', 'Tobillo hinchado', NULL, 1, 9, 4, 'Consulta', 0), -- M1-Trauma
('9', DATEADD(day, 6, CAST(GETDATE() AS DATE)), DATEADD(day, 6, CAST(GETDATE() AS DATE)), '10:00:00', '10:30:00', 'Acné', NULL, 2, 10, 2, 'Consulta', 0), -- M2-Derma
-- (Cancelados por el paciente)
('10', DATEADD(day, 2, CAST(GETDATE() AS DATE)), DATEADD(day, 2, CAST(GETDATE() AS DATE)), '10:30:00', '11:00:00', 'Dolor de muñeca', NULL, 4, 11, 4, 'Consulta', 2), -- M4-Trauma
('11', DATEADD(day, 4, CAST(GETDATE() AS DATE)), DATEADD(day, 4, CAST(GETDATE() AS DATE)), '15:30:00', '16:00:00', 'Presión alta', NULL, 1, 12, 1, 'Control', 2), -- M1-Cardio
('12', DATEADD(day, 7, CAST(GETDATE() AS DATE)), DATEADD(day, 7, CAST(GETDATE() AS DATE)), '09:30:00', '10:00:00', 'Control niño sano', NULL, 3, 1, 3, 'Control', 2), -- M3-Pedia


 --(Para el Dashboard)

('13', DATEADD(day, 0, CAST(GETDATE() AS DATE)), DATEADD(day, 0, CAST(GETDATE() AS DATE)), '09:00:00', '09:30:00', 'Electrocardiograma', NULL, 1, 13, 1, 'Estudio', 0), -- M1-Cardio (Pendiente)
('14', DATEADD(day, 0, CAST(GETDATE() AS DATE)), DATEADD(day, 0, CAST(GETDATE() AS DATE)), '10:00:00', '10:30:00', 'Fiebre niño', 'Se receta paracetamol', 3, 14, 3, 'Urgencia', 1), -- M3-Pedia (Completado)
('15', DATEADD(day, 0, CAST(GETDATE() AS DATE)), DATEADD(day, 0, CAST(GETDATE() AS DATE)), '15:00:00', '15:30:00', 'Consulta dermatológica', NULL, 2, 15, 2, 'Consulta', 0), -- M2-Derma (Pendiente)
('16', DATEADD(day, 0, CAST(GETDATE() AS DATE)), DATEADD(day, 0, CAST(GETDATE() AS DATE)), '16:00:00', '16:30:00', 'Radiografía de tórax', 'Se deriva a especialista', 1, 3, 1, 'Estudio', 1), -- M1-Cardio (Completado)


-- TURNOS PASADOS (14)


('17', DATEADD(day, -5, CAST(GETDATE() AS DATE)), DATEADD(day, -5, CAST(GETDATE() AS DATE)), '10:30:00', '11:00:00', 'Dolor de rodilla', 'Reposo y antiinflamatorios', 4, 3, 4, 'Urgencia', 1), -- M4-Trauma
('18', DATEADD(day, -1, CAST(GETDATE() AS DATE)), DATEADD(day, -1, CAST(GETDATE() AS DATE)), '09:00:00', '09:30:00', 'Chequeo general', 'Paciente saludable', 1, 1, 1, 'Control', 1), -- M1-Cardio
('19', DATEADD(day, -2, CAST(GETDATE() AS DATE)), DATEADD(day, -2, CAST(GETDATE() AS DATE)), '15:00:00', '15:30:00', 'Consulta por erupción', 'Alergia leve, se medica', 2, 2, 2, 'Consulta', 1), -- M2-Derma
('20', DATEADD(day, -3, CAST(GETDATE() AS DATE)), DATEADD(day, -3, CAST(GETDATE() AS DATE)), '11:00:00', '11:30:00', 'Control pediátrico', 'Esquema de vacunación completo', 3, 4, 3, 'Control', 1), -- M3-Pedia
('21', DATEADD(day, -7, CAST(GETDATE() AS DATE)), DATEADD(day, -7, CAST(GETDATE() AS DATE)), '10:00:00', '10:30:00', 'Control de lunar', 'Sin anormalías', 2, 5, 2, 'Consulta', 1), -- M2-Derma
('22', DATEADD(day, -8, CAST(GETDATE() AS DATE)), DATEADD(day, -8, CAST(GETDATE() AS DATE)), '11:30:00', '12:00:00', 'Dolor de espalda', 'Contractura. Kinesiología', 4, 6, 4, 'Urgencia', 1), -- M4-Trauma
('23', DATEADD(day, -10, CAST(GETDATE() AS DATE)), DATEADD(day, -10, CAST(GETDATE() AS DATE)), '08:30:00', '09:00:00', 'Apto físico', 'Aprobado', 1, 7, 1, 'Control', 1), -- M1-Cardio
('24', DATEADD(day, -12, CAST(GETDATE() AS DATE)), DATEADD(day, -12, CAST(GETDATE() AS DATE)), '14:00:00', '14:30:00', 'Vacunación', 'Se aplican vacunas de calendario', 3, 8, 3, 'Control', 1), -- M3-Pedia
('25', DATEADD(day, -15, CAST(GETDATE() AS DATE)), DATEADD(day, -15, CAST(GETDATE() AS DATE)), '16:00:00', '16:30:00', 'Tobillo hinchado', 'Esguince leve', 1, 9, 4, 'Consulta', 1), -- M1-Trauma
('26', DATEADD(day, -20, CAST(GETDATE() AS DATE)), DATEADD(day, -20, CAST(GETDATE() AS DATE)), '10:00:00', '10:30:00', 'Acné', 'Tratamiento tópico', 2, 10, 2, 'Consulta', 1), -- M2-Derma

('27', DATEADD(day, -1, CAST(GETDATE() AS DATE)), DATEADD(day, -1, CAST(GETDATE() AS DATE)), '10:30:00', '11:00:00', 'Dolor de muñeca', 'Paciente no se presentó', 4, 11, 4, 'Consulta', 2), -- M4-Trauma
('28', DATEADD(day, -2, CAST(GETDATE() AS DATE)), DATEADD(day, -2, CAST(GETDATE() AS DATE)), '15:30:00', '16:00:00', 'Presión alta', 'Paciente cancela por viaje', 1, 12, 1, 'Control', 2), -- M1-Cardio
('29', DATEADD(day, -4, CAST(GETDATE() AS DATE)), DATEADD(day, -4, CAST(GETDATE() AS DATE)), '09:30:00', '10:00:00', 'Control niño sano', 'Paciente cancela', 3, 1, 3, 'Control', 2), -- M3-Pedia
('30', DATEADD(day, -6, CAST(GETDATE() AS DATE)), DATEADD(day, -6, CAST(GETDATE() AS DATE)), '17:00:00', '17:30:00', 'Verrugas', 'Paciente no asiste', 2, 5, 2, 'Consulta', 2); -- M2-Derma
GO

PRINT '--- 30 Turnos Creados ---';
SELECT * FROM Turnos;
GO