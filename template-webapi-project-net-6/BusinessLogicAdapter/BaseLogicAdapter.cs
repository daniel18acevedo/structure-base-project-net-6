using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace BusinessLogicAdapter
{
    public class BaseLogicAdapter
    {
        protected readonly IMapper _mapper;
        public BaseLogicAdapter(IMapper mapper)
        {
            this._mapper = mapper;
        }
    }
}