using System.Runtime.Serialization;

namespace FlightSimulator
{

    [DataContract]
    /// <summary>
    /// Класс отвечающий за управление моделью самолета и вывод информации о ней.
    /// </summary>
    public class AirplaneController
    {
        [DataMember]
        ///<value> Модель самолета </value>
        private AirplaneModel _airplane;
        [DataMember]
        ///<value> Проверяющий на аварийные ситуации класс </value>
        private AirplaneState _airplale_state;

        public AirplaneController(double mass, double engine_power, double wingspan)
        {
            _airplane = new AirplaneModel(mass, engine_power, wingspan);
            _airplale_state = new AirplaneState();
        }

        /// <summary> Поведение обькта с течением времени </summary>
        public void Update()
        {
            _airplane.Tick();
        }

        /// <summary> Вывод информации об объекте </summary>
        public string Report()
        {
            return $"Используемая мощность: {_airplane.Current_power.ToString("00.00")}\n" +
                    $"Скорость: {_airplane.GetSpeed().ToString("0.00")}\n" +
                    $"Acacceleration: {_airplane.GetAcacceleration().ToString("0.00")}\n" +
                    $"Координата X: {_airplane.X.ToString("0.00")} \nКоордината Y: {_airplane.Y.ToString("0.00")}\n" +
                    $"Угол поворота {_airplane.Angle.ToString("0.00")}\n" +
                    $"Угол для возвышения/снижения {_airplane.AngleClimb.ToString("0.00")}\n" +
                    $"Высота: {_airplane.Height.ToString("0.00")}\n" +
                    $"Шасси: {(_airplane.Chassis ? "Не убраны" : "Убраны")}\n";
        }

        public bool SimulationStatusChecked() => _airplale_state.AccidentChecked(_airplane);

        public void AddPower() => _airplane.Current_power += 20;

        public void ReducePower() => _airplane.Current_power -= 20;

        public void TurnAngle(bool plus) => _airplane.Angle += plus ? 5 : -5;

        public void Сlimb(bool plus) => _airplane.AngleClimb += plus ? 5 : -5;

        public void ChassisStatusChange() => _airplane.Chassis = !_airplane.Chassis;
    }
}
