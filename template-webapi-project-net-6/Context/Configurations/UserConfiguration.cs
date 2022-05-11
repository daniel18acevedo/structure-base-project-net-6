using Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Context.Configurations;

internal class UserConfiguration : BaseConfiguration<User>
{
    protected override void ConfigureStartupValues(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User
        {
            Id = 1,
            Name = "name",
            Email = "mail"
        },
        new User
        {
            Id = 2,
            Name = "name2",
            Email = "mail2"
        },
        new User
        {
            Id = 3,
            Name = "name3",
            Email = "mail3"
        },
        new User
        {
            Id = 4,
            Name = "name4",
            Email = "mail4"
        });
    }
}