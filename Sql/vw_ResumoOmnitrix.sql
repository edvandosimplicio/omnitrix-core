CREATE OR ALTER VIEW vw_ResumoOmnitrix
AS
    SELECT
        COUNT(IdAlien) AS TotalAliensDesbloqueados,
        AVG(ForcaBase) AS ForcaMediaDoOmnitrix,
        MAX(ForcaBase) AS ForcaDoMaisForte,
        MIN(ForcaBase) AS ForcaDoMaisFraco

    FROM Alien;
GO