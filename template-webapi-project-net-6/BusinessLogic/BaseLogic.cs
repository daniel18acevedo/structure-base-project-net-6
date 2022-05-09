using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessInterface;

namespace BusinessLogic;
public class BaseLogic
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseLogic(IUnitOfWork unitOfWork)
    {
        this._unitOfWork = unitOfWork;
    }
}