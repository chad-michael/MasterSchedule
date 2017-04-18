
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessUsers_Add]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessUsers_Add]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessUsers_Delete]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessUsers_Delete]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessUsers_Fill]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessUsers_Fill]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessUsers_Get]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessUsers_Get]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ProcessUsers_Update]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ProcessUsers_Update]
GO


SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessUsers_Delete
	@ID int
AS

DELETE FROM [ProcessUsers]
WHERE
	[ID] = @ID
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessUsers_Get
	@ID int,
	@ProcessId int output,
	@UserId varchar(64) output
AS

SELECT
	@ProcessId=[ProcessId],
	@UserId=[UserId]
FROM
	[ProcessUsers]
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessUsers_Fill
AS

SELECT * FROM [ProcessUsers]

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessUsers_Add
	@ProcessId int,
	@UserId varchar(64),
	@ID int OUTPUT
AS

INSERT INTO [ProcessUsers] (
	[ProcessId],
	[UserId]
) VALUES (
	@ProcessId,
	@UserId
)

SET @ID = @@IDENTITY

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO





SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE dbo.ProcessUsers_Update
	@ID int,
	@ProcessId int,
	@UserId varchar(64)
AS

UPDATE [ProcessUsers] SET
	[ProcessId] = @ProcessId,
	[UserId] = @UserId
WHERE
	[ID] = @ID

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO




