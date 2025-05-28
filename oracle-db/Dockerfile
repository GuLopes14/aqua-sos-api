FROM container-registry.oracle.com/database/express:21.3.0 

# Senha do usuário administrador SYSTEM
ENV ORACLE_PWD=senha123

# Cópia do script que cria o usuário personalizado
COPY scripts/create_user.sql /opt/oracle/scripts/setup/
