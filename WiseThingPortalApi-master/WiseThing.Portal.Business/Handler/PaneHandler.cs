using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public class PaneHandler : IPaneHandler
    {
        private readonly IPaneRepository _paneRepo;
        public PaneHandler(IPaneRepository paneRepo)
        {
            _paneRepo = paneRepo;
        }
        public async Task<IEnumerable<PaneDetailsDTO>> GetPaneDetailsById(int Id)
        {
            var pane = await _paneRepo.GetPaneDetailsById(Id);
            
            return pane;
        }

        public async Task<int> AddPaneDetails(PaneDetailsDTO paneDetails)
        {
            
           var paneId= await _paneRepo.AddNewPaneDetails(paneDetails);
            return paneId;
            
        }
        public async Task UpdatePaneDetails(PaneDetailsDTO paneDetails)
        {

            await _paneRepo.UpdatePaneDetails(paneDetails);
            

        }

        public async Task RemovePaneDetails(int id)
        {
            await _paneRepo.RemovePaneDetails(id);
        }
        public async Task<IEnumerable<ConfigDetailsDTO>> GetConfigDetailsById(int Id)
        {
            var pane = await _paneRepo.GetConfigDetailsById(Id);

            return pane;
        }

        public async Task AddConfigDetails(ConfigDetailsDTO[] configDetails)
        {

            await _paneRepo.AddNewConfigDetails(configDetails);

        }

       public async Task<IEnumerable<ConfigDetailsDTO>> PaneDetailsByUser(int Id)
        {
          var pane=  await _paneRepo.PaneDetailsByUser(Id);
            return pane;
        }

    }
}
