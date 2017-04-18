
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Processes_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Processes_Add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Processes_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Processes_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Processes_Fill]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Processes_Fill]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Processes_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Processes_Get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Processes_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[Processes_Update]
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.Processes_Delete
	@ProcessID int
AS

DELETE FROM [Processes]
WHERE
	[ProcessID] = @ProcessID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.Processes_Get
	@ProcessID int,
	@ProcessName varchar(50) output,
	@ProcessGroup int output
AS

SELECT
	@ProcessName=[ProcessName],
	@ProcessGroup=[ProcessGroup]
FROM
	[Processes]
WHERE
	[ProcessID] = @ProcessID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.Processes_Fill
AS

SELECT * FROM [Processes]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.Processes_Add
	@ProcessName varchar(50),
	@ProcessGroup int,
	@ProcessID int OUTPUT
AS

INSERT INTO [Processes] (
	[ProcessName],
	[ProcessGroup]
) VALUES (
	@ProcessName,
	@ProcessGroup
)

SET @ProcessID = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.Processes_Update
	@ProcessID int,
	@ProcessName varchar(50),
	@ProcessGroup int
AS

UPDATE [Processes] SET
	[ProcessName] = @ProcessName,
	[ProcessGroup] = @ProcessGroup
WHERE
	[ProcessID] = @ProcessID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




