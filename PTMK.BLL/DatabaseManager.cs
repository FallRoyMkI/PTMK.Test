using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PTMK.DAL;
using PTMK.Models;
using System.Data;

namespace PTMK.BLL;

public class DatabaseManager 
{
    private readonly DataTable _personInfoTable = new("PersonInfo");
    private readonly Context _context;


    public DatabaseManager(Context context)
    {
        _context = context;
    }

    public void InitialMigration()
    {
        _context.Database.Migrate();
        _context.Database.EnsureCreated();
    }

    public void FillDatabase()
    {
        CreateTable();

        try
        {
            int numberOfExistedRows = _context.Persons.ToList().Count();

            for (int i = 1; i < 1000001; i++)
            {
                GeneratePerson(i + numberOfExistedRows);
            }
            for (int i = 1; i < 101; i++)
            {
                GenerateOneHundredMales(i + 1000000 + numberOfExistedRows);
            }
        }
        finally
        {
            WriteToServer();
        }
    }


    private void CreateTable()
    {
        DataColumn id = new DataColumn();
        id.DataType = Type.GetType("System.Int32");
        id.ColumnName = "Id";
        id.AutoIncrement = true;
        id.AllowDBNull = false;
        _personInfoTable.Columns.Add(id);

        DataColumn fullName = new DataColumn();
        fullName.DataType = Type.GetType("System.String");
        fullName.ColumnName = "FullName";
        fullName.AllowDBNull = false;
        _personInfoTable.Columns.Add(fullName);

        DataColumn dateOfBirth = new DataColumn();
        dateOfBirth.DataType = Type.GetType("System.DateTime");
        dateOfBirth.ColumnName = "DateOfBirth";
        dateOfBirth.AllowDBNull = false;
        _personInfoTable.Columns.Add(dateOfBirth);

        DataColumn sex = new DataColumn();
        sex.DataType = Type.GetType("System.String");
        sex.ColumnName = "Sex";
        sex.AllowDBNull = false;
        _personInfoTable.Columns.Add(sex);
    }

    private void GeneratePerson(int i)
    {
        DataRow _row = _personInfoTable.NewRow();
        _row["Id"] = i;
        _row["FullName"] = Faker.Name.FullName();
        _row["DateOfBirth"] = GenerateBirthdayDate();
        _row["Sex"] = (Faker.Boolean.Random()) ? "Мужской" : "Женский";


        _personInfoTable.Rows.Add(_row);
    }

    private void GenerateOneHundredMales(int i)
    {
        string name;
        do
        {
            name = Faker.Name.FullName();
        } while (name[0] != 'F');

        DataRow _row = _personInfoTable.NewRow();
        _row["Id"] = i;
        _row["FullName"] = name;
        _row["DateOfBirth"] = GenerateBirthdayDate();
        _row["Sex"] = "Мужской";

        _personInfoTable.Rows.Add(_row);
    }

    private DateTime GenerateBirthdayDate()
    {
        Random randomiser = new();
        int randomYear = randomiser.Next(1950, 2010);
        int randomMonth = randomiser.Next(1, 12);
        int randomDay = randomiser.Next(1, DateTime.DaysInMonth(randomYear, randomMonth));
        DateTime result = new(randomYear, randomMonth, randomDay);
        return result;
    }

    private void WriteToServer()
    {
        _personInfoTable.AcceptChanges();
        using (SqlConnection connection = new SqlConnection(ConnectionString.ConStr))
        {
            connection.Open();

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "dbo.Persons";
                try
                {
                    bulkCopy.WriteToServer(_personInfoTable);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            _personInfoTable.Rows.Clear();
        }
    }
}