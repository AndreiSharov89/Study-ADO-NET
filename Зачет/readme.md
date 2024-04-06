# Приложение для работы с почтовыми индексами
##### Выполнено по зачетному заданию курса Технология ADO.NET:
Разработать настольное Windows-приложение, предоставляющее следующие функциональные возможности:
1. Подключение к БД с выбранным пользователем и паролем
2. Чтение и вывод на экран содержимого одной или нескольких таблиц БД
3. Изменение данных в таблицах
4. Добавление новых и/или удаление данных в таблицы БД
##### Источник данных:
[GitHub Zeeshanahmad4](https://github.com/Zeeshanahmad4/Zip-code-of-all-countries-cities-in-the-world-CSV-TXT-SQL-DATABASE)   [Google Drive](https://drive.google.com/drive/folders/1mN47iWtoVVqBUNuUiFeq7UQ65yAv-fps)
Zip Code Data for All Countries and Cities in the World
This repository contains a comprehensive list of zip codes (postal codes) for countries and cities around the world. The data is available in CSV, TXT, SQL, and database formats, which you can download from Here
##### Приведение данных:
Скриптом питона (папка etl-zip) собираем .CSV таблицы по нашему пожеланию, с нашими первичными ключами, денормализованные.
Скриптом .SQL для BD postgres собираем базу данных, импортируя таблицы из файлов COUNTRIES_OUT.CSV ZIPADDRESSES2.CSV ZIPCOORDS2.CSV.
Само прилодение WinForms расположено в папке zip-codes.
##### Учетные записи:
zip_user :: user - права на чтение;
zip-admin :: admin - права на запись;
##### Добавление новых данных
Производится выбором страны, введением индекса (которого нет в БД), затем нажатием "Добавить" индекс добавляется. Снова жмём кнопку "Найти" - получаем поля для редактирования.
Кнопка "Изменить" производит запись изменений в БД.
##### Модель
Зная страны и индексы приложение позволяет расчитать расстояние между почтовыми отделениями.
