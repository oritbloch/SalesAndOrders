USE [SalesDB]
GO

/****** Object:  StoredProcedure [dbo].[SP_GetOrderTimesAndDates]    Script Date: 26/01/2023 21:38:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- ==========================================================================================
-- Author:		Orit
-- Create date: 2021-01-22
-- Description:	get orders information: before / after time
-- ==========================================================================================
CREATE PROCEDURE [dbo].[SP_GetOrderTimesAndDates]
	
	@beforeOrAfterTime varchar(20)= 'after',
	@topNum int =10
	
AS
BEGIN


select top (@topNum) order_id,order_date,required_date,shipped_Date,
Case @beforeOrAfterTime
when 'after' then
	DATEDIFF(day,required_date, shipped_Date)
else
	DATEDIFF(day,shipped_Date,required_date )
end
diff_days
from [sales].[orders]
order by diff_days desc
;


 
END
GO


