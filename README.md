# SIE
## Sistema Integrado Educacional

O **Sistema Integrado Educacional** (SIE) permitirá facilitar o gerenciamento de notas de turmas da disciplina Engenharia de Software de diversas instituições, abordar turmas e distribuir pontos em forma de troféus e categorias. Essa aplicação afeta diretamente a professores e alunos envolvidos na disciplina em questão.
O sistema terá, como grande oportunidade, alavancar os estudos em Engenharia de Software, impulsionar o desejo dos alunos a praticar a disciplina e motivá-los a ganhar o prêmio, como um troféu. Um sistema que gere uma turma, seus alunos e salas e professores, têm muito a agregar e facilitar os processos das partes interessadas.

## Sobre o projeto

O SIE é desenvolvido usando as ferramentes [ASP.NET Core 2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.0), [PostgreSQL](https://www.postgresql.org/) e [Vue.js](https://vuejs.org/).

## Árvore de diretórios
```bash
├───Auxiliary/ #métodos auxiliares
│   ├───SessionExtensions.cs
│   └───StringExtensions.cs
├───Business/ #métodos para salvar, alterar, deletar
│   ├───BInstitution.cs
│   └───BPerson.cs
├───Context/ #métodos de contexto do banco usando migrations
│   ├───Institution.cs
│   ├───Person.cs
│   └───SieContext.cs
├───Controllers/ #métodos controlares que interagem com a interface
│   ├───DashboardController.cs
│   ├───InstitutionController.cs
│   └───PersonController.cs
├───Helpers/ #métodos para facilicar o desenvolvimento
│   └───ResponseContent.cs
├───Interface/ #diretório de interface 
│   ├───build/ #arquivos para gerar release
│   │   ├───build.js
│   │   ├───check-versions.js
│   │   ├───logo.png
│   │   ├───utils.js
│   │   ├───vue-loader.conf.js
│   │   ├───webpack.base.conf.js
│   │   ├───webpack.dev.conf.js
│   │   └───webpack.prod.conf.js
│   ├───config/ #arquivos de ambiente
│   │   ├───dev.env.js
│   │   ├───index.js
│   │   └───prod.env.js
│   ├───src/ #pasta com os arquivos source da interface
│   │   ├───assets/ #arquivos staticos
│   │   │   ├───logo.png
│   │   │   └───logo.xcf
│   │   ├───components/ #componentes do Vue.js
│   │   │   ├───dashboard/
│   │   │   │   └───Dashboard.vue
│   │   │   ├───login/
│   │   │   │   └───Login.vue
│   │   │   ├───register/
│   │   │   │   └───Register.vue
│   │   │   └───shared/
│   │   │       └───Box.vue
│   │   ├───domain/ #classes para objetos
│   │   │   ├───Institution.js
│   │   │   ├───Login.js
│   │   │   └───Person.js
│   │   ├───router/ #classe de rotas
│   │   │   └───index.js
│   │   ├───services/ #serviços de comunicação com a lógica
│   │   │   ├───DashboardService.js
│   │   │   ├───InstitutionService.js
│   │   │   ├───PersonService.js
│   │   │   └───Utils.js
│   │   ├───App.vue #componente principal para o vue
│   │   ├───bus.js #arquivo de emissão e sinais
│   │   └───main.js #arquivo principal para carregar o vue
│   ├───static/
│   │   └───.gitkeep
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
│   ├───20180415233427_InitialMigration.Designer.cs
│   ├───20180415233427_InitialMigration.cs
│   ├───20180415234528_MigrationNullable.Designer.cs
│   ├───20180415234528_MigrationNullable.cs
│   ├───20180415235014_MigrationLength.Designer.cs
│   ├───20180415235014_MigrationLength.cs
│   ├───20180415235522_MigrationLengthEmail.Designer.cs
│   ├───20180415235522_MigrationLengthEmail.cs
│   ├───20180417002210_MigrationProfile.Designer.cs
│   ├───20180417002210_MigrationProfile.cs
│   ├───20180424225210_InstitutionMigration.Designer.cs
│   ├───20180424225210_InstitutionMigration.cs
│   └───SIEContextModelSnapshot.cs
├───Models/ #classes modelos para facilicar o desenvolvimento
│   ├───MInstitution.cs
│   ├───MLogin.cs
│   ├───MModelError.cs
│   ├───MPerson.cs
│   └───MResponseContent.cs
├───Properties/ #configurações de desenvolvimento
│   └───launchSettings.json
├───SIE prototipos/ #protótipos
│   ├───SIE - Cadastro.bmpr
│   ├───SIE - Cadastro.pdf
│   ├───SIE - Dashboard Professor.bmpr
│   └───SIE - Dashboard Professor.pdf
├───Utils/ #classes de buscas do banco
│   ├───UInstitution.cs
│   └───UPerson.cs
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
