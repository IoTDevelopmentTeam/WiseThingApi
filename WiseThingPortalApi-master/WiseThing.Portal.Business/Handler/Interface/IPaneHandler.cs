using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WiseThing.Data.Respository;

namespace WiseThing.Portal.Business
{
    public interface IPaneHandler
    {
        Task<int> AddPaneDetails(PaneDetailsDTO paneDetails);
        Task UpdatePaneDetails(PaneDetailsDTO paneDetails);
        Task RemovePaneDetails(int id);
        Task<IEnumerable<PaneDetailsDTO>> GetPaneDetailsById(int Id);
        Task AddConfigDetails(ConfigDetailsDTO[] configDetails);
        Task<IEnumerable<ConfigDetailsDTO>> GetConfigDetailsById(int Id);

        Task<IEnumerable<ConfigDetailsDTO>> PaneDetailsByUser(int Id);
    }
}
