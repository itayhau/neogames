using CoreProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreProject
{
    public interface IDataFetcher
    {
        // this will also save the index of last position in the Db for future use
        List<PurchaseInfo> GetBulkFromGivenDateTime(DateTime startFrom, int bulkSize, out string requestId);

        // Since the API should continue, there is no sense in using a date time here
        // if the user wants a different date then he should use the GetBulkFromGivenDateTime
        // otherwise it makes more sense to continue from the last point
        // assuming multiple customers will be using this app
        // will use request id to keep track of different requests
        List<PurchaseInfo> ContinueToNextBulk(string requestId, int bulkSize);

        bool CheckIfRequestIdExist(string requestId);

    }
}
