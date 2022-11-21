Feature: Admin_could_make_CRUD_and_Search_operations_with_articles
	Как администратор
	Я хочу управлять статьями, реализуя CRUD операции и осуществлять поиск по статьям
	Background: 	
		Given В базу данных добавлена корневая категория с именем "First"
		And В базу данных добавлена корневая категория с именем "Second"
		And В базу данных добавлена дочерняя категория с именем "Third"
		And В базу данных добавлена дочерняя категория с именем "Fourth"

Scenario: Администратор добавляет статью с корректными данными
	When Администратор добавляет статью с корректными данными
	Then Статья успешно создана
	When Администратор добавляет статью с некорректным идентификатором родителя
	Then Получено сообщение об ошибке NotFound 
	
Scenario: Администратор получает сообщение об ошибке если список всех статей пуст
	When Администратор запрашивает все статьи
	Then Получено сообщение об ошибке NotFound
	
Scenario: Администратор получает список всех статей
    Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
    When Администратор запрашивает все статьи
    Then Администратор получает все статьи
     
Scenario: Администратор получает статью по идентификатору
	Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	When Администратор запрашивает статью по идентификатору
	Then Администратор получает статью с верным идентификатором
	When Администратор запрашивает статью с некорректным идентификатором
	Then Получено сообщение об ошибке NotFound
	
Scenario: Администратор удаляет статью по идентификатору
	Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	When Администратор удаляет статью по идентификатору
	Then Статья успешно удалена
	When Администратор удаляет статью с некорректным идентификатором
	Then Получено сообщение об ошибке NotFound
	
Scenario: Администратор изменяет статью
	Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	When Администратор обновляет статью с корректными данными 
    Then Статья успешно обновлена
    When Администратор обновляет статью с некорректным идентификатором
    Then Получено сообщение об ошибке NotFound
    When Администратор обновляет статью устанавливая идентификатор несуществующего родителя
    Then Получено сообщение об ошибке NotFound
    When Администратор обновляет статью устанавливая родителем корневую категорию
    Then Получено сообщение об ошибке BadRequest
	
Scenario: Администратор отправляет поисковый запрос
	Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	And В базу данных добавлена статья с вопросом "Fourth", ответом "Answer", позицией "4"
	And В базу данных добавлена статья с вопросом "Fifth", ответом "Answer", позицией "5"
	And В базу данных добавлена статья с вопросом "Sixth", ответом "Answer", позицией "6"
	And В базу данных добавлена статья с вопросом "Seventh", ответом "Answer", позицией "7"
	When Администратор запрашивает статьи по слову <SearchQuery> на странице <Page> с размером страницы <PageSize>
	Then Администратор получает статьи в количестве <Count>
	Examples: 
	  |SearchQuery |Page |PageSize |Count |
	  |Answer      |1    |4        |4     |
	  |Answer      |2    |4        |3     |
	  |Answer      |3    |4        |0     |
	  |First       |1    |10       |1     |
   
Scenario: Администратор отправляет неверный поисковый запрос
	When Администратор запрашивает статьи по запросу "" на странице 1 с размером страницы 10
	Then Получено сообщение об ошибке BadRequest	
		
Scenario: Администратор устанавливает вопросу тэги
	Given В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	And В базу данных добавлены тэг с названием "First tag"
	And В базу данных добавлены тэг с названием "Second tag"
	And В базу данных добавлены тэг с названием "Third tag"
	And В базу данных добавлены тэг с названием "Fourth tag"
	And В базу данных добавлены тэг с названием "Fifth tag"
	And В базу данных добавлены тэг с названием "Sixth tag"
	And В базу данных добавлены тэг с названием "Седьмой тэг"
   When Администратор устанавливает тэги с некорректными идентификаторами
   Then Получено сообщение об ошибке BadRequest
   When Администратор устанавливает тэги с названиями "First tag, Second tag, Third tag, Седьмой тэг"
   Then Получено сообщение об ошибке BadRequest   
   When Администратор устанавливает тэги с названиями "First tag, Second tag, Седьмой тэг"
   Then Тэги с названиями "First tag, Second tag, Седьмой тэг" успешно добавлены вопросу
   When Администратор запрашивает статьи по запросу "тэг" на странице 1 с размером страницы 100
   Then Администратор получает пагинированный ответ со статьей в количестве 1
   
