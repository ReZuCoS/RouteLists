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
    - [Сервер](#Сервер)
    - [Приложение](#Приложение)
- [Использование](#Использование)

# Установка

В данной главе описываются требования и процесс установки RouteLists

## Требования

Для установки и использования приложения требуется установленный [Microsoft SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), 
а так же установленный и обновлённый [.NET Framework](https://dotnet.microsoft.com/en-us/download/dotnet-framework),
версии [4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48) и выше.

## Сервер

Для работы приложения требуется развернуть базу данных на SQL Server используя скрипт,
расположенный в файле `DatabaseScript.sql`. Сделать это можно с помощью команды:

```powershell
sqlcmd -S "имя_компьютера"\"имя_сервера" -i "Путь_до_файла_DatabaseScript.sql"
```

## Приложение

Для установки приложения требуется двойным нажатием выполнить файл `Setup.exe`
и проследовать шагам установки.
