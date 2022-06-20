<h1 align="center">
    <img src="https://user-images.githubusercontent.com/31922891/174683394-143a82f2-0085-445b-8212-6873ba7ad110.png" aling="center" />
</h1>
<p align="center">🚀 API em .Net Core para disponibilizar todas as feiras livres cadastradas em São Paulo</p>




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

<h4 align="center"> 
	🚧  API 🚀 Finalizada  🚧
</h4>

### Features

- [x] Obter todas as feiras cadastradas
- [x] Obter feiras por nome
- [x] Cadastro de Feira
- [x] Edição de Feira
- [x] Upload de CSV
- [x] Exclusão de feira

### Autor
---


 <img style="border-radius: 50%;" src="https://avatars.githubusercontent.com/u/31922891?s=96&v=4" width="100px;" alt=""/>
 <br />
 <sub><b>Anderson Moroni</b></sub>


Feito com ❤️ por Anderson Moroni 👋🏽 Entre em contato!

[![Linkedin Badge](https://img.shields.io/badge/-Anderson-blue?style=flat-square&logo=Linkedin&logoColor=white&link=linkedin.com/in/anderson-moroni-03b075b5/)](linkedin.com/in/anderson-moroni-03b075b5/)
[![Gmail Badge](https://img.shields.io/badge/-andersonmoroni92@gmail.com-c14438?style=flat-square&logo=Gmail&logoColor=white&link=mailto:andersonmoroni92@gmail.com)](mailto:andersonmoroni92@gmail.com)
