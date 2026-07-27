CREATE TABLE Alien
(
    IdAlien INT PRIMARY KEY IDENTITY(1,1),
    Nome VARCHAR(50) NOT NULL,
    Especie VARCHAR(50) NOT NULL,
    ForcaBase INT NOT NULL,
    IdPlaneta INT NOT NULL,

    CONSTRAINT CHK_Forca CHECK (ForcaBase > 0),

    CONSTRAINT FK_Alien_Planeta FOREIGN KEY (IdPlaneta)
        REFERENCES Planeta(IdPlaneta)
);
GO