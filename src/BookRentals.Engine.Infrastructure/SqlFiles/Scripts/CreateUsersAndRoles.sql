
create role engine_users
go

create user [br_engine] for login [br_engine] with default_schema=[engine]
go

exec sp_addrolemember 'engine_users', 'br_engine'  



