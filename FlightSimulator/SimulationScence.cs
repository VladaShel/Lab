using System.Runtime.Serialization;

namespace FlightSimulator
{
    [DataContract]
    /// <summary>
    /// Окно симуляции
    /// </summary>
    public class SimulationScence
    {
        [DataMember]
        private int _a;
        [DataMember]
        private AirplaneController _airplaneController;
        [DataMember]
        private ConsoleKeyInfo _key;

        /// <summary> Запуск новой симуляции </summary>
        public void Start() {
            InformationOutput();

            _airplaneController = new AirplaneController(250, 1000, 8);
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 400;
            timer.Elapsed += (sender, e) => {
                Simulation();
            };
            timer.Start();

            while (true)
                Thread.Sleep(1);

        }

        /// <summary> Загрузка симуляции / десериалищация </summary>
        public void LoadedStart() {
            InformationOutput();

            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 400;
            timer.Elapsed += (sender, e) => {
                Simulation();
            };
            timer.Start();

            while (true)
                Thread.Sleep(1);
        }

        /// <summary> Вывод информации об управлении </summary>
        private void InformationOutput() {
            Console.Clear();
            Console.WriteLine("Управление:\n");
            Console.WriteLine("Tab - убрать/выдвинуть шасси");
            Console.WriteLine("Стрелки вправо/влево - наклон самолета для поворота");
            Console.WriteLine("Стрелки вперед/назад - наклон самолета вверх/вниз для изменения высоты");
            Console.WriteLine("Enter/Backspace - изменение мощности двигателей");
            Console.WriteLine("Escape - для выхода");

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        /// <summary> Алгорит симуляции </summary>
        private void Simulation()
        {
            Console.Clear();
            Console.WriteLine(_a);
            Console.WriteLine(_airplaneController.Report());

            _key = new ConsoleKeyInfo('a', ConsoleKey.Insert, false, false, false);
            _a++;

            _airplaneController.Update();
            if (_airplaneController.SimulationStatusChecked())
                BreakSimulation();

            if (Console.KeyAvailable)
            {
                _key = Console.ReadKey(true);
                switch (_key.Key)
                {
                    case ConsoleKey.Escape: EndSimulation(); break;
                    case ConsoleKey.Enter: _airplaneController.AddPower(); break;
                    case ConsoleKey.Backspace: _airplaneController.ReducePower(); break;
                    case ConsoleKey.RightArrow: _airplaneController.TurnAngle(true); break;
                    case ConsoleKey.LeftArrow: _airplaneController.TurnAngle(false); break;
                    case ConsoleKey.UpArrow: _airplaneController.Сlimb(true); break;
                    case ConsoleKey.DownArrow: _airplaneController.Сlimb(false); break;
                    case ConsoleKey.Tab: _airplaneController.ChassisStatusChange(); break;
                }
            }
        }

        /// <summary> Завершение симулиции пользователем </summary>
        private void EndSimulation() {
            Console.WriteLine("Симуляция прервана");
            Console.WriteLine("Сохранение...");
            try
            {
                DataContractSerializer jsonF = new DataContractSerializer(typeof(SimulationScence));

                using (FileStream fileStream = new FileStream("SimulationSave.json", FileMode.Create))
                {
                    jsonF.WriteObject(fileStream, this);
                }
                Console.WriteLine("Сохранение успешно выполнено");
            }
            catch(Exception ex) {
                Console.WriteLine("Сохранение произоло с ошибкой");
            }

            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();

            Environment.Exit(0);
        }

        /// <summary> Завершение симулиции  при аварии </summary>
        private void BreakSimulation()
        {
            Console.WriteLine("Произошла авария");
            Console.WriteLine("Симуляция прервана");
            Console.WriteLine("Нажмите любую клавишу для продолжения");
            Console.ReadKey();

            Environment.Exit(0);
        }
    }
}
