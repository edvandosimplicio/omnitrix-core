INSERT INTO Planeta
    (Nome, Galaxia)
VALUES
    ('Pyros', 'Via Láctea'),
    ('Khoros', 'Via Láctea'),
    ('Galvan Prime', 'Via Láctea');
GO

INSERT INTO Alien
    (Nome, Especie, ForcaBase, IdPlaneta)
VALUES
    ('Chama', 'Pyronita', 75, 1),
    ('Quatro Braços', 'Pyronita', 95, 2),
    ('Massa Cinzenta', 'Pyronita', 10, 3);
GO