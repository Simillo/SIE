# SIE
## Sistema Integrado Educacional

[![Build Status](https://travis-ci.org/Simillo/SIE.svg?branch=master)](https://travis-ci.org/Simillo/SIE)

![SIE](/Interface/src/assets/logo.png)


O **Sistema Integrado Educacional** (SIE) permitirá facilitar o gerenciamento de notas de turmas da disciplina Engenharia de Software de diversas instituições, abordar turmas e distribuir pontos em forma de troféus e categorias. Essa aplicação afeta diretamente a professores e alunos envolvidos na disciplina em questão.
O sistema terá, como grande oportunidade, alavancar os estudos em Engenharia de Software, impulsionar o desejo dos alunos a praticar a disciplina e motivá-los a ganhar o prêmio, como um troféu. Um sistema que gere uma turma, seus alunos e salas e professores, têm muito a agregar e facilitar os processos das partes interessadas.

## Sobre o projeto

O SIE é desenvolvido usando as ferramentes [ASP.NET Core 2](https://docs.microsoft.com/en-us/aspnet/core/?view=aspnetcore-2.0), [PostgreSQL](https://www.postgresql.org/) e [Vue.js](https://vuejs.org/).

## Árvore de diretórios
```bash
│───Interface/ #diretório de interface 
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
│   ├───.babelrc #confis do babel
│   ├───.editorconfig #configs do editor
│   ├───.eslintignore #configs de ignore do lint
│   ├───.eslintrc.js #configs do lint
│   ├───.gitignore #arquivos para ignorar no git
│   ├───index.html #arquivo raiz
│   ├───package-lock.json #versionamento de pacotes
│   ├───package.json #versionamento de pacotes
├───SIE
│   ├───Auxiliary/ #métodos auxiliares
│   ├───Business/ #métodos para salvar, alterar, deletar
│   ├───Context/ #métodos de contexto do banco usando migrations
│   ├───Controllers/ #métodos controlares que interagem com a interface
│   ├───Enums/ #enums da aplicação
│   ├───Helpers/ #métodos para facilicar o desenvolvimento
│   ├───Interfaces/ #interfaces dos sistema
│   ├───Middleware/ #classes arquivos para interceptar requisições do sistema
│   ├───Migrations/ #histórico de mudanças do banco
│   ├───Models/ #classes modelos para facilicar o desenvolvimento
│   ├───Properties/ #configurações de desenvolvimento
│   ├───Services/ #serviços da aplicação
│   ├───Utils/ #classes de buscas do banco
│   ├───Validations/ #classes de validações do sistema
│   ├───Program.cs #classe de carregamento da aplicação
│   ├───SIE.csproj #versionamento de pacotes da aplicação
│   ├───SIE.sln #solução da aplicação
│   ├───Startup.cs #classe de configuração runtime da aplicação
│   ├───appsettings.Development.json #arquivo json de variáveis de desenvolvimento da aplicação
│   ├───appsettings.json #arquivo json de variáveis de release da aplicação
│   ├───web.config #arquivo de configurações
├───SIE.Scheduler #Aplicação do scheduler separado da aplicação
│   ├───Cron/ #métodos para utilizar o CRON, exemplo: "0 0 * * *" o job roda todos os dias as 00:00
│   ├───Extensions/ #métodos de extensão do cron do scheduler
│   ├───Interfaces/ #interfaces do scheduler
│   ├───Scheduler/ #principais classes do scheduler
│   │   └───Tasks/ #tarefas que serão executadas
│   ├───Program.cs #classe de carregamento da aplicação
│   ├───SIE.Scheduler.csproj #versionamento de pacotes da aplicação
│   ├───SIE.Scheduler.sln #solução da aplicação
│   ├───Startup.cs #classe de configuração runtime da aplicação
│   ├───appsettings.Development.json #arquivo json de variáveis de desenvolvimento da aplicação
│   ├───appsettings.json #arquivo json de variáveis de release da aplicação
│   ├───web.config #arquivo de configurações
├───SIE prototipos/ #protótipos
├───.gitignore #arquivos para ignorar no git
└───README.md #leia-me
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
> na pasta `/SIE/SIE/` digite `dotnet restore`, `dotnet build`, `dotnet ef database update`

> na pasta `/SIE/Interface/` digite `npm i` e logo em seguida `npm run dev`

Altere a propriedade  `setProfileEnvironment` no arquivo `%WINDIR%\System32\inetsrv\config\applicationHost.config` para:

```
<add name="DefaultAppPool" autoStart="true" enable32BitAppOnWin64="true" managedRuntimeVersion="v4.0" startMode="AlwaysRunning">
    <processModel setProfileEnvironment="true" />
</add>
```

Para executar o Job Scheduler do sistema:
> na pasta `/SIE/SIE.Scheduler/` digite `dotnet restore` e logo em seguida `dotnet run`

## Padrões
### Api
* Separação de responsabilidades
  * Controllers
  * Utils
  * Services
  * Context
  * Auxiliary/helpers
* Seguir estrutura do diretório
* Código deverá ser escrito em inglês, salvo mensagens de retorno ao usuário
* Manter o código legível
* Mensagens de feedback deverá ser enviado da Api, nunca do Client
### Client
* Usar SPA (Single Page Application)
* Components Single-File
* Código deverá ser escrito em inglês, salvo mensagens de retorno ao usuário
* Não enviar mensagem de feedback diretamente pelo Client