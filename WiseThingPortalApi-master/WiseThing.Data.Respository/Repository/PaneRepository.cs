using AutoMapper;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WiseThing.Data.Respository
{
    public class PaneRepository : BaseRepository, IPaneRepository
    {
        //private readonly WisethingPortalContext _context;
        //private readonly IMapper _mapper;
        public PaneRepository(WisethingPortalContext context, IMapper mapper ):base(context, mapper)
        {
            //_context = context;
            //_mapper = mapper;
        }

        public async Task<int> AddNewPaneDetails(PaneDetailsDTO paneDetailsDto)
        {
           var paneDetails= _mapper.Map<PaneDetail>(paneDetailsDto);
            _context.PaneDetails.Add(paneDetails);
            await _context.SaveChangesAsync();
           return  _context.PaneDetails.OrderBy(e=>e.PaneId).Last().PaneId;
        }

        public async Task<IEnumerable<PaneDetailsDTO>> GetPaneDetailsById(int Id)
        {
            List<PaneDetailsDTO> paneList = new List<PaneDetailsDTO>();
            var paneDetails = await _context.PaneDetails.Where(x=>x.DeviceId== Id).ToListAsync();
            paneDetails.ForEach(x =>
            {
                var dto = _mapper.Map<PaneDetailsDTO>(x);
                paneList.Add(dto);
            });

            return paneList;

           
        }
        public async Task AddNewConfigDetails(ConfigDetailsDTO[] configDetailsDto)
        {
            var configDetails = _mapper.Map<ConfigDetail[]>(configDetailsDto);
            for(int i=0;i< configDetailsDto.Length;i++)
            _context.ConfigDetail.Add(configDetails[i]);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ConfigDetailsDTO>> GetConfigDetailsById(int Id)
        {
            List<ConfigDetailsDTO> configList = new List<ConfigDetailsDTO>();
            var configDetails = await _context.ConfigDetail.Where(x => x.PaneId == Id).ToListAsync();
            configDetails.ForEach(x =>
            {
                var dto = _mapper.Map<ConfigDetailsDTO>(x);
                configList.Add(dto);
            });

            return configList;

        }


        public async Task<IEnumerable<ConfigDetailsDTO>> PaneDetailsByUser(int Id)
        {
            List<ConfigDetailsDTO> configList = new List<ConfigDetailsDTO>();
            List<PaneDetailsDTO> paneList = new List<PaneDetailsDTO>();
            List<DeviceDTO> deviceList = new List<DeviceDTO>();
            var devices = await _context.Userdevices.Where(x => x.UserId == Id).Include(y => y.Device).ToListAsync();
            devices.ForEach(x =>
            {
                var dto = _mapper.Map<DeviceDTO>(x.Device);
                var paneDetails =_context.PaneDetails.Where(x => x.DeviceId == dto.DeviceId);
                for (int y = 0; y < paneDetails.Count(); y++)
                {
                    var dtoPane = _mapper.Map<PaneDetailsDTO>(paneDetails);
                    paneList.Add(dtoPane);
                    var configDetails = _context.ConfigDetail.Where(x => x.PaneId == dtoPane.PaneId);
                    for (int z = 0; z < configDetails.Count(); z++)
                    {
                        var dtoConfig = _mapper.Map<ConfigDetailsDTO>(configDetails);
                        configList.Add(dtoConfig);
                    }
                }
            });

            return configList;

        }

    }
}
