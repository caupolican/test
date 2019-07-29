using System;
using System.Collections.Generic;
using System.IO;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akavache;
using Newtonsoft.Json;
using Spectrum.ViewModels;

namespace Spectrum.Services
{
    public interface IRepository
    {
        Task<IList<UserViewModel>> GetAllAsync();

        Task AddUserAsync(UserViewModel user);
    }

    //public class Repository : IRepository
    //{
    //    private const string LOCAL_STORAGE_KEY = "LOCAL_STORAGE_KEY";

    //    public async Task AddUserAsync(UserViewModel user)
    //    {
    //        List<UserViewModel> users;
    //        try
    //        {
    //            users = await BlobCache
    //                .LocalMachine
    //                .GetObject<List<UserViewModel>>(LOCAL_STORAGE_KEY);
    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            users = new List<UserViewModel>();
    //        }

    //        users.Add(user);

    //        await BlobCache.LocalMachine.InsertObject(LOCAL_STORAGE_KEY, users);
    //    }

    //    public async Task<IList<UserViewModel>> GetAllAsync()
    //    {
    //        try
    //        {
    //            var users = await BlobCache
    //                .LocalMachine
    //                .GetObject<List<UserViewModel>>(LOCAL_STORAGE_KEY);

    //            return users;
    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            return new List<UserViewModel>();
    //        }
    //    }
    //}

    /// <summary>
    ///  Interimg Store solution, Having issues with Akavache and SQLite after updating packages.
    /// </summary>
    public class Repository : IRepository
    {
        public readonly Lazy<IList<UserViewModel>> _store;

        public Repository()
        {
            _store = new Lazy<IList<UserViewModel>>(() =>
            {

                if (!File.Exists(ContentPath))
                {
                    File.WriteAllText(ContentPath, JsonConvert.SerializeObject(new List<UserViewModel>()));
                }

                var json = File.ReadAllText(ContentPath);
                return JsonConvert.DeserializeObject<List<UserViewModel>>(json);

            }, LazyThreadSafetyMode.PublicationOnly);
        }

        public string ContentPath => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "store.json");

        public Task AddUserAsync(UserViewModel user)
        {
            var task = Task
                .Factory
                .StartNew(() =>
                {
                    _store.Value.Add(user);
                    var json = JsonConvert.SerializeObject(_store.Value);
                    File.WriteAllText(ContentPath, json);
                });

            return task;
        }

        public Task<IList<UserViewModel>> GetAllAsync()
        {
            return Task.FromResult(_store.Value);
        }
    }
}
