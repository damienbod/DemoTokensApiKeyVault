
[![Build status](https://ci.appveyor.com/api/projects/status/hiehvbh6kqfr5p23?svg=true)](https://ci.appveyor.com/project/damienbod/demotokensapikeyvault)

## Database migrations STS

Add-Migration "init_sts" -c ApplicationDbContext

Update-Database -c ApplicationDbContext

Add-Migration "init_id4_store" -c PersistedGrantDbContext 

Update-Database -c PersistedGrantDbContext 

## Links

https://azure.microsoft.com/en-us/services/key-vault/

https://openid.net/connect/