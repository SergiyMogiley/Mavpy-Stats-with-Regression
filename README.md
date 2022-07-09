# Mavpy-Stats-with-Regression
Analysis of Cherkasy Mavpy statistics (seasons 2013-14, 2014-15) using regression and ML.NET

Задача: розробити модель аналізу статистики матчів баскетбольної команди (навчальна вибірка), оцінити її (тестова вибірка) та спрогнозувати результат наступного матчу (поза навчальною вибіркою – першого матчу тестової вибірки).
Засоби: Visual Studio, ML.NET (C#/.NET).
Алгоритм машинного навчання: регресія.
Приклад програмного коду та аналізу даних за обраним алгоритмом машинного навчання: https://docs.microsoft.com/en-us/dotnet/machine-learning/tutorials/predict-prices
Дані (джерело):
-	Команда (сайт): «Черкаські мавпи» (http://mavpabasket.com)
-	Сезони: 2013-14, 2014-15 рр.
Розшифровка даних csv-файлів:
Features:
-	location: домашній (1) або виїзний (0) матч;
-	2x_shots: кількість спроб 2-очкових кидків;
-	3x_shots: кількість спроб 3-очкових кидків;
-	reboundings: загальна кількість підбирань м’яча;
-	interceptions: кількість перехоплень м’яча;
-	losses: кількість втрат м’яча;
-	fouls: кількість персональних фолів.
Label: scores_dif – різниця між кількістю закинутих та пропущених очок (додатня різниця означає перемогу, від’ємна – поразку, нульова – переведення матчу в overtime).
Частка навчальної вибірки в загальній кількості даних: близько 85%.
Результати роботи обраного алгоритму машинного навчання на різних вибірках:
1. Аналіз даних сезону 2013-14 рр.
Співвідношення навчальної вибірки до тестової: 22:4 (84.6% та 15.4%).
RSquared Score: 0.31; Root Mean Squared Error: 8.33;
Predicted scores dif: -3.3636; actual scores dif: -6.
2. Аналіз даних сезону 2014-15 рр.
Співвідношення навчальної вибірки до тестової: 26:4 (86.7% та 13.3%).
RSquared Score: -0.14; Root Mean Squared Error: 11.21;
Predicted scores dif: -7.4167; actual scores dif: -9.
3. Аналіз даних сезонів 2013-15 рр.
Співвідношення навчальної вибірки до тестової: 48:8 (85.7% та 14.3%).
RSquared Score: 0; Root Mean Squared Error: 15.35;
Predicted scores dif: -5.5232; actual scores dif: -29.
Висновок:
1) в усіх трьох випадках надано вірний прогноз щодо результатів ігор (поразки);
2) малі значення RSquared Score можуть вказувати на низький зв’язок між Label та Features;
3)в третьому випадку значення RSquared Score = 0, що може пояснюватися неоднорідністю вибірок через різні підходи до гри в різних сезонах.

Посилання на github-репозиторій: https://github.com/SergiyMogiley/Mavpy-Stats-with-Regression
Посилання на профіль в LinkedIn: https://www.linkedin.com/in/sergiy-mogiley-5348a615b/
