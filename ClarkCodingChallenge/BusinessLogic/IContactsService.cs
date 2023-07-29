using ClarkCodingChallenge.DataAccess;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.BusinessLogic
{
    public interface IContactsService
    {
        Task SaveContactAsync(Contact model);
        Task<IEnumerable<Contact>> GetContactsAsync(string lastName, bool sortAsc);
    }
}
