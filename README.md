# SIE
## Sistema Integrado Educacional

![SIE](/Interface/src/assets/logo.png)


O **Sistema Integrado Educacional** (SIE) permitirá facilitar o gerenciamento de notas de turmas da disciplina Engenharia de Software de diversas instituições, abordar turmas e distribuir pontos em forma de troféus e categorias. Essa aplicação afeta diretamente a professores e alunos envolvidos na disciplina em questão.
O sistema terá, como grande oportunidade, alavancar os estudos em Engenharia de Software, impulsionar o desejo dos alunos a praticar a disciplina e motivá-los a ganhar o prêmio, como um troféu. Um sistema que gere uma turma, seus alunos e salas e professores, têm muito a agregar e facilitar os processos das partes interessadas.

## Sobre o projeto

O SIE é desenvolvido usando as ferramentes [ASP.NET Core 2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.0), [PostgreSQL](https://www.postgresql.org/) e [Vue.js](https://vuejs.org/).

## Árvore de diretórios
```bash
├───Auxiliary/ #métodos auxiliares
├───Business/ #métodos para salvar, alterar, deletar
├───Context/ #métodos de contexto do banco usando migrations
├───Controllers/ #métodos controlares que interagem com a interface
├───Helpers/ #métodos para facilicar o desenvolvimento
├───Interface/ #diretório de interface 
│   ├───build/ #arquivos para gerar release
│   ├───config/ #arquivos de ambiente
│   ├───src/ #pasta com os arquivos source da interface
│   │   ├───assets/ #arquivos staticos
│   │   ├───components/ #componentes do Vue.js
│   │   ├───domain/ #classes para objetos
│   │   ├───router/ #classe de rotas
│   │   ├───services/ #serviços de comunicação com a lógica
│   │   ├───App.vue #componente principal para o vue
│   │   ├───bus.js #arquivo de emissão e sinais
│   │   └───main.js #arquivo principal para carregar o vue
│   ├───static/
│   ├───.babelrc
│   ├───.editorconfig
│   ├───.eslintignore
│   ├───.eslintrc.js
│   ├───.gitignore
│   ├───.postcssrc.js
│   ├───README.md
│   ├───index.html
│   ├───package-lock.json
│   └───package.json
├───Migrations/ #histório de mudanças do banco
├───Models/ #classes modelos para facilicar o desenvolvimento
├───Properties/ #configurações de desenvolvimento
├───SIE prototipos/ #protótipos
├───Utils/ #classes de buscas do banco
├───.gitignore
├───Program.cs #classe de carregamento da aplicação
├───README.md #leia-me
├───SIE.csproj
├───SIE.csproj.user
├───SIE.sln
├───Startup.cs
├───appsettings.Development.json
├───appsettings.json
└───web.config
```

## Executando a aplicação
Instale os seguintes programas:
>  [.Net Core 2 sdk](https://www.microsoft.com/net/download/dotnet-core/sdk-2.1.4)

> [PostgreSQL](https://www.postgresql.org/)
  * Será necessário adicionar o `role` do usuário `sie` com a senha `sie`.

> [Node.js](https://nodejs.org/en/)

> [Visual Studio 2017](https://www.visualstudio.com/downloads/?rr=https%3A%2F%2Fwww.google.com.br%2F)

  * Nos sistemas operacionais Linux ou MacOS também funcionará porém use o editor de preferência.
  * Para executar o build use o comando `dotnet build`.

Execute os comandos:
> na pasta `/SIE/` digite `dotnet ef database update`

> na pasta `/SIE/interface/` digite `npm i` e logo em seguida `npm run dev`
