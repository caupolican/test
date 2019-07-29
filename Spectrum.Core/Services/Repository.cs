using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Akavache;
using Spectrum.ViewModels;

namespace Spectrum.Services
{
    public interface IRepository
    {
        Task<IList<UserViewModel>> GetAllAsync();

        Task AddUserAsync(UserViewModel user);
    }

    public class Repository : IRepository
    {
        private const string LOCAL_STORAGE_KEY = "LOCAL_STORAGE_KEY";

        public async Task AddUserAsync(UserViewModel user)
        {
            List<UserViewModel> users;
            try
            {
                users = await BlobCache
                    .LocalMachine
                    .GetObject<List<UserViewModel>>(LOCAL_STORAGE_KEY);
            }
            catch (KeyNotFoundException)
            {
                users = new List<UserViewModel>();
            }

            users.Add(user);

            await BlobCache.LocalMachine.InsertObject(LOCAL_STORAGE_KEY, users);
        }

        public async Task<IList<UserViewModel>> GetAllAsync()
        {
            try
            {
                var users = await BlobCache
                    .LocalMachine
                    .GetObject<List<UserViewModel>>(LOCAL_STORAGE_KEY);

                return users;
            }
            catch (KeyNotFoundException)
            {
                return new List<UserViewModel>();
            }
        }
    }

}
