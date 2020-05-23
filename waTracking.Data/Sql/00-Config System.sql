insert into Company (Name, Description,CreationDate, Enabled) values ('V-Tracking','Only system Admin',GETDATE(),1)

SET IDENTITY_INSERT ConfigScreen ON

--Roles
insert into [dbo].[ConfigScreen] (Id,Name,Enabled,Entity,CompanyId) values (1,'Roles',1,'SecurityRole',1)
insert into [dbo].[ConfigScreen] (Id,Name,Enabled,Entity,CompanyId) values (1,'Marcas',1,'SecurityRole',1)

SET IDENTITY_INSERT ConfigScreen OFF
insert into [dbo].[ConfigScreenField] (ConfigScreenId,Name,Required,FieldName,Enabled) values (1,'Nombre',1,'nombre',1)
insert into [dbo].[ConfigScreenField] (ConfigScreenId,Name,Required,FieldName,Enabled) values (1,'Descripcion',0,'descripcion',1)
insert into [dbo].[ConfigScreenField] (ConfigScreenId,Name,Required,FieldName,Enabled) values (1,'Estado',1,'condicion',1)



select * from [dbo].[SecurityAction]