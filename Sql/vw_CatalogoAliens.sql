CREATE OR ALTER VIEW vw_CatalogoAliens
AS
    SELECT
        A.IdAlien,
        A.Nome AS NomeAlien,
        A.Especie,
        A.ForcaBase,
        P.Nome AS PlanetaOrigem,
        P.Galaxia

    FROM Alien A
        INNER JOIN Planeta P ON A.IdPlaneta = P.IdPlaneta;
GO