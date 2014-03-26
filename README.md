tiny-workflow
=============

Крошечная система документооборота. Я создал этот проект, для того чтобы поиграться с такими технологиями, как Entity Framework 6 и ASP.NET MVC OWIN. 

## License: GNU 2

Технологический стек:
=====================

- ASP.NET MVC 5.1 (веб-фреймворк)
- Entity Framework 6 (ORM)
- MS-SQL 2008 
- NUnit + Rhino Mocks (юнит тест фреймворк + mock фреймворк)
- ASP.NET Identity (авторизация и аутентификация пользователей)
- Bootstrap 3 (CSS фреймворк)
- Ninject (Dependency Injector для .NET)

Общая архитектура проекта. 
==========================

##Веб-приложение состоит из 5 проектов. 

1. Bizagi - ASP.NET MVC приложение. Используется в качестве Presentation layer (т.е. выполняет функции приёма запросов от пользователя, их передресации на нижние слои и возрващения результата пользователям). 
2. Domain - описание предметной области. Этот проект содержит POCO классы (для Code-first подхода), а также Контракты - Интерфейсы того, какие бизнес функции должны быть реализованы в Сервисах. 
3. Infrastructure - проект, работающий с базой данных. Реализует два паттерна, которые обычно используются в связке: Unit of Work и Repository. Данный проект предоставляет Репозитории для всех POCO-классов. 
4. Service - реализация Контактов, описанных в Domain. Проект Service с помощью Infrastructure реализует весь нужный бизнес-функционал. 

Зависимости проектов:
=====================

1. Bizagi зависит от Domain.
2. Domain не имеет зависимостей.
3. Infrastructure зависит от Domain.
4. Service зависит от Domain и Infrastructure.

Как запустить приложение:
=========================

1. Скачать нужные nuget пакеты.
2. Изменить в файле ~/tiny-workflow/Bizagi/Web.config connectionStrings на свою.
3. Создать миграции, запустив в Package Manager Console следующий код:
    
   Add-Migration -ProjectName "Infrastructure" -StartUpProjectName "Bizagi" MyNewMigration
4. Выполнить созданные миграции:
   
   Update-Database -ProjectName "Infrastructure" -StartUpProjectName "Bizagi" -Verbose
	
Описание проекта:
=================

## Роли данного веб-приложения:
- админ;
- сотрудник;
- директор;
- бухгалтер.

### Функции админа:
- назначать роли другим пользователям;
- менять приоритеты следования заявки;

### Функции пользователя:
- создавать запрос (заявку) на выдачу некоторую суммы денег;
- просматривать свои заявки;

### Функции директора:
- утверждать завки;
- просматривать все заявки в системе;

### Фукцнии бухгалтера:
- утверждать завки;
- просматривать все заявки в системе;
- выдавать деньги;

Описание основного процесса:
============================

1. Пользователь создаёт запрос на выдачу денег, указывая причину (зачем ему нужны деньги), а также количество денег, которые ему нужны.
2. В зависимости от настройки приоритетов заявка приходит к бухгалтеру или директору.
3. 
- Если заявка была одобрена, она отправляется к другой роли.
- Если заявка была отклонена, она уходит в архив с соответсвующим статусом. 
4. Когда заявку одобрили все участники процесса, бухгалтер выдаёт по ней нужную сумму денег.