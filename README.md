Поле 10 на 10 ячеек.
На поле расставлены непроходимые блоки - необходимо сделать авто генерацию уровня и обеспечить минимум один путь к выходу.

На поле есть вражеские объекты 1 и 2 с секторами видимости. Отобразить их сектора видимости. Враги перемещаются по карте по случайно сгенерированному маршруту в режиме патрулирования. Создать для них режим патрулирования и сгенерировать им пути патрулирования. Скорость перемещения вражеских объектов равна скорости нашего героя.

На карте есть точка Начала (Н) и Выхода (В).
В точке Н появляется наш Герой (управление - клавиатура)
При движении героя, те по нажатию на клавишу перемещения (скорость перемещения персонажа 1 клетка в секунду) срабатывает датчик шума, показать его в формате столбика. Интенсивность шума возрастает при перемещении со скоростью: +3 ед. шума в секунду, спадающее со скоростью 1 ед. шума за 0.5 сек. в момент остановки героя.

При достижении уровня шума 10 баллов, вражеские объекты обнаруживают вас и бросают свой путь патрулирования, стремятся к герою чтобы поймать его. 

Наша цель не попасть в зоны видимости (секторы) вражеских объектов 1 и 2 и добраться до выхода. При попадании на сектор обзора врага - враг стремится к нашему игроку, при коллизии с нашим игроком - конец игры.
