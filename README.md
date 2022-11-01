[![AppLogo](../media/WideLogo.png)](https://github.com/ReZuCoS/RouteLists/releases/)

---

![Release Last Version](https://img.shields.io/github/v/release/rezucos/routelists?label=Latest%20Version&logo=github)
![Repo Size](https://img.shields.io/github/repo-size/rezucos/routelists?label=Repo%20Size&logo=Git)
![GitHub](https://img.shields.io/github/license/rezucos/routelists)

RouteLists - приложение для создания и хранения маршрутных листов, данных о водителях,
автомобилях компании, а так же менеджерах компаний-партнёров. 

# Содержание

- [Установка](#Установка)
    - [Требования](#Требования)
    - [Настройка базы данных](#Настройка-базы-данных)
    - [Установка приложения](#Установка-приложения)
        - [Служба SQL Server](#Служба-SQL-Server)
- [Описание разделов интерфейса](#Описание-разделов-интерфейса) 
- [Использование](#Использование)
    - [Первый запуск](#Первый-запуск)
    - [Добавление информации об автомобиле](#Добавление-информации-об-автомобиле)

# Установка

В данной главе описываются требования и процесс установки RouteLists.

## Требования

Для установки и использования приложения требуется установленный [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), 
а так же установленный и обновлённый [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework),
версии [4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) и выше. Также для использования
приложения потребуется наличие прав администратора у пользователя(требуется для включения и отключения
системной службы SQL Server).

## Настройка базы данных

Для работы приложения требуется развернуть базу данных на SQL Server используя скрипт,
расположенный в файле `DatabaseScript.sql`. Сделать это можно с помощью команды:

```powershell
sqlcmd -S "имя_компьютера"\"имя_сервера" -i "Путь_до_файла_DatabaseScript.sql"
```

## Установка приложения

Для установки приложения требуется двойным нажатием выполнить файл `Setup.exe`
и проследовать шагам установки.

### Служба SQL Server

Если название службы базы данных отличается от **`MSSQL$SQLEXPRESS`**, требуется внести изменение в файл `RouteLists.exe.config`, расположенный в папке установки. Ниже указан параметр, который требуется изменить:

```xml
<add key="ServiceName" value="название_службы_SQL_Server" />
```

# Описание разделов интерфейса

[![Appication Sections](../media/AppSection.png)](https://github.com/ReZuCoS/RouteLists/releases/)

![#d198bd](https://placehold.co/20x20/d198bd/d198bd) Раздел выбора таблицы

![#80edbe](https://placehold.co/20x20/80edbe/80edbe) Раздел поиска и фильтрации

![#bacde2](https://placehold.co/20x20/bacde2/bacde2) Список записей

![#edcabb](https://placehold.co/20x20/edcabb/edcabb) Раздел работы с записями

## Первый запуск

При первом запуске приложения требуется заполнить поля ***Фамилия***, ***Имя***, ***Отчество***(при наличии), 
***Название компании***, ***Должность***. Также при первом запуске можно настроить оключение службы
SQL Server после завершения работы приложения.
