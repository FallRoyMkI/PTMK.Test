using PTMK.Console;
using PTMK.Models.Dtos;

if (args.Length is 0)
{
    Console.WriteLine("Не указаны аргументы, завершение работы.");
    return;
}


Console.WriteLine("Полученные параметры:");
args.ToList().ForEach(Console.WriteLine);
Core core = Core.Instance;

switch (args[0])
{
    case "1":
        ArgumentsCheck(args, 1);
        core.DatabaseManager.InitialMigration();
        break;

    case "2":
        ArgumentsCheck(args, 4);
        DateTime time = DateTime.TryParse(args[2], out time) ? time : throw new Exception("Не валидная дата");

        core.PersonManager.CreateLine(args[1], time, args[3]);
        break;

    case "3":
        ArgumentsCheck(args, 1);
        List<PersonInfoResponse> response = core.PersonManager.GetAllLinesWithUniqueFullNamesAndData();
        foreach (var element in response)
        {
            Console.WriteLine($"{element.FullName}, {element.DateOfBirth}, пол {element.Sex}, полных лет {element.FullYears}");
        }
        break;

    case "4":
        ArgumentsCheck(args, 1);
        core.DatabaseManager.FillDatabase();
        break;

    case "5":
        DateTime startTime = DateTime.Now;
        ArgumentsCheck(args, 1);
        int counter = 1;
        List<PersonLowInfoResponse> result = core.PersonManager.GetAllMalesWithFirstLetterFInFullName();

        foreach (var element in result)
        {
            Console.WriteLine($"{counter}  {element.FullName}, {element.DateOfBirth}, {element.Sex}");
            counter++;
        }

        DateTime endTime = DateTime.Now;
        Console.WriteLine($"Команда выполнена за {(endTime - startTime).TotalMilliseconds} миллисекунд");
        break;

    default:
        Console.WriteLine("Нет такой команды, завершение работы.");
        break;
}
Console.WriteLine("Команда выполнена успешно");

void ArgumentsCheck(string[] args, int count)
{
    if (args.Length != count)
    {
        throw new Exception("Не верно указаны аргументы, завершение работы.");
    }
}