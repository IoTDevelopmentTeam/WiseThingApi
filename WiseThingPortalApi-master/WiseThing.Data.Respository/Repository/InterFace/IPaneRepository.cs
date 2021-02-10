using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WiseThing.Data.Respository
{
    public interface IPaneRepository
    {
        Task<int> AddNewPaneDetails(PaneDetailsDTO paneDetailsDto);
        Task<IEnumerable<PaneDetailsDTO>> GetPaneDetailsById(int Id);

        Task AddNewConfigDetails(ConfigDetailsDTO[] configDetailsDto);
        Task<IEnumerable<ConfigDetailsDTO>> GetConfigDetailsById(int Id);

        Task<IEnumerable<ConfigDetailsDTO>> PaneDetailsByUser(int Id);


    }
}
