using Nito.AsyncEx;
using System.Collections.Generic;
using System.Threading.Tasks;
using WindesHeartApp.Data.Interfaces;
using WindesHeartApp.Models;

namespace WindesHeartApp.Data.Repository
{
    public class HeartrateRepository : IHeartrateRepository
    {
        private Database _database;
        public HeartrateRepository(Database database)
        {
            _database = database;
        }

        public void Add(Heartrate heartrate) //change
        {
            var query = "INSERT INTO Heartrates(DateTime, HeartrateValue) VALUES(?,?)";
            var command = _database.Instance.CreateCommand(query, new object[] { heartrate.DateTime, heartrate.HeartrateValue });
            command.ExecuteNonQuery();
            //Task.Run(() => _database.SaveItem(heartrate)).Wait();
        }

        public IEnumerable<Heartrate> GetAll() //change
        {
            return _database.Instance.Table<Heartrate>().OrderBy(x => x.DateTime).ToList();
        }

        public void RemoveAll() //change
        {
            var heartrates = this.GetAll();
            foreach (var heartrate in heartrates)
            {
                var query = "DELETE FROM Heartrates WHERE Id = ?";
                var command = _database.Instance.CreateCommand(query, new object[] { heartrate.Id });
                command.ExecuteNonQuery();
            }
        }
    }
}
