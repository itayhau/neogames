using CoreProject;
using DALSimulator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MainDataFetcher
    {
        private IDataFetcher _dataFetcher;

        public MainDataFetcher()
        {
            // should be read from configuration
            // or perhaps send as an argument to the ctor
            _dataFetcher = new SimulatorDataFetcher();
        }

        public List<PurchaseInfo> GetBulk(DateTime startFrom, int bulkSize, out string requestId)
        {
            return _dataFetcher.GetBulkFromGivenDateTime(startFrom, bulkSize, out requestId);
        }

        // Since the API should continue, there is no sense in using a date time here
        // if the user wants a different date then he should use the GetBulkFromGivenDateTime
        // otherwise it makes more sense to continue from the last point
        public List<PurchaseInfo> ContinueToNextBulk(string requestId, int bulkSize)
        {
            return _dataFetcher.ContinueToNextBulk(requestId, bulkSize);
        }

        public bool CheckIfRequestIdExist(string requestId)
        {
            return _dataFetcher.CheckIfRequestIdExist(requestId);
        }
    }
}
