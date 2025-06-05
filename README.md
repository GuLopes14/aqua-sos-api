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

1. **API REST (.NET 8 / ASP.NET Core)**  
   - Cadastro de pontos de distribuição de água  
   - Registro de solicitações de abastecimento  
   - Controle de estoque e status das entregas  

2. **Aplicativo Mobile (React Native)**  
   - Usuários podem solicitar água e acompanhar o status do pedido  
   - Voluntários ou gestores visualizam solicitações e atualizam status  
   - Interface simples, acessível e responsiva  

3. **Sistema IoT (Arduino/ESP32 via Wokwi)**  
   - Sensores simulam o nível de água em reservatórios  
   - LED e buzzer indicam níveis críticos  
   - Comunicação via MQTT com Node-RED, que encaminha alertas para a API  

4. **Dashboard (Node-RED)**  
   - Visualização em tempo real dos níveis de água por ponto de distribuição  
   - Alerta automático para pontos com níveis abaixo de 20%  
   - Interface administrativa para tomada de decisão rápida  

5. **Containerização (Docker)**  
   - API e banco de dados rodando em containers separados  
   - Persistência de dados garantida com volumes  
   - Deploy automatizado e replicável em qualquer ambiente  

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
https://localhost:[PORTA FORNECIDA PELA APLICAÇÃO]/swagger
```

Para acessar a interface gráfica:

```
https://localhost:[PORTA FORNECIDA PELA APLICAÇÂO]/Home
```

Você poderá visualizar e testar todas as rotas da API diretamente pelo navegador.

---

