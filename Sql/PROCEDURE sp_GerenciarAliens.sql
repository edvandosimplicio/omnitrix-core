CREATE OR ALTER PROCEDURE sp_GerenciarAlien
    @Id INT = 0,
    @Nome VARCHAR(50) = NULL,
    @Especie VARCHAR(50) = NULL,
    @Forca INT = 80,
    @PlanetaOrigem VARCHAR(50) = 'Desconhecido',
    @Galaxia VARCHAR(50) = 'Via Láctea',
    @Operacao VARCHAR(1) = 'i'
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @IdPlaneta INT;

    -- Busca ou cadastra planeta apenas no INSERT ou UPDATE
    IF @Operacao IN ('i', 'u')
    BEGIN
        SELECT @IdPlaneta = IdPlaneta
        FROM Planeta
        WHERE Nome = @PlanetaOrigem;

        IF @IdPlaneta IS NULL
        BEGIN
            INSERT INTO Planeta
                (Nome, Galaxia)
            VALUES
                (@PlanetaOrigem, @Galaxia);

            SET @IdPlaneta = CONVERT(INT, SCOPE_IDENTITY());
        END
    END

    IF @Operacao = 'i'
    BEGIN
        INSERT INTO Alien
            (Nome, Especie, ForcaBase, IdPlaneta)
        VALUES
            (@Nome, @Especie, @Forca, @IdPlaneta);

        SELECT CONVERT(INT, SCOPE_IDENTITY()) AS IdGerado;
    END

    ELSE IF @Operacao = 'u'
    BEGIN
        UPDATE Alien
        SET Nome = @Nome,
            Especie = @Especie,
            ForcaBase = @Forca,
            IdPlaneta = @IdPlaneta
        WHERE IdAlien = @Id;

        SELECT @Id AS IdGerado;
    END

    ELSE IF @Operacao = 'd'
    BEGIN
        DELETE FROM Alien
        WHERE IdAlien = @Id;

        SELECT @Id AS IdGerado;
    END
END;
GO