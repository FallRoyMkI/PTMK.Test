using Microsoft.Extensions.Configuration;
using PTMK.BLL;
using PTMK.DAL;
using PTMK.Mapper;

namespace PTMK.Console;

public class Core
{
    private static Core? _core;
    public string ConnectionString {get; private set; }
    public DatabaseManager DatabaseManager { get; private set; }
    public PersonManager PersonManager { get; private set; }

    private Core() { Initialization(); }
    public static Core Instance => _core ??= new Core();

    private void Initialization()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false);

        IConfiguration config = builder.Build();
        ConnectionString = config["ConnectionString"] ?? throw new Exception("Не указана строка подключения");

        Context context = new(ConnectionString);
        PersonMapper mapper = new();
        PersonRepository personRepository = new(context);
        DatabaseManager = new(context, ConnectionString);
        PersonManager = new(personRepository, mapper);
    }
}