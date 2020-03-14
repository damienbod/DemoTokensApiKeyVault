
## Database migrations STS

Add-Migration "init_sts" -c ApplicationDbContext

Update-Database -c ApplicationDbContext

Add-Migration "init_id4_store" -c PersistedGrantDbContext 

Update-Database -c PersistedGrantDbContext 
