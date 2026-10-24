# ASP.NET Boilerplate

This boilerplate project is to demonstrate some various setups when building APIs with Microsoft technology stacks.
And will be using an n-tier architecture model to break up projects into services, libraries, etc.
Also will be using both Core 2.1 LTS and 3.1 LTS SDK, along with Net Standard and ASP.Net Framework.

As of this writing, Core 2.1 is supported in Visual Studio 2017 and 2019. Core 3.1 is supported in Visual Studio 2019 ONLY.
May also use Visual Code, which the details are outside of this document.

When compiling/building project, will need to adjust for the proper SDK version. Do this by editing the `global.json` file.

> NOTE! May need to delete the .vs directory when switching SDK/projects

## Main Application - Entry Points / Highlights

- AES
  - Console application tool encrypt/decrypt text using American Encryption Standards (AES)
- CorsClient
  - HTML project to test various CORS OPTIONS preflight against a Window Authentication web server via Fetch API and axios.
- IISExpCORS
  - Setup project using Wix to create a MSI to setup IIS Express to use `IIS CORS Module`
- NeuParser
  - Console application to read \*.neu files generated from a 3rd-party app
- VTSC.Watcher
  - console application to watch \*.vue files and convert to a Typescript equivalent.
- WebApp.Api - NET Core 2.1

  - Logger
  - Thycotic
  - WkHtmlToPDF w/ RazorLight (a NET Core converse)
  - Windows Authentication w/ Netcenter
  - General Swagger page setup
  - User's roles injected from 3rd party API (Netcenter) upon every network request
  - SMTP and EMail

- WebApp.AppGallery - WebSite

  - Typescript
  - XML database
  - Encryption/Decryption with AES
  - IIS Handler
  - Jquery
  - Bootstrap

- WebApp.FW - NET Framework 4.7

  - Thycotic
  - Documentum
  - Oracle
  - CORS
  - Netcenter Role Provider / Authorize
  - SMTP and EMail
  - Grant access to `App_Data` by using publishing with \*.wpp.targets
  - Swagger
  - Emailing Execptions
  - `FileResult` ApiController extension

- WebApp.GQL - NET Core 3.1

  - GraphQL (EntityGraphQL, custom nuget package with my extras)
  - GraphiQL
  - JWT
  - SQLite database
  - CORS (w/ custom origin)
  - Simple Use of WebSocket (non-SignalR)

- WebApp.Mvc - NET Framework 4.7

  - Generic Repository service
  - Razor Helpers
  - RazorToPdf library

- WebApp.Nuget - Net Framework 4.6

  - On-premise Nuget feed server

- WebApp.PlugableApi - NET Framework 4.7.2
  - API Routeable plugins
  - Swagger documentation

# WebApp.BusinessObjects [NetStandard2.0;NET47]

This project contains a set of shared classes that is used in many others projects. Often times in a N-Tier architect, objects needs to be pass around and also helps avoid
mapping similar objects between namespaces.

Since other projects reference this project, able to include Nuget packages here and other project are able to piggy-back. Example of this is `EntityGraphQL`, of
nuget package for using GraphQL library. Seems to apply in NET Core, not Framework projects.

# WebApp.DB [SQL Project]

At the heart of just about every project, lies a database. This SQL project builds and publish a database to MSLocalDB and be used throughout various project within
this solution. SQL project are great to maintain version control and simple testing for local development.

# WebApp.Extensions [NetStandard2.0]

Extensions are a great way to enhance objects by chaining additional functions.

> NOTE: Attempted to include common NET.Core extensions within this projects. But the project required many more dependencies than I cared to add.

# WebApp.Infrastructure [NetStandard2.0]

This project houses the main services used throughout the other projects in this solution.  
Some listed services:

- General Application
- Documentum
- Email
- GraphQl
- PDF
- Repositories (may consist of DBContext, Concrete Repos, and/or Generic Repos)
- Security

# WebApp.Nuget [NET46]

This application uses a nuget package to create a Nuget feed server. Must use Framework v4.6.0. Nuget packages can be copied/paste into the `Packages` directory.

Packages -> Readme.txt

```
To add packages to the feed put package files (.nupkg files) in this folder.
NuGet Server will automatically copy the package to the correct location.
```

# WebApp.PDF [NET47]

This project demonstrate how to create an assembly (DLL) library that embeds PDF as a resource. PDFs are fillable form that returns flatten.

# WebApp.PlugableApi

Demonstrate how to setup an API to handle routable plugins with Swagger documentation.
See README.md in the plugins directory for details on creating a Class project plugin.
The Class project is basically a controller portion of the API.

> Be sure to select XML documentation file option. Properties -> Build -> Output. Set path to `\Plugins\PROJECTNAME.xml`.

# WebApp.RazorToPdf [NET47]

This project is a clone from Github [repo](https://gitub.com/andyhutch77/MvcRazorToPdf). Added support for Headers/Footers among other things.

# WebApp.SqlHander [NET47]

This project is class library to run managed code within a SQL Stored Procedure. Was trying to fetch data from a RESTful API. I was unable to reference the DLL
in SQL server due to company's permission. Saved this in-case another use-case my appear.

# WebApp.Utilities [NetStandard2.0;NET47]

Provides basic Active Directory functionality

TIP: Getting User Identity

```
Window application:
	Environment.UserDomainName + '\\' + Environment.UserName

Web application:
	Page.HttpContext.User.Identity.Name

	IMPORTANT! Must set IISExpress to Window authentication and apply code below in web.config.

	<authentication mode="Windows"/>
    <authorization>
      <deny users="?"/>
    </authorization>
```

# WebApp.WebSocketClient [Empty Web Project]

This project started with an Empty Web Project to test out WebSocket against either `WebSocketServer` or `WebApp.GQL` projects.

# WebApp.WindowService [NET47]

This project is a console application setup to run as a Windows Service. This is bare project, so just add some code.

# WebApp.WkHtmlToPdf [NetStandard2.0]

This project is a wrapper for [WK<html>ToPdf](https://wkhtmltopdf.org) and was converted to NET Core from a nuget package
[NReco.PdfGenerator](https://www.nuget.org/packages/NReco.PdfGenerator/) that currently only supported NET Framework.  
I especially like this package because it included the wkhtmltopdf.exe and DLLs as a resource. Which automatically setup upon using the library.

Upgraded WkHtmlToPdf version to 0.12.6 from 0.12.4 on 7/28/2020 following release on June 11, 2020.

# WebSocketServer [NET47]

A console application to act has a WebSocket server.

# NPM.Server - [NET Core 6.0]

Simple on-premise NPM server in .NET 6.0 with either flat-file (JSON) or SQLite support. This project has been update from 3.1 -> 5.0 -> 6.0.

We (my company) needed a on-premise NPM server to house some of our NPM packages. Similar projects runs on NodeJs, which wasn't a good fit.

> See `README.md` within the project directory for more details

## Terms:

LTS � Long Term Support
SDK � Software Development Kit
