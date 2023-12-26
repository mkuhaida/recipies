# Лабораторная работа 2
***Разработка модуля справочной информации бизнес-приложения***

***Кугайдо Марина Александровна, 2023, 4 курс, 4 группа***
## Шаг 1.
### Справочник 1. "Пользователь"
- ID пользователя
- Email
- Номер телефона
- ФИО
- Дата рождения
- Уровень пользователя (возможные варианты (в порядке возрастания уровня): новичок, продвинутый, опытный, профи, мастер)


### Справочник 2. "Рецепт"
- ID рецепта
- ID пользователя, создавшего рецепт
- Описание
- Раздел еды (возможные варианты: завтраки, обеды, ужины, десерты, закуски)
- Уровень сложности рецепта (по пятибалльной шкале, значение с фиксированной запятой)
- Дата создания рецепта

## Шаг 2.
СУБД - SQLite.

Схема:
```
CREATE TABLE Users (
  Id TEXT PRIMARY KEY NOT NULL,
  FullName TEXT NOT NULL,
  Email TEXT NOT NULL,
  Phone TEXT NOT NULL,
  BirthDate DATE NOT NULL,
  UserLevel INTEGER NOT NULL
);

CREATE TABLE Recipe (
  Id TEXT PRIMARY KEY NOT NULL,
  UserId TEXT,
  Name TEXT NOT NULL,
  Description TEXT NOT NULL,
  DifficultyLevel DECIMAL(4, 2) NOT NULL,
  CreatedAt DATETIME NOT NULL,
  Section INTEGER NOT NULL,
  FOREIGN KEY (UserId) REFERENCES Users (Id) ON DELETE SET NULL
);

```

**Пример заполненных таблиц**

Рецепты:

| Id                                   | UserId                               | Name                  | Description                                                                                                                                                                                                                                                            | DifficultyLevel | CreatedAt                  | Section |
|--------------------------------------|--------------------------------------|-----------------------|------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|-----------------|----------------------------|---------|
| 1d546a78-8cb8-11ee-a4a2-80e82c270b17 | 92575b65-8cb7-11ee-a4a2-80e82c270b17 | Baked Potato Soup     | You'll find the full, step-by-step recipe below — but here's a brief overview of what you can expect when you make baked potato soup at home:  Cook the bacon.  Melt the butter, then whisk in the flour and milk.  Add the potatoes and onions and bring to a ...     | 3.30 | 2023-11-27 03:01:28.000000 | 3  |
| 1d54e2af-8cb8-11ee-a4a2-80e82c270b17 | 9257793e-8cb7-11ee-a4a2-80e82c270b17 | Club French toast     | Preheat the oven to 400 degrees F (200 degrees C). Oil a 10-inch springform pan, line with parchment, and set aside. Line a baking sheet with parchment.  Cut butternut squash into thick crosswise slices, remove seeds, and place on the lined baking sheet. ...     | 3.70 | 2023-11-27 03:01:28.000000 | 2  |
| 1d54efdc-8cb8-11ee-a4a2-80e82c270b17 | 92577cc2-8cb7-11ee-a4a2-80e82c270b17 | Chef John's BLT Pasta | Pour olive oil into a heavy skillet over medium heat, add bacon, and cook until almost crisp, 5 to 8 minutes. Turn off the heat. Hold a paper towel with tongs and mop up excess bacon grease with the paper towel, leave about 2 teaspoons bacon grease in th...      | 3.90 | 2023-11-27 03:01:28.000000 | 3  |
| 1d55002a-8cb8-11ee-a4a2-80e82c270b17 | 92577e08-8cb7-11ee-a4a2-80e82c270b17 | Draniki               | Instructions:  Grate potatoes and give the mass a good squeeze.  Put potatoes into the large bowl, add an onion, egg, flour, salt, milk, oil and pepper.  Heat some oil in the frying pan.  Put some potato mass onto the frying pan with the tablespoon. Fry dran...  | 4.20 | 2023-11-27 03:01:28.000000 | 3  |
| 1d552343-8cb8-11ee-a4a2-80e82c270b17 | 92577f7b-8cb7-11ee-a4a2-80e82c270b17 | Apple crumble         | Instructions:  Slice the peeled apples.  Pour a bit of water into the saucepan. Put the apples into the water and add 50 g of sugar.  Stew the apples for 10 minutes.  Mix the flour, butter and 50 g of sugar in a bowl to make the dough.  Place the prepared app... | 2.90 | 2023-11-27 03:01:28.000000 | 3  |

Пользователи:

| Id                                   | FullName                     | Email                        | Phone             | BirthDate  | UserLevel |
|--------------------------------------|------------------------------|------------------------------|-------------------|------------|-----------|
| 92575b65-8cb7-11ee-a4a2-80e82c270b17 | Nefedova Vasilisa Fedorovna  | nefedova.v@gmail.com         | +375(29)783-56-05 | 2000-11-12 | 1  |
| 9257793e-8cb7-11ee-a4a2-80e82c270b17 | Smirnova Alena Rostislavovna | smirnova123456@mail.ru       | +375(33)853-86-98 | 2002-07-19 | 2  |
| 92577cc2-8cb7-11ee-a4a2-80e82c270b17 | Berezina Alisa Arsenovna     | berezina.alisa@gmail.com     | +375(44)257-59-17 | 1999-02-28 | 3  |
| 92577e08-8cb7-11ee-a4a2-80e82c270b17 | Arkhipov Makar Dmitrievich   | makar9876@mail.ru            | +375(33)459-56-68 | 1990-10-18 | 4  |
| 92577f7b-8cb7-11ee-a4a2-80e82c270b17 | Gavrilov Nikolay Vasilyevich | gavrilov.nikolay3456@mail.ru | +375(29)089-34-78 | 1995-01-22 | 5  |

## Шаг 3.

Для запуска приложения необходимо скачать publish.zip и разархивировать его. Из папки запустить файл Recipes.exe. После чего по адресам https://localhost:7031 и http://localhost:5196 будет доступен сайт.
