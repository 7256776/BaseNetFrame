using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace WebApiAuthService
{

    public class PersistedGrantStore : IPersistedGrantStore
    {

        public PersistedGrantStore()
        {
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync(string subjectId)
        {
            return Task.FromResult<IEnumerable<PersistedGrant>>(null);
        }

        public Task<PersistedGrant> GetAsync(string key)
        {
            return Task.FromResult<PersistedGrant>(new PersistedGrant());
        }

        public Task RemoveAllAsync(string subjectId, string clientId)
        {
            return Task.FromResult(true);
        }

        public Task RemoveAllAsync(string subjectId, string clientId, string type)
        {
            return Task.FromResult(true);
        }

        public Task RemoveAsync(string key)
        {
            return Task.FromResult(true);
        }

        public Task StoreAsync(PersistedGrant grant)
        {
            return Task.FromResult(true);
        }



    }




}
