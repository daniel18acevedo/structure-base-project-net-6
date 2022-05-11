using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Context;
public class MyContextDesign : IDesignTimeDbContextFactory<MyContext>
{
    public MyContext CreateDbContext(string[] args)
    {
        var builderOptions = new DbContextOptionsBuilder<MyContext>();
        builderOptions.UseSqlServer("Server=localhost;Database=MyDataBase;Trusted_Connection=True;MultipleActiveResultSets=True;");

        var myContext = new MyContext(builderOptions.Options);

        return myContext;
    }
}