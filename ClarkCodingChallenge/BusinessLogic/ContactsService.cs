using ClarkCodingChallenge.DataAccess;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClarkCodingChallenge.BusinessLogic
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly ILogger<ContactsService> _logger;
        public ContactsService(IContactsRepository contactsRepository, ILogger<ContactsService> logger)
        {
            _contactsRepository = contactsRepository;
            _logger = logger;
        }

        private string MaskData(string data)
        {
            if (string.IsNullOrEmpty(data))
                return data;

            return data.Substring(0, 1) + new string('*', data.Length - 1);
        }

        public async Task SaveContactAsync(Contact model)
        {
            try
            {
                _logger.LogInformation($"Saving contact {MaskData(model.FirstName)} {MaskData(model.LastName)} {MaskData(model.EmailAddress)}");
                await _contactsRepository.SaveContactAsync(model.FirstName, model.LastName, model.EmailAddress);
            }
            catch(Exception ex)
            {
                // should save more data with the log file, but must be sensitive to PII
                _logger.LogError(ex, $"Error saving contact, message = \"{ex.Message}\"");
                throw;
            }
        }

        public async Task<IEnumerable<Contact>> GetContactsAsync(string lastName = "", bool sortAscending = true)
        {
            try
            {
                // allowing the database to do the filtering and sorting, simplifying the .Net code and letting the database
                // do what it does best.
                _logger.LogInformation($"Getting contacts with lastName = \"{lastName}\", sortAscending = {sortAscending}");
                return await _contactsRepository.GetContactsAsync(lastName, sortAscending);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error getting contacts, message = \"{ex.Message}\"");
                throw;
            }
        }
    }
}
