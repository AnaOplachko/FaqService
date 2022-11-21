Feature: Admin_could_make_CRUD_operations_with_sections
	Как администратор 
	Я хочу управлять категориями, реализуя CRUD операции
	
Scenario: Администратор добавляет категорию с корректными данными
	When Администратор добавляет новую категорию с именем "Новая категория" и идентификатором родительской категории ""
	Then Категория успешно добавлена
	And Сообщение отправлено в брокер	

Scenario: Администратор получает сообщение об ошибке если не найден родитель для новой категории
	When Администратор добавляет новую категорию с именем "Новая категория" и идентификатором родительской категории "9999"
	Then Получено сообщение об ошибке NotFound
	And Сообщение не отправлено в брокер		
	
Scenario: Администратор получает сообщение об ошибке если список всех категорий пуст
	When Администратор запрашивает все категории
	Then Получено сообщение об ошибке NotFound	
		
Scenario: Администратор получает список всех добавленных категорий
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена корневая категория с именем "Second"
	And В базу данных добавлена дочерняя категория с именем "Third"
	And В базу данных добавлена дочерняя категория с именем "Fourth"
	And В базу данных добавлена дочерняя категория с именем "Fifth"
	And В базу данных добавлена дочерняя категория с именем "Sixth"
	And В базу данных добавлена дочерняя категория с именем "Seventh"
	When Администратор запрашивает все категории
	Then Администратор получает все категории	
		
Scenario: Администратор изменет категорию
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена корневая категория с именем "Second"
	And В базу данных добавлена дочерняя категория с именем "Third"
	And В базу данных добавлена дочерняя категория с именем "Fourth"
	And В базу данных добавлена дочерняя категория с именем "Fifth"
	And В базу данных добавлена дочерняя категория с именем "Sixth"
	And В базу данных добавлена дочерняя категория с именем "Seventh"
	When Администратор обновляет категорию на корректное имя "Новое имя" и существуещего родителя
	Then Категория успешно обновлена
	When Администратор обновляет категорию с некорректным идентификатором
	Then Получено сообщение об ошибке NotFound
	When Администратор обновляет категорию с некорректным идентификатором родителя
	Then Получено сообщение об ошибке NotFound
	When Администратор обновляет категорию нарушая вложенность подкатегорий
	Then Получено сообщение об ошибке BadRequest

Scenario: Администратор получает категорию по идентификатору
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена корневая категория с именем "Second"
	And В базу данных добавлена дочерняя категория с именем "Third"
	And В базу данных добавлена дочерняя категория с именем "Fourth"
	And В базу данных добавлена дочерняя категория с именем "Fifth"
	And В базу данных добавлена дочерняя категория с именем "Sixth"
	And В базу данных добавлена дочерняя категория с именем "Seventh"
    When Администратор запрашивает категорию по id
    Then Администратор получает категорию с id
    When Администратор запрашивает категорию с некорректным id
    Then Получено сообщение об ошибке NotFound
	
Scenario: Администратор удаляет категорию по идентификатору
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена дочерняя категория с именем "Second"
	And В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	And В базу данных добавлена статья с вопросом "Third", ответом "Answer", позицией "3"
	When Администратор запрашивает все статьи
	Then Администратор получает все статьи
	When Администратор удаляет категорию
	Then Категория успешно удалена
	When Администратор запрашивает все статьи
	Then Получено сообщение об ошибке NotFound
	When Администратор удаляет категорию с некорректным идентификатором
	Then Получено сообщение об ошибке NotFound
	
Scenario: Администратор удаляет корневую категорию
	Given В базу данных добавлена корневая категория с именем "First"
	And В базу данных добавлена дочерняя категория с именем "Second"
	And В базу данных добавлена статья с вопросом "First", ответом "Answer", позицией "1"
	And В базу данных добавлена статья с вопросом "Second", ответом "Answer", позицией "2"
	When Администратор удаляет корневую категорию 
	Then Категория успешно удалена
	When Администратор запрашивает все категории
	Then Получено сообщение об ошибке NotFound
	When Администратор запрашивает все статьи
	Then Получено сообщение об ошибке NotFound