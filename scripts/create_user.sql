-- Criação do usuário da aplicação
CREATE USER aquasos IDENTIFIED BY aquasos123;

-- Permissões básicas de desenvolvimento
GRANT CONNECT, RESOURCE TO aquasos;

-- Liberação de espaço no tablespace padrão
ALTER USER aquasos DEFAULT TABLESPACE USERS QUOTA UNLIMITED ON USERS;
