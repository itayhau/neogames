Hi,

the problem with the API is in the continue functionallity.

Since a continue request should have some identifier to continue from its last point
i added to the GetFromDate [API] method ,in the result, an identifier GUID which will be used
for all sequential requests (as part of the return JSON result)

This GUID will be stored in the server side (in memory or DB)

Later on in each Continue [API] request, this GUID should be sent and validated to return the sequential requested data

In the Continue [API] method i thought the startdate is not needed since we already remember the last position
therefor i only request the request id (GUID) and the bulk size
if there is a need for  specific date, then the GetFromDate [API] method should be invoked

Thanks,
Itay.

