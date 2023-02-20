using FlightSimulator;
using System.Runtime.Serialization;


static class Program
{
    static void Main(string[] args)
    {
        SimulationScence simulation = new SimulationScence();
        Menu(simulation);
    }

    static void Menu(SimulationScence simulation) {
        Console.Clear();
        Console.WriteLine("__________________Меню__________________");
        Console.WriteLine("1. Запустить симуляцию");
        Console.WriteLine("2. Загрузить симуляцию");
        Console.WriteLine("3. Выйти");

        switch (Console.ReadKey().KeyChar)
        {
            case '1': simulation.Start(); break;
            case '2': LoadSimulation(); break;
            case '3': Environment.Exit(0); break;

            default: Menu(simulation); break;
        }
    }

    static void LoadSimulation() {

        if (File.Exists("SimulationSave.json"))
        {
            DataContractSerializer jsonF = new DataContractSerializer(typeof(SimulationScence));

            using (FileStream fileStream = new FileStream("SimulationSave.json", FileMode.Open))
            {
                SimulationScence load_simulation = (SimulationScence)jsonF.ReadObject(fileStream);
                load_simulation.LoadedStart();
            }
        }
        else {
            Console.WriteLine("Ошибка: сохранения отсутствуют");
        }
    }
}
