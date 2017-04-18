
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessGroups_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessGroups_Add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessGroups_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessGroups_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessGroups_Fill]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessGroups_Fill]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessGroups_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessGroups_Get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessGroups_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessGroups_Update]
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessGroups_Delete
	@ProcessGroupId int
AS

DELETE FROM [ProcessGroups]
WHERE
	[ProcessGroupId] = @ProcessGroupId
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessGroups_Get
	@ProcessGroupId int,
	@GroupName varchar(50) output
AS

SELECT
	@GroupName=[GroupName]
FROM
	[ProcessGroups]
WHERE
	[ProcessGroupId] = @ProcessGroupId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessGroups_Fill
AS

SELECT * FROM [ProcessGroups]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessGroups_Add
	@GroupName varchar(50),
	@ProcessGroupId int OUTPUT
AS

INSERT INTO [ProcessGroups] (
	[GroupName]
) VALUES (
	@GroupName
)

SET @ProcessGroupId = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessGroups_Update
	@ProcessGroupId int,
	@GroupName varchar(50)
AS

UPDATE [ProcessGroups] SET
	[GroupName] = @GroupName
WHERE
	[ProcessGroupId] = @ProcessGroupId

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




