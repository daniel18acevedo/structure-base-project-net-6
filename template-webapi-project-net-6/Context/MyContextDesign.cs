using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Context;
public class MyContextDesign : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

        var builderOptions = new DbContextOptionsBuilder<MyContext>();
        builderOptions.UseSqlServer(connectionString);

        var myContext = new MyContext(builderOptions.Options);

        return myContext;
    }
}