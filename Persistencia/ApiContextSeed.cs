using System.Globalization;
using CsvHelper;
using Dominio.Entities;
using Microsoft.Extensions.Logging;

namespace Persistencia;

public class ApiContextSeed
{
    public static async Task SeedAsync(ApiContext context, ILoggerFactory loggerFactory)
    {
        try
        {
            if (!context.Users.Any())
            {
                using (var readerTipoPersonas = new StreamReader("../Persistencia/Data/Csvs/User.csv"))
                {
                    using (var csv = new CsvReader(readerTipoPersonas, CultureInfo.InvariantCulture))
                    {
                        var list = csv.GetRecords<User>();
                        context.Users.AddRange(list);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<ApiContext>();
            logger.LogError(ex.Message);
        }
    }
}