<h1 align="center">
    <img src="https://user-images.githubusercontent.com/31922891/174677868-7afae169-fc1c-4583-8ebb-1d7042981d45.png" aling="center" />
</h1>
<p align="center">🚀 API para disponibilizar todas as feiras livres cadastradas em São Paulo</p>


### Pré-requisitos

Antes de começar, você vai precisar ter instalado em sua máquina as seguintes ferramentas:
Além disto é bom ter um editor para trabalhar com o código como [VisualStudio](https://code.visualstudio.com/)

### 🎲 Rodando a API 

```bash
# Clone este repositório
$ git clone <https://github.com/andersonmorony/FreeMarketAPI>

# Acesse o projeto pelo visual studio
# Após abrir o projeto restore o nuget

# Configure o banco de dados
$ Update-migration



```
### 🎲 verificando o teste coverage 

```bash
# Acesse a pasta principal do projeto com o terminal

# Execute o comando a baixo para gerar o xml
$ dotnet test --verbosity minimal --collect:"XPlat Code Coverage"

# Execulte o próximo comando para gerar os arquivos HTML
$ reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coverage_report

# Acesse a pasta coverage_report e abrar o index.html

```
