

SET IDENTITY_INSERT SystemScreen ON

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(1,'Menú',1,1,null,1,null,null,null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(2,'Configuraciones',1,1,null,2,null,null,null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(3,'SYS',1,0,null,3,null,null,null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(4,'Tablero',1,1,1,2,'Georeference','/dashboards','mdi mdi-airplay')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(5,'Mapa',1,1,1,3,'Georeference','/visor','mdi mdi-map-marker-radius')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(6,'Vehiculos',1,1,1,4,'Vehicle','/vehicles','mdi mdi-car-connected')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(7,'Dispositivos',1,1,1,5,'GeoTracker','/devices','mdi mdi-developer-board')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(8,'Conductores',1,1,1,6,'Driver','/drivers','mdi mdi-steering')


insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(9,'Tipificaciones',1,1,2,1,null,null,'mdi mdi-keyboard')

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(10,'Vehiculos',1,1,9,1,null,null,null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(11,'Marca',1,1,10,1,'VehicleBrand','/tipification/brand',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(12,'Modelo',1,1,10,2,'VehicleModel','/tipification/model',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(13,'Segmento',1,1,10,3,'VehicleSegment','/tipification/segment',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(14,'Tipo Vehiculo',1,1,10,4,'VehicleType','/tipification/type',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(15,'Documentación',1,1,10,5,'VehicleDocumentation','/tipification/documentation',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(16,'Tipo de Usos',1,1,10,6,'TypeOfUse','/tipification/typeofuse',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(17,'Estado Patrimonial',1,1,10,7,'Status','/tipification/status',null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(18,'Departamentos',1,1,9,2,null,null,null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(19,'Secretaria',1,1,18,1,'Deparment','/departments/department',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(20,'Area',1,1,18,2,'DepartmentChild','/departments/child',null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(21,'Servicios',1,1,9,3,null,null,null)

insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(22,'Servicio',1,1,21,1,'Service','/services/service',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(23,'Tipo Servicio',1,1,21,2,'ServiceType','/services/type',null)


insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(24,'Perfiles',1,1,2,2,null,null,'mdi mdi-lock')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(25,'Roles',1,1,24,1,'SecurityRole','/security/role',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(26,'Usuarios',1,1,24,2,'SecurityUser','/security/user',null)
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(27,'Sistema',1,1,2,3,null,'/system','mdi mdi-settings-box')


insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(28,'Configuraciones',1,0,3,1,null,'/config','mdi mdi-apps')
insert into SystemScreen (Id,Description,Enabled,IsDefault,ParentId,Orden,Entity,Path,Icon) values(29,'TrackerLogs',1,0,3,2,'TrackerLog','/logs','mdi mdi-clipboard-text')

SET IDENTITY_INSERT SystemScreen OFF




select * from SystemScreen


--VehicleBrand
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (11,'ID',1,'Id',0,1,null)
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (11,'Marca',1,'Description',1,1,null)
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (11,'Habilitado',1,'Enabled',0,1,null)


--VehicleModel
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (12,'ID',1,'Id',0,1,null)
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (12,'Modelo',1,'Description',1,1,null)
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (12,'Habilitado',1,'Enabled',0,1,null)
insert into SystemScreenField (SystemScreenId, Name, Required, FieldName, Enabled, Visible,DefaultValue) 
values (12,'Marca',1,'VehicleBrandId',1,1,null)


