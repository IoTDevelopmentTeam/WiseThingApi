using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace WiseThing.Data.Respository
{
    public class BaseRepository
    {
        protected readonly WisethingPortalContext _context;
        protected readonly IMapper _mapper;
        public BaseRepository(WisethingPortalContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}
