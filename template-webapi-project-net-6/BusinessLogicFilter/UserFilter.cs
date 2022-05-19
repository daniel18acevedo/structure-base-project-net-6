using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain;
using ExtenisonsService;

namespace BusinessLogicFilter;
public class UserFilter : PaginationFilter<User>
{
    public string? Name { get; set; }
    public string? Email { get; set; }

    public override Expression<Func<User, bool>> Filter()
    {
        Expression<Func<User, bool>> expression = u => true;

        if(!string.IsNullOrEmpty(this.Name))
        {
            expression = expression.AddLeafToRightWithAnd(user => user.Name.ToLower().Contains(this.Name.ToLower()));
        } 

        if(!string.IsNullOrEmpty(this.Email))
        {
            expression = expression.AddLeafToRightWithAnd(user => user.Email.ToLower().Contains(this.Email.ToLower()));
        }

        return expression;
    }
}