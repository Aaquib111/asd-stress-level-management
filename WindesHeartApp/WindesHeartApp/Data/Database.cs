using Amazon.CognitoIdentity;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindesHeartApp.Models;
using WindesHeartApp.Resources;
using static Amazon.Internal.RegionEndpointProviderV2;

namespace WindesHeartApp.Data
{
    public class Database //change
    {
        public string DbPath;

        public SQLiteConnection Instance;

        AmazonDynamoDBClient client;
        DynamoDBContext context;
        CognitoAWSCredentials credentials;

        string userName = "AAQUIB SYED";
        public Database()
        {
            CreateDatabase();
        }

        public void EmptyDatabase()
        {
            
            //Transaction for emptying DB-data
            Instance.BeginTransaction();
            Globals.HeartrateRepository.RemoveAll();
            Globals.StepsRepository.RemoveAll();
            Globals.SleepRepository.RemoveAll();
            Instance.Commit();
            
            //throw new NotImplementedException();
        }

        private void CreateDatabase()
        {
            //Set DbPath
            DbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal),
                "WindesHeart.db");

            //Set Database
            Instance = new SQLiteConnection(DbPath);

            //Create the tables if not existing
            Instance.CreateTable<Heartrate>();
            Instance.CreateTable<Step>();
            Instance.CreateTable<Sleep>(); 

            credentials = new CognitoAWSCredentials(
            "us-east-2:894a949e-0364-494d-873a-204de417c1b6", // Your identity pool ID
             Amazon.RegionEndpoint.GetBySystemName("us-east-1")// Region
            );


            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.GetBySystemName("us-east-2"));
            context = new DynamoDBContext(client);
        }

        private Table LoadTable() {
            CreateDatabase();
            Table table = Table.LoadTable(client, "HeartRate");
            return table;
        }

        public async Task SaveItem(Heartrate heartrate) {
            Table table = LoadTable();
            var id = Guid.NewGuid().ToString();
            var heartrateVals = new Document();
            heartrateVals["id"] = id;
            heartrateVals["CreatedAt"] = heartrate.DateTime.ToString("YYYY - MM - DD HH: mm:ss.SSS");
            heartrateVals["Name"] = userName;
            heartrateVals["HeartrateValue"] = heartrate.HeartrateValue;

            var heartRateVal = await table.PutItemAsync(heartrateVals);

        }

        public async Task<List<Heartrate>> GetAllHeartrateAsync() {
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.GetBySystemName("us-east-2"));
            Table table = LoadTable();
            var search = table.Query(new QueryOperationConfig()
            {
                IndexName = "Name-CreatedAt-Index",
                Filter = new QueryFilter("Name", QueryOperator.Between, userName),
                AttributesToGet = new List<string> { "CreatedAt", "HeartrateValue"}
            });

            var documents = await search.GetRemainingAsync();
            documents.Sort();
            return documents.Cast<Heartrate>().ToList();
        }
    }
}
