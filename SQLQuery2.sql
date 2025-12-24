INSERT INTO AdminUsers (Email, MotDePasseHash)
VALUES (
    'walidwaoua@gmail.com',
    CONVERT(VARCHAR(64), HASHBYTES('SHA2_256', 'walidwalid'), 2)
);