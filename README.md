# AquaSOS 🌊

## Integrantes

- Gustavo Lopes Santos da Silva - RM: 556859  
- Renato de Freitas David Campiteli - RM: 555627  
- Gabriel Santos Jablonski - RM: 555452  

## Problemática

Eventos extremos como ondas de calor, seca severa ou enchentes comprometem diretamente o acesso à água potável, principalmente em comunidades carentes ou áreas isoladas. Em muitos casos, a infraestrutura local é afetada, os sistemas de abastecimento são interrompidos e não há monitoramento em tempo real dos recursos disponíveis, dificultando a coordenação de socorro.

A escassez de água em cenários de desastre representa não apenas desconforto, mas também riscos graves à saúde pública, como desidratação, contaminação e proliferação de doenças.

## Nossa Solução: AquaSOS

AquaSOS é uma plataforma integrada que visa monitorar, organizar e facilitar o acesso à água potável durante eventos extremos. A solução combina tecnologia acessível com inovação social para apoiar comunidades afetadas e auxiliar equipes de apoio.

## Componentes da Solução

 **API REST (.NET 8 / ASP.NET Core)**  
   - Cadastro de pontos de distribuição de água  
   - Registro de solicitações de abastecimento  
   - Controle de estoque e status das entregas  


## Como rodar o projeto localmente (API)

### 1. Clone o repositório

```bash
git clone https://github.com/renatofdavidc/aqua-sos-api.git
cd AquaSOSProject
```

### 2. Configure o banco de dados

Edite o arquivo `appsettings.json` com sua string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=AquaSOSDb;User Id=usuario;Password=senha;"
}
```

### 3. Execute as migrações do Entity Framework

```bash
dotnet ef database update
```

> Caso não tenha o `dotnet-ef` instalado:
>
> ```bash
> dotnet tool install --global dotnet-ef
> ```

### 4. Inicie o projeto

```bash
dotnet run
```

### 5. Acesse o Swagger ou as Razor Pages

Para acessar o Swagger:

```
https://localhost:5263/swagger
```

Para acessar a interface gráfica:

```
https://localhost:5263/
```

Você poderá visualizar e testar todas as rotas da API diretamente pelo navegador.

---

Vamos fazer um teste agora.
### 6.1 - Acessando a tela inicial

Ao acessar a url, somos apresentados à tela inicial:

![image](https://github.com/user-attachments/assets/8d26aba5-7df9-4d34-a2c4-0b02f6d0563c)

### 6.2 - Criando um novo usuário

Vamos acessar a página de usuários, e criar um novo usuário:

![image](https://github.com/user-attachments/assets/15fc120d-84de-4277-acac-c9f508cf5f4e)
![image](https://github.com/user-attachments/assets/72f3dbd9-365a-4fca-9f48-9284319f0e4e)
![image](https://github.com/user-attachments/assets/4de9fda8-a140-4f75-848e-f618b2f33ad7)

### 6.3 - Criando pontos de distribuição

Agora, vamos registrar um novo ponto de distribuição:

![image](https://github.com/user-attachments/assets/9faff825-39aa-4eef-8d58-442cf79b327a)
![image](https://github.com/user-attachments/assets/35b96001-4907-4ce6-9f3d-26b7ce5ab712)
![image](https://github.com/user-attachments/assets/5da8b5f4-d630-4a3f-a8ee-1370246715a4)

### 6.4 - Fazendo um pedido de água

Vamos fazer um pedido para o usuário Tranquilotech, no ponto amigos da água:

![image](https://github.com/user-attachments/assets/664d2dbb-7beb-45a1-ab45-8f73b42e0920)
![image](https://github.com/user-attachments/assets/86a16ad9-f1bb-46f3-b964-7de857d6c15f)
![image](https://github.com/user-attachments/assets/44ddbb31-ea42-420e-ba37-6d50a9525ec2)

### 6.5 - Editando dados

Caso necessário, podemos também editar qualquer informação que cadastramos:

![image](https://github.com/user-attachments/assets/ab7aaf6e-e07e-41ea-bfbf-e58ddb055c3a)

### 6.7 - Deletando dados

Ou também, podemos deletar as informações:

![image](https://github.com/user-attachments/assets/a5e86616-a060-444a-8f5f-832be4fa72de)
