 Integrantes

Guilherme Costeira Braganholo – RM560628

Julio – RM560494

Gabriel Nakamura – RM560671



Sobre o Projeto

Essa é a parte .NET do projeto NextPath, que faz o painel administrativo usado para gerenciar os cursos da plataforma.

Aqui foi criada uma API em ASP.NET Core, separada em camadas (Api, Application, Domain e Infrastructure) para deixar o código organizado e fácil de manter.

A API permite cadastrar cursos, editar, listar, buscar e apagar.
Tudo foi documentado no Swagger, e o banco de dados é criado automaticamente quando a API sobe.

Tecnologias

.NET 8

ASP.NET Core Web API

Entity Framework Core

SQL Server LocalDB

Swagger


 Funcionalidades principais

Criar curso

Editar curso

Excluir curso

Listar todos os cursos

Pesquisar com filtros e paginação

Endpoints estão disponíveis no Swagger quando o projeto roda.

▶ Como rodar

Abra a solução no Visual Studio 2022

Marque NextPath.Api como Startup Project

Aperte F5

Abra no navegador:

http://localhost:5000/swagger