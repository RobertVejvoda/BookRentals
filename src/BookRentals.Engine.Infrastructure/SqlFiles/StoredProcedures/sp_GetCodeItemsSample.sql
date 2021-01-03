create or alter procedure engine.sp_GetCodeItemsSample
as
select * from engine.codeitem
go

grant execute on engine.sp_GetCodeItemsSample to engine_users  