Scenario: После добавления статьи с позицией выполняется выравнивание статей
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена корневая категория с именем "Second"
	And В базу данных добавлена дочерняя категория с именем "Third"
	And В базу данных добавлена дочерняя категория с именем "Fourth"
	When Администратор добавляет новую статью с вопросом "Первый" ответом "Ответ" в категорию с именем "Third" и позицией "100"
	And Администратор добавляет новую статью с вопросом "Второй" ответом "Ответ" в категорию с именем "Third" и позицией ""
	And Администратор добавляет новую статью с вопросом "Третий" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор запрашивает список отсортированных статей для категории с именем "Third"
	Then Получен список статей для категории с именем "Third"
	And Статьи отсортированы по позиции

Scenario: После удаления статьи с позицией выполняется выравнивание статей
	When Администратор добавляет новую статью с вопросом "Первый" ответом "Ответ" в категорию с именем "Third" и позицией "100"
	And Администратор добавляет новую статью с вопросом "Второй" ответом "Ответ" в категорию с именем "Third" и позицией ""
	And Администратор добавляет новую статью с вопросом "Третий" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Четвертый" ответом "Ответ" в категорию с именем "Third" и позицией "3"
	And Администратор удаляет статью с вопросом "Четвертый"
	And Администратор запрашивает список отсортированных статей для категории с именем "Third"
	Then Получен список статей для категории с именем "Third"
	And Статьи отсортированы по позиции
	
Scenario: После изменения позиции статьи в той же категории выполняется выравнивание статей
	When Администратор добавляет новую статью с вопросом "Первый" ответом "Ответ" в категорию с именем "Third" и позицией "100"
	And Администратор добавляет новую статью с вопросом "Второй" ответом "Ответ" в категорию с именем "Third" и позицией ""
	And Администратор добавляет новую статью с вопросом "Третий" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Четвертый" ответом "Ответ" в категорию с именем "Third" и позицией "3"
	And Администратор добавляет новую статью с вопросом "Пятый" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Шестой" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Седьмой" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор обновляет статью с вопросом "Второй" устанавливая вопрос "Второй" ответ "Ответ" родителя с именем "Third" и позицию "1"
	Then Статья успешно обновлена
	When Администратор запрашивает список отсортированных статей для категории с именем "Third"
	Then Получен список статей для категории с именем "Third"
	And Статьи отсортированы по позиции	
	
Scenario: После перемещения статьи с позицией в другую категорию выполняется выравнивание статей в прежней и новой категориях
	When Администратор добавляет новую статью с вопросом "Первый" ответом "Ответ" в категорию с именем "Third" и позицией "100"
	And Администратор добавляет новую статью с вопросом "Второй" ответом "Ответ" в категорию с именем "Third" и позицией ""
	And Администратор добавляет новую статью с вопросом "Третий" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	When Администратор добавляет новую статью с вопросом "Четвертый" ответом "Ответ" в категорию с именем "Third" и позицией "3"
	When Администратор добавляет новую статью с вопросом "Пятый" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Шестой" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	And Администратор добавляет новую статью с вопросом "Седьмой" ответом "Ответ" в категорию с именем "Third" и позицией "1"
	When Администратор обновляет статью с вопросом "Второй" устанавливая вопрос "Второй" ответ "Ответ" родителя с именем "Fourth" и позицию "1"
	Then Статья успешно обновлена
	When Администратор обновляет статью с вопросом "Первый" устанавливая вопрос "Первый" ответ "Ответ" родителя с именем "Fourth" и позицию "1"
	Then Статья успешно обновлена
	When Администратор запрашивает список отсортированных статей для категории с именем "Third"
	Then Получен список статей для категории с именем "Third"
	And Статьи отсортированы по позиции
	When Администратор запрашивает список отсортированных статей для категории с именем "Fourth"
	Then Получен список статей для категории с именем "Fourth"
	And Статьи отсортированы по позиции